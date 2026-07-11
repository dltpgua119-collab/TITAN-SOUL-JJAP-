---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/custom-controls.html
created: 2026-04-16
---

# Creating Custom Controls in UI Toolkit (E-Book Chapter)

## 핵심 접근법
VisualElement 또는 적합한 서브클래스(Button, BaseField<T> 등)를 상속.

### [UxmlElement] 어트리뷰트
```csharp
[UxmlElement]
public partial class CustomToggle : VisualElement
{
    // Implementation
}
```
→ UI Builder Library의 **Custom Controls (C#)** 아래 자동 등록.

### [UxmlAttribute] 어트리뷰트
```csharp
[UxmlAttribute]
public string Label { get; set; }

[UxmlAttribute(name:"bg-color")]
public Color BackgroundColor { get; set; }
```
→ Inspector에서 설정 가능. Range, Tooltip, Header 등 데코레이터 어트리뷰트 지원.

## 초기화 패턴
GameObjects(Awake/OnEnable)와 달리 생성자에서 초기화:
```csharp
public CustomToggle()
{
    // 시각 구조 설정
}
```
지연 초기화: `AttachToPanelEvent` / `DetachFromPanelEvent` 콜백 등록.

## 모범 사례
- 자기 완결적·재사용 가능한 요소로 캡슐화
- 적절한 기반 클래스 상속 (Button-like → Button, 폼 입력 → BaseField<T>)
- 스타일링·애니메이션에 USS 클래스 사용
- 이벤트 핸들링: ClickEvent, KeyDownEvent
- 무한 업데이트 루프 방지: `SetValueWithoutNotify()`로 시각 상태 업데이트

## SlideToggle 예시
- `BaseField<bool>` 상속
- 텍스트 레이블·색상을 UxmlAttribute로 노출
- 클릭·키보드 이벤트 처리
- 동적으로 visual tree 업데이트
- USS 트랜지션으로 폴리시
