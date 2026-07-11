---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-VisualTree.html
created: 2026-04-16
---

# Introduction to Visual Elements and the Visual Tree

## What is a VisualElement?
"a node of a visual tree that instantiates or derives from the C# `VisualElement` class." UI Toolkit의 기본 빌딩 블록.

- 스타일 지정, 동작 정의, 화면 표시 가능
- 부모-자식 계층 구조 지원
- 기본 속성: styles, layout data, event handlers

## Visual Tree 계층
"an object graph, made of lightweight nodes, that holds all the elements in a window or panel."

- Editor UI 루트: `EditorWindow.rootVisualElement`
- Runtime UI 루트: `UIDocument.rootVisualElement`
- Box > Label + Checkbox + Slider 구조 예시

## 커스터마이즈 옵션
- 인라인 스타일 및 스타일시트 (시각적 외관)
- 이벤트 콜백 (동작 수정)
- 커스텀 컨트롤 (요소 조합 및 동작 수정)

## Built-in Controls
VisualElement 서브클래스: Button, Toggle, TextField 등 — 미리 정의된 동작과 시각 구조.

---

# Panels

## 패널이란?
"the parent object of a visual tree" — `rootVisualElement`를 소유하지만 자체는 visual element가 아님.

## 역할
- visual tree 컨테이너로 렌더링 활성화
- visual tree의 포커스 제어
- visual element 전반의 이벤트 디스패칭 관리

## 패널 유형
1. **Editor Panels** — EditorWindow 인스턴스에 속함
2. **Runtime Panels** — UIDocument 컴포넌트에 속함

## Visual Tree와의 관계
모든 visual element는 부모 패널에 대한 직접 참조 유지.
`VisualElement.panel` 프로퍼티로 연결 확인 (연결 안 됨 = `null` 반환).

---

# Draw Order

## 렌더링 순서
"depth-first search" 패턴 — 자식이 부모 위에, 형제는 리스트 순서대로.

1. 최상위 visual element
2. 첫 번째 자식
3. 해당 자식의 하위 요소들

## Draw Order 조정 메서드 (C#)
- `BringToFront()` — 요소를 앞으로 이동
- `SendToBack()` — 요소를 뒤로 이동
- `PlaceBehind(other)` — 형제 중에서 뒤로 배치
- `PlaceInFront(other)` — 형제 중에서 앞으로 배치

---

# Coordinate and Position Systems

## 위치 유형

**Relative Positioning**: 계산된 위치에서 오프셋. 부모가 자식 크기/위치에 영향.
**Absolute Positioning**: 부모 기준 위치, 자동 레이아웃 우회. 형제에 영향 없음.

## 좌표계
- **Local Space**: 요소 자신의 기준계, 원점 = 좌상단
- **Parent Space**: `layout.position` — 부모 좌표 기준
- **Window Space**: `worldBound` — 레이아웃+트랜스폼 적용 후 최종 좌표

## 핵심 변환 메서드 (VisualElementExtensions)
- `WorldToLocal` — Panel 공간 → 요소 로컬
- `LocalToWorld` — 요소 로컬 → Panel 공간
- `ChangeCoordinatesTo` — 두 요소 로컬 공간 간 변환

## 코드 예시
```csharp
var newElement = new VisualElement();
newElement.style.position = Position.Relative;
newElement.style.left = 15;
newElement.style.top = 35;
```
