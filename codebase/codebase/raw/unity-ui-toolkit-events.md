---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html
created: 2026-04-16
---

# UI Toolkit 이벤트 시스템

## 이벤트 전파 단계

이벤트는 visual tree를 세 단계로 전파:

1. **Trickle-Down (하강)**: 루트 → 타겟 방향으로 콜백 실행
2. **Target Phase**: 이벤트 타겟 직접 수신
3. **Bubble-Up (상승)**: 타겟 → 루트 방향으로 콜백 실행

모든 이벤트 유형이 세 단계를 모두 사용하지는 않음.

## 핵심 프로퍼티
- `EventBase.target` — 이벤트 발생 요소 (포인터 아래 최상단 요소)
- `EventBase.currentTarget` — 콜백이 등록된 요소

## 콜백 등록

### 기본 등록 (Bubble-Up 단계)
```csharp
myElement.RegisterCallback<PointerDownEvent>(MyCallback);

void MyCallback(PointerDownEvent evt) {
    Debug.Log($"Pointer down on {evt.target.name}");
}
```

### Trickle-Down 등록 (하강 단계)
```csharp
myElement.RegisterCallback<PointerDownEvent>(MyCallback, TrickleDown.TrickleDown);
```

### 커스텀 데이터 전달
```csharp
myElement.RegisterCallback<PointerDownEvent, MyType>(MyCallbackWithData, myData);

void MyCallbackWithData(PointerDownEvent evt, MyType data) { /* ... */ }
```

### 등록 해제
```csharp
myElement.UnregisterCallback<PointerDownEvent>(MyCallback);
```

## 값 변경 콜백

```csharp
// 값 읽기
int val = myIntegerField.value;

// 변경 감지
myIntegerField.RegisterValueChangedCallback(OnIntegerFieldChange);

void OnIntegerFieldChange(ChangeEvent<int> evt) {
    Debug.Log($"Value changed: {evt.previousValue} → {evt.newValue}");
}

// 알림 없이 값 변경 (무한 루프 방지)
myControl.SetValueWithoutNotify(newValue);
```

## Picking Mode
- `PickingMode.Position` (기본): 위치 기반 직사각형 피킹
- `PickingMode.Ignore`: 포인터 이벤트 피킹 방지
- 커스텀 교차 로직: `VisualElement.ContainsPoint()` 오버라이드

## 중요 노트
"If you hide or disable an element, it won't receive events. Events still propagate to the ancestors and descendants of a hidden or disabled element."

---

# 조작기 (Manipulators)

## 정의
"state machines that handle user interaction with UI elements" — 이벤트 처리 로직 캡슐화, 콜백 저장/등록/해제 관리.

## 내장 조작기 계층

| 클래스 | 설명 |
|--------|------|
| `Manipulator` | 모든 조작기의 기반 클래스 |
| `KeyboardNavigationManipulator` | 키보드 입력 → 네비게이션 변환 |
| `MouseManipulator` | 마우스 입력 + 활성화 필터 |
| `PointerManipulator` | 포인터 입력 + 활성화 필터 |
| `Clickable` | 클릭 이벤트 감지 (같은 요소에서 누름+뗌) |
| `ContextualMenuManipulator` | 우클릭/메뉴 키로 컨텍스트 메뉴 표시 |

## 커스텀 조작기 구현
1. `PointerManipulator` 상속
2. `RegisterCallbacksOnTarget()` / `UnregisterCallbacksFromTarget()` 오버라이드
3. `target.AddManipulator(new MyManipulator())` 으로 요소에 연결

### ExampleDragger 패턴
```csharp
public class ExampleDragger : PointerManipulator
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
        if (target.HasPointerCapture(evt.pointerId))
        {
            target.style.left = target.layout.x + evt.deltaPosition.x;
            target.style.top = target.layout.y + evt.deltaPosition.y;
        }
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        target.ReleasePointer(evt.pointerId);
    }
}
```
