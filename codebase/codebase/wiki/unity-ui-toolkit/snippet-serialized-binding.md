---
type: snippet
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, data-binding, serialized-object, editor]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-data-binding-runtime.md]
---

# SerializedObject 바인딩 스니펫 (Editor 전용)

> `SerializedObject` + `Bind()` 패턴으로 Editor 창의 UI 요소를 Unity 직렬화 프로퍼티에 자동 연결한다.

## 기본 패턴 — Bind()

```cs
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class SimpleBindingExample : EditorWindow
{
    TextField m_ObjectNameBinding;

    public void CreateGUI()
    {
        // bindingPath = 직렬화 내부 이름 (예: GameObject.name → "m_Name")
        m_ObjectNameBinding = new TextField("Object Name");
        m_ObjectNameBinding.bindingPath = "m_Name";
        rootVisualElement.Add(m_ObjectNameBinding);
    }

    public void OnSelectionChange()
    {
        if (Selection.activeObject is GameObject go)
        {
            var so = new SerializedObject(go);
            rootVisualElement.Bind(so);   // 트리 전체 바인딩
        }
        else
        {
            rootVisualElement.Unbind();
            m_ObjectNameBinding.value = "";
        }
    }
}
```

## 개별 프로퍼티 바인딩 — BindProperty()

```cs
public void CreateGUI()
{
    var target = Selection.activeObject as MyComponent;
    var so = new SerializedObject(target);

    var field = new IntegerField("Count");
    // 특정 SerializedProperty에 직접 바인딩
    field.BindProperty(so.FindProperty("count"));
    rootVisualElement.Add(field);
}
```

## PropertyField (자동 타입 감지)

```cs
public void CreateGUI()
{
    var so = new SerializedObject(target);

    // PropertyField는 타입에 맞는 UI 자동 생성
    var propField = new PropertyField(so.FindProperty("myValue"));
    propField.Bind(so);
    rootVisualElement.Add(propField);
}
```

## UXML에서 binding-path

```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements" editor-extension-mode="True">
    <ui:TextField label="Name" binding-path="m_Name" />
    <ui:IntegerField label="Count" binding-path="count" />
</ui:UXML>
```

C#에서 Bind()만 호출하면 UXML에 선언된 binding-path가 자동 적용:
```cs
rootVisualElement.Bind(new SerializedObject(target));
```

## ViewData Persistence (Editor 전용)

스크롤 위치, 선택 등 UI 상태를 Editor 재시작 후에도 유지:

```cs
// C#
scrollView.viewDataKey = "myEditor.scrollView";
foldout.viewDataKey = "myEditor.foldout";

// UXML
// <ui:ScrollView view-data-key="myEditor.scrollView" />
```

지원 컨트롤: `ScrollView`, `ListView`, `Foldout`, `TreeView`, `TabView`, `MultiColumnListView`

**주의:** 키는 같은 EditorWindow 내에서 유일해야 한다.

## 의존성
- `UnityEditor.UIElements`
- Editor-only (Runtime 바인딩은 `data-binding-runtime` 참조)

## 관련 페이지
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]] — MVVM, 바인딩 시스템 비교
- [[wiki/unity-ui-toolkit/data-binding-runtime|Runtime Data Binding]] — [CreateProperty] 런타임 패턴
- [[wiki/unity-ui-toolkit/editor-ui|Editor UI 구현]] — EditorWindow, CustomEditor

## 출처
- `raw/unity-ui-toolkit-data-binding-runtime.md`
