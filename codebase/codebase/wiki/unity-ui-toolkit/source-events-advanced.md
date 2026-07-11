---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, events, manipulators, dispatch]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-events-advanced.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html
---

# 소스: Events 고급 — Dispatching, Callbacks, Manipulators

## 핵심 내용
이벤트 전파 경로(trickle-down → target → bubble-up), EventBase.target vs currentTarget, PickingMode, TrickleDown 콜백 등록, Manipulator 계층, ExampleDragger/ExampleResizer 전체 구현.

## 주요 인사이트
- `CapturePointer()` / `ReleasePointer()` 패턴으로 드래그 중 다른 요소가 포인터 탈취 방지
- `CanStartManipulation(e)` / `CanStopManipulation(e)` 활성화 필터 검사
- `StopImmediatePropagation()`으로 같은 요소의 다른 콜백도 차단

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/snippet-manipulators|Manipulator 스니펫]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Dispatching.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Events-Handling.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manipulators.html
