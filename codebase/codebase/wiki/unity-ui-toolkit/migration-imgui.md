---
type: how-to
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, migration, imgui, imguicontainer]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-migration-imgui.md]
---

# IMGUI → UI Toolkit 마이그레이션

> IMGUI의 즉시 모드(OnGUI)에서 UI Toolkit의 보유 모드(VisualElement)로 전환하는 가이드. 기존 코드는 IMGUIContainer로 감싸 공존 가능.

## 핵심 아키텍처 차이

| 항목 | IMGUI | UI Toolkit |
|------|-------|------------|
| 렌더링 모드 | 즉시 모드 (매 프레임 OnGUI 실행) | 보유 모드 (영속 visual tree) |
| UI 정의 방법 | C# 절차형 코드 | C#, UI Builder, UXML |
| 이벤트 처리 | 프레임마다 조건 실행 | 이벤트 드리븐 콜백 |
| 상태 관리 | 프레임마다 재그리기 | 트리 상태 유지, 변경 시만 업데이트 |

---

## 코드 패턴 비교

### 버튼

```cs
// IMGUI
void OnGUI()
{
    if (GUILayout.Button("Click me!"))
    {
        // 클릭된 프레임에만 실행
    }
}

// UI Toolkit
public override VisualElement CreateGUI()
{
    var button = new Button();
    button.text = "Click me!";
    button.RegisterCallback<ClickEvent>(evt => {
        // ClickEvent 수신 시 실행
    });
    return button;
}
```

### EditorWindow

```cs
// IMGUI
public class MyWindow : EditorWindow
{
    void OnGUI() { /* 모든 UI 코드 */ }
}

// UI Toolkit
public class MyWindow : EditorWindow
{
    public override void CreateGUI()
    {
        var label = new Label("Hello");
        rootVisualElement.Add(label);
    }
}
```

---

## 함수 대응표

| IMGUI | UI Toolkit |
|-------|------------|
| `EditorWindow.OnGUI()` | `EditorWindow.CreateGUI()` |
| `PropertyDrawer.OnGUI()` | `PropertyDrawer.CreatePropertyGUI()` |
| `Editor.OnInspectorGUI()` | `Editor.CreateInspectorGUI()` |
| `BeginDisabledGroup()` / `EndDisabledGroup()` | `element.SetEnabled(false)` |
| `GUILayout.Button()` | `Button` 클래스 |
| `GUILayout.Label()` | `Label` 클래스 |
| `EditorGUILayout.Slider()` | `Slider` |
| `EditorGUILayout.TextField()` | `TextField` |
| `EditorGUILayout.Toggle()` | `Toggle` |
| `EditorGUILayout.ObjectField()` | `ObjectField` |

---

## IMGUIContainer로 기존 코드 임베딩

기존 IMGUI 코드를 UI Toolkit에 통합할 때 사용:

```cs
public override void CreateGUI()
{
    // 기존 IMGUI 코드를 IMGUIContainer로 감쌈
    var legacySection = new IMGUIContainer(() => {
        GUILayout.Label("Legacy IMGUI Section");
        if (GUILayout.Button("Legacy Button"))
        {
            Debug.Log("clicked");
        }
        mySerializedObject.Update();
        EditorGUILayout.PropertyField(myProperty);
        mySerializedObject.ApplyModifiedProperties();
    });

    // UI Toolkit 요소와 공존
    rootVisualElement.Add(new Label("UI Toolkit Label"));
    rootVisualElement.Add(legacySection);
}
```

**주의:** `VisualElement`는 `IMGUIContainer` 내부에 중첩 불가.

---

## 마이그레이션 전략

1. **신규 코드**: 처음부터 UI Toolkit으로 작성
2. **기존 코드 공존**: `IMGUIContainer`로 감싸서 유지
3. **점진적 교체**: `IMGUIContainer` 블록을 UI Toolkit 컨트롤로 순차 교체

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/migration-overview|마이그레이션 가이드 (uGUI)]] — uGUI→UI Toolkit 컴포넌트 대응
- [[wiki/unity-ui-toolkit/editor-ui|Editor UI 구현]] — EditorWindow, Inspector, Property Drawer

## 출처
- `raw/unity-ui-toolkit-migration-imgui.md`
