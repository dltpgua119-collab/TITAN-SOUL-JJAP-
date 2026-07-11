---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, agentic, orchestrator, anthropic]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-anthropic-building-effective-agents.md, raw/design-pattern-azure-genai-gateway.md]
---

# 에이전틱 워크플로우 패턴

> LLM이 연속된 행동을 취하는 시스템의 표준 패턴. 단순한 것부터 시작해 필요할 때만 복잡성을 추가한다.

## 핵심 분류

**워크플로우(Workflow)**: LLM과 도구가 사전 정의된 코드 경로로 동작
**에이전트(Agent)**: LLM이 자신의 프로세스와 도구 사용을 동적으로 지시

"에이전틱 시스템은 종종 지연 시간과 비용을 더 나은 태스크 성능과 교환한다."

## 7가지 패턴 (복잡도 오름차순)

### 1. Augmented LLM (기본 빌딩 블록)

```text
[사용자 입력] → [LLM + 검색(RAG) + 도구 + 메모리] → [출력]
```

모든 에이전틱 패턴의 핵심 단위. 단독으로도 많은 태스크 처리 가능.

**언제 사용**: 태스크가 단순하고 단일 LLM 호출로 처리 가능할 때

---

### 2. 프롬프트 체이닝 (Prompt Chaining)

```text
[입력] → [LLM₁] → [출력₁] → [LLM₂] → [출력₂] → ...
```

태스크를 순차 단계로 분해. 각 LLM 호출이 이전 출력을 처리.

**언제 사용**: 태스크가 고정된 서브태스크로 명확히 분해될 때
**예시**: 번역 → 품질 검토 → 최종 편집

---

### 3. 라우팅 (Routing)

```text
[입력] → [분류기] → 케이스 A? → [전문 LLM A]
                  → 케이스 B? → [전문 LLM B]
                  → 케이스 C? → [전문 LLM C]
```

입력을 분류한 후 전문화된 다운스트림 태스크로 안내.

**언제 사용**: 입력 유형에 따라 최적 처리 방법이 다를 때
**예시**: 고객 문의를 기술/청구/환불 팀으로 분류

---

### 4. 병렬화 (Parallelization)

**Sectioning** (독립 서브태스크):
```text
[입력] → [LLM₁] ─┐
       → [LLM₂] ─┼→ [집계] → [출력]
       → [LLM₃] ─┘
```

**Voting** (신뢰도 향상):
```text
[입력] → [LLM₁] ─┐
       → [LLM₂] ─┼→ [투표/합의] → [출력]
       → [LLM₃] ─┘
```

**언제 사용**:
- Sectioning: 태스크가 독립적인 병렬 서브태스크로 분해될 때
- Voting: 중요한 결정에서 신뢰도를 높여야 할 때

---

### 5. 오케스트레이터-워커 (Orchestrator-Workers)

```text
[입력] → [오케스트레이터 LLM]
            ↓ 동적 태스크 분해
         [워커 LLM₁] [워커 LLM₂] [워커 LLM₃]
            ↓
         [오케스트레이터: 결과 통합]
```

중앙 LLM이 동적으로 태스크를 분해하고 워커 LLM에 위임.

**언제 사용**: 실행 전에 필요한 서브태스크를 예측할 수 없을 때
**예시**: 코딩 에이전트 (어떤 파일을 수정할지 미리 알 수 없음)

---

### 6. 평가자-최적화자 (Evaluator-Optimizer)

```text
[입력] → [생성 LLM] → [출력 초안]
                           ↓
                     [평가 LLM] → 피드백
                           ↓ (기준 충족 시 종료)
                     [생성 LLM] → [개선된 출력]
```

한 LLM이 응답을 생성하고 다른 LLM이 피드백 루프에서 평가.

**언제 사용**: 출력 품질 기준이 명확하고 반복 개선이 가능할 때
**예시**: 코드 생성 후 테스트 실행 결과로 개선

---

### 7. 자율 에이전트 (Autonomous Agent)

```text
[목표] → [계획 수립] → [도구 실행] → [관찰]
                           ↑              ↓
                      [다음 행동 결정] ←─┘
                           ↓ (목표 달성 or 인간 개입 요청)
                        [종료]
```

독립적으로 계획하고 행동. 필요 시 인간에게 안내 요청.

**언제 사용**: 태스크가 매우 복잡하고 긴 시간 수평이 필요할 때
**주의**: 비용·지연이 크게 증가. 실패 모드 예측 어려움.

---

## 게이트웨이 패턴 (프로덕션 스케일)

여러 에이전틱 패턴이 동일한 LLM API를 사용할 때 API 게이트웨이를 중간에 삽입:

```text
[에이전트/워크플로우] → [API 게이트웨이] → [LLM API]
                              ↓
                    인증·속도제한·로드밸런싱·모니터링·회로차단기
```

**게이트웨이가 해결하는 문제**:
- 여러 클라이언트 간 할당량 공정 배분
- 모델 배포 장애 시 자동 페일오버 (회로 차단기)
- 교차 에이전트 사용량 모니터링·비용 귀속

## 패턴 선택 가이드

1. 단순한 것부터 시작 — Augmented LLM로 가능하면 더 이상 필요 없음
2. 태스크 분해 가능 여부로 체이닝 vs 에이전트 결정
3. 독립성 여부로 병렬화 결정
4. 서브태스크 예측 가능 여부로 오케스트레이터-워커 결정

## 관련 페이지
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]]
- [[wiki/design-pattern/howto-build-agentic-pipeline|에이전틱 파이프라인 구축]]
- [[wiki/design-pattern/howto-prompt-chaining|프롬프트 체이닝 구현]]

## 출처
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
- [[wiki/design-pattern/source-azure-genai-gateway|Azure GenAI Gateway]]
