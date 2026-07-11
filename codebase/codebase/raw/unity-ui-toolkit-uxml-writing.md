---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-WritingUXMLTemplate.html
created: 2026-04-16
---

# Introduction to UXML

## UXML이란?
Unity Extensible Markup Language — UI 구조를 정의하는 텍스트 파일.
"UXML along with USS makes it easier for less technical users to define the layout and the style of the UI."

## UXML 파일 구조
```xml
<?xml version="1.0" encoding="utf-8"?>
<engine:UXML
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:engine="UnityEngine.UIElements"
    xmlns:editor="UnityEditor.UIElements"
    xsi:noNamespaceSchemaLocation="../../UIElementsSchema/UIElements.xsd"
>
    <!-- UI elements -->
</engine:UXML>
```

## 네임스페이스
- `UnityEngine.UIElements` — 런타임 요소
- `UnityEditor.UIElements` — 에디터 전용 요소
- 약식 선언: `xmlns:engine="UnityEngine.UIElements"` → `<engine:Button />`
- 기본 네임스페이스: `xmlns="UnityEngine.UIElements"` → `<Button />`

## 요소 작성 예시
```xml
<engine:Box>
    <engine:Toggle name="boots" label="Boots" value="false" />
    <engine:Button name="ok" text="OK" />
</engine:Box>
```

## VisualElement 공통 속성
- `name` — 고유 식별자
- `picking-mode` — `Position` 또는 `Ignore` (마우스 이벤트)
- `tabindex` — 탭 순서 정수
- `focusable` — 키보드 포커스 가능 여부
- `class` — 공백 구분 스타일 식별자
- `tooltip` — 호버 텍스트
- `view-data-key` — 직렬화 식별자

## 스키마 검증
`xsi:noNamespaceSchemaLocation` — 스키마 파일 위치 지정, 허용 속성·자식 요소 검증.

---

# Add Styles to UXML

## 스타일 적용 두 가지 방법

### 1. 인라인 스타일
```xml
<ui:VisualElement style="width: 200px; height: 200px; background-color: red;" />
```

### 2. 외부 스타일시트 (권장)
```xml
<ui:UXML ...>
    <Style src="<path>/styles.uss" />
    <ui:VisualElement name="root" />
</ui:UXML>
```

## 경로 형식
- **절대 경로**: `/Assets/myFolder/myFile.uss` 또는 `project://database/...`
- **상대 경로**: `../myFolder/myFile.uss`
- **패키지 파일**: `/Packages/com.unity.package.name/file-name.uss`

---

# Reuse UXML Files

## 템플릿 생성
```xml
<!-- Portrait.uxml -->
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:VisualElement class="portrait">
        <ui:Image name="portraitImage" style="--unity-image: url(a.png)"/>
        <ui:Label name="nameLabel" text="Name"/>
        <ui:Label name="levelLabel" text="42"/>
    </ui:VisualElement>
</ui:UXML>
```

## 템플릿 인스턴스화
```xml
<ui:Template src="Portrait.uxml" name="Portrait"/>
<ui:Instance template="Portrait" name="player1"/>
<ui:Instance template="Portrait" name="player2"/>
```

## AttributeOverrides
```xml
<ui:Instance template="Portrait" name="player1">
    <ui:AttributeOverrides element-name="nameLabel" text="Player 1"/>
</ui:Instance>
```

## 중요 제한
`class`, `name`, `style` 속성은 오버라이드 불가. 스타일 커스터마이즈는 USS 셀렉터 사용.

## content-container
자식 요소가 중첩될 위치 지정 — 유연한 컴포넌트 구성 가능.
