---
type: how-to
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, uss, filter, filter-function-definition, shader, tutorial]
created: 2026-05-03
updated: 2026-05-03
sources: [raw/unity-ui-toolkit-uss-custom-filter.md]
---

# 커스텀 USS 필터 만들기 (Swirl 예제)

> USS `filter()` 함수에 직접 만든 셰이더를 붙여 VisualElement에 픽셀 후처리 효과를 적용하는 방법. `FilterFunctionDefinition` 자산이 셰이더 프로퍼티 ↔ USS 인자 매핑을 담당한다.

## 전제 조건

- UXML, USS, USS `filter` 메커니즘 기본 이해
- Unity 6.3 LTS 이상 (FilterFunctionDefinition은 6.3 신규)
- Swirl 예제 자산: [Unity 공식 GitHub 샘플](https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-custom-swirl-filter)에서 `Swirl.shader`, `Swirl.mat` 다운로드

## 단계

### 1. 자산 폴더 준비

`Assets/SwirlFilter/` 폴더 생성 후 GitHub 샘플의 `Swirl.shader`, `Swirl.mat` 두 파일을 넣는다.

### 2. FilterFunctionDefinition 자산 생성

`SwirlFilter/` 폴더 우클릭 → **Create > UI Toolkit > Filter Function Definition** → 이름 `SwirlFilter`.

### 3. Inspector에서 정의 설정

| 항목 | 값 |
|---|---|
| **Filter Name** | `swirl` |
| **Parameters** | `Angle` (Type: float), `Radius` (Type: float) |
| **Passes** | Material에 `Swirl.shader` 할당 |
| **Parameter Bindings** | Index 0 → Property `_Angle`, Index 1 → Property `_Radius` |

`Parameter Bindings`가 USS에서 넘긴 인자 위치(Index)를 셰이더 프로퍼티 이름에 연결한다.

### 4. UXML / USS 작성

`SwirlFilterExample.uss`:

```css
.outside {
    flex-grow: 1;
    position: absolute;
    height: 207px;
    width: 234px;
    top: 46px;
    left: 27px;
    background-color: rgb(255, 0, 0);
}

.inside {
    flex-grow: 1;
    position: absolute;
    height: 75px;
    width: 100px;
    top: 46px;
    left: 27px;
    background-color: rgb(0, 255, 247);
}
```

`SwirlFilterExample.uxml`:

```xml
<engine:UXML xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
             xmlns:engine="UnityEngine.UIElements"
             xmlns:editor="UnityEditor.UIElements"
             noNamespaceSchemaLocation="../UIElementsSchema/UIElements.xsd"
             editor-extension-mode="False">
    <Style src="SwirlFilterExample.uss" />
    <engine:VisualElement class="outside">
        <engine:VisualElement class="inside" />
    </engine:VisualElement>
</engine:UXML>
```

### 5. UI Builder에서 필터 연결

1. `SwirlFilterExample.uxml` 더블클릭으로 UI Builder 진입
2. **StyleSheets 패널** → **+** → **Add Existing USS** → `SwirlFilterExample.uss` 선택
3. **Hierarchy** 에서 부모 `VisualElement` 선택
4. Inspector의 **Inline Styles > Filter** → **Add (+)**
5. **Function** 드롭다운에서 `Custom` 선택
6. **Definition**에 `SwirlFilter` 자산 지정
7. 인자 입력: **Angle**=`58.9`, **Radius**=`2.3`

### 6. (선택) USS 클래스로 추출

Style Class List에 `.filter-effect` 추가 → "Extract Inlined Style to New Class" 실행 → 다음과 같이 USS로 떨어진다:

```css
.filterEffect {
    filter: filter("SwirlFilter/SwirlFilterFunction.asset" 58.9 2.3);
}
```

`filter()` 함수는 첫 인자가 FilterFunctionDefinition 자산 경로, 이후 인자는 Index 순서대로 매핑된다.

## 검증 방법

- UI Builder Viewport에서 빨강 박스가 swirl 회전·반경 값에 따라 왜곡되는지 확인
- Angle/Radius 값을 바꿔 실시간 반응이 있어야 함
- 자식(`.inside`)도 함께 왜곡됨 — 필터는 visual tree 서브트리에 적용

## UI Shader Graph와의 차이

| 구분 | UI Shader Graph | FilterFunctionDefinition (USS filter) |
|---|---|---|
| 적용 대상 | 요소 메시 렌더링 자체 | 요소를 그린 결과 픽셀(렌더 타깃) |
| 진입점 | `-unity-material` / Material 슬롯 | USS `filter:` 속성 |
| 효과 종류 | 색·UV·텍스트 등 mesh-level | blur·distort·color grading 등 post-process |
| URP 필수 | ✅ | ❌ (Built-in도 가능) |

## 주의사항

- USS `filter:`는 부모에 적용 시 자식까지 함께 영향 받음
- 셰이더 프로퍼티 이름과 Parameter Bindings의 Property 문자열이 정확히 일치해야 함 (`_Angle` 의 언더스코어 포함)
- Filter Name(`swirl`)은 내부 식별자이며 USS에선 자산 경로 문자열로 참조하므로 헷갈리지 말 것

## 관련 페이지

- [[wiki/unity-ui-toolkit/concept-ui-shader-graph|UI Shader Graph 개요]] — mesh-level 셰이더 작성
- [[wiki/unity-ui-toolkit/uss-workarounds|미지원 CSS 패턴 USS 우회]] — 6.3 이전 filter 우회 (이젠 본 페이지로 대체 가능)
- [[wiki/unity-ui-toolkit/howto-ui-builder|UI Builder 워크플로우]]

## 출처

- [[wiki/unity-ui-toolkit/source-uss-custom-filter|Create a custom swirl filter (Unity 6.3 Manual)]]
- 예제 코드: https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-custom-swirl-filter
