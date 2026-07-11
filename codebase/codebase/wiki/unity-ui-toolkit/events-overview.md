---
type: concept
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, events, callback, trickle-down, bubble-up, manipulator, pointer]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-events.md]
---

# 이벤트 시스템

> UI Toolkit의 HTML 기반 이벤트 시스템 — 3단계 전파, 콜백 등록, 조작기 패턴

## 이벤트 전파 흐름

```text
루트 (Panel)
  │  ← Trickle-Down (하강) Phase
  ▼
중간 요소
  │
  ▼
타겟 요소  ← Target Phase
  │
  ▲
중간 요소
  │  ← Bubble-Up (상승) Phase
루트 (Panel)
```

| 단계 | 방향 | 기본 콜백 실행 |
|------|------|---------------|
| **Trickle-Down** | 루트 → 타겟 | 명시적 TrickleDown 등록 시 |
| **Target** | 타겟 | 항상 |
| **Bubble-Up** | 타겟 → 루트 | 기본 (명시적 없을 때) |

## 핵심 프로퍼티

| 프로퍼티 | 설명 |
|---------|------|
| `event.target` | 이벤트 발생 원본 요소 (변하지 않음) |
| `event.currentTarget` | 현재 콜백이 실행 중인 요소 |

## 콜백 등록

### 기본 등록 (Bubble-Up 단계)
```csharp
myElement.RegisterCallback<PointerDownEvent>(OnPointerDown);

private void OnPointerDown(PointerDownEvent evt)
{
    Debug.Log($"Clicked: {evt.target.name}, Position: {evt.position}");
}
```

### Trickle-Down 등록 (하강 단계)
```csharp
// 부모가 자식보다 먼저 이벤트 가로채기
parentElement.RegisterCallback<PointerDownEvent>(
    OnPointerDown, TrickleDown.TrickleDown);
```

### 커스텀 데이터 전달
```csharp
myElement.RegisterCallback<ClickEvent, string>(
    (evt, data) => Debug.Log($"Clicked with data: {data}"),
    "my-custom-data"
);
```

### 콜백 해제
```csharp
myElement.UnregisterCallback<PointerDownEvent>(OnPointerDown);
```

## 값 변경 이벤트

```csharp
// ChangeEvent<T> 등록
mySlider.RegisterValueChangedCallback(evt =>
{
    Debug.Log($"Slider: {evt.previousValue} → {evt.newValue}");
    healthBar.style.width = new Length(evt.newValue, LengthUnit.Percent);
});

// 알림 없이 값 변경 (무한 루프 방지)
myControl.SetValueWithoutNotify(100f);
```

## 이벤트 전파 제어

```csharp
private void OnPointerDown(PointerDownEvent evt)
{
    // 전파 중단 (부모에게 전달 안 함)
    evt.StopPropagation();
    
    // 즉시 전파 중단 (같은 요소의 다른 콜백도 차단)
    evt.StopImmediatePropagation();
    
    // 기본 동작 방지
    evt.PreventDefault();
}
```

## Picking Mode

```csharp
// 기본: 위치 기반 직사각형 피킹
myElement.pickingMode = PickingMode.Position;

// 포인터 이벤트 무시 (클릭 통과)
overlay.pickingMode = PickingMode.Ignore;
```

## 조작기 (Manipulators)

이벤트 처리 로직을 캡슐화하는 상태 머신.

### 내장 조작기

| 조작기 | 용도 |
|--------|------|
| `Clickable` | 클릭 감지 (press + release 같은 요소) |
| `PointerManipulator` | 포인터 입력 + 활성화 필터 |
| `MouseManipulator` | 마우스 입력 + 활성화 필터 |
| `ContextualMenuManipulator` | 우클릭 컨텍스트 메뉴 |
| `KeyboardNavigationManipulator` | 키보드 → 네비게이션 변환 |

```csharp
// Clickable 조작기 사용
var button = new VisualElement();
button.AddManipulator(new Clickable(OnButtonClick));
```

### 커스텀 드래그 조작기
```csharp
public class DragManipulator : PointerManipulator
{
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        target.CapturePointer(evt.pointerId);
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!target.HasPointerCapture(evt.pointerId)) return;
        target.style.left = target.layout.x + evt.deltaPosition.x;
        target.style.top = target.layout.y + evt.deltaPosition.y;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        target.ReleasePointer(evt.pointerId);
    }
}

// 요소에 연결
myElement.AddManipulator(new DragManipulator());
```

## 주요 이벤트 유형

| 카테고리 | 이벤트 |
|---------|--------|
| 포인터 | `PointerDownEvent`, `PointerMoveEvent`, `PointerUpEvent` |
| 마우스 | `MouseDownEvent`, `MouseMoveEvent`, `MouseUpEvent` |
| 클릭 | `ClickEvent` |
| 키보드 | `KeyDownEvent`, `KeyUpEvent` |
| 변경 | `ChangeEvent<T>` |
| 포커스 | `FocusEvent`, `BlurEvent` |
| 드래그 | `DragEnterEvent`, `DragLeaveEvent`, `DragUpdatedEvent` |
| 트랜지션 | `TransitionStartEvent`, `TransitionEndEvent` |
| 패널 | `AttachToPanelEvent`, `DetachFromPanelEvent` |

## 주의사항
- 숨겨진/비활성화된 요소는 이벤트 수신 안 함 (조상/하위는 계속 전파)
- `target`은 전파 중 변하지 않음, `currentTarget`은 변함
- Trickle-Down은 부모가 자식보다 먼저 이벤트 가로채야 할 때 사용
- 조작기는 `RegisterCallbacksOnTarget`/`UnregisterCallbacksFromTarget` 구현 필수

## 관련 페이지
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]]
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]]

## 출처
- [Events overview](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events.html)
- [Dispatch events](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html)
- [Handle event callbacks](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Handling.html)
- [Manipulators](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manipulators.html)
