---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, custom-controls, pooling]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-custom-controls-advanced.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-create-custom-controls.html
---

# 소스: Custom Controls — 생성 패턴 및 요소 관리

## 핵심 내용
UXML-First vs Element-First(CloneTree) 캡슐화 패턴, 요소 숨기기 방식별 성능 비교(visibility/opacity/display/translate/RemoveFromHierarchy), 풀링 시 콜백 해제 주의사항.

## 주요 인사이트
- Element-First는 런타임 조건부 UXML 선택 가능
- 풀 반환 전 콜백 미해제 → 메모리 누수
- ListView가 가상화된 목록의 표준 해법

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/custom-controls-advanced|커스텀 컨트롤 — UXML 캡슐화 & 요소 관리]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-create-custom-controls.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-encapsulate-uxml-with-logic.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-best-practices-for-managing-elements.html
