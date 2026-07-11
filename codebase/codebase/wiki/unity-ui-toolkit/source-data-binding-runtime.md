---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, data-binding, runtime-binding]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-data-binding-runtime.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-get-started-runtime-binding.html
---

# 소스: Runtime Data Binding

## 핵심 내용
`[CreateProperty]` 데이터 소스 클래스, UXML data-source/data-source-path/binding-mode 선언, TwoWay/ToTarget/ToSource/ToTargetOnce 바인딩 모드, 업데이트 트리거(Every Frame / On Change Detection / When Marked Dirty).

## 주요 인사이트
- `ToTargetOnce`는 초기값만 적용 — 이후 `MarkDirty()` 필요
- `Every Frame` 트리거는 성능 비용이 있어 꼭 필요한 경우만 사용
- UI Builder에서 Preview 모드로 바인딩 동작 미리 확인 가능

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/data-binding-runtime|Runtime Data Binding 설정하기]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-get-started-runtime-binding.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-runtime-binding-mode-update.html
