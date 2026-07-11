---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, custom-controls, data-binding]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-bindable-control.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-bind-custom-control.html
---

# 소스: 바인딩 가능한 커스텀 컨트롤

## 핵심 내용
BindableElement + INotifyValueChanged<T> 구현. SetValueWithoutNotify()로 중복 ChangeEvent 방지. GetPooled() 패턴으로 이벤트 Pool 재활용. binding-path로 UXML에서 자동 바인딩.

## 주요 인사이트
- 서브 컨트롤(ObjectField 등)의 값 갱신 시 반드시 `SetValueWithoutNotify()` 사용
- `evt.target = this` 설정으로 이벤트 발원지 명시
- `using (var evt = ChangeEvent<T>.GetPooled(...))` 패턴이 표준

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/snippet-bindable-custom-control|바인딩 가능한 커스텀 컨트롤 스니펫]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-bind-custom-control.html
