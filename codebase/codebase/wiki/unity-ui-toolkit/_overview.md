---
type: overview
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, uxml, uss, ui-builder]
created: 2026-04-16
updated: 2026-06-11
---

# Unity UI Toolkit

> Unity Editor 및 런타임 게임/앱 UI를 개발하기 위한 웹 기반 프레임워크 (UXML + USS + C#)

## 이 주제에서 다루는 것

- UI Toolkit의 개념·아키텍처·핵심 컴포넌트
- UI Builder, UXML, USS 사용법
- Visual Tree 구조 및 요소 다루기
- USS 스타일링·셀렉터·변수·트랜지션·Transform
- 이벤트 시스템 및 데이터 바인딩
- 커스텀 컨트롤 생성 및 UXML 캡슐화
- Editor UI 및 Runtime UI 구현
- 텍스트·폰트·리치 텍스트 태그
- 마이그레이션 (uGUI → UI Toolkit, IMGUI → UI Toolkit)
- 성능 최적화 및 모범 사례
- 2D 커스텀 비주얼 (Painter2D, Mesh API)
- UI Shader Graph (URP 전용 커스텀 셰이더, Render Type Branch)
- 커스텀 USS 필터 (FilterFunctionDefinition, 픽셀 후처리)

---

## 페이지 목록

### 개요 및 시작하기
- [[wiki/unity-ui-toolkit/ui-systems-comparison|UI 시스템 비교]] — concept · multi
- [[wiki/unity-ui-toolkit/introduction|UI Toolkit 소개]] — concept · multi
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]] — how-to · cs/uxml

### Structure UI (UXML)
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]] — concept · cs
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]] — concept · uxml/cs
- [[wiki/unity-ui-toolkit/uquery|UQuery]] — concept · cs
- [[wiki/unity-ui-toolkit/snippet-load-uxml|UXML/USS 로드 패턴]] — snippet · cs
- [[wiki/unity-ui-toolkit/uxml-element-reference|UXML 내장 요소 레퍼런스]] — concept · uxml/cs

### Style UI (USS)
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] — concept · uss
- [[wiki/unity-ui-toolkit/uss-selectors|USS 셀렉터 레퍼런스]] — concept · uss
- [[wiki/unity-ui-toolkit/uss-layout-engine|레이아웃 엔진 (Flexbox)]] — concept · uss/cs
- [[wiki/unity-ui-toolkit/uss-naming-conventions|USS 네이밍 컨벤션 (BEM)]] — concept · uss/cs
- [[wiki/unity-ui-toolkit/uss-transitions|USS Transitions & Transform]] — concept · uss/cs
- [[wiki/unity-ui-toolkit/uss-variables|USS Variables]] — concept · uss/cs
- [[wiki/unity-ui-toolkit/uss-tss|USS TSS & 스타일 적용]] — concept · uss/cs

### 이벤트 시스템
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]] — concept · cs
- [[wiki/unity-ui-toolkit/events-reference|이벤트 타입 레퍼런스]] — concept · cs
- [[wiki/unity-ui-toolkit/snippet-manipulators|Manipulator 스니펫]] — snippet · cs

### 데이터 바인딩
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]] — concept · cs/uxml
- [[wiki/unity-ui-toolkit/data-binding-runtime|Runtime Data Binding]] — how-to · cs/uxml
- [[wiki/unity-ui-toolkit/snippet-serialized-binding|SerializedObject 바인딩]] — snippet · cs
- [[wiki/unity-ui-toolkit/snippet-bindable-custom-control|바인딩 가능한 커스텀 컨트롤]] — snippet · cs/uxml

### 커스텀 컨트롤
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤 기초]] — concept · cs
- [[wiki/unity-ui-toolkit/custom-controls-advanced|커스텀 컨트롤 고급]] — concept · cs/uxml

### Editor UI
- [[wiki/unity-ui-toolkit/editor-ui|Editor UI 구현]] — how-to · cs
- [[wiki/unity-ui-toolkit/ui-builder|UI Builder 개요]] — concept · multi
- [[wiki/unity-ui-toolkit/howto-ui-builder|UI Builder 워크플로우]] — how-to · uxml/uss

### Runtime UI
- [[wiki/unity-ui-toolkit/runtime-ui|Runtime UI 구현]] — how-to · cs/uxml
- [[wiki/unity-ui-toolkit/runtime-ui-advanced|Runtime UI 고급]] — concept · cs

### 텍스트
- [[wiki/unity-ui-toolkit/text-overview|텍스트 시스템]] — concept · cs/uss
- [[wiki/unity-ui-toolkit/text-rich-tags|Rich Text Tags 레퍼런스]] — concept · cs
- [[wiki/unity-ui-toolkit/graphic-font-assets|그래픽 및 폰트 에셋 준비]] — concept · multi

### 마이그레이션
- [[wiki/unity-ui-toolkit/migration-overview|마이그레이션 가이드 (uGUI)]] — how-to · cs/uxml
- [[wiki/unity-ui-toolkit/migration-imgui|IMGUI → UI Toolkit 마이그레이션]] — how-to · cs

### 성능 최적화
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — concept · cs/uss

### 로컬라이제이션
- [[wiki/unity-ui-toolkit/localization|로컬라이제이션]] — how-to · cs/uxml

### UI Renderer
- [[wiki/unity-ui-toolkit/ui-renderer|UI Renderer (Painter2D / Mesh API)]] — concept · cs

### Shader Graph & USS Filter
- [[wiki/unity-ui-toolkit/concept-ui-shader-graph|UI Shader Graph 개요]] — concept · multi
- [[wiki/unity-ui-toolkit/howto-ui-shader-graph-gradient|UI Shader Graph 그라디언트 버튼]] — how-to · multi
- [[wiki/unity-ui-toolkit/howto-uss-custom-filter|커스텀 USS 필터 (Swirl)]] — how-to · multi

