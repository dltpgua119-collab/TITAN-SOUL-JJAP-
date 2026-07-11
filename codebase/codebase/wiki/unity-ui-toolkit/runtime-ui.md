---
type: how-to
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, runtime-ui, uidocument, panel-settings, monobehaviour]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-editor-runtime-ui.md]
---

# Runtime UI 구현

> UIDocument 컴포넌트로 게임 런타임에 UI Toolkit UI를 씬에 추가하는 방법

## 전제 조건
- Unity 6 이상 (UI Toolkit 내장)

## 단계

### 1. UXML UI 파일 생성
```xml
<!-- Assets/UI/GameHUD.uxml -->
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:VisualElement style="flex-grow: 1; padding: 16px;">
        <ui:Label text="Score: 0" name="score-label" class="hud__score"/>
        <ui:Button text="Pause" name="pause-button" class="hud__button"/>
        <ui:Toggle label="Mute?" name="sound-toggle"/>
    </ui:VisualElement>
</ui:UXML>
```

### 2. 씬에 UIDocument 추가
```text
GameObject > UI Toolkit > UI Document
```
자동 생성:
- `UI Toolkit/` 폴더 (Panel Settings + 런타임 테마)
- UIDocument 컴포넌트가 있는 GameObject

UIDocument의 **Source Asset** 필드에 UXML 파일 할당.

### 3. MonoBehaviour로 UI 로직 구현
```csharp
using UnityEngine;
using UnityEngine.UIElements;

public class GameHUD : MonoBehaviour
{
    private Button _pauseButton;
    private Label _scoreLabel;
    private Toggle _soundToggle;

    private void OnEnable()  // UXML이 이 시점에 로드됨
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // UI 요소 캐시 (매번 쿼리 금지)
        _scoreLabel = root.Q<Label>("score-label");
        _pauseButton = root.Q<Button>("pause-button");
        _soundToggle = root.Q<Toggle>("sound-toggle");
        
        // 이벤트 등록
        _pauseButton.RegisterCallback<ClickEvent>(OnPauseClick);
        _soundToggle.RegisterValueChangedCallback(OnSoundToggle);
    }

    private void OnDisable()  // 메모리 누수 방지
    {
        _pauseButton?.UnregisterCallback<ClickEvent>(OnPauseClick);
    }

    public void UpdateScore(int score)
    {
        _scoreLabel.text = $"Score: {score}";
    }

    private void OnPauseClick(ClickEvent evt)
    {
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    private void OnSoundToggle(ChangeEvent<bool> evt)
    {
        AudioListener.mute = !evt.newValue;
    }
}
```

## PanelSettings 설정

Panel Settings (`Assets/UI Toolkit/PanelSettings.asset`) 주요 설정:

| 설정 | 설명 |
|------|------|
| **Scale Mode** | `Scale with Screen Size` (권장) |
| **Reference Resolution** | UI 디자인 기준 해상도 (예: 1920×1080) |
| **Sort Order** | 여러 UIDocument 간 렌더 순서 |

## 여러 UIDocument 사용

```csharp
// 동일한 PanelSettings를 여러 UIDocument가 공유 가능 (성능 최적화)
// UIDocument의 Sort Order로 렌더 순서 제어
// 예: HUD(Sort=0), Inventory(Sort=1), Pause Menu(Sort=2)
```

## 주의사항
- `OnEnable()`에서 UI 요소 접근 (UXML은 이때 로드됨)
- `OnDisable()`에서 콜백 해제 (메모리 누수 방지)
- 초기화 시 요소 캐시 (매번 Q<T>() 호출 금지)
- UIDocument 없이 Editor UI는 `EditorWindow.CreateGUI()` 사용

## 관련 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/uquery|UQuery]]
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]]
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]]

## 출처
- [Get started with runtime UI](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-get-started-with-runtime-ui.html)
- [Support for Runtime UI](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-support-for-runtime-ui.html)
