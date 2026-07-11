---
type: source
topic: unity-ui-toolkit
lang: uss/cs
tags: [unity, ui-toolkit, uss, transitions, transform]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-uss-transitions.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitions.html
---

# 소스: USS Transitions & Transform

## 핵심 내용
USS 트랜지션(transition-property/duration/timing-function/delay)과 Transform 속성(translate/scale/rotate/transform-origin) 레퍼런스. CSS 트랜지션과 동일한 메커니즘. Transform은 레이아웃 재계산 없이 요소를 변환.

## 주요 인사이트
- 변환 적용 순서: Scale → Rotate → Translate
- 첫 프레임에는 이전 상태가 없어 트랜지션이 발동하지 않음
- `width`/`height` 트랜지션은 레이아웃 재계산을 유발 → `scale`/`translate` 대체 권장
- 음수 `scale`은 요소를 반전시킴

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uss-transitions|USS Transitions & Transform]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitions.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transform.html
