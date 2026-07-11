---
type: concept
topic: unity-ui-toolkit
lang: uxml/cs
tags: [unity, ui-toolkit, uxml, template, stylesheet]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-uxml-writing.md]
---

# UXML 기초

> Unity의 UI 구조를 선언적으로 정의하는 마크업 언어 — HTML과 유사한 문법으로 구조·스타일·재사용 가능 템플릿 작성

## UXML이란?

Unity Extensible Markup Language. "UXML along with USS makes it easier for less technical users to define the layout and the style of the UI."

→ 구조(UXML)와 스타일(USS)의 분리로 디자이너·개발자 협업 용이.

## 파일 구조

```xml
<?xml version="1.0" encoding="utf-8"?>
<engine:UXML
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:engine="UnityEngine.UIElements"
    xmlns:editor="UnityEditor.UIElements"
    xsi:noNamespaceSchemaLocation="../../UIElementsSchema/UIElements.xsd"
>
    <engine:Box>
        <engine:Toggle name="boots" label="Boots" value="false" />
        <engine:Button name="ok" text="OK" />
    </engine:Box>
</engine:UXML>
```

## 네임스페이스

| 네임스페이스 | 용도 | 약식 |
|------------|------|------|
| `UnityEngine.UIElements` | 런타임 요소 | `ui:` or `engine:` |
| `UnityEditor.UIElements` | 에디터 전용 요소 | `uie:` or `editor:` |

기본 네임스페이스 선언 시 접두사 생략:
```xml
<UXML xmlns="UnityEngine.UIElements">
    <Button text="OK"/>  <!-- 접두사 없이 사용 -->
</UXML>
```

## VisualElement 공통 속성

| 속성 | 설명 |
|------|------|
| `name` | 고유 식별자 (UQuery에서 사용) |
| `class` | 공백 구분 USS 클래스 목록 |
| `picking-mode` | `Position` / `Ignore` (마우스 이벤트 처리 여부) |
| `tabindex` | 탭 이동 순서 |
| `focusable` | 키보드 포커스 가능 여부 |
| `tooltip` | 호버 툴팁 텍스트 |
| `view-data-key` | 에디터 상태 직렬화 키 |

## 스타일 적용

### 1. 인라인 스타일 (간단한 경우)
```xml
<ui:VisualElement style="width: 200px; height: 200px; background-color: red;" />
```

### 2. 외부 USS 파일 참조 (권장)
```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <Style src="../Styles/myStyle.uss" />
    <ui:VisualElement name="root" />
</ui:UXML>
```

### USS 파일 경로 형식
```text
/Assets/myFolder/myFile.uss          ← 절대 경로 (Assets 기준)
../myFolder/myFile.uss               ← 상대 경로
/Packages/com.unity.pkg/file.uss    ← 패키지 파일
```

## UXML 템플릿 재사용

### 템플릿 정의 (`Portrait.uxml`)
```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:VisualElement class="portrait">
        <ui:Image name="portraitImage"/>
        <ui:Label name="nameLabel" text="Name"/>
        <ui:Label name="levelLabel" text="1"/>
    </ui:VisualElement>
</ui:UXML>
```

### 다른 UXML에서 인스턴스화
```xml
<ui:Template src="Portrait.uxml" name="Portrait"/>
<ui:Instance template="Portrait" name="player1"/>
<ui:Instance template="Portrait" name="player2"/>
```

### AttributeOverrides로 커스터마이즈
```xml
<ui:Instance template="Portrait" name="player1">
    <ui:AttributeOverrides element-name="nameLabel" text="Player 1"/>
    <ui:AttributeOverrides element-name="levelLabel" text="42"/>
</ui:Instance>
```

> **제한**: `class`, `name`, `style` 속성은 오버라이드 불가. 스타일 변경은 USS 셀렉터 사용.

## C#에서 UXML 로드

```csharp
// AssetDatabase로 로드 (에디터)
var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
    "Assets/UI/MyUI.uxml");
VisualElement ui = visualTree.Instantiate();
root.Add(ui);

// SerializeField로 직접 참조 (권장)
[SerializeField] private VisualTreeAsset m_VisualTreeAsset;
// ...
root.Add(m_VisualTreeAsset.Instantiate());
```

## 스키마 검증
`xsi:noNamespaceSchemaLocation`으로 스키마 파일 위치 지정 → IDE에서 자동완성 및 유효성 검사 활성화.

## 주의사항
- 템플릿 `class`/`name`/`style` 오버라이드 불가 → USS 셀렉터로 대체
- 중첩 템플릿의 오버라이드는 가장 얕은 오버라이드가 우선
- `content-container` 속성으로 자식 요소 중첩 위치 지정 가능

## 관련 페이지
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]]
- [[wiki/unity-ui-toolkit/uquery|UQuery]]
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]]
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]]

## 출처
- [Introduction to UXML](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-WritingUXMLTemplate.html)
- [Add styles to UXML](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-add-style-to-uxml.html)
- [Reuse UXML files](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-reuse-uxml-files.html)
- [Structure UI with UXML overview](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-UXML.html)
