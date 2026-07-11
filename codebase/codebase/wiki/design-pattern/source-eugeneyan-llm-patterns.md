---
type: source
topic: design-pattern
lang: multi
tags: [llm, design-pattern, rag, guardrails, evals, caching]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/design-pattern-eugeneyan-llm-patterns.md
source_url: https://eugeneyan.com/writing/llm-patterns/
---

# Eugene Yan: LLM Patterns

## 핵심 내용

LLM 기반 시스템·제품 구축을 위한 7가지 핵심 패턴. 성능 향상 축(data-centric: Evals, RAG, Fine-tuning) vs 비용/위험 감소 축(user-centric: Caching, Guardrails, Defensive UX, 피드백)으로 분류됨.

## 주요 인사이트

1. **Evals 우선**: "팀에게 eval이 얼마나 중요한지는 쓰레기를 서둘러 내보내는 팀과 진지하게 제품을 만드는 팀을 가르는 주요 차별화 요소"
2. **RAG vs Fine-tuning**: RAG는 지속적 사전학습보다 저렴하고 문제 데이터 제거 용이; Fine-tuning은 특화 성능·행동 제어에 적합
3. **Caching 주의사항**: 의미론적 유사성 캐싱은 관련은 있지만 다른 쿼리에 잘못된 응답 위험. ID 기반·배치 캐싱이 더 안전
4. **Guardrails 전략**: 사후 검증보다 직접적인 구조 제어 선호
5. **데이터 플라이휠**: 더 나은 모델 → 개선된 UX → 사용량 증가 → 더 많은 학습 데이터 — 데이터를 전략적 자산으로 취급

## 이 소스로 생성된 페이지
- [[wiki/design-pattern/concept-rag-pattern|RAG 패턴]]
- [[wiki/design-pattern/concept-hallucination-reduction|환각 감소 패턴]]
- [[wiki/design-pattern/concept-llm-friendly-code|LLM 친화적 코드 구조]]

## 원문 링크
https://eugeneyan.com/writing/llm-patterns/
