---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html
created: 2026-04-16
---

# Events — 고급 (Dispatching, Callbacks, Manipulators)

## 이벤트 전파 경로

1. **Trickle-down**: 루트 → 타겟 방향 내려감
2. **Target**: 이벤트가 타겟 요소에 도달
3. **Bubble-up**: 타겟 → 루트 방향 올라감

- `EventBase.target`: 이벤트가 발생한 요소 (변하지 않음)
- `EventBase.currentTarget`: 현재 콜백이 등록된 요소 (전파 중 변함)

## Picking Mode

```csharp
element.pickingMode = PickingMode.Position; // 기본 — 위치 기반 타겟
element.pickingMode = PickingMode.Ignore;   // 포인터 이벤트 무시
```

`VisualElement.ContainsPoint()` 오버라이드로 커스텀 교차 로직 구현 가능.

---

## 이벤트 콜백 등록

```csharp
// Bubble-up/Target 단계 (기본)
myElement.RegisterCallback<PointerDownEvent>(MyCallback);

// Trickle-down 단계 (부모가 자식보다 먼저 처리)
myElement.RegisterCallback<PointerDownEvent>(
    MyCallback, TrickleDown.TrickleDown);

// 추가 데이터 전달
myElement.RegisterCallback<PointerDownEvent, MyType>(
    MyCallbackWithData, myData);
```

## 콜백 해제

```csharp
myElement.UnregisterCallback<PointerDownEvent>(MyCallback);
```

## 값 변경 이벤트

```csharp
myIntegerField.RegisterValueChangedCallback(evt => {
    Debug.Log($"Previous: {evt.previousValue}, New: {evt.newValue}");
});

myControl.SetValueWithoutNotify(newValue); // ChangeEvent 없이 조용히 갱신
```

---

## Manipulator 계층

| 클래스 | 부모 | 용도 |
|--------|------|------|
| `Manipulator` | — | 기본 클래스 |
| `KeyboardNavigationManipulator` | Manipulator | 키보드 네비게이션 |
| `MouseManipulator` | Manipulator | 마우스 입력 + 활성화 필터 |
| `PointerManipulator` | MouseManipulator | 포인터 입력 + 활성화 필터 |
| `ContextualMenuManipulator` | MouseManipulator | 우클릭 컨텍스트 메뉴 |
| `Clickable` | PointerManipulator | 클릭 추적 (press + release) |

## ExampleDragger (드래그 이동)

```csharp
public class ExampleDragger : PointerManipulator
{
    private Vector3 m_Start;
    protected bool m_Active;
    private int m_PointerId;

    public ExampleDragger()
    {
        m_PointerId = -1;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        m_Active = false;
    }

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

    private void OnPointerDown(PointerDownEvent e)
    {
        if (m_Active) { e.StopImmediatePropagation(); return; }
        if (CanStartManipulation(e))
        {
            m_Start = e.localPosition;
            m_PointerId = e.pointerId;
            m_Active = true;
            target.CapturePointer(m_PointerId);
            e.StopPropagation();
        }
    }

    private void OnPointerMove(PointerMoveEvent e)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerId)) return;
        Vector2 diff = e.localPosition - m_Start;
        target.style.top = target.layout.y + diff.y;
        target.style.left = target.layout.x + diff.x;
        e.StopPropagation();
    }

    private void OnPointerUp(PointerUpEvent e)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerId) || !CanStopManipulation(e)) return;
        m_Active = false;
        target.ReleaseMouse();
        e.StopPropagation();
    }
}
```

## ExampleResizer (드래그 크기 조정)

```csharp
public class ExampleResizer : PointerManipulator
{
    private Vector3 m_Start;
    protected bool m_Active;
    private int m_PointerId;
    private Vector2 m_StartSize;

    public ExampleResizer()
    {
        m_PointerId = -1;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        m_Active = false;
    }

    protected override void RegisterCallbacksOnTarget() {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget() {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent e)
    {
        if (m_Active) { e.StopImmediatePropagation(); return; }
        if (CanStartManipulation(e))
        {
            m_Start = e.localPosition;
            m_StartSize = target.layout.size;
            m_PointerId = e.pointerId;
            m_Active = true;
            target.CapturePointer(m_PointerId);
            e.StopPropagation();
        }
    }

    private void OnPointerMove(PointerMoveEvent e)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerId)) return;
        Vector2 diff = e.localPosition - m_Start;
        target.style.height = m_StartSize.y + diff.y;
        target.style.width = m_StartSize.x + diff.x;
        e.StopPropagation();
    }

    private void OnPointerUp(PointerUpEvent e)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerId) || !CanStopManipulation(e)) return;
        m_Active = false;
        target.ReleasePointer(m_PointerId);
        m_PointerId = -1;
        e.StopPropagation();
    }
}
```

## 사용 방법

```csharp
var box = new VisualElement();
box.AddManipulator(new ExampleDragger());
box.AddManipulator(new ExampleResizer());

// 제거
box.RemoveManipulator(dragger);
```
