---
topic: unity-ui-toolkit
original_type: md
created: 2026-04-17
---

# 미지원 CSS 패턴 및 USS 우회 방법

USS에서 지원하지 않는 주요 CSS 패턴과 Unity UI Toolkit에서 동일한 효과를 내는 대안을 정리한다.

## box-shadow

```css
/* CSS */
.card { box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15); }
```

우회 방법 1: 배경 VisualElement 중첩
```uxml
<VisualElement class="card-wrapper">
  <VisualElement class="card-shadow"/>
  <VisualElement class="card"/>
</VisualElement>
```
```uss
.card-wrapper { position: relative; }
.card-shadow {
  position: absolute;
  top: 4px; left: 4px;
  width: 100%; height: 100%;
  background-color: rgba(0, 0, 0, 0.15);
  border-radius: 8px;
}
```

우회 방법 2: border로 단순화
```uss
.card {
  border-width: 1px;
  border-color: rgba(0, 0, 0, 0.12);
}
```

## text-shadow

```css
/* CSS */
.title { text-shadow: 1px 1px 2px rgba(0,0,0,0.5); }
```

우회: `-unity-text-outline`으로 근사
```uss
.title {
  -unity-text-outline-width: 1px;
  -unity-text-outline-color: rgba(0, 0, 0, 0.4);
}
```

## @keyframes 애니메이션

```css
/* CSS */
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(-10px); }
  to   { opacity: 1; transform: translateY(0); }
}
.element { animation: fadeIn 0.3s ease; }
```

우회 방법 1: USS transition (단순 두 상태)
```uss
.element {
  opacity: 0;
  translate: 0 -10px;
  transition: opacity 0.3s ease, translate 0.3s ease;
}
.element.visible {
  opacity: 1;
  translate: 0 0;
}
```
```csharp
element.AddToClassList("visible");
```

우회 방법 2: C# 코루틴 (복잡한 키프레임)
```csharp
IEnumerator AnimateFadeIn(VisualElement el, float duration)
{
    float elapsed = 0f;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);
        el.style.opacity = Mathf.Lerp(0f, 1f, t);
        yield return null;
    }
    el.style.opacity = 1f;
}
```

우회 방법 3: 실험적 Animation API (Unity 2022.2+)
```csharp
using UnityEngine.UIElements.Experimental;

element.experimental.animation
    .Start(new StyleValues { opacity = 1f }, 300)
    .Ease(Easing.InOutSine);
```

## ::before / ::after 가상 요소

```css
/* CSS */
.badge::before { content: "★"; color: gold; }
```

우회: C#으로 자식 VisualElement 삽입
```csharp
var icon = new Label("★");
icon.AddToClassList("badge-icon");
badge.Insert(0, icon);
```
```uss
.badge-icon { color: rgb(255, 215, 0); }
```

## filter (blur, brightness, grayscale)

USS에서 대안 없음. 가능한 근사:
- `brightness(0.5)` → 반투명 검정 오버레이 (position: absolute)
- `blur` → 미리 blur된 텍스처를 에셋으로 준비하거나 커스텀 셰이더 사용
- `grayscale` → 커스텀 셰이더 Material

## @media (미디어 쿼리)

우회: C# GeometryChangedEvent
```csharp
root.RegisterCallback<GeometryChangedEvent>(evt =>
{
    float width = evt.newRect.width;
    sidebar.style.display = width < 768f
        ? DisplayStyle.None
        : DisplayStyle.Flex;
});
```

## calc()

우회 방법 1: flex-grow로 남은 공간 채우기
```uss
/* calc(100% - 240px) 대체 */
.sidebar { width: 240px; }
.panel   { flex-grow: 1; }
```

우회 방법 2: C#으로 계산
```csharp
root.RegisterCallback<GeometryChangedEvent>(evt =>
{
    panel.style.width = evt.newRect.width - 240f;
});
```

## z-index

```csharp
element.BringToFront();
element.SendToBack();
element.PlaceInFront(referenceElement);
element.PlaceBehind(referenceElement);
```

## text-decoration (underline, line-through)

우회 방법 1: Rich Text Tags
```csharp
label.text = "<u>링크 텍스트</u>";
label.text = "<s>취소선 텍스트</s>";
```

우회 방법 2: border로 밑줄 근사
```uxml
<VisualElement class="underline-wrapper">
  <Label text="링크 텍스트"/>
  <VisualElement class="underline"/>
</VisualElement>
```
```uss
.underline-wrapper { flex-direction: column; }
.underline { height: 1px; background-color: rgb(0, 0, 255); }
```

## hsl() 색상

미지원. rgb()로 사전 변환 필요.
```uss
.element { color: rgb(33, 150, 243); } /* hsl(210, 100%, 56%) → rgb */
```

## border-style (dashed, dotted)

USS는 항상 solid. 점선 효과가 필요하면 점선 패턴 텍스처를 background-image로 사용.

## pointer-events: none

```csharp
overlay.pickingMode = PickingMode.Ignore;   /* pointer-events: none */
overlay.pickingMode = PickingMode.Position;  /* pointer-events: auto */
```

UXML 속성:
```uxml
<VisualElement picking-mode="Ignore"/>
```

## CSS 변수 (var()) — 지원됨, 색상 포맷 주의

```uss
/* hex 대신 rgb() 사용 */
:root { --primary: rgb(33, 150, 243); }
.btn  { background-color: var(--primary); }
```