### HTML/CSS → UXML/USS 변환
- [[wiki/unity-ui-toolkit/html-to-uxml-guide|변환 가이드 개요]] — concept · multi
- [[wiki/unity-ui-toolkit/html-to-uxml-elements|HTML → UXML 요소 대응표]] — concept · uxml/cs
- [[wiki/unity-ui-toolkit/css-to-uss-support|CSS 속성 USS 지원 여부]] — concept · uss
- [[wiki/unity-ui-toolkit/html-to-uxml-layout|레이아웃 변환 가이드 (CSS → USS Flexbox)]] — how-to · uss/cs
- [[wiki/unity-ui-toolkit/uss-workarounds|미지원 CSS 패턴 USS 우회 방법]] — how-to · uss/cs
- [[wiki/unity-ui-toolkit/uss-exclusive-properties|USS 전용 속성 (-unity-*)]] — concept · uss/cs

---

## Sources

### 공식 문서 소스
- [[wiki/unity-ui-toolkit/source-compare|UI 시스템 비교]]
- [[wiki/unity-ui-toolkit/source-introduction|UI Toolkit 소개]]
- [[wiki/unity-ui-toolkit/source-main|메인 랜딩]]
- [[wiki/unity-ui-toolkit/source-get-started|Get Started 튜토리얼]]
- [[wiki/unity-ui-toolkit/source-ebook-index|E-Book 인덱스]]
- [[wiki/unity-ui-toolkit/source-visual-tree|Visual Tree]]
- [[wiki/unity-ui-toolkit/source-uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/source-uquery|UQuery]]
- [[wiki/unity-ui-toolkit/source-uxml-loading|UXML 로딩]]
- [[wiki/unity-ui-toolkit/source-ebook-layouts|Flexbox 레이아웃]]
- [[wiki/unity-ui-toolkit/source-ebook-naming|BEM 네이밍]]
- [[wiki/unity-ui-toolkit/source-ebook-performance|성능 최적화]]
- [[wiki/unity-ui-toolkit/source-custom-controls|커스텀 컨트롤]]
- [[wiki/unity-ui-toolkit/source-data-binding|데이터 바인딩]]
- [[wiki/unity-ui-toolkit/source-ebook-styling|Styling Best Practices (E-Book)]]
- [[wiki/unity-ui-toolkit/source-ui-builder|UI Builder]]
- [[wiki/unity-ui-toolkit/source-text|텍스트 시스템]]
- [[wiki/unity-ui-toolkit/source-localization|로컬라이제이션]]
- [[wiki/unity-ui-toolkit/source-events|이벤트 시스템]]
- [[wiki/unity-ui-toolkit/source-editor-runtime-ui|Editor/Runtime UI]]
- [[wiki/unity-ui-toolkit/source-migration-ugui|uGUI 마이그레이션]]
- [[wiki/unity-ui-toolkit/source-uss-transitions|USS Transitions & Transform]]
- [[wiki/unity-ui-toolkit/source-uss-variables|USS Variables]]
- [[wiki/unity-ui-toolkit/source-uss-tss|TSS & C# 스타일 적용]]
- [[wiki/unity-ui-toolkit/source-ebook-graphic-font|그래픽/폰트 에셋]]
- [[wiki/unity-ui-toolkit/source-custom-controls-advanced|커스텀 컨트롤 고급]]
- [[wiki/unity-ui-toolkit/source-events-advanced|Events 고급]]
- [[wiki/unity-ui-toolkit/source-data-binding-runtime|Runtime Binding]]
- [[wiki/unity-ui-toolkit/source-bindable-control|바인딩 커스텀 컨트롤]]
- [[wiki/unity-ui-toolkit/source-migration-imgui|IMGUI 마이그레이션]]
- [[wiki/unity-ui-toolkit/source-events-reference|Events Reference]]
- [[wiki/unity-ui-toolkit/source-runtime-ui-advanced|Runtime UI 고급]]
- [[wiki/unity-ui-toolkit/source-text-rich-tags|Rich Text Tags]]
- [[wiki/unity-ui-toolkit/source-uss-selectors|USS 셀렉터]]
- [[wiki/unity-ui-toolkit/source-uxml-element-reference|UXML 요소 레퍼런스]]
- [[wiki/unity-ui-toolkit/source-ui-builder-workflow|UI Builder 워크플로우]]
- [[wiki/unity-ui-toolkit/source-ui-renderer|UI Renderer]]
- [[wiki/unity-ui-toolkit/source-shader-graph-intro|UI Shader Graph 소개]]
- [[wiki/unity-ui-toolkit/source-shader-graph-tutorial|UI Shader Graph 튜토리얼]]
- [[wiki/unity-ui-toolkit/source-uss-custom-filter|커스텀 USS 필터 (Swirl)]]

### HTML/CSS → UXML/USS 변환 소스
- [[wiki/unity-ui-toolkit/source-html-to-uxml-overview|변환 레퍼런스 개요]]
- [[wiki/unity-ui-toolkit/source-html-to-uxml-element-mapping|HTML 요소 → UXML 대응표]]
- [[wiki/unity-ui-toolkit/source-html-to-uxml-css-property-support|CSS 속성 USS 지원 여부]]
- [[wiki/unity-ui-toolkit/source-html-to-uxml-layout-conversion|레이아웃 변환 가이드]]
- [[wiki/unity-ui-toolkit/source-html-to-uxml-unsupported-patterns|미지원 CSS 패턴 우회 방법]]
- [[wiki/unity-ui-toolkit/source-html-to-uxml-uss-exclusive|USS 전용 속성 레퍼런스]]
