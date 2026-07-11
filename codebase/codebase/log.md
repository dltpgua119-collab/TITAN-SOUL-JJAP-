# Activity Log

Append-only. 절대 기존 항목 수정 금지.
형식: `## [YYYY-MM-DD] {action} | {제목}`
action: `ingest` | `query` | `error` | `lint` | `update`

파싱 팁: `grep "^## \[" log.md | tail -10`

---

## [2026-04-16] update | 위키 초기 구조 생성

디렉토리 구조, CLAUDE.md 스키마, index.md, log.md 초기화.

## [2026-04-16] update | Option A (주제 폴더 우선) 구조로 재설계

- wiki/ 하위를 언어/프레임워크 분류에서 topic 폴더 분류로 변경
- 각 topic 폴더에 _overview.md 규칙 추가
- index.md를 topic 섹션 기반으로 재구성
- CLAUDE.md 워크플로우 전면 업데이트

## [2026-04-16] ingest | UI 시스템 비교 (UI Toolkit vs uGUI vs IMGUI)

## [2026-04-16] ingest | UI Toolkit 소개 — 아키텍처, UXML/USS/C# 컴포넌트

## [2026-04-16] ingest | UI Toolkit 메인 랜딩 페이지

## [2026-04-16] ingest | Get Started with UI Toolkit — 커스텀 Editor 창 튜토리얼

## [2026-04-16] ingest | UI Toolkit for Advanced Developers E-Book 인덱스

## [2026-04-16] ingest | Visual Tree — VisualElement, Panel, Draw Order, 좌표계

## [2026-04-16] ingest | UXML 기초 — 파일 구조, 스타일 추가, 템플릿 재사용

## [2026-04-16] ingest | UQuery — visual tree 요소 검색

## [2026-04-16] ingest | UXML/USS 로드 및 인스턴스화 C# 패턴

## [2026-04-16] ingest | Built-in controls 기본 사용 패턴

## [2026-04-16] ingest | Flexbox 레이아웃 엔진 (Yoga) — E-Book Layouts 챕터

## [2026-04-16] ingest | USS 네이밍 컨벤션 (BEM) — E-Book Naming Conventions 챕터

## [2026-04-16] ingest | UI Toolkit 성능 최적화 — E-Book Optimizing Performance 챕터

## [2026-04-16] ingest | UI Builder — 인터페이스 패널 구성 및 워크플로우

## [2026-04-16] ingest | USS 기초 — 셀렉터, 속성, 변수, 트랜지션, TSS

## [2026-04-16] ingest | 커스텀 컨트롤 — UxmlElement/UxmlAttribute, BaseField<T>

## [2026-04-16] ingest | 데이터 바인딩 개요 — MVVM, Runtime Binding, INotifyBindablePropertyChanged

## [2026-04-16] ingest | 텍스트 시스템 — TextCore/SDF, 폰트 에셋, 리치 텍스트 태그

## [2026-04-16] ingest | 로컬라이제이션 — Unity Localization 패키지 + UI Toolkit 연동

## [2026-04-16] ingest | 이벤트 시스템 — 3단계 전파, RegisterCallback, Manipulator

## [2026-04-16] ingest | Editor UI 구현 — EditorWindow, 커스텀 Inspector, Property Drawer

## [2026-04-16] ingest | Runtime UI 구현 — UIDocument, PanelSettings, MonoBehaviour 패턴

## [2026-04-16] ingest | 마이그레이션 가이드 — uGUI → UI Toolkit 컴포넌트 대응표

## [2026-04-16] ingest | USS Transitions & Transform — 트랜지션 속성, translate/scale/rotate

## [2026-04-16] ingest | USS Variables — --variable-name, var(), Unity 내장 변수

## [2026-04-16] ingest | USS TSS & 스타일 적용 — @import 테마, C# element.style, resolvedStyle, Best Practices

## [2026-04-16] ingest | 그래픽 및 폰트 에셋 준비 — Sprite Atlas, Dynamic Atlas, FontAsset SDF

## [2026-04-16] ingest | 커스텀 컨트롤 UXML 캡슐화 — UXML-First vs Element-First, 요소 관리 패턴

## [2026-04-16] ingest | Manipulator 스니펫 — ExampleDragger, ExampleResizer, CapturePointer 패턴

## [2026-04-16] ingest | Runtime Data Binding 설정하기 — [CreateProperty], 바인딩 모드, 업데이트 트리거

## [2026-04-16] ingest | 바인딩 가능한 커스텀 컨트롤 — BindableElement, INotifyValueChanged, SetValueWithoutNotify

