---
type: concept
topic: unity-ui-toolkit
lang: uxml/cs
tags: [unity, ui-toolkit, uxml, controls, reference, elements]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-uxml-element-reference.md]
---

# UXML 내장 요소 레퍼런스

> UI Toolkit의 내장 컨트롤 전체 목록. UXML 또는 C#에서 사용 가능. Editor-only 표시가 없는 요소는 Runtime에서도 사용 가능.

## 기본 요소

| 요소 | 용도 |
|------|------|
| `VisualElement` | 모든 UI 요소 기반 클래스 |
| `BindableElement` | 데이터 바인딩 지원 기반 클래스 |

---

## 표시 요소

| 요소 | 용도 |
|------|------|
| `Label` | 텍스트 표시 |
| `Image` | 텍스처/스프라이트 표시 |
| `ProgressBar` | 진행률 표시 (읽기 전용) |
| `HelpBox` | 정보/경고 메시지 박스 |

---

## 버튼 & 액션

| 요소 | 용도 |
|------|------|
| `Button` | 클릭 버튼 |
| `RepeatButton` | 누르고 있으면 반복 발화 |

---

## 텍스트 입력

| 요소 | 용도 |
|------|------|
| `TextField` | 문자열 (multiline, password 지원) |
| `IntegerField` | 정수 |
| `FloatField` | float |
| `DoubleField` | double |
| `LongField` | long |
| `UnsignedIntegerField` | uint |
| `UnsignedLongField` | ulong |

---

## 벡터 & 기하

| 요소 | 용도 |
|------|------|
| `Vector2Field` / `Vector3Field` / `Vector4Field` | float 벡터 |
| `Vector2IntField` / `Vector3IntField` | int 벡터 |
| `RectField` / `RectIntField` | 사각형 |
| `BoundsField` / `BoundsIntField` | 경계 볼륨 |
| `Hash128Field` | 128비트 해시 |

---

## 선택 컨트롤

| 요소 | 용도 |
|------|------|
| `Toggle` | 체크박스 (bool) |
| `RadioButton` | 라디오 버튼 단일 |
| `RadioButtonGroup` | 라디오 버튼 그룹 |
| `DropdownField` | 드롭다운 선택 |
| `EnumField` | enum 선택 |
| `EnumFlagsField` | enum flags 다중 선택 |
| `ToggleButtonGroup` | 버튼 그룹 토글 |

---

## 슬라이더

| 요소 | 용도 |
|------|------|
| `Slider` | float 범위 슬라이더 |
| `SliderInt` | int 범위 슬라이더 |
| `MinMaxSlider` | 최솟값/최댓값 슬라이더 |

---

## 컨테이너 & 레이아웃

| 요소 | 용도 |
|------|------|
| `Box` | 시각적 컨테이너 |
| `GroupBox` | 레이블 있는 컨트롤 그룹 |
| `ScrollView` | 스크롤 가능한 컨테이너 |
| `TwoPaneSplitView` | 크기 조정 2패널 |
| `Foldout` | 접기/펴기 섹션 |

---

## 목록 & 트리

| 요소 | 용도 |
|------|------|
| `ListView` | 가상화 스크롤 목록 (아이템 재활용) |
| `TreeView` | 계층형 트리 |
| `MultiColumnListView` | 다중 컬럼 목록 |
| `MultiColumnTreeView` | 다중 컬럼 트리 |

---

## 탭 & 팝업

| 요소 | 용도 |
|------|------|
| `TabView` + `Tab` | 탭 인터페이스 |
| `PopupWindow` | 플로팅 팝업 |

---

## Editor 전용 컨트롤

| 요소 | 용도 |
|------|------|
| `ColorField` | 색상 선택 |
| `GradientField` | 그라디언트 편집 |
| `CurveField` | 애니메이션 커브 |
| `ObjectField` | 에셋/오브젝트 참조 |
| `PropertyField` | Inspector 스타일 자동 UI |
| `InspectorElement` | 인스펙터 패널 |
| `TagField` / `LayerField` / `LayerMaskField` | Unity 태그/레이어 |
| `MaskField` / `Mask64Field` | 마스크 선택 |
| `IMGUIContainer` | 기존 IMGUI 코드 임베딩 |

---

## 툴바 (Editor 전용)

`Toolbar`, `ToolbarButton`, `ToolbarMenu`, `ToolbarToggle`, `ToolbarSpacer`, `ToolbarSearchField`, `ToolbarPopupSearchField`, `ToolbarBreadcrumbs`

---

## UXML 사용 예

```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:Label text="Name:" />
    <ui:TextField name="name-field" label="Name" />
    <ui:Toggle name="active-toggle" label="Active" />
    <ui:Slider name="speed-slider" label="Speed" low-value="0" high-value="100" />
    <ui:Button name="submit-btn" text="Submit" />
    <ui:ListView name="item-list" />
</ui:UXML>
```

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]] — UXML 파일 구조, 네임스페이스
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]] — 내장 요소 확장
- [[wiki/unity-ui-toolkit/snippet-serialized-binding|SerializedObject 바인딩]] — PropertyField 활용

## 출처
- `raw/unity-ui-toolkit-uxml-element-reference.md`
