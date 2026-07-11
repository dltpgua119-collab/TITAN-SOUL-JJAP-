---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Reference.html
created: 2026-04-16
---

# Events Reference — 이벤트 타입 요약

## Pointer Events

"Pointer events fire for UI interactions with a pointing device." Mouse Events보다 먼저 발생.

### 핵심 이벤트

| 이벤트 | 발생 시점 |
|--------|-----------|
| `PointerDownEvent` | 포인터 누름 |
| `PointerUpEvent` | 포인터 뗌 |
| `PointerMoveEvent` | 포인터 이동/상태 변경 |
| `PointerEnterEvent` | 요소 또는 하위에 진입 |
| `PointerLeaveEvent` | 요소 및 모든 하위에서 이탈 |
| `PointerOverEvent` | 요소 위로 들어옴 |
| `PointerOutEvent` | 요소에서 나감 |

### 주요 프로퍼티

| 프로퍼티 | 타입 | 설명 |
|----------|------|------|
| `position` | Vector3 | 화면/월드 좌표 |
| `localPosition` | Vector3 | 타겟 기준 상대 좌표 |
| `deltaPosition` | Vector3 | 이전 위치와의 차이 |
| `pointerId` | int | 포인터 식별자 |
| `button` | int | 0=좌, 1=우, 2=중간 |
| `pressure` | float | 터치 압력 (0~1), 미지원 시 1.0f |
| `modifiers` | EventModifiers | Shift/Ctrl/Alt 등 |

## Change Events

`ChangeEvent<T>` — 컨트롤 값 변경 시 발생:
```csharp
mySlider.RegisterValueChangedCallback(evt => {
    Debug.Log($"이전: {evt.previousValue}, 현재: {evt.newValue}");
});
```

## Click Events

`ClickEvent` — `Clickable` Manipulator가 click 판정 시 발생.
`PointerDownEvent` + `PointerUpEvent` 조합으로 구성.

## Keyboard Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `KeyDownEvent` | 키 누름 |
| `KeyUpEvent` | 키 뗌 |

주요 프로퍼티: `keyCode`, `character`, `modifiers`

## Focus Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `FocusInEvent` | 요소 또는 하위가 포커스 얻음 |
| `FocusOutEvent` | 포커스 잃음 |
| `FocusEvent` | 요소 자체가 포커스 얻음 |
| `BlurEvent` | 요소 자체 포커스 잃음 |

## Drag and Drop Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `DragEnterEvent` | 드래그 아이템이 요소에 진입 |
| `DragLeaveEvent` | 드래그 아이템이 요소를 이탈 |
| `DragUpdatedEvent` | 드래그 중 포인터 이동 |
| `DragPerformEvent` | 드롭 실행 |
| `DragExitedEvent` | 드래그 취소 |

## Layout Events

`GeometryChangedEvent` — 요소의 위치 또는 크기 변경 후 발생:
```csharp
element.RegisterCallback<GeometryChangedEvent>(evt => {
    Debug.Log($"새 크기: {evt.newRect.size}");
});
```

## Transition Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `TransitionRunEvent` | 트랜지션 실행 시작 |
| `TransitionStartEvent` | 트랜지션 딜레이 후 시작 |
| `TransitionEndEvent` | 트랜지션 완료 |
| `TransitionCancelEvent` | 트랜지션 취소 |

## Navigation Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `NavigateMoveEvent` | 방향 이동 (화살표 키) |
| `NavigateSubmitEvent` | 확인 (Enter) |
| `NavigateCancelEvent` | 취소 (Escape) |

## Panel Events

| 이벤트 | 발생 시점 |
|--------|-----------|
| `AttachToPanelEvent` | 요소가 Panel에 추가됨 |
| `DetachFromPanelEvent` | 요소가 Panel에서 제거됨 |
