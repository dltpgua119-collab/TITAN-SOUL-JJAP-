---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, hallucination, guardrails, evals, defensive-ux]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-eugeneyan-llm-patterns.md, raw/design-pattern-anthropic-building-effective-agents.md]
---

# 환각 감소 패턴

> LLM이 사실과 다른 정보를 생성하는 "환각(hallucination)"을 구조적으로 줄이는 패턴. 프롬프트 개선만으로는 부족하며, 시스템 수준의 접근이 필요하다.

## 환각의 원인과 유형

| 유형 | 원인 | 예시 |
|---|---|---|
| 사실 환각 | 훈련 데이터 부족 또는 오래된 정보 | 존재하지 않는 논문 인용 |
| 구조 환각 | 요청한 형식과 다른 출력 | JSON 대신 자연어 응답 |
| 소스 환각 | 컨텍스트에 없는 정보 생성 | 제공한 문서에 없는 내용 주장 |
| 코드 환각 | 존재하지 않는 API/함수 사용 | 없는 메서드 호출 |

## 패턴 1: Evals (측정 기반)

환각을 측정하지 않으면 줄일 수 없다.

```python
# Eval 구성 예시
def eval_factual_accuracy(output: str, ground_truth: str) -> float:
    """BERTScore로 의미론적 유사도 측정"""
    return bert_score(output, ground_truth)

def eval_with_llm_judge(output: str, criteria: str) -> bool:
    """강력한 LLM을 평가자로 사용 (G-Eval 방식)"""
    return judge_llm.evaluate(output, criteria)
```

**Eval 기반 개발(EDD) 사이클**:
1. 실패 케이스 수집
2. Eval 작성
3. 시스템 수정
4. Eval 재실행 → 회귀 확인

---

## 패턴 2: RAG (검색 기반 grounding)

모델의 내부 지식 대신 검색된 실제 문서를 기반으로 응답.

```text
[사용자 질문] → [관련 문서 검색] → [문서 + 질문 → LLM] → [응답]
```

LLM에게 "다음 문서를 기반으로만 답변하세요. 문서에 없으면 '모름'이라고 하세요"라고 지시하면 소스 환각 크게 감소.

---

## 패턴 3: Guardrails (출력 검증)

LLM 출력을 시스템 레벨에서 검증·수정·차단.

### 구조적 검증 (가장 안전)
```python
from pydantic import BaseModel

class AnalysisResult(BaseModel):
    sentiment: Literal["positive", "negative", "neutral"]
    confidence: float = Field(ge=0.0, le=1.0)
    summary: str = Field(max_length=500)

# LLM이 JSON을 반환하면 자동 검증
result = AnalysisResult.model_validate_json(llm_output)
```

### 의미론적 검증
```python
# 입력과 출력의 관련성 확인
def check_relevance(input_query: str, output: str) -> bool:
    relevance_score = similarity(embed(input_query), embed(output))
    return relevance_score > THRESHOLD
```

**Guardrails 설계 원칙**:
- 사후 검증보다 **직접적인 구조 제어** 선호
- 검증 실패 시 동작 정의: 재시도, 사람 검토, 기본값 반환
- 검증 자체가 비용 발생 — 필요한 것만 검증

---

## 패턴 4: Evaluator-Optimizer 루프

생성 LLM과 평가 LLM을 분리해 품질 개선 루프 구성.

```text
[초안 생성] → [평가 LLM: "이 응답에 잘못된 사실이 있나요?"]
                        ↓
                  사실 오류 발견 → [수정 지시] → [재생성]
                        ↓
                  오류 없음 → [최종 출력]
```

**언제 사용**: 사실 정확성이 매우 중요하고 자동화된 검증이 가능할 때.

---

## 패턴 5: Defensive UX

사용자가 LLM의 한계를 이해하고 출력을 비판적으로 평가하도록 UI 설계.

**핵심 원칙**:
- **기대치 설정**: "이 답변은 부정확할 수 있습니다"
- **거부 가능성**: AI 제안을 쉽게 무시할 수 있게
- **귀속 제공**: 소스 문서 인용 표시
- **친숙한 UX**: 새로운 인터랙션보다 기존 패턴 사용

---

## 패턴 6: 코드 수준 환각 감소

### 타입 안전성으로 grounding
```typescript
// LLM이 타입을 활용해 올바른 코드 생성
interface UserProfile {
  id: string;
  email: string;
  createdAt: Date;  // string이 아님 — LLM이 타입에서 힌트 얻음
}
```

### 절대 경로 강제 (poka-yoke)
```text
# 도구 정의에서 상대 경로 오류 방지
"file_path": {
  "description": "Absolute path only (e.g., /home/user/project/main.py). Relative paths cause errors."
}
```

### 인터페이스 기반 모킹 테스트
```typescript
// 인터페이스 기반 모킹 → LLM이 올바른 모킹 패턴 이해
interface UserRepository {
  findById(id: string): Promise<User | null>;
}
```

## 패턴 선택 가이드

| 환각 유형 | 권장 패턴 |
|---|---|
| 사실 오류 | RAG + Evals + Evaluator-Optimizer |
| 구조 오류 | Guardrails (Pydantic/JSON Schema) |
| 소스 오류 | RAG + "문서 기반으로만 답변" 지시 |
| 코드 오류 | 타입 힌트 + 테스트 자동화 + Evaluator-Optimizer |

## 관련 페이지
- [[wiki/design-pattern/concept-rag-pattern|RAG 패턴]]
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]] (Evaluator-Optimizer)
- [[wiki/design-pattern/concept-llm-friendly-code|LLM 친화적 코드 구조]]

## 출처
- [[wiki/design-pattern/source-eugeneyan-llm-patterns|Eugene Yan: LLM Patterns]]
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
