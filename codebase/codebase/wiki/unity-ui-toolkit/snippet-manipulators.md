---
type: snippet
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, events, manipulators, drag, pointer]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-events-advanced.md]
---

# Manipulator 스니펫 — 드래그 이동 & 크기 조정

> `PointerManipulator`를 상속해 드래그 이동(ExampleDragger)과 크기 조정(ExampleResizer)을 구현하는 재사용 가능 코드.

## 드래그 이동 (ExampleDragger)

요소를 마우스로 드래그해 이동시키는 Manipulator.

```cs
using UnityEngine;
using UnityEngine.UIElements;

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

---

## 드래그 크기 조정 (ExampleResizer)

요소의 오른쪽 하단을 드래그해 크기를 조정하는 Manipulator.

```cs
using UnityEngine;
using UnityEngine.UIElements;

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

---

## 사용 방법

```cs
// 요소에 Manipulator 추가
var box = new VisualElement();
box.style.width = 100;
box.style.height = 100;
box.style.position = Position.Absolute;
box.pickingMode = PickingMode.Position;  // 필수

box.AddManipulator(new ExampleDragger());
box.AddManipulator(new ExampleResizer());

// 제거
box.RemoveManipulator(dragger);
```

---

## Manipulator 계층

| 클래스 | 용도 |
|--------|------|
| `Manipulator` | 기본 클래스 |
| `MouseManipulator` | 마우스 + 활성화 필터 |
| `PointerManipulator` | 포인터 + 활성화 필터 (권장) |
| `ContextualMenuManipulator` | 우클릭 컨텍스트 메뉴 |
| `Clickable` | 클릭 추적 (press + release) |

## 커스텀 Manipulator 패턴

```cs
public class MyManipulator : PointerManipulator
{
    protected override void RegisterCallbacksOnTarget()
    {
        // 이벤트 등록
        target.RegisterCallback<PointerDownEvent>(OnDown);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        // 반드시 모두 해제
        target.UnregisterCallback<PointerDownEvent>(OnDown);
    }

    private void OnDown(PointerDownEvent e)
    {
        if (!CanStartManipulation(e)) return;
        // 로직
        e.StopPropagation();
    }
}
```

## 의존성
- `UnityEngine.UIElements`

## 관련 페이지
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]] — 3단계 전파, RegisterCallback 기초
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]] — TrickleDown, 값 변경 이벤트

## 출처
- `raw/unity-ui-toolkit-events-advanced.md`
