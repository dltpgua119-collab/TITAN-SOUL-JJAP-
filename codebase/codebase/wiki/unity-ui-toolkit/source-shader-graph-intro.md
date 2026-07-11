---
type: source
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, shader-graph, urp]
created: 2026-05-03
updated: 2026-05-03
source_path: raw/unity-ui-toolkit-shader-graph-intro.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/introduction-to-ui-shader-graph.html
---

# 소스: Introduction to UI Shader Graph

## 핵심 내용

UI Shader Graph는 URP 전용 기능으로, UI Toolkit이 단일 머티리얼로 5종 렌더 타입(Solid·Texture·SDF Text·Bitmap Text·Gradient)을 한꺼번에 그리는 구조를 위해 `Render Type Branch` 노드로 분기를 처리한다. UI 전용 입력 노드(Element Texture UV / Layout UV / Texture Size / Sample Element Texture)와 5개의 Default 노드를 제공한다.

## 주요 인사이트

- **mesh-level 셰이더만 만든다** — 픽셀 후처리(filter)는 별도 `FilterFunctionDefinition` 경로
- Render Type Branch의 미연결 입력은 자동으로 Default 동작 사용 → 분기 효율을 위해 굳이 Default 노드 연결 금지
- `From Template > UI` 템플릿이 Render Type Branch를 미리 깔아 줌
- `Sample Element Texture` 가 가져오는 텍스처는 element가 그 시점에 그리는 텍스처(폰트 atlas, background image 등)다 — 별도 슬롯이 아님

## 이 소스로 생성된 페이지

- [[wiki/unity-ui-toolkit/concept-ui-shader-graph|UI Shader Graph 개요]]

## 원문 링크

https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/introduction-to-ui-shader-graph.html
