---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, renderer, painter2d, mesh-api]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-ui-renderer.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-generate-2d-visual-content.html
---

# 소스: UI Renderer — 2D 비주얼 콘텐츠 생성

## 핵심 내용
Painter2D(BeginPath/LineTo/Arc/Fill/Stroke)와 Mesh API(Allocate/SetNextVertex/SetNextIndex) 전체 코드 예시. FillRule.OddEven으로 구멍 있는 도형.

## 주요 인사이트
- `Vertex.nearZ` 필수 — UI 클리핑과 z-depth 일치
- Painter2D가 Mesh API보다 사용하기 쉬움 (권장)
- `generateVisualContent` += 로 여러 드로잉 핸들러 누적 가능

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/ui-renderer|UI Renderer — 2D 커스텀 비주얼 콘텐츠]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-generate-2d-visual-content.html
