---
updated: 2026-07-06
total_topics: 2
total_pages: 58
---

# Coding Inventory — Index

LLM 탐색 진입점. topic 폴더별로 구성된 전체 페이지 카탈로그.
형식: `- [[경로|제목]] — {type} · {lang} — 한 줄 설명`

새 topic이 생기면 여기에 섹션을 추가한다.

---

## unity-ui-toolkit
- [[wiki/unity-ui-toolkit/ui-systems-comparison|UI 시스템 비교]] — concept · multi — UI Toolkit vs uGUI vs IMGUI 비교표 (Unity 6.3 권장 사항 포함)
- [[wiki/unity-ui-toolkit/introduction|UI Toolkit 소개]] — concept · multi — 아키텍처 개요, UXML/USS/C# 핵심 컴포넌트, UI Builder/Debugger 도구
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]] — how-to · cs/uxml — 커스텀 Editor 창 만들기 (UI Builder·UXML·C# 세 방법 비교)
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]] — concept · cs — VisualElement 계층, Panel, Draw Order, 좌표계 변환
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]] — concept · uxml/cs — UXML 파일 구조, 네임스페이스, 스타일 추가, 템플릿 재사용
- [[wiki/unity-ui-toolkit/uquery|UQuery]] — concept · cs — visual tree 요소 검색 (Q/Query, 체이닝, ForEach, 캐싱)
- [[wiki/unity-ui-toolkit/snippet-load-uxml|UXML/USS 로드 패턴]] — snippet · cs — SerializeField/AssetDatabase/Resources 로드, Instantiate/CloneTree 패턴
- [[wiki/unity-ui-toolkit/uss-layout-engine|레이아웃 엔진 (Flexbox)]] — concept · uss/cs — Yoga Flexbox, flex-direction/grow/shrink, Relative vs Absolute 위치
- [[wiki/unity-ui-toolkit/uss-naming-conventions|USS 네이밍 컨벤션 (BEM)]] — concept · uss/cs — BEM 패턴 (block__element--modifier), Kebab case 규칙
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — concept · cs/uss — 배칭, 텍스처 아틀라스, UsageHints, display=None, Source Generation 바인딩
- [[wiki/unity-ui-toolkit/ui-builder|UI Builder]] — concept · multi — StyleSheets/Hierarchy/Library/Viewport/Inspector 6개 패널 시각 편집기
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] — concept · uss — 셀렉터 유선순위, 속성, 변수, 트랜지션, TSS 테마
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]] — concept · cs — [UxmlElement]/[UxmlAttribute], 생성자 초기화, BaseField<T> 상속
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]] — concept · cs/uxml — MVVM, [CreateProperty], UXML 선언적 바인딩, INotifyBindablePropertyChanged
- [[wiki/unity-ui-toolkit/text-overview|텍스트 시스템]] — concept · cs/uss — TextCore/SDF, 폰트 에셋, 리치 텍스트 태그, OS 이모지
- [[wiki/unity-ui-toolkit/localization|로컬라이제이션]] — how-to · cs/uxml — String/Asset Tables, SelectedLocale API, Smart Strings
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]] — concept · cs — 3단계 전파, RegisterCallback, Manipulator, 드래그 조작기
- [[wiki/unity-ui-toolkit/editor-ui|Editor UI 구현]] — how-to · cs — EditorWindow+CreateGUI, 커스텀 Inspector, Property Drawer
- [[wiki/unity-ui-toolkit/runtime-ui|Runtime UI 구현]] — how-to · cs/uxml — UIDocument+MonoBehaviour, OnEnable UI 접근, PanelSettings
- [[wiki/unity-ui-toolkit/migration-overview|마이그레이션 가이드]] — how-to · cs/uxml — uGUI→UI Toolkit 컴포넌트 대응, 아키텍처 차이
- [[wiki/unity-ui-toolkit/uss-transitions|USS Transitions & Transform]] — concept · uss/cs — transition-property/duration/timing-function, translate/scale/rotate 애니메이션
- [[wiki/unity-ui-toolkit/uss-variables|USS Variables]] — concept · uss/cs — --variable-name 선언, var() 사용, Unity 내장 --unity-* 변수
- [[wiki/unity-ui-toolkit/uss-tss|USS TSS & 스타일 적용]] — concept · uss/cs — TSS @import 테마, element.style/styleSheets C# 적용, resolvedStyle, USS 성능
- [[wiki/unity-ui-toolkit/graphic-font-assets|그래픽 및 폰트 에셋 준비]] — concept · multi — Sprite Atlas, Dynamic Atlas, FontAsset/SDF, 에셋 임포트 Best Practices
- [[wiki/unity-ui-toolkit/custom-controls-advanced|커스텀 컨트롤 UXML 캡슐화]] — concept · cs/uxml — UXML-First vs Element-First(CloneTree), 요소 숨기기 성능 비교, 풀링
- [[wiki/unity-ui-toolkit/snippet-manipulators|Manipulator 스니펫]] — snippet · cs — ExampleDragger/ExampleResizer 전체 구현, CapturePointer 패턴
- [[wiki/unity-ui-toolkit/data-binding-runtime|Runtime Data Binding 설정하기]] — how-to · cs/uxml — [CreateProperty], TwoWay/ToTarget 모드, 업데이트 트리거
- [[wiki/unity-ui-toolkit/snippet-bindable-custom-control|바인딩 가능한 커스텀 컨트롤]] — snippet · cs/uxml — BindableElement+INotifyValueChanged, SetValueWithoutNotify, ChangeEvent<T> 발송
- [[wiki/unity-ui-toolkit/migration-imgui|IMGUI → UI Toolkit 마이그레이션]] — how-to · cs — OnGUI→CreateGUI 대응, IMGUIContainer 임베딩, 점진적 마이그레이션
- [[wiki/unity-ui-toolkit/events-reference|이벤트 타입 레퍼런스]] — concept · cs — Pointer/Change/Keyboard/Focus/Transition/Panel 이벤트 요약표
- [[wiki/unity-ui-toolkit/snippet-serialized-binding|SerializedObject 바인딩]] — snippet · cs — Bind(), BindProperty(), ViewData Persistence (Editor 전용)
- [[wiki/unity-ui-toolkit/runtime-ui-advanced|Runtime UI 고급]] — concept · cs — style.translate+DynamicTransform 이동 패턴, Panel Settings, World Space UI
- [[wiki/unity-ui-toolkit/text-rich-tags|Rich Text Tags 레퍼런스]] — concept · cs — b/i/color/size/link/sprite/gradient 전체 태그 목록
- [[wiki/unity-ui-toolkit/uss-selectors|USS 셀렉터 레퍼런스]] — concept · uss — 9가지 셀렉터 타입, pseudo-class, Specificity 우선순위
- [[wiki/unity-ui-toolkit/uxml-element-reference|UXML 내장 요소 레퍼런스]] — concept · uxml/cs — 내장 컨트롤 전체 카탈로그 (Runtime+Editor 구분)
- [[wiki/unity-ui-toolkit/howto-ui-builder|UI Builder 워크플로우]] — how-to · uxml/uss — 계층 구성→Inspector 스타일→USS 추출→Debugger 6단계
- [[wiki/unity-ui-toolkit/ui-renderer|UI Renderer]] — concept · cs — Painter2D(BeginPath/Arc/Fill) 및 Mesh API로 커스텀 2D 비주얼 생성
- [[wiki/unity-ui-toolkit/html-to-uxml-guide|HTML/CSS → UXML/USS 변환 가이드]] — concept · multi — 변환 흐름 5단계, 핵심 제약 요약, HTML/CSS 작성 10대 원칙
- [[wiki/unity-ui-toolkit/html-to-uxml-elements|HTML → UXML 요소 대응표]] — concept · uxml/cs — HTML 전 카테고리(구조·텍스트·폼·미디어) 요소의 UXML 매핑
- [[wiki/unity-ui-toolkit/css-to-uss-support|CSS 속성 USS 지원 여부]] — concept · uss — CSS 전 속성 ✅/⚠️/❌ 지원 여부 레퍼런스
- [[wiki/unity-ui-toolkit/html-to-uxml-layout|레이아웃 변환 가이드 (CSS → USS Flexbox)]] — how-to · uss/cs — Grid/Float/Block→Flexbox 변환 패턴, z-index·fixed·gap·@media 대체
- [[wiki/unity-ui-toolkit/uss-workarounds|미지원 CSS 패턴 USS 우회 방법]] — how-to · uss/cs — box-shadow/@keyframes/::before/filter 등 12가지 우회 구현
- [[wiki/unity-ui-toolkit/uss-exclusive-properties|USS 전용 속성 (-unity-*)]] — concept · uss/cs — -unity-font/-text-align/-background-scale-mode/-slice 등 전체 레퍼런스
- [[wiki/unity-ui-toolkit/concept-ui-shader-graph|UI Shader Graph 개요]] — concept · multi — URP 전용, Render Type Branch로 Solid/Texture/SDF/Bitmap/Gradient 분기 셰이더 작성
- [[wiki/unity-ui-toolkit/howto-ui-shader-graph-gradient|UI Shader Graph 그라디언트 버튼]] — how-to · multi — URP 프로젝트에서 셰이더 자산 생성→Material→UI Builder Button 적용 6단계
- [[wiki/unity-ui-toolkit/howto-uss-custom-filter|커스텀 USS 필터 (Swirl)]] — how-to · multi — FilterFunctionDefinition으로 USS filter() 인자↔셰이더 프로퍼티 매핑 (6.3 신규)

