---
type: how-to
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, migration, ugui, imgui, legacy]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-migration-ugui.md]
---

# 마이그레이션 가이드 (uGUI → UI Toolkit)

> uGUI(Unity UI)에서 UI Toolkit으로 이주할 때의 주요 변경 사항과 컴포넌트 대응

## 핵심 아키텍처 차이

| 측면 | uGUI | UI Toolkit |
|------|------|-----------|
| UI 계층 표시 | Hierarchy에 GameObject | Virtual visual tree (숨겨짐) |
| 계층 확인 | Hierarchy 창 | **Window > UI Toolkit > Debugger** |
| 레이아웃 방식 | 수동 앵커·피벗·RectTransform | 자동 Flexbox 레이아웃 |
| 이벤트 설정 | Inspector에서 직렬화 | 런타임 코드로 콜백 등록 |
| 구조 저장 | Prefab (레이아웃+스크립트) | UXML(레이아웃) + C#(로직) 분리 |
| 재사용 방식 | Prefab | UXML 템플릿 + 커스텀 컨트롤 |

## 컴포넌트 대응

| 목적 | uGUI | UI Toolkit |
|------|------|-----------|
| 루트 컨테이너 | Canvas + Canvas Scaler | UIDocument + PanelSettings |
| 기본 요소 클래스 | UIBehaviour | VisualElement |
| 요소 검색 | `GetComponentInChildren<T>()` | `root.Q<T>()` 또는 `root.Query<T>()` |
| 요소 참조 | Inspector 직접 할당 | 런타임 쿼리로 해결 |
| 이미지 | Image 컴포넌트 | `VisualElement` + `background-image` |
| 텍스트 | Text / TextMeshProUGUI | `Label` |
| 버튼 | Button 컴포넌트 | `Button` |
| 슬라이더 | Slider 컴포넌트 | `Slider` |
| 입력창 | InputField | `TextField` |
| 토글 | Toggle 컴포넌트 | `Toggle` |
| 스크롤 뷰 | ScrollRect | `ScrollView` |

## 이벤트 처리 마이그레이션

```csharp
// uGUI (Inspector 설정 방식)
// Inspector에서 Button.onClick에 메서드 할당

// UI Toolkit (코드 방식)
private void OnEnable()
{
    var root = GetComponent<UIDocument>().rootVisualElement;
    var button = root.Q<Button>("my-button");
    button.RegisterCallback<ClickEvent>(OnButtonClick);
}

private void OnButtonClick(ClickEvent evt)
{
    Debug.Log("Button clicked!");
}
```

## 레이아웃 마이그레이션

```css
/* uGUI에서: RectTransform으로 수동 위치 지정 */

/* UI Toolkit에서: Flexbox 자동 레이아웃 */
.container {
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
}

/* 절대 위치 (uGUI anchor 유사) */
.overlay {
    position: absolute;
    left: 0;
    top: 0;
    right: 0;
    bottom: 0;
}
```

## 혼용 시 제한사항 (uGUI + UI Toolkit)
- 키보드로 UI Toolkit과 uGUI 포커스 요소 간 자유 이동 불가 (수동 스크립트 개입 필요)
- 크로스 시스템 임베딩 어려움
- 통합 스타일링 어려움

## 마이그레이션 권장 순서
1. 간단한 메뉴/HUD부터 시작
2. UXML + USS로 레이아웃 재구성
3. MonoBehaviour에서 코드로 이벤트 재연결
4. SerializedObject 바인딩으로 Inspector 데이터 연동
5. Prefab 재사용성 → UXML 템플릿 + 커스텀 컨트롤로 대체

## 관련 페이지
- [[wiki/unity-ui-toolkit/ui-systems-comparison|UI 시스템 비교]]
- [[wiki/unity-ui-toolkit/runtime-ui|Runtime UI]]
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]

## 출처
- [Migrate from uGUI to UI Toolkit](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitioning-From-UGUI.html)
