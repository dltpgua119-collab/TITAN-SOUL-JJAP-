---
type: concept
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, events, pointer, keyboard, focus, transition]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-events-reference.md]
---

# 이벤트 타입 레퍼런스

> UI Toolkit 이벤트 계층의 모든 주요 이벤트 타입 요약. 등록 방법: `element.RegisterCallback<T>(handler)`.

## Pointer Events

포인팅 장치(마우스, 펜, 터치)와의 상호작용. Mouse Events보다 먼저 발생하며 압력/기울기 데이터 포함.

### 이벤트 목록

| 이벤트 | 발생 시점 |
|--------|-----------|
| `PointerDownEvent` | 포인터 누름 |
| `PointerUpEvent` | 포인터 뗌 |
| `PointerMoveEvent` | 포인터 이동/상태 변경 |
| `PointerEnterEvent` | 요소 또는 하위에 진입 |
| `PointerLeaveEvent` | 요소 및 모든 하위에서 이탈 |
| `PointerOverEvent` | 요소 바로 위로 진입 |
| `PointerOutEvent` | 요소에서 나감 |

### 주요 프로퍼티

| 프로퍼티 | 타입 | 설명 |
|----------|------|------|
| `position` | Vector3 | 화면 좌표 |
| `localPosition` | Vector3 | 타겟 기준 상대 좌표 |
| `deltaPosition` | Vector3 | 이전 위치 대비 델타 |
| `pointerId` | int | 포인터 식별자 |
| `button` | int | 0=좌클릭, 1=우클릭, 2=중간 |
| `pressure` | float | 터치 압력 0~1 (미지원 장치: 1.0f) |
| `modifiers` | EventModifiers | Shift/Ctrl/Alt 상태 |

```cs
element.RegisterCallback<PointerDownEvent>(evt => {
    Debug.Log($"Button: {evt.button}, Pos: {evt.localPosition}");
});
```

---

## Change Events

컨트롤 값 변경 시 발생. 모든 `BaseField<T>` 기반 컨트롤이 지원.

```cs
// 값 변경 수신
mySlider.RegisterValueChangedCallback(evt => {
    Debug.Log($"이전: {evt.previousValue}, 현재: {evt.newValue}");
});

// 이벤트 없이 값만 변경
myControl.SetValueWithoutNotify(newValue);
```

---

## Click Events

`Clickable` Manipulator가 press + release 패턴을 감지해 발송.

```cs
button.RegisterCallback<ClickEvent>(evt => {
    Debug.Log("Clicked!");
});
```

---

## Keyboard Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `KeyDownEvent` | 키 누름 |
| `KeyUpEvent` | 키 뗌 |

```cs
element.RegisterCallback<KeyDownEvent>(evt => {
    if (evt.keyCode == KeyCode.Escape) Close();
});
```

---

## Focus Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `FocusInEvent` | 요소 또는 하위가 포커스 얻음 (버블업) |
| `FocusOutEvent` | 포커스 잃음 (버블업) |
| `FocusEvent` | 요소 자체 포커스 획득 |
| `BlurEvent` | 요소 자체 포커스 상실 |

---

## Layout Events

```cs
// 레이아웃 변경 완료 시 호출
element.RegisterCallback<GeometryChangedEvent>(evt => {
    Debug.Log($"새 크기: {evt.newRect.size}");
});
```

---

## Drag and Drop Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `DragEnterEvent` | 드래그 아이템이 요소에 진입 |
| `DragLeaveEvent` | 요소를 이탈 |
| `DragUpdatedEvent` | 드래그 중 이동 |
| `DragPerformEvent` | 드롭 확정 |
| `DragExitedEvent` | 드래그 취소 |

---

## Transition Events

USS 트랜지션 라이프사이클:

| 이벤트 | 발생 시점 |
|--------|-----------|
| `TransitionRunEvent` | 트랜지션 실행 시작 |
| `TransitionStartEvent` | 딜레이 이후 실제 시작 |
| `TransitionEndEvent` | 완료 |
| `TransitionCancelEvent` | 취소 |

```cs
element.RegisterCallback<TransitionEndEvent>(evt => {
    Debug.Log($"Transition ended: {evt.stylePropertyNames}");
});
```

---

## Panel Events

```cs
// 요소가 Panel에 연결/해제될 때 초기화/정리
myElement.RegisterCallback<AttachToPanelEvent>(e => Initialize());
myElement.RegisterCallback<DetachFromPanelEvent>(e => Cleanup());
```

---

## Navigation Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `NavigateMoveEvent` | 방향키 이동 |
| `NavigateSubmitEvent` | Enter 확인 |
| `NavigateCancelEvent` | Escape 취소 |

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]] — 3단계 전파, RegisterCallback 기초
- [[wiki/unity-ui-toolkit/snippet-manipulators|Manipulator 스니펫]] — Pointer 이벤트 활용 드래그 구현
- [[wiki/unity-ui-toolkit/uss-transitions|USS Transitions & Transform]] — TransitionEndEvent 활용

## 출처
- `raw/unity-ui-toolkit-events-reference.md`
