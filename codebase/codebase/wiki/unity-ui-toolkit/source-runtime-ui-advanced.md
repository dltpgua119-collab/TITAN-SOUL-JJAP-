---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, runtime, panel-settings]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-runtime-ui-advanced.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-move-elements-at-runtime.html
---

# 소스: Runtime UI 고급

## 핵심 내용
`style.translate` + `UsageHints.DynamicTransform`이 런타임 이동 권장 패턴. Panel Settings 속성(Sort Order, Scale Mode, Target Texture). World Space UI 배치.

## 주요 인사이트
- `style.left`/`style.top`은 레이아웃 재계산 유발 → `style.translate` 사용
- `DynamicTransform` hint를 이동하는 요소에만 설정 (자식 불필요)
- 여러 UIDocument가 있을 때 Sort Order로 렌더 순서 제어

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/runtime-ui-advanced|Runtime UI 고급]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-move-elements-at-runtime.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-render-runtime-ui.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-runtime-panel-settings.html