---

## design-pattern
- [[wiki/design-pattern/concept-gof-vs-llm-era|GoF vs LLM 시대 패턴]] — concept · multi — GoF 패턴과 LLM 친화적 패턴의 차이, 새로 등장한 패턴 분류
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]] — concept · multi — 프롬프트 체이닝·라우팅·병렬화·오케스트레이터-워커·자율 에이전트 7가지 패턴
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]] — concept · multi — MCP 아키텍처, ACI 설계 원칙, JSON Schema 기반 도구 정의
- [[wiki/design-pattern/concept-prompt-engineering-patterns|프롬프트 엔지니어링 패턴]] — concept · multi — zero-shot·few-shot·CoT·XML 구조화·역할 프롬프팅·Generated Knowledge
- [[wiki/design-pattern/concept-rag-pattern|RAG 패턴]] — concept · multi — RAG 아키텍처, 청킹 전략, 하이브리드 검색, fine-tuning과의 비교
- [[wiki/design-pattern/concept-llm-friendly-code|LLM 친화적 코드 구조]] — concept · multi — boring technology 원칙, 자기완결 모듈, CLAUDE.md 작성
- [[wiki/design-pattern/concept-hallucination-reduction|환각 감소 패턴]] — concept · multi — Evals·RAG·Guardrails·Evaluator-Optimizer·Defensive UX 5가지 패턴
- [[wiki/design-pattern/howto-structure-code-for-llm|LLM 보조 개발을 위한 코드베이스 구조화]] — how-to · multi — CLAUDE.md 작성, 라이브러리 선택, 모듈 경계, 컨텍스트 제공 방법
- [[wiki/design-pattern/howto-build-agentic-pipeline|에이전틱 파이프라인 구축]] — how-to · multi — 도구 정의, 오케스트레이터 루프, 오류 복구, eval 추가
- [[wiki/design-pattern/howto-prompt-chaining|프롬프트 체이닝 구현]] — how-to · multi — 태스크 분해, Generated Knowledge Prompting, 컨텍스트 재설정 전략
- [[wiki/design-pattern/snippet-chain-of-thought-prompt|CoT 프롬프트 템플릿]] — snippet · md — XML 태그 기반 CoT, 역할+XML+Few-Shot 조합, Generated Knowledge 패턴
- [[wiki/design-pattern/snippet-tool-use-schema|툴 스키마 정의 예시]] — snippet · json — Anthropic 함수 호출 스키마, MCP 서버 툴 정의, 좋은 설명 vs 나쁜 설명 비교

---

_총 topic: 2 / 총 페이지: 58 / 마지막 갱신: 2026-07-06_
