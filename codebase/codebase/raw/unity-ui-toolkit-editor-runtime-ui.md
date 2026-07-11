---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-HowTo-CreateEditorWindow.html
created: 2026-04-16
---

# Editor UI 및 Runtime UI

## 커스텀 Editor 창

```csharp
public class SpriteLibraryEditor : EditorWindow
{
    [SerializeField] private int m_SelectedIndex = -1;

    [MenuItem("Window/Sprite Library")]
    public static void ShowWindow() => GetWindow<SpriteLibraryEditor>();

    public void CreateGUI()  // Unity가 창 표시 시 자동 호출
    {
        var splitView = new TwoPaneSplitView(0, 250, 
            TwoPaneSplitViewOrientation.Horizontal);
        rootVisualElement.Add(splitView);

        var leftPane = new ListView();
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => 
            (item as Label).text = allObjects[index].name;
        leftPane.itemsSource = allObjects;
        leftPane.selectedIndex = m_SelectedIndex;  // 핫 리로드 복원
        splitView.Add(leftPane);
    }
}
```

### 핵심 포인트
- `Editor` 폴더에 스크립트 배치 (에디터 컴파일용)
- `CreateGUI()`는 재컴파일 후 자동 재호출
- `[SerializeField]` 멤버 변수로 핫 리로드 시 상태 보존

## 커스텀 Inspector

```csharp
[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        
        // PropertyField로 직렬화된 프로퍼티 자동 바인딩
        var nameField = new PropertyField(serializedObject.FindProperty("m_Name"));
        root.Add(nameField);
        
        // UXML로 Inspector 구성
        var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/Editor/MyComponentInspector.uxml");
        uxml.CloneTree(root);
        
        return root;
    }
}
```

### 커스텀 Property Drawer
```csharp
[CustomPropertyDrawer(typeof(MySerializableClass))]
public class MyPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var root = new VisualElement();
        // 커스텀 프로퍼티 UI 구성
        return root;
    }
}
```

## Runtime UI 설정

### 씬 설정
1. GameObject > UI Toolkit > UI Document → UIDocument 컴포넌트 자동 생성
2. Panel Settings 및 기본 런타임 테마 자동 생성
3. UIDocument의 Source Asset 필드에 UXML 파일 할당

### MonoBehaviour에서 UI 접근
```csharp
public class UIController : MonoBehaviour
{
    private Button _button;
    private Toggle _toggle;

    private void OnEnable()  // UXML이 이 시점에서 로드됨
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        
        _button = root.Q<Button>("button");
        _toggle = root.Q<Toggle>("toggle");
        
        _button.RegisterCallback<ClickEvent>(HandleClick);
        _toggle.RegisterValueChangedCallback(HandleToggleChange);
    }

    private void OnDisable()  // 메모리 누수 방지
    {
        _button.UnregisterCallback<ClickEvent>(HandleClick);
    }

    private void HandleClick(ClickEvent evt) { /* ... */ }
    private void HandleToggleChange(ChangeEvent<bool> evt) { /* ... */ }
}
```

### 런타임 UXML 예시
```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:VisualElement style="flex-grow: 1;">
        <ui:Label text="Score: 0" name="score-label"/>
        <ui:Button text="Play Again" name="play-button"/>
        <ui:Toggle label="Sound?" name="sound-toggle"/>
    </ui:VisualElement>
</ui:UXML>
```
