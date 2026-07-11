---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, gof, architecture]
created: 2026-04-17
updated: 2026-04-17
sources: [raw/design-pattern-anthropic-building-effective-agents.md, raw/design-pattern-martinfowler-llm-patterns.md]
---

# GoF vs LLM 시대 패턴

> 전통적 GoF 패턴은 인간 개발자의 유지보수·확장성을 위해 설계됐다. LLM이 코드를 생성·수정·탐색하는 시대에는 다른 차원의 패턴이 등장했다.

## 두 패턴 체계의 차이

| 차원 | GoF 패턴 | LLM 친화적 패턴 |
|---|---|---|
| 설계 대상 | 인간 개발자 | LLM + 인간 협업 |
| 핵심 문제 | 코드 재사용·유지보수·확장 | 자율 추론·맥락 관리·안전한 행동 |
| 단위 | 클래스·인터페이스 관계 | 프롬프트·도구·에이전트 흐름 |
| 검증 방법 | 컴파일·정적 분석·단위 테스트 | Eval·인간 피드백·출력 검증 |
| 실패 모드 | 컴파일 오류·예외 | 환각·무한 루프·부적절한 도구 호출 |

## GoF 패턴의 LLM 시대 번역

### 잘 번역되는 GoF 패턴

**Strategy 패턴** → **라우팅 패턴**
- GoF: 알고리즘 군을 캡슐화해 교체 가능하게
- LLM: 입력 분류 후 전문화된 LLM/프롬프트로 라우팅

**Chain of Responsibility** → **프롬프트 체이닝**
- GoF: 요청을 처리할 수 있는 핸들러를 연결
- LLM: LLM 호출을 순차 연결, 이전 출력이 다음 입력이 됨

**Template Method** → **프롬프트 템플릿**
- GoF: 알고리즘 골격 정의, 세부사항은 서브클래스에 위임
- LLM: 프롬프트 구조(역할·컨텍스트·지시·출력 형식) 고정, 가변 부분만 채움

**Observer** → **알림 패턴 (MCP)**
- GoF: 상태 변화를 구독자에게 자동 알림
- LLM: MCP `notifications/tools/list_changed`로 도구 목록 변경 알림

### 새로 등장한 LLM 시대 패턴

**Augmented LLM** (기본 빌딩 블록)
: LLM + 검색(RAG) + 도구(함수 호출) + 메모리를 결합한 핵심 단위

**Evaluator-Optimizer** (LLM 시대 신규)
: 생성 LLM + 평가 LLM의 피드백 루프. GoF에는 대응 패턴 없음 — LLM의 비결정론적 출력을 다루기 위해 등장

**Orchestrator-Workers** (LLM 시대 신규)
: 중앙 LLM이 동적으로 태스크를 분해하고 워커 LLM에 위임. GoF Facade/Mediator와 유사하지만 위임 대상이 런타임에 결정됨

**Generated Knowledge Prompting** (LLM 시대 신규)
: 최종 결과 요청 전 먼저 관련 지식/계획을 생성하게 함 — GoF에는 대응 없음

## 패턴 선택 기준

**GoF 패턴 우선 적용**:
- 도메인 로직이 명확하고 결정론적인 경우
- 코드베이스가 인간 개발자가 주로 수정하는 경우
- 정확한 타입 안전성이 필요한 경우

**LLM 패턴 우선 적용**:
- 입력이 비구조적이고 다양한 경우 (자연어, 이미지 등)
- 태스크가 복잡해 단일 함수로 구현하기 어려운 경우
- 도메인 지식이 훈련 데이터에 이미 내재된 경우
- 사용자 의도가 모호하고 해석이 필요한 경우

## 주의사항

LLM 패턴은 비결정론적이다 — 동일한 입력에 다른 출력이 나올 수 있다. Eval 기반 검증이 필수.

## 관련 페이지
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]]
- [[wiki/design-pattern/concept-prompt-engineering-patterns|프롬프트 엔지니어링 패턴]]

## 출처
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
- [[wiki/design-pattern/source-martinfowler-llm-patterns|Martin Fowler: ChatGPT Test Automation Patterns]]
