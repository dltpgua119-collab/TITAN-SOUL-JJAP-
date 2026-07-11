---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, prompt-engineering, chain-of-thought, few-shot]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-anthropic-prompt-engineering.md, raw/design-pattern-martinfowler-llm-patterns.md]
---

# 프롬프트 엔지니어링 패턴

> GoF 패턴처럼 이름 붙여진 재사용 가능한 프롬프트 설계 해법들. 각 패턴은 특정 문제를 해결하며 조합해서 사용할 수 있다.

## 기본 패턴

### Zero-Shot Prompting

예시 없이 태스크를 직접 지시.

```text
다음 텍스트를 한국어로 번역해:
"The quick brown fox jumps over the lazy dog."
```

**언제 사용**: 태스크가 명확하고 단순할 때. 모델이 이미 충분한 지식을 가질 때.
**한계**: 복잡하거나 모호한 태스크에서 출력 품질 저하.

---

### Few-Shot Prompting

예시를 2–5개 제공해 패턴 학습.

```text
다음 형식으로 감정을 분류해:
입력: "오늘 정말 좋은 날이야" → 긍정
입력: "왜 이렇게 힘들지" → 부정
입력: "그냥 그런 하루였어" → ?
```

**언제 사용**: 특정 출력 형식이나 분류 기준을 정의해야 할 때.
**주의**: 예시가 편향되면 출력도 편향됨.

---

### Chain-of-Thought (CoT) Prompting

단계별 추론 과정을 명시적으로 요청.

```text
다음 문제를 단계별로 풀어:
18 × 24 = ?

단계 1: 18 × 20 = 360
단계 2: 18 × 4 = 72
단계 3: 360 + 72 = 432
```

**언제 사용**: 수학, 논리, 다단계 추론이 필요한 태스크.
**효과**: 복잡한 문제에서 정확도 향상. 추론 과정이 투명해짐.

---

### XML 구조화 (Anthropic 권장)

XML 태그로 입력의 각 섹션을 명확히 구분.

```xml
<system>당신은 코드 리뷰어입니다. 보안 취약점에 집중하세요.</system>

<context>
다음은 사용자 인증 코드입니다:
</context>

<code>
def login(username, password):
    query = f"SELECT * FROM users WHERE username='{username}'"
    ...
</code>

<task>위 코드의 보안 취약점을 찾아 개선 방법을 제안하세요.</task>
```

**언제 사용**: 입력이 복잡하고 여러 섹션으로 구성될 때 (특히 Claude).
**효과**: 모델이 각 부분의 역할을 명확히 이해. 출력 품질 향상.

---

### 역할 프롬프팅 (Role Prompting)

LLM에게 특정 역할을 부여.

```text
당신은 15년 경력의 시니어 소프트웨어 엔지니어입니다.
다음 코드를 프로덕션 배포 관점에서 리뷰해주세요.
```

**언제 사용**: 특정 전문성 관점이 필요할 때.
**주의**: 역할이 구체적일수록 효과적. "전문가처럼"보다 "15년 경력의 SRE처럼"이 더 나음.

---

### Generated Knowledge Prompting

최종 결과 전에 먼저 관련 지식/계획을 생성.

```text
1단계 (계획):
다음 기능을 구현하기 위한 마스터 플랜을 작성해. 코드는 생성하지 마.
[태스크 설명]

2단계 (실행):
위 플랜의 3번 태스크를 구현해.
```

**언제 사용**: 복잡한 구현 태스크 (Xu Hao 워크플로우).
**효과**: LLM이 전체 구조를 파악한 후 각 부분을 생성 → 더 일관된 출력.

---

## 패턴 조합 규칙

패턴들은 조합 가능하다:

```text
역할 + XML 구조화 + Few-Shot + CoT
```

```xml
<role>당신은 알고리즘 전문가입니다.</role>

<examples>
문제: 배열 정렬
CoT: 1. 배열 크기 확인 2. 적합한 알고리즘 선택 3. 구현
출력: 퀵소트 구현

문제: 문자열 역순
CoT: 1. 문자열 길이 확인 2. ...
</examples>

<task>다음 문제를 단계별로 해결해: ...</task>
```

## Eval 기반 개발 (EDD)

프롬프트 엔지니어링은 eval 없이 진행하면 안 된다:

1. 성공 기준 정의 (무엇이 좋은 출력인가?)
2. 테스트 케이스 작성
3. 프롬프트 반복 개선
4. Eval로 회귀 확인

> "지연 시간·비용 문제는 다른 모델 선택으로 해결 — 모든 실패가 프롬프트 문제는 아님" — Anthropic

## 관련 페이지
- [[wiki/design-pattern/concept-gof-vs-llm-era|GoF vs LLM 시대 패턴]]
- [[wiki/design-pattern/howto-prompt-chaining|프롬프트 체이닝 구현]]
- [[wiki/design-pattern/snippet-chain-of-thought-prompt|CoT 프롬프트 템플릿]]

## 출처
- [[wiki/design-pattern/source-anthropic-prompt-engineering|Anthropic Prompt Engineering Overview]]
- [[wiki/design-pattern/source-martinfowler-llm-patterns|Martin Fowler: ChatGPT Test Automation Patterns]]
