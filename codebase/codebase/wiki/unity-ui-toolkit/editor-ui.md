---
type: how-to
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, editor-window, inspector, property-drawer, editor-ui]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-editor-runtime-ui.md]
---

# Editor UI 구현

> UI Toolkit으로 커스텀 Editor 창, 커스텀 Inspector, 커스텀 Property Drawer를 만드는 방법

## 커스텀 Editor 창

### 전제 조건
- `Editor` 폴더에 스크립트 배치
- `EditorWindow` 상속

### 기본 구조
```csharp
using UnityEditor;
using UnityEngine.UIElements;

public class MyEditorWindow : EditorWindow
{
    // 핫 리로드 시 상태 보존
    [SerializeField] private int m_SelectedIndex = -1;

    [MenuItem("Window/My Tool")]
    public static void ShowWindow() => GetWindow<MyEditorWindow>("My Tool");

    // Unity가 창 표시 시 자동 호출 (재컴파일 후에도 재호출)
    public void CreateGUI()
    {
        // rootVisualElement에 UI 추가
        var label = new Label("Hello Editor Window!");
        rootVisualElement.Add(label);

        var button = new Button(() => Debug.Log("Clicked!")) { text = "Action" };
        rootVisualElement.Add(button);
    }
}
```

### 분할 패널 레이아웃
```csharp
public void CreateGUI()
{
    // 250px 왼쪽 패널, 나머지 오른쪽
    var splitView = new TwoPaneSplitView(0, 250, 
        TwoPaneSplitViewOrientation.Horizontal);
    rootVisualElement.Add(splitView);

    // 왼쪽: 목록
    var leftPane = new ListView();
    leftPane.makeItem = () => new Label();
    leftPane.bindItem = (item, index) => 
        (item as Label).text = myItems[index].name;
    leftPane.itemsSource = myItems;
    leftPane.selectedIndex = m_SelectedIndex;  // 상태 복원
    leftPane.onSelectionChange += OnSelectionChanged;
    splitView.Add(leftPane);

    // 오른쪽: 상세 패널
    var rightPane = new VisualElement();
    splitView.Add(rightPane);
}
```

## 커스텀 Inspector

```csharp
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        // 1. PropertyField로 직렬화 프로퍼티 자동 바인딩
        var nameField = new PropertyField(serializedObject.FindProperty("m_Name"));
        var healthField = new PropertyField(serializedObject.FindProperty("m_MaxHealth"));
        root.Add(nameField);
        root.Add(healthField);

        // 2. UXML 파일로 Inspector 구성
        var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Editor/MyComponentInspector.uxml");
        uxml.CloneTree(root);

        return root;
    }
}
```

**주의**: `Editor` 폴더 또는 에디터 전용 어셈블리에 배치.

## 커스텀 Property Drawer

```csharp
[CustomPropertyDrawer(typeof(MySerializableClass))]
public class MyPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var root = new VisualElement();

        // 중첩 프로퍼티 접근
        var nameField = new PropertyField(property.FindPropertyRelative("m_Name"));
        var valueField = new PropertyField(property.FindPropertyRelative("m_Value"));
        root.Add(nameField);
        root.Add(valueField);

        return root;
    }
}
```

## 데이터 바인딩 (SerializedObject)

```csharp
// PropertyField는 binding-path로 자동 바인딩
// 지원 타입: TextField, IntegerField, FloatField, Toggle, PropertyField 등
```

## 검증 방법
- Window > My Tool로 에디터 창 열기
- Inspector에서 MyComponent 선택 후 커스텀 UI 확인
- 재컴파일 후 상태 보존 확인

## 관련 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/snippet-load-uxml|UXML 로드 패턴]]
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]]

## 출처
- [Create a custom Editor window](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-HowTo-CreateEditorWindow.html)
- [Create a custom Inspector](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-HowTo-CreateCustomInspector.html)
