---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, visual-tree, panel, draw-order, coordinate]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-visual-tree.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-VisualTree.html
---

# 소스: Visual Tree (VisualElement, Panels, Draw Order, Coordinates)

## 핵심 내용
Visual tree는 경량 노드(VisualElement)의 오브젝트 그래프. Panel이 rootVisualElement를 소유하며 렌더링·이벤트·포커스를 관리. Draw order는 depth-first. 좌표계는 Local/Parent/Window 세 가지.

## 주요 인사이트
- `VisualElement.panel == null` → 아직 렌더링 불가 상태
- BringToFront/SendToBack/PlaceBehind/PlaceInFront으로 draw order 조정
- Absolute 위치 = 형제에 영향 없음, 자동 레이아웃 우회
- `WorldToLocal`, `LocalToWorld`, `ChangeCoordinatesTo` — 좌표계 변환

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]]

## 원문 링크
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-VisualTree-landing.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-VisualTree.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-panels.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-draw-order.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-coordinate-and-position-system.html
