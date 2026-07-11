---
topic: unity-ui-toolkit
original_type: md
created: 2026-04-17
---

# 레이아웃 변환 가이드 (CSS → USS Flexbox)

USS는 Flexbox 레이아웃만 지원한다. Grid, Float, Block, Inline, Table 레이아웃은 모두 Flexbox로 재설계해야 한다.

## CSS Grid → Flexbox

### 기본 원칙
- CSS Grid의 2차원 레이아웃(행+열)을 Flexbox의 1차원 레이아웃으로 분해한다.
- 행 컨테이너를 만들고, 각 행 안에 열 컨테이너를 중첩한다.
- `flex-wrap: wrap`으로 자동 줄바꿈 그리드를 구현한다.

### 패턴 1: 고정 열 그리드

```css
/* CSS Grid (변환 전) */
.grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 16px;
}
```

```uss
/* USS Flexbox (변환 후) */
.grid {
  flex-direction: row;
  flex-wrap: wrap;
}
.grid-item {
  width: 32%;
  margin-right: 2%;
  margin-bottom: 16px;
}
```

### 패턴 2: 명시적 행/열 레이아웃 (header + sidebar + content + footer)

```css
/* CSS Grid (변환 전) */
.layout {
  display: grid;
  grid-template-columns: 240px 1fr;
  grid-template-rows: 60px 1fr 40px;
}
```

```uss
/* USS Flexbox 중첩 (변환 후) */
.layout   { flex-direction: column; }
.header   { height: 60px; }
.body     { flex-direction: row; flex-grow: 1; }
.sidebar  { width: 240px; }
.content  { flex-grow: 1; }
.footer   { height: 40px; }
```

### 패턴 3: grid-template-areas

```uss
/* 각 row를 VisualElement로 래핑 */
.layout     { flex-direction: column; }
.row-header { /* header 한 행 */ }
.row-body   { flex-direction: row; flex-grow: 1; }
.row-footer { /* footer 한 행 */ }
```

## Float → Flexbox

Float은 USS에서 완전히 미지원이다. Flexbox row로 변환한다.

### 패턴 1: 이미지 + 텍스트 float

```css
/* Float (변환 전) */
.container { overflow: hidden; }
.image { float: left; width: 200px; margin-right: 16px; }
```

```uss
/* Flexbox (변환 후) */
.container { flex-direction: row; }
.image     { width: 200px; margin-right: 16px; flex-shrink: 0; }
.text      { flex-grow: 1; }
```

### 패턴 2: 양쪽 정렬 (float left + float right)

```uss
.bar  { flex-direction: row; justify-content: space-between; }
```

## Block / Inline → Flexbox

### Block 요소 (세로 쌓기)

```uss
.container { flex-direction: column; } /* USS 기본값이므로 생략 가능 */
```

### Inline 요소 (가로 나열)

```uss
.inline-group {
  flex-direction: row;
  flex-wrap: wrap;
}
```

## Position 변환

### fixed → absolute (루트 기준)

```uss
/* fixed 대체: 루트 VisualElement의 직계 자식으로 배치 */
.modal-overlay {
  position: absolute;
  top: 0; left: 0;
  width: 100%; height: 100%;
}
```

### sticky → 미지원

C#에서 ScrollView 이벤트를 감지해 직접 위치를 계산해야 한다.

## z-index → 트리 순서

USS는 z-index를 지원하지 않는다. 나중에 선언된 요소가 위에 렌더링된다.

```xml
<!-- 트리 순서로 렌더링 레이어 제어 -->
<VisualElement class="background"/>   <!-- 맨 아래 -->
<VisualElement class="content"/>      <!-- 중간 -->
<VisualElement class="overlay"/>      <!-- 맨 위 -->
```

동적 z-order 변경:
```csharp
element.BringToFront();
element.SendToBack();
element.PlaceInFront(referenceElement);
element.PlaceBehind(referenceElement);
```

## Flexbox 동작 차이 (CSS vs USS)

| 항목 | CSS Flexbox | USS Flexbox |
|------|------------|------------|
| 기본 `flex-direction` | row | **column** |
| `gap` 속성 | 지원 | **미지원** (margin으로 대체) |
| `%` 단위 | 부모 기준 | 동일 |
| `auto` margin | 남은 공간 흡수 | 동일 |

USS에서 가로 나열 시 반드시 `flex-direction: row`를 명시해야 한다. CSS와 달리 기본값이 column이다.

## gap 대체 패턴

```uss
/* gap: 16px 대체 */
.child {
  margin-right: 16px;
  margin-bottom: 16px;
}
/* :last-child 미지원이므로 모든 자식에 동일 margin 적용 후
   부모에 음수 margin 또는 overflow: hidden으로 보정 */
```

## 반응형 레이아웃 (@media 대체)

USS는 @media를 지원하지 않는다. C#으로 처리한다.

```csharp
root.RegisterCallback<GeometryChangedEvent>(evt =>
{
    float width = evt.newRect.width;
    if (width < 768f)
    {
        root.AddToClassList("layout-mobile");
        root.RemoveFromClassList("layout-desktop");
    }
    else
    {
        root.AddToClassList("layout-desktop");
        root.RemoveFromClassList("layout-mobile");
    }
});
```

```uss
.layout-mobile .content  { flex-direction: column; }
.layout-desktop .content { flex-direction: row; }
```
