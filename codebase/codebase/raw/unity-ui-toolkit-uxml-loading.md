---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manage-asset-reference.html
created: 2026-04-16
---

# Load UXML and USS from C#

## 네 가지 로딩 방법

### 1. Serialization References (권장)
```csharp
public class MyBehaviour : MonoBehaviour
{
    public VisualTreeAsset exampleUI;
    public StyleSheet[] exampleStyle;
}
```
- `MonoBehaviour` → 씬에 저장
- `EditorWindow`/`Editor` → 스크립트 메타 파일에 저장
- `ScriptableObject` → 에셋 직렬화 데이터에 저장

### 2. Asset Database (에디터 전용)
```csharp
VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
    "Assets/Editor/main_window.uxml");
StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(
    "Assets/Editor/main_styles.uss");
// 패키지: "Packages/<package-name>/file.uxml"
```

### 3. Addressables
런타임 에셋 관리. UI Document 프리팹을 addressable로 등록.

### 4. Resources 폴더
```csharp
VisualTreeAsset uxml = Resources.Load<VisualTreeAsset>("main_window");
StyleSheet uss = Resources.Load<StyleSheet>("main_styles");
```
**주의**: 빌드 크기·메모리 대폭 증가. 프로덕션은 Addressables 권장.

---

# Instantiate UXML with C#

## 두 가지 인스턴스화 방법

### Instantiate() — 독립 컴포넌트
```csharp
VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
    "Assets/MyWindow.uxml");
VisualElement ui = uiAsset.Instantiate();
rootVisualElement.Add(ui);
```

### CloneTree(parent) — 기존 계층에 통합
```csharp
uiAsset.CloneTree(existingParent);
```

## 인스턴스화 후 UQuery로 요소 접근
```csharp
VisualElement ui = uiAsset.Instantiate();
root.Add(ui);
var myButton = ui.Q<Button>("myButton");
```

---

# Reference Other Files from UXML

## src 속성 (권장)
```xml
<Template src="../UI/Portrait.uxml" name="Portrait"/>
<Style src="/Assets/Styles/common.uss"/>
```
경로 형식: 절대(`/Assets/...`), 상대(`../...`), 패키지(`/Packages/com.pkg/...`)

## path 속성 (레거시)
- Resources 폴더: 확장자 생략 (`path="template"`)
- Editor Default Resources: 확장자 포함 (`path="template.uxml"`)
- 상대 경로 미지원

---

# Built-in Controls

## 컨트롤 사용 기본 패턴
```csharp
// 컨트롤 생성 및 추가
var toggle = new Toggle("Enable Feature");
root.Add(toggle);

// 값 읽기
bool isEnabled = toggle.value;

// 값 변경 콜백
toggle.RegisterValueChangedCallback(evt => {
    Debug.Log($"Toggle changed: {evt.newValue}");
});

// 비활성화
toggle.SetEnabled(false);
```
전체 컨트롤 목록은 UIE-ElementRef.html 참조.
