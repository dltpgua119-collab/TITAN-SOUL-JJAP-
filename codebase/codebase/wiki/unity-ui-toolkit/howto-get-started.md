---
type: how-to
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, editor-window, getting-started, uxml, uss]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-get-started.md]
---

# UI Toolkit 시작하기 — 첫 번째 커스텀 Editor 창 만들기

> 세 가지 방법(UI Builder, UXML, C#)으로 커스텀 Editor 창을 만들어 UI Toolkit 워크플로를 처음 익힐 때 사용

## 전제 조건
- Unity Editor (임의 템플릿)
- Unity 프로젝트 구조 기본 이해

## 무엇을 만드는가
`SimpleCustomEditor` 커스텀 Editor 창 — 레이블·버튼·토글 컨트롤 세트를 각각 다른 방법으로 3벌 생성.

## 단계

### 1. 커스텀 Editor 창 생성
1. Assets 폴더 우클릭 → **Create > UI Toolkit > Editor Window**
2. C# 클래스명: `SimpleCustomEditor`
3. UXML 체크박스 선택, USS 체크박스 해제
4. Window > UI Toolkit > SimpleCustomEditor로 접근

### 2-A. UI Builder로 UI 컨트롤 추가 (시각적 방법)
1. `SimpleCustomEditor.uxml`을 UI Builder에서 열기
2. Library > Controls 패널에서 Button, Toggle 드래그
3. Button 텍스트: "This is button1", Toggle 레이블: "Number?"
4. 저장 후 닫기

### 2-B. UXML 마크업으로 UI 컨트롤 추가
`SimpleCustomEditor_uxml.uxml` 파일 생성:
```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements" xmlns:uie="UnityEditor.UIElements">
    <ui:Label text="These controls were created with UXML." />
    <ui:Button text="This is button2" name="button2"/>
    <ui:Toggle label="Number?" name="toggle2"/>
</ui:UXML>
```

C# `CreateGUI()`에서 임포트:
```csharp
var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
    "Assets/Editor/SimpleCustomEditor_uxml.uxml");
VisualElement labelFromUXML = visualTree.Instantiate();
root.Add(labelFromUXML);
```

### 2-C. C# 스크립트로 UI 컨트롤 추가 (프로그래매틱 방법)
```csharp
using UnityEngine.UIElements;

// CreateGUI() 메서드 내부
Label label = new Label("These controls were created using C# code.");
root.Add(label);

Button button = new Button();
button.name = "button3";
button.text = "This is button3.";
root.Add(button);

Toggle toggle = new Toggle();
toggle.name = "toggle3";
toggle.label = "Number?";
root.Add(toggle);
```

### 3. 이벤트 핸들러로 동작 정의
```csharp
private void SetupButtonHandler()
{
    VisualElement root = rootVisualElement;
    var buttons = root.Query<Button>();
    buttons.ForEach(RegisterHandler);
}

private void RegisterHandler(Button button)
{
    button.RegisterCallback<ClickEvent>(PrintClickMessage);
}
```

## 필수 using 문
```csharp
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
```

## UXML 참조를 위한 SerializeField
```csharp
[SerializeField]
private VisualTreeAsset m_VisualTreeAsset = default;
```

## 검증 방법
Window > UI Toolkit > SimpleCustomEditor 선택 → 버튼 클릭 시 Console에 메시지 출력 확인.

## 관련 페이지
- [[wiki/unity-ui-toolkit/introduction|UI Toolkit 소개]]
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/editor-ui|Editor UI]]

## 출처
- [Get started with UI Toolkit](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-simple-ui-toolkit-workflow.html)
