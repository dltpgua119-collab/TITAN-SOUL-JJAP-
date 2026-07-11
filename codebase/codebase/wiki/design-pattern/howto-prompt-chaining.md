---
type: how-to
topic: design-pattern
lang: multi
tags: [llm, design-pattern, prompt-chaining, workflow]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-anthropic-building-effective-agents.md, raw/design-pattern-martinfowler-llm-patterns.md]
---

# 프롬프트 체이닝 구현

> 복잡한 태스크를 순차적인 LLM 호출로 분해해 각 단계의 출력이 다음 단계의 입력이 되도록 연결하는 방법.

## 언제 이 가이드를 쓰는가

- 태스크가 명확한 순차 단계로 분해될 때
- 각 단계에서 중간 결과 검증이 필요할 때
- 단일 LLM 호출로는 컨텍스트 윈도우 한계를 초과할 때
- 복잡한 구현 태스크에서 "계획 → 실행" 흐름이 필요할 때

## 전제 조건

- LLM API 기본 사용 경험
- 태스크를 단계로 분해할 수 있는 도메인 지식

---

## 단계

### 1. 태스크 분해 설계

먼저 태스크를 독립적인 단계로 나눈다. 각 단계는:
- 명확한 입력·출력이 있어야 함
- 이전 단계의 출력에만 의존해야 함
- 검증 가능한 결과를 생성해야 함

**예시: 코드 리뷰 + 개선 파이프라인**
```text
단계 1: 코드 분석 → 문제점 목록
단계 2: 문제점 우선순위화 → 우선순위 목록
단계 3: 개선 코드 생성 → 개선된 코드
단계 4: 테스트 작성 → 테스트 코드
```

---

### 2. 기본 프롬프트 체인 구현

```python
import anthropic

client = anthropic.Anthropic()

def call_llm(prompt: str, system: str = "") -> str:
    messages = [{"role": "user", "content": prompt}]
    kwargs = {"model": "claude-sonnet-4-6", "max_tokens": 2048, "messages": messages}
    if system:
        kwargs["system"] = system
    response = client.messages.create(**kwargs)
    return response.content[0].text

def review_and_improve_code(code: str) -> dict:
    # 단계 1: 문제점 분석
    problems = call_llm(
        f"다음 코드의 문제점을 목록으로 분석해:\n\n```\n{code}\n```",
        system="당신은 시니어 소프트웨어 엔지니어입니다."
    )
    
    # 단계 2: 우선순위화
    prioritized = call_llm(
        f"다음 문제점을 심각도 순으로 정렬하고 상위 3개만 선택해:\n\n{problems}"
    )
    
    # 단계 3: 개선 코드 생성
    improved_code = call_llm(
        f"원본 코드:\n```\n{code}\n```\n\n"
        f"다음 문제점을 수정한 개선된 코드를 작성해:\n{prioritized}"
    )
    
    return {
        "problems": problems,
        "prioritized": prioritized,
        "improved_code": improved_code
    }
```

---

### 3. Generated Knowledge Prompting 적용 (Xu Hao 패턴)

복잡한 구현 태스크에서는 코드 생성 전에 먼저 마스터 플랜을 요청한다.

```python
def implement_feature_with_plan(
    feature_description: str,
    tech_stack: str,
    architecture_guidelines: str
) -> dict:
    # 단계 1: 코드 생성 없이 마스터 플랜 작성
    master_plan = call_llm(
        f"""
기술 스택: {tech_stack}

아키텍처 가이드라인:
{architecture_guidelines}

기능 설명: {feature_description}

위 기능을 구현하기 위한 마스터 플랜을 번호 목록으로 작성해.
코드는 생성하지 마. 태스크 목록만.
        """,
        system="당신은 소프트웨어 아키텍트입니다."
    )
    
    # 단계 2: 플랜 검토 (선택적으로 인간 검토)
    # ... (여기서 master_plan을 사람이 검토하거나 eval로 검증)
    
    # 단계 3: 플랜의 각 태스크 구현
    implementations = {}
    for task_num in range(1, 5):  # 플랜에서 태스크 번호 파악
        implementation = call_llm(
            f"""
마스터 플랜:
{master_plan}

아키텍처 가이드라인:
{architecture_guidelines}

태스크 {task_num}의 코드를 구현해.
다른 태스크와 일관성을 유지해.
            """
        )
        implementations[f"task_{task_num}"] = implementation
    
    return {"plan": master_plan, "implementations": implementations}
```

---

### 4. 컨텍스트 재설정 전략 (긴 체인)

세션이 길어지거나 새 세션을 시작할 때 컨텍스트 재공급:

```python
CONTEXT_RESET_TEMPLATE = """
## 현재 작업 컨텍스트

아키텍처 가이드라인:
{guidelines}

마스터 플랜:
{master_plan}

지금까지 완료된 태스크:
{completed_tasks}

## 현재 태스크

{current_task}
"""

def continue_implementation(
    guidelines: str,
    master_plan: str,
    completed: list[str],
    next_task: str
) -> str:
    prompt = CONTEXT_RESET_TEMPLATE.format(
        guidelines=guidelines,
        master_plan=master_plan,
        completed_tasks="\n".join(f"- {t}" for t in completed),
        current_task=next_task
    )
    return call_llm(prompt)
```

---

### 5. 단계별 오류 처리

```python
def safe_chain_step(
    prompt: str,
    validator: callable,
    max_retries: int = 3
) -> str:
    """각 체인 단계에서 출력 검증 후 실패 시 재시도"""
    for attempt in range(max_retries):
        output = call_llm(prompt)
        
        if validator(output):
            return output
        
        # 이전 실패를 컨텍스트에 포함
        prompt += f"\n\n이전 시도가 실패했습니다: {output}\n다시 시도해주세요."
    
    raise ValueError(f"Failed after {max_retries} attempts")

# 사용 예시
json_output = safe_chain_step(
    "다음 결과를 JSON으로 반환해: ...",
    validator=lambda s: is_valid_json(s)
)
```

---

## 검증 방법

1. 각 단계의 출력을 개별적으로 테스트 (단위 테스트처럼)
2. 단계 간 데이터 전달이 올바른지 확인
3. 전체 체인의 최종 출력을 eval로 품질 측정
4. 컨텍스트 재설정 후 출력 일관성 확인

## 관련 페이지
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]]
- [[wiki/design-pattern/concept-prompt-engineering-patterns|프롬프트 엔지니어링 패턴]]
- [[wiki/design-pattern/snippet-chain-of-thought-prompt|CoT 프롬프트 템플릿]]

## 출처
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
- [[wiki/design-pattern/source-martinfowler-llm-patterns|Martin Fowler: ChatGPT Test Automation Patterns]]
