---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, rag, retrieval, hallucination]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-eugeneyan-llm-patterns.md]
---

# RAG 패턴 (Retrieval-Augmented Generation)

> LLM 응답을 외부의 최신 데이터로 grounding해 환각 감소, 지식 업데이트 비용 절감.

## 왜 RAG인가

| 방법 | 장점 | 단점 |
|---|---|---|
| RAG | 저렴, 데이터 제거 용이, 최신 정보 반영 | 검색 품질에 의존, 청킹 전략 필요 |
| Fine-tuning | 특화 성능, 행동 제어 | 비쌈, 데이터 유지보수 어려움 |
| Context stuffing | 단순 | 컨텍스트 한계, 비용 증가 |

## RAG 아키텍처

```text
[사용자 쿼리]
      ↓
[쿼리 임베딩]
      ↓
[벡터 DB 검색] ← [문서 청킹 + 인덱싱] ← [외부 데이터]
      ↓
[관련 문서 k개 검색]
      ↓
[LLM 프롬프트 구성: 컨텍스트 + 쿼리]
      ↓
[LLM 응답 생성 (grounded)]
```

## 핵심 컴포넌트

### 1. 텍스트 임베딩

문서를 압축된 벡터 표현으로 변환. 의미론적으로 유사한 텍스트가 벡터 공간에서 가까워짐.

**추천 임베딩 모델**: E5, Instructor, GTE 계열

### 2. 문서 청킹 전략

| 전략 | 설명 | 언제 사용 |
|---|---|---|
| 고정 크기 청킹 | 512/1024 토큰으로 분할 | 단순 문서 |
| 문장/단락 경계 | 의미론적 경계에서 분할 | 구조화된 문서 |
| 계층적 청킹 | 문서→섹션→단락 계층 | 긴 기술 문서 |
| 슬라이딩 윈도우 | 겹치는 청크로 맥락 손실 방지 | 연속적 내용 |

### 3. 검색 방법

**하이브리드 검색 (권장)**:
```text
최종 점수 = α × BM25 점수 + (1-α) × 시맨틱 점수
```

- **BM25**: 키워드 기반, 희소 검색. 정확한 용어 일치에 강함
- **시맨틱 검색 (Dense)**: 의미론적 유사도. 패러프레이즈에 강함

**근사 최근접 이웃 (ANN)** — 대규모 배포:
- FAISS: 페이스북, 성숙한 라이브러리
- HNSW: 빠른 그래프 기반
- ScaNN: 구글, 정밀도/속도 균형

### 4. 메타데이터 활용

```python
# 메타데이터로 검색 필터링 예시
results = vector_db.search(
    query_embedding,
    filter={"date": {"$gte": "2024-01-01"}, "source": "official_docs"},
    top_k=5
)
```

날짜·카테고리·소스로 필터링해 검색 품질 향상.

## RAG 진화 (학술)

| 이름 | 특징 |
|---|---|
| DPR | 질문-문단 매칭을 위한 파인튜닝된 dual encoder |
| RAG (원본) | 검색 + seq2seq 생성 결합 |
| FiD (Fusion-in-Decoder) | encoder에서 문단을 독립적으로 처리 |
| RETRO | 사전학습 전체에서 청크 교차 어텐션으로 검색 |

## RAG vs Fine-tuning 선택 기준

**RAG 선택**:
- 지식이 자주 업데이트됨
- 특정 문서를 소스로 인용해야 함
- 도메인 데이터 양이 적음
- 프라이버시 이유로 데이터를 모델에 내장 불가

**Fine-tuning 선택**:
- 특정 스타일·형식을 학습시켜야 함
- 도메인 특화 추론 패턴이 필요
- 지연 시간에 민감 (검색 레이턴시 제거)

## 주의사항

- 검색 품질이 RAG 전체 품질을 결정함 — "garbage in, garbage out"
- 너무 많은 컨텍스트를 주입하면 LLM이 오히려 관련 없는 정보에 집중할 수 있음
- 인용 정확성 검증: LLM이 검색된 내용을 정확히 인용하는지 확인 필요

## 관련 페이지
- [[wiki/design-pattern/concept-hallucination-reduction|환각 감소 패턴]]
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]]

## 출처
- [[wiki/design-pattern/source-eugeneyan-llm-patterns|Eugene Yan: LLM Patterns]]
