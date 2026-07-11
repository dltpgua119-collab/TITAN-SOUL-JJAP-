---
topic: design-pattern
original_type: url
source_url: https://eugeneyan.com/writing/llm-patterns/
created: 2026-04-17
---

# Patterns for Building LLM-based Systems & Products — Eugene Yan

## Overview

7가지 핵심 패턴으로 LLM 기반 시스템을 구축하는 방법. 성능 향상 축(data-centric) vs 비용/위험 감소 축(user-centric)으로 분류됨.

## 7가지 핵심 패턴

### 1. Evals: 성능 측정

**목적**: 시스템 성능 평가 및 회귀 감지.

"How important evals are to the team is a major differentiator between folks rushing out hot garbage and those seriously building products."

**메트릭 종류**:
- BLEU: Precision 기반, n-gram 오버랩
- ROUGE: Recall 지향, 요약 평가용
- BERTScore: 임베딩 기반 의미 유사도
- MoverScore: Soft alignment (many-to-one)

**신흥 접근법**: GPT-4 같은 강력한 LLM을 reference-free evaluator로 사용 (G-Eval 프레임워크).

**전략**: 범용 벤치마크보다 태스크 특화 eval부터 시작. Eval Driven Development(EDD) 적용.

---

### 2. RAG (Retrieval-Augmented Generation): 지식 추가

**목적**: 외부의 최신 데이터로 LLM 응답을 grounding해 환각 감소 및 지식 업데이트 비용 절감.

**왜 RAG인가**: 지속적 사전학습보다 저렴; 문제 데이터 제거 용이; 사실 오류 감소.

**핵심 컴포넌트**:
- 텍스트 임베딩 (압축된 텍스트 표현)
- 밀집 벡터 검색 (희소 방법보다 우수)
- 문서 청킹

**RAG 진화**:
- Dense Passage Retrieval (DPR): 질문-문단 매칭을 위한 파인튜닝된 dual encoder
- RAG (원본): 검색 + seq2seq 생성 결합
- Fusion-in-Decoder (FiD): encoder에서 문단을 독립적으로 처리
- RETRO: 사전학습 전체에서 검색 + chunked cross-attention

**실용적 구현**:
- 하이브리드 검색 (BM25 + semantic search)
- 필터링·랭킹을 위한 메타데이터 활용
- 임베딩 모델: E5, Instructor, GTE 계열
- 근사 최근접 이웃(ANN): FAISS, HNSW, ScaNN

---

### 3. Fine-tuning: 특정 태스크 성능 향상

**목적**: 사전학습 모델을 특정 태스크에 적응시켜 성능과 제어력 향상.

**동기**:
- 범용 모델보다 나은 성능
- 더 큰 행동 제어력
- 태스크별 모델 모듈화
- 외부 API 없이 데이터 프라이버시

**파인튜닝 접근법**:
- Continued pre-training: 도메인 특화 next-token prediction
- Instruction fine-tuning: 지시 따르기 학습
- Single-task fine-tuning: 좁은 성능 특화
- RLHF: 인간 선호도로 보상 모델링

**파라미터 효율적 기법**:
- Soft prompt tuning: 학습 가능한 입력 임베딩
- Prefix tuning: 히든 스테이트의 학습 파라미터 (0.1%)
- Adapters: 태스크별 FC 레이어 (3.6%)
- LoRA: 저랭크 가중치 업데이트 (정규화 이점)
- QLoRA: 4비트 양자화로 65B 모델을 780GB+에서 48GB로 감소

---

### 4. Caching: 지연 시간·비용 감소

**목적**: 이전에 계산된 응답을 저장·재사용해 생성 지연 및 API 비용 절감.

**도전**: 의미론적 유사성 캐싱은 관련은 있지만 다른 쿼리에 잘못된 응답을 반환할 위험.

**더 안전한 접근법**:
- 사전 계산된 요약을 위해 항목 ID로 캐싱
- 특정 비교를 위해 항목 쌍 캐싱
- 제한된 입력 캐싱 (드롭다운 선택, 구조화된 쿼리)
- 오프라인 배치 모드로 사전 계산

**도구**: GPTCache 프레임워크 (설정 가능한 유사도 임계값).

**성공 지표**: 캐시 히트율 추적; 비랜덤 쿼리 분포(power-law 패턴)에서만 효과적.

---

### 5. Guardrails: 출력 품질 보장

**목적**: 구문 정확성, 사실성, 안전성, 적절한 구조를 위해 LLM 출력 검증 및 제약.

**검증 카테고리**:
- 구조적: JSON 스키마 강제
- 구문적: URL 유효성, 코드/SQL 구문 검사
- 의미론적: 입력과의 관련성, 환각 감지
- 안전성: 유해 콘텐츠 필터링

**도구 및 접근법**:
- Guardrails 패키지: 수정 액션이 있는 Pydantic 스타일 검증
- NeMo-Guardrails: 대화를 위한 LLM 기반 의미론적 guardrail
- Microsoft Guidance: 필요한 토큰을 주입하는 DSL

**전략**: 사후 검증보다 가능하면 직접적인 구조 제어 선호.

---

### 6. Defensive UX: 오류를 우아하게 처리

**목적**: 사용자 교육, 오류 처리, 신뢰 구축을 통해 ML/LLM 불완전성을 인정하는 시스템 설계.

**핵심 원칙** (Microsoft, Google, Apple 가이드라인):

**기대치 설정**:
- 기능과 한계 명확히 전달
- 부정확성에 대한 면책 조항 포함
- 투명성을 통한 사용자 신뢰 조정

**거부 가능성 제공**:
- AI 제안을 쉽게 무시할 수 있게
- 기능이 침해적으로 되지 않도록
- 사용자 자율성 유지

**귀속 제공**:
- 소스 자료 인용 포함
- 소셜 증명과 커뮤니티 검증 표시
- 사용자가 정보 품질 평가할 수 있도록

**친숙함 앵커링**:
- 새로운 인터랙션보다 확립된 UX 패턴 사용
- 기대치 대비 사용자 노력 최소화
- 채팅을 주요가 아닌 보조 인터페이스로 고려

---

### 7. 사용자 피드백 수집: 데이터 플라이휠 구축

**목적**: 모델 지속적 개선과 사용자 선호도 이해를 위한 명시적·암시적 피드백 수집.

**명시적 피드백**: 직접 사용자 응답 (엄지 척도, 재생성)
- ChatGPT의 평가 시스템
- Midjourney의 변형/업스케일 선택

**암시적 피드백**: 직접 프롬프트 없는 행동 신호
- Copilot의 코드 수락률
- 대화 길이 및 빈도
- 사용 패턴 변화

---

## 관련 패턴

**데이터 플라이휠**: 더 나은 모델 → 개선된 UX → 사용량 증가 → 더 많은 학습 데이터

**Cascade**: 복잡한 태스크를 특화된 서브태스크로 분해 (RAG 예시: 검색과 추론 분리)

**모니터링**: 시스템 성능 저하 추적

## 핵심 인사이트

이 패턴들은 상호 연결된 시스템을 형성함: eval이 진행을 측정하고, RAG가 지식을 grounding하고, fine-tuning이 능력을 특화하고, caching이 서빙을 최적화하고, guardrail이 안전성을 보장하고, defensive UX가 신뢰를 구축하고, 사용자 피드백이 개선 사이클을 이어감.

성공하려면 데이터(시범 예시, 인간 선호도, 사용자 피드백)를 전략적 자산으로 취급해야 함.
