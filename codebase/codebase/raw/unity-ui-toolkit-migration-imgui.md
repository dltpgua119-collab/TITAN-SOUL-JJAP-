---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-IMGUI-migration.html
created: 2026-04-16
---

# IMGUI → UI Toolkit 마이그레이션

## 핵심 아키텍처 차이

| 항목 | IMGUI | UI Toolkit |
|------|-------|------------|
| 방식 | 즉시 모드 (매 프레임 OnGUI) | 보유 모드 (persistent visual tree) |
| 정의 방법 | C# 절차형 코드 | C#, UI Builder, UXML |
| 이벤트 처리 | 프레임마다 조건 실행 | 이벤트 드리븐 콜백 |

## 코드 비교

```csharp
// IMGUI
if (GUILayout.Button("Click me!"))
{
    // 클릭된 프레임에만 실행
}

// UI Toolkit
Button button = new Button();
button.text = "Click me!";
button.RegisterCallback<ClickEvent>(evt => {
    // ClickEvent 수신 시 실행
});
document.rootVisualElement.Add(button);
```

## 함수 대응표

| IMGUI | UI Toolkit |
|-------|------------|
| `EditorWindow.OnGUI()` | `EditorWindow.CreateGUI()` |
| `PropertyDrawer.OnGUI()` | `PropertyDrawer.CreatePropertyGUI()` |
| `Editor.OnInspectorGUI()` | `Editor.CreateInspectorGUI()` |
| `BeginDisabledGroup()` | `element.SetEnabled(false)` |
| `Button()` | `Button` 클래스 |
| `Slider()` | `Slider` |
| `TextField()` | `TextField` (multiline/password 옵션) |

## IMGUI 임베딩 (IMGUIContainer)

기존 IMGUI 코드를 UI Toolkit 안에 포함할 때:

```csharp
var container = new IMGUIContainer(() => {
    // 기존 OnGUI() 코드 그대로
    GUILayout.Label("Legacy IMGUI");
    if (GUILayout.Button("Legacy Button"))
    {
        Debug.Log("clicked");
    }
});
root.Add(container);
```

"Everything you can do inside `OnGUI()` works within IMGUIContainer."
주의: `VisualElement`는 `IMGUIContainer` 안에 중첩 불가.

## 마이그레이션 전략

1. 신규 코드는 UI Toolkit으로 작성
2. 기존 IMGUI는 `IMGUIContainer`로 감싸 공존
3. 점진적으로 `IMGUIContainer`를 UI Toolkit 컨트롤로 교체
