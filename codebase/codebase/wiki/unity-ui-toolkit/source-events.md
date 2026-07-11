---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, events, manipulator, callback]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-events.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html
---

# 소스: 이벤트 시스템 (Dispatch, Handling, Manipulators)

## 핵심 내용
Trickle-Down/Target/Bubble-Up 3단계 전파. RegisterCallback<T>(). RegisterValueChangedCallback(). StopPropagation(). Manipulator 계층 (Clickable/PointerManipulator 등). 커스텀 드래그 조작기 구현 패턴.

## 주요 인사이트
- event.target은 전파 중 변하지 않음, currentTarget은 변함
- 숨김/비활성 요소는 이벤트 수신 안 하지만 전파는 계속
- SetValueWithoutNotify()로 무한 루프 방지
- AddManipulator()로 요소에 조작기 연결

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/events-overview|이벤트 시스템]]

## 원문 링크
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Handling.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manipulators.html
