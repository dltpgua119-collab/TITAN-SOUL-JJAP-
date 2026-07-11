---
type: snippet
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, uxml, load, instantiate, asset-database, resources]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-uxml-loading.md]
---

# UXML/USS 로드 및 인스턴스화 패턴

> C#에서 UXML 에셋을 로드하고 visual tree에 추가하는 네 가지 방법

## 코드

### 방법 1: SerializeField (권장)
```csharp
using UnityEngine.UIElements;

public class MyEditorWindow : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
    [SerializeField] private StyleSheet m_StyleSheet;
    
    public void CreateGUI()
    {
        // 인스턴스화 후 추가
        VisualElement ui = m_VisualTreeAsset.Instantiate();
        rootVisualElement.Add(ui);
        
        // 스타일시트 적용
        rootVisualElement.styleSheets.Add(m_StyleSheet);
    }
}
```

### 방법 2: AssetDatabase (에디터 전용)
```csharp
using UnityEditor;
using UnityEngine.UIElements;

public void CreateGUI()
{
    var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
        "Assets/Editor/MyWindow.uxml");
    var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(
        "Assets/Editor/MyWindow.uss");
    
    VisualElement ui = uxml.Instantiate();
    rootVisualElement.Add(ui);
    rootVisualElement.styleSheets.Add(uss);
}
```

### 방법 3: Resources.Load (런타임, 비권장)
```csharp
// Assets/Resources/UI/MyUI.uxml
var uxml = Resources.Load<VisualTreeAsset>("UI/MyUI");
rootVisualElement.Add(uxml.Instantiate());
// ⚠️ 빌드 크기 증가 주의
```

### 방법 4: CloneTree — 기존 계층에 직접 클론
```csharp
uxml.CloneTree(existingParent); // parent에 직접 추가
```

## 인스턴스화 후 UQuery로 요소 접근
```csharp
VisualElement ui = uxml.Instantiate();
root.Add(ui);

// 인스턴스화 후 바로 쿼리
Button okBtn = ui.Q<Button>("ok");
Label titleLabel = ui.Q<Label>("title");
```

## 사용 방법
- **에디터 창**: `SerializeField` 또는 `AssetDatabase` 방법 사용
- **런타임 UI**: `SerializeField` (MonoBehaviour) 또는 `Addressables` 권장
- `Instantiate()` — 독립 UI 컴포넌트 (TemplateContainer 반환)
- `CloneTree()` — 기존 부모에 통합 시

## 의존성
- `UnityEngine.UIElements`
- `UnityEditor` (AssetDatabase 방법)

## 관련 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/uquery|UQuery]]

## 출처
- [Load UXML and USS from C#](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manage-asset-reference.html)
- [Instantiate UXML with C#](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-LoadingUXMLcsharp.html)
