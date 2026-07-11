---
type: source
topic: unity-ui-toolkit
lang: uss
tags: [unity, ui-toolkit, uss, variables, custom-properties]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-uss-variables.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-USS-CustomProperties.html
---

# 소스: USS Variables (Custom Properties)

## 핵심 내용
`--variable-name` 선언, `var(--name, fallback)` 사용. CSS와 달리 `var()` 중첩 및 수학 연산 불가. Unity 내장 `--unity-*` 변수로 테마 자동 호환.

## 주요 인사이트
- `:root`에 선언해야 전체 트리에서 참조 가능
- 내장 변수(`--unity-colors-button-background` 등)로 커스텀 컨트롤이 Editor 테마 자동 적용
- `--unity-metrics-single_line-height` 등으로 표준 크기 준수 가능

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uss-variables|USS Variables]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-USS-CustomProperties.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-uss-built-in-variable-reference.html