## [2026-04-16] ingest | IMGUI → UI Toolkit 마이그레이션 — OnGUI→CreateGUI, IMGUIContainer 임베딩

## [2026-04-16] ingest | 이벤트 타입 레퍼런스 — Pointer/Change/Keyboard/Focus/Transition/Panel 요약

## [2026-04-16] ingest | SerializedObject 바인딩 스니펫 — Bind(), BindProperty(), ViewData Persistence

## [2026-04-16] ingest | Runtime UI 고급 — style.translate+DynamicTransform, Panel Settings, World Space

## [2026-04-16] ingest | Rich Text Tags 레퍼런스 — b/i/color/size/link/sprite/gradient 전체 태그

## [2026-04-16] ingest | USS 셀렉터 레퍼런스 — 9가지 셀렉터 타입, pseudo-class, Specificity

## [2026-04-16] ingest | UXML 내장 요소 레퍼런스 — 전체 컨트롤 카탈로그 (Runtime+Editor)

## [2026-04-16] ingest | UI Builder 워크플로우 — 6단계, Debugger 활용, Live Reload

## [2026-04-16] ingest | UI Renderer — Painter2D(경로 드로잉), Mesh API(정점/인덱스), FillRule

## [2026-04-17] lint | 구조 문제 2건, 내용 문제 1건

## [2026-04-17] ingest | HTML/CSS → UXML/USS 변환 가이드 — 변환 흐름 5단계, 핵심 제약, 10대 원칙

## [2026-04-17] ingest | HTML → UXML 요소 대응표 — 구조/텍스트/폼/미디어 전 카테고리 매핑

## [2026-04-17] ingest | CSS 속성 USS 지원 여부 — 전 카테고리 ✅/⚠️/❌ 레퍼런스

## [2026-04-17] ingest | 레이아웃 변환 가이드 (CSS → USS Flexbox) — Grid/Float/Block 변환 패턴

## [2026-04-17] ingest | 미지원 CSS 패턴 USS 우회 방법 — box-shadow/@keyframes/filter 등 12가지

## [2026-04-17] ingest | USS 전용 속성 (-unity-*) — 폰트/배경/9-슬라이스/텍스트 외곽선 레퍼런스

## [2026-04-17] ingest | Anthropic: Building Effective Agents — 에이전틱 워크플로우 7가지 패턴 공식 가이드
## [2026-04-17] ingest | Eugene Yan: LLM Patterns — RAG·Guardrails·Evals·Caching·Defensive UX 패턴 카탈로그
## [2026-04-17] ingest | Anthropic Prompt Engineering Overview — 프롬프트 엔지니어링 기법 공식 문서
## [2026-04-17] ingest | Model Context Protocol Introduction & Architecture — MCP Host/Client/Server 아키텍처
## [2026-04-17] ingest | Martin Fowler: ChatGPT Test Automation Patterns — Generated Knowledge·CoT·마스터 플랜 패턴
## [2026-04-17] ingest | Azure: Access Azure OpenAI Through a Gateway — 게이트웨이 오프로딩·회로 차단기 아키텍처 패턴
## [2026-04-17] ingest | Simon Willison: Using LLMs for Code — LLM 보조 개발 워크플로우·boring technology 원칙

## [2026-04-17] lint | 구조 문제 2건 수정 (중복 구분선, 빨간 링크)
## [2026-05-03] query | UI Toolkit 셰이더 그래프 제작 문서 존재 여부 — 전용 페이지 없음, 4개 페이지에 단편 언급
## [2026-05-03] ingest | Unity 6.3 Manual: UI Shader Graph 3종 (Introduction · Get Started 튜토리얼 · Custom Swirl Filter)

## [2026-06-11] lint | 구조 문제 94건 수정(빨간링크 6·고아 overview 2·코드 펜스 언어 태그 85·index 카운트 1), 내용 문제 2건 방향만 제시(uss-workarounds·css-to-uss-support에 Unity 6.3 filter() 미반영)

## [2026-07-02] update | 배포용 정리 — unity-cameras·supabase-static-site 토픽 전체 제거(wiki·raw·index·log·교차링크), CLAUDE.md를 참조 전용으로 재작성. 이후 이 위키는 추가 없이 참조만 함 (topic 5→3, index 페이지 164→135)

## [2026-07-06] update | 토픽 전체 제거 및 관련 기록 소거 — 해당 wiki·raw 문서, index.md 섹션·프런트매터, CLAUDE.md 주제 목록, 교차링크, 과거 log 항목까지 전부 삭제 (topic 3→2, index 페이지 135→58)
