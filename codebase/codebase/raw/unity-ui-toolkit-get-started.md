---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-simple-ui-toolkit-workflow.html
created: 2026-04-16
---

# Get Started with UI Toolkit

## Overview
커스텀 Editor 창을 세 가지 방법(UI Builder, UXML 마크업, C# 스크립트)으로 만드는 튜토리얼.

## Prerequisites
- Unity Editor (임의 템플릿)
- Unity 프로젝트 구조 기본 이해

## What You'll Create
`SimpleCustomEditor` 커스텀 Editor 창 — 레이블, 버튼, 토글 컨트롤 세트 3벌 (각각 다른 방법으로 생성).

## 단계

### 1. 커스텀 Editor 창 생성
- Assets 폴더 우클릭 → Create > UI Toolkit > Editor Window
- C# 클래스명: `SimpleCustomEditor`, UXML 선택, USS 해제
- Window > UI Toolkit > SimpleCustomEditor 로 접근

### 2. UI 컨트롤 추가 — 세 가지 방법

**방법 A: UI Builder (시각적)**
- `SimpleCustomEditor.uxml`을 UI Builder에서 열기
- Library > Controls에서 Button, Toggle 드래그
- Button 텍스트: "This is button1", Toggle 레이블: "Number?"

**방법 B: UXML (마크업)**
```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements" xmlns:uie="UnityEditor.UIElements">
    <ui:Label text="These controls were created with UXML." />
    <ui:Button text="This is button2" name="button2"/>
    <ui:Toggle label="Number?" name="toggle2"/>
</ui:UXML>
```

C#에서 임포트:
```csharp
var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
    "Assets/Editor/SimpleCustomEditor_uxml.uxml");
VisualElement labelFromUXML = visualTree.Instantiate();
root.Add(labelFromUXML);
```

**방법 C: C# 스크립트**
```csharp
using UnityEngine.UIElements;

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

### 3. 이벤트 핸들러 정의

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

## 테스트
Window > UI Toolkit > SimpleCustomEditor 선택 → 완성된 창 확인.
