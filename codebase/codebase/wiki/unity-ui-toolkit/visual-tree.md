---
type: concept
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, visual-tree, visual-element, panel, draw-order, coordinate]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-visual-tree.md]
---

# Visual Tree

> UI Toolkit의 모든 UI 요소를 담는 경량 노드 계층 구조 — VisualElement를 기본 단위로 사용

## Visual Tree란?

"an object graph, made of lightweight nodes, that holds all the elements in a window or panel."

모든 UI 요소(VisualElement)를 부모-자식 계층으로 구성. 렌더링·이벤트·레이아웃 연산의 기반.

## VisualElement

**정의**: `VisualElement` 클래스를 인스턴스화하거나 상속받은 visual tree의 노드.

기본 속성:
- `styles` — 시각적 외관 스타일
- `layout` — 레이아웃 데이터 (Rect 타입)
- event handlers — 이벤트 처리

계층 구성:
```csharp
var container = new VisualElement();
var label = new Label("Hello");
var button = new Button(() => Debug.Log("Clicked")) { text = "OK" };

container.Add(label);
container.Add(button);
root.Add(container);
```

## 루트 요소

| 컨텍스트 | 루트 |
|---------|------|
| Editor UI | `EditorWindow.rootVisualElement` |
| Runtime UI | `UIDocument.rootVisualElement` |

## 패널 (Panel)

패널은 visual tree의 **부모 오브젝트** — `rootVisualElement`를 소유하지만 자체는 visual element가 아님.

| 역할 | 설명 |
|------|------|
| 렌더링 활성화 | visual tree 컨테이너 |
| 포커스 제어 | visual tree 포커스 관리 |
| 이벤트 디스패치 | 요소 전반의 이벤트 관리 |

### 패널 유형
- **Editor Panels** — `EditorWindow` 인스턴스에 속함
- **Runtime Panels** — `UIDocument` 컴포넌트에 속함

```csharp
// 패널 연결 확인
if (myElement.panel != null) {
    // 패널에 연결됨, 렌더링 가능
}
```

## Draw Order (렌더링 순서)

**Depth-first search** 패턴 — 자식이 부모 위에, 형제는 리스트 순서대로.

```text
Parent
├── Child A  (먼저 렌더)
│   └── Child A-1  (A 위에 렌더)
└── Child B  (마지막 렌더, 최상단)
```

### Draw Order 조정
```csharp
element.BringToFront();         // 형제 중 맨 앞으로
element.SendToBack();           // 형제 중 맨 뒤로
element.PlaceBehind(sibling);   // 특정 형제 뒤로
element.PlaceInFront(sibling);  // 특정 형제 앞으로
```

## 좌표 및 위치 시스템

### 위치 유형

**Relative (기본값)**:
```csharp
element.style.position = Position.Relative;
element.style.left = 15;   // 계산된 위치에서 +15
element.style.top = 35;    // 계산된 위치에서 +35
```

**Absolute**:
```csharp
element.style.position = Position.Absolute;
// 부모 기준 절대 좌표, 자동 레이아웃 우회
// 형제에 영향 없음
```

### 좌표계

| 좌표계 | 설명 | 접근 방법 |
|--------|------|----------|
| **Local Space** | 요소 자신의 기준계, 원점 = 좌상단 | - |
| **Parent Space** | 부모 좌표 기준 | `layout.position` |
| **Window Space** | 최종 화면 좌표 | `worldBound` |

### 좌표 변환 (VisualElementExtensions)
```csharp
// Panel 공간 → 요소 로컬
Vector2 local = element.WorldToLocal(worldPoint);

// 요소 로컬 → Panel 공간
Vector2 world = element.LocalToWorld(localPoint);

// 두 요소 로컬 공간 간 변환
Vector2 inOther = element.ChangeCoordinatesTo(otherElement, localPoint);
```

## 주의사항
- `VisualElement.panel`이 `null`이면 아직 렌더링 불가 상태
- Absolute 위치 요소는 형제의 레이아웃에 영향을 주지 않음
- Draw order 조정 메서드는 형제(sibling) 간에만 동작

## 관련 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/uquery|UQuery]]
- [[wiki/unity-ui-toolkit/uss-layout-engine|레이아웃 엔진 (Flexbox)]]
- [[wiki/unity-ui-toolkit/introduction|UI Toolkit 소개]]

## 출처
- [Introduction to visual elements](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-VisualTree.html)
- [Panels](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-panels.html)
- [Draw order](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-draw-order.html)
- [Coordinate and position systems](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-coordinate-and-position-system.html)
