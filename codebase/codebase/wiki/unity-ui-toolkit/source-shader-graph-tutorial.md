---
type: source
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, shader-graph, urp, tutorial]
created: 2026-05-03
updated: 2026-05-03
source_path: raw/unity-ui-toolkit-shader-graph-tutorial.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/get-started-with-ui-shader-graph.html
---

# 소스: Get started with UI Shader Graph

## 핵심 내용

URP 프로젝트에서 `Create > Shader Graph > URP > UI Shader Graph`로 셰이더 자산 생성 → Render Type Branch + UV Distortion 노드 연결 → Material 생성 후 Shader 할당 → UI Builder의 Button Inspector에서 Material 드롭다운으로 적용. 부모 요소에 적용한 머티리얼은 모든 자식에 전파된다.

## 주요 인사이트

- **URP 필수** — 메뉴에 항목 자체가 안 나타날 수 있음
- 머티리얼 적용 단위는 element 단위지만 자식 요소까지 영향
- UV Distortion → Render Type Branch.Solid → Fragment Base Color/Alpha 가 가장 단순한 그라디언트 셰이더 패턴

## 이 소스로 생성된 페이지

- [[wiki/unity-ui-toolkit/howto-ui-shader-graph-gradient|UI Shader Graph로 그라디언트 버튼 만들기]]

## 원문 링크

https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/get-started-with-ui-shader-graph.html
