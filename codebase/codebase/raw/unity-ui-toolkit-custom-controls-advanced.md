---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-create-custom-controls.html
created: 2026-04-16
---

# Custom Controls — 생성 패턴 및 요소 관리 Best Practices

## 커스텀 컨트롤 핵심 구조

```csharp
[UxmlElement]
public partial class ExampleElement : VisualElement {}
```

3가지 필수: `[UxmlElement]` 속성 + `partial` 선언 + `VisualElement` 상속

## 초기화 패턴

MonoBehaviour 라이프사이클 없음 → 생성자 또는 패널 이벤트 사용:

```csharp
public CustomControl() {
    RegisterCallback<AttachToPanelEvent>(e => { /* panel에 추가될 때 초기화 */ });
    RegisterCallback<DetachFromPanelEvent>(e => { /* 제거될 때 정리 */ });
}
```

## UXML에서 사용

UI Builder Library 탭에 자동 등록:
```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ExampleElement />
</ui:UXML>
```

Library에서 숨기려면: `[HideInInspector]`

## 커스텀 USS 속성

```csharp
CustomStyleProperty<Color> customColor = new CustomStyleProperty<Color>("--custom-color");

RegisterCallback<CustomStylesResolvedEvent>(e => {
    e.customStyle.TryGetValue(customColor, out Color value);
});
```

---

# UXML 캡슐화 패턴

## UXML-First 방식

UXML에서 구조 정의 → C#에서 Q<>()로 참조:

```csharp
[UxmlElement]
public partial class CardElement : VisualElement {
    private VisualElement portraitImage => this.Q("image");
    private Label attackBadge => this.Q<Label>("attack-badge");
    private Label healthBadge => this.Q<Label>("health-badge");

    public void Init(Texture2D image, int health, int attack) {
        portraitImage.style.backgroundImage = image;
        attackBadge.text = health.ToString();
        healthBadge.text = attack.ToString();
    }
}
```

```xml
<!-- CardElement.uxml -->
<CardElement>
    <ui:VisualElement name="image" />
    <ui:VisualElement name="stats">
        <ui:Label name="attack-badge" class="badge" />
        <ui:Label name="health-badge" class="badge" />
    </ui:VisualElement>
</CardElement>
```

## Element-First 방식 (생성자에서 UXML 로드)

```csharp
[UxmlElement]
public partial class CardElement : VisualElement {
    public CardElement(Texture2D image, int health, int attack) {
        var asset = Resources.Load<VisualTreeAsset>("CardElement");
        asset.CloneTree(this);  // 자신에게 붙임

        this.Q("image").style.backgroundImage = image;
        this.Q<Label>("attack-badge").text = attack.ToString();
        this.Q<Label>("health-badge").text = health.ToString();
    }
}
```

```csharp
// 사용 (C#)
foreach (Card card in GetCards()) {
    var cardElement = new CardElement(card.image, card.health, card.attack);
    document.rootVisualElement.Add(cardElement);
}
```

### 두 방식 비교
| 방식 | 특징 |
|------|------|
| UXML-First | 구조 고정, UXML 템플릿 단순 |
| Element-First | 유연, 런타임 조건에 따라 다른 UXML 로드 가능 |

---

# 요소 관리 Best Practices

## 숨기기 방식 비교

| 방법 | CPU | GPU | 메모리 | 용도 |
|------|-----|-----|--------|------|
| `visibility: hidden` | 중 | 낮 | 렌더 명령 해제 | 잠시 숨기기 |
| `opacity: 0` | 낮 | 높 | 메시 유지 | 페이드 효과 |
| `display: none` | 높 | 없음 | 명령/메시 유지 | 영구 비활성화 |
| `translate: -5000px` | 낮 | 중 | 지오메트리 활성 | 화면 밖 이동 |
| `RemoveFromHierarchy()` | 없음 | 없음 | 완전 해제 | 재생성 비용 감수 |

## 요소 풀링

"Elements pooling is to keep hold of elements that you might recreate later on, rather than creating elements with `new()` every time."

**주의**: 풀에 반환 전 이벤트 콜백 반드시 해제 → 미해제 시 메모리 누수/예기치 않은 동작

## 대량 목록

`ListView` 사용 권장 — "pools and recycles elements as the user scrolls."

커스텀 리사이클이 필요하면:
- `GeometryChangedEvent`로 컨테이너 크기 감지
- `VisualElement.layout` 프로퍼티로 자식 크기 계산

## Best Practices 정리
- 기능적 측면 → UXML 속성으로 노출
- 외관 측면 → USS 속성으로 노출
- UXML 속성은 프리미티브 타입만, 복잡한 데이터는 C# 런타임 전달
- USS 클래스명은 상수로 노출 (UQuery 사용 편의)
- BEM 명명 준수
- 정적 콜백으로 메모리 오버헤드 최소화
