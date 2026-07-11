---
type: concept
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, custom-controls, uxml, pooling, lifecycle]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-custom-controls-advanced.md]
---

# 커스텀 컨트롤 — UXML 캡슐화 & 요소 관리

> 커스텀 컨트롤에 UXML 구조를 통합하는 두 가지 패턴과, 요소 숨기기/풀링의 성능 트레이드오프를 정리한다.

## UXML 캡슐화 패턴

### UXML-First 방식

UXML에서 구조를 정의하고, C#에서 `Q<>()`로 참조한다.

```cs
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
<!-- CardElement.uxml — 루트가 CardElement -->
<CardElement>
    <ui:VisualElement name="image" />
    <ui:VisualElement name="stats">
        <ui:Label name="attack-badge" class="badge" />
        <ui:Label name="health-badge" class="badge" />
    </ui:VisualElement>
</CardElement>
```

C#에서 인스턴스화:
```cs
var templateContainer = template.Instantiate();
var cardElement = templateContainer.Q<CardElement>();
document.rootVisualElement.Add(cardElement);
cardElement.Init(card.image, card.health, card.attack);
```

---

### Element-First 방식 (생성자에서 UXML 로드)

생성자에서 UXML을 로드해 자신에게 `CloneTree()`.

```cs
[UxmlElement]
public partial class CardElement : VisualElement {
    public CardElement(Texture2D image, int health, int attack) {
        var asset = Resources.Load<VisualTreeAsset>("CardElement");
        asset.CloneTree(this);

        this.Q("image").style.backgroundImage = image;
        this.Q<Label>("attack-badge").text = attack.ToString();
        this.Q<Label>("health-badge").text = health.ToString();
    }
}

// 사용
foreach (Card card in GetCards()) {
    document.rootVisualElement.Add(
        new CardElement(card.image, card.health, card.attack)
    );
}
```

---

### 두 방식 비교

| 방식 | 적합한 상황 |
|------|-------------|
| UXML-First | 구조가 고정적, 단순한 UXML 템플릿 |
| Element-First | 런타임 조건에 따라 다른 UXML 로드 필요 |

---

## 요소 숨기기 — 성능 비교

| 방법 | CPU | GPU | 재사용 |
|------|-----|-----|--------|
| `visibility: hidden` | 중 | 낮 | 빠름 |
| `opacity: 0` | 낮 | 높 (overdraw) | 빠름 |
| `display: none` | 높 | 없음 | 빠름 |
| `translate: -5000px` | 낮 | 중 | 빠름 |
| `RemoveFromHierarchy()` | 없음 | 없음 | 느림 (재생성 비용) |

> 자주 토글하는 요소는 `display: none` 권장. 완전히 제거 후 드물게 재생성하는 경우에만 `RemoveFromHierarchy()`.

---

## 요소 풀링

"Elements pooling — keep hold of elements that you might recreate later, rather than creating with `new()` every time."

```cs
// 풀에서 꺼낼 때
var element = pool.Get();
element.style.display = DisplayStyle.Flex;

// 풀에 반환할 때 — 반드시 콜백 해제
element.UnregisterCallback<ClickEvent>(OnClick);
element.style.display = DisplayStyle.None;
pool.Return(element);
```

**주의**: 콜백 미해제 상태로 풀에 반환 → 메모리 누수 + 예기치 않은 동작

---

## 대량 목록 처리

`ListView` 사용 권장:
- 뷰포트에 보이는 항목만 렌더링
- 스크롤 시 자동 요소 재활용

커스텀 가상화 구현 시:
```cs
container.RegisterCallback<GeometryChangedEvent>(evt => {
    // evt.newRect으로 컨테이너 크기 파악
    // element.layout으로 자식 크기 계산
});
```

---

## 커스텀 USS 속성 읽기

```cs
static readonly CustomStyleProperty<Color> s_MyColor =
    new CustomStyleProperty<Color>("--my-color");

void RegisterCustomStyles() {
    RegisterCallback<CustomStylesResolvedEvent>(OnStylesResolved);
}

void OnStylesResolved(CustomStylesResolvedEvent e) {
    if (e.customStyle.TryGetValue(s_MyColor, out Color color)) {
        // color 사용
    }
}
```

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤 기초]] — [UxmlElement]/[UxmlAttribute], BaseField<T>
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — display, UsageHints
- [[wiki/unity-ui-toolkit/uquery|UQuery]] — Q<T>() 패턴

## 출처
- `raw/unity-ui-toolkit-custom-controls-advanced.md`
