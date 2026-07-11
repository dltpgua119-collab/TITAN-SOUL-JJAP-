---
topic: unity-ui-toolkit
original_type: md
created: 2026-04-17
---

# CSS 속성 USS 지원 여부 레퍼런스

범례: ✅ 지원 | ⚠️ 부분 지원 (동작 차이 있음) | ❌ 미지원

## 박스 모델

| CSS 속성 | USS | 비고 |
|----------|-----|------|
| `width` | ✅ | px, %, auto 지원 |
| `height` | ✅ | px, %, auto 지원 |
| `min-width` | ✅ | |
| `max-width` | ✅ | |
| `min-height` | ✅ | |
| `max-height` | ✅ | |
| `margin` | ✅ | 단축 속성 및 방향별 모두 지원 |
| `margin-top/right/bottom/left` | ✅ | |
| `padding` | ✅ | 단축 속성 및 방향별 모두 지원 |
| `padding-top/right/bottom/left` | ✅ | |
| `border-width` | ✅ | 방향별 지정 가능 |
| `border-color` | ✅ | 방향별 지정 가능 |
| `border-radius` | ✅ | `border-top-left-radius` 등 방향별 지원 |
| `border-style` | ❌ | 항상 solid. dashed/dotted 미지원 |
| `box-sizing` | ❌ | 항상 border-box처럼 동작 |
| `outline` | ❌ | border로 대체 |
| `box-shadow` | ❌ | 미지원 |

## 레이아웃

| CSS 속성 | USS | 비고 |
|----------|-----|------|
| `display: flex` | ✅ | 기본 레이아웃 방식 |
| `display: none` | ✅ | 요소 숨김 |
| `display: grid` | ❌ | 미지원. Flexbox로 대체 필요 |
| `display: block` | ❌ | 미지원 |
| `display: inline` | ❌ | 미지원 |
| `display: inline-flex` | ❌ | 미지원 |
| `display: inline-block` | ❌ | 미지원 |
| `flex-direction` | ✅ | row/row-reverse/column/column-reverse |
| `flex-wrap` | ✅ | nowrap/wrap/wrap-reverse |
| `flex-grow` | ✅ | |
| `flex-shrink` | ✅ | |
| `flex-basis` | ✅ | |
| `flex` (단축) | ✅ | |
| `align-items` | ✅ | |
| `align-self` | ✅ | |
| `align-content` | ✅ | |
| `justify-content` | ✅ | |
| `justify-items` | ❌ | 미지원 |
| `justify-self` | ❌ | 미지원 |
| `gap` | ❌ | 미지원. margin으로 대체 |
| `row-gap` / `column-gap` | ❌ | 미지원 |
| `grid-*` 전체 | ❌ | CSS Grid 전체 미지원 |
| `float` | ❌ | 미지원. Flexbox로 대체 |
| `clear` | ❌ | 미지원 |
| `position: relative` | ✅ | |
| `position: absolute` | ✅ | 부모 VisualElement 기준 |
| `position: fixed` | ❌ | 미지원. 루트에 absolute로 대체 |
| `position: sticky` | ❌ | 미지원 |
| `position: static` | ❌ | 미지원 (기본값은 relative) |
| `top/right/bottom/left` | ✅ | position: absolute일 때 동작 |
| `z-index` | ❌ | 미지원. 트리 순서로 제어 |
| `overflow: hidden` | ✅ | |
| `overflow: visible` | ✅ | |
| `overflow: scroll` | ✅ | ScrollView 사용 권장 |
| `overflow: auto` | ❌ | 미지원 |
| `overflow-x` / `overflow-y` | ❌ | 미지원 |
| `columns` / `column-*` | ❌ | CSS 다단 레이아웃 미지원 |

## 색상 / 배경

| CSS 속성 | USS | 비고 |
|----------|-----|------|
| `color` | ✅ | 텍스트 색상 |
| `background-color` | ✅ | |
| `background-image` | ✅ | USS에서 에셋 참조: `url("path")` |
| `background-size` | ⚠️ | `-unity-background-scale-mode`로 대체 |
| `background-position` | ❌ | 미지원 |
| `background-repeat` | ❌ | 미지원 |
| `background-attachment` | ❌ | 미지원 |
| `background` (단축) | ❌ | 각 속성을 개별 지정 |
| `background: linear-gradient()` | ❌ | 미지원. 텍스처 또는 커스텀 셰이더로 대체 |
| `background: radial-gradient()` | ❌ | 미지원 |
| `opacity` | ✅ | 0.0 ~ 1.0 |
| `filter` | ❌ | blur, brightness 등 전체 미지원 |
| `backdrop-filter` | ❌ | 미지원 |
| `mix-blend-mode` | ❌ | 미지원 |

## 타이포그래피

| CSS 속성 | USS | 비고 |
|----------|-----|------|
| `font-size` | ✅ | px만 지원 (em/rem 미지원) |
| `font-family` | ⚠️ | `-unity-font` 또는 `-unity-font-definition`으로 대체 |
| `font-weight` | ⚠️ | `-unity-font-style: bold` (숫자값 미지원) |
| `font-style` | ⚠️ | `-unity-font-style: italic` |
| `font` (단축) | ❌ | 개별 속성 사용 |
| `color` | ✅ | |
| `letter-spacing` | ✅ | px 단위 |
| `word-spacing` | ✅ | px 단위 |
| `line-height` | ❌ | 미지원 |
| `text-align` | ⚠️ | `-unity-text-align`으로 대체 (9방향) |
| `text-decoration` | ❌ | underline/strikethrough 미지원 |
| `text-transform` | ❌ | uppercase/lowercase 미지원 |
| `text-overflow` | ✅ | ellipsis 지원 |
| `text-shadow` | ❌ | 미지원 |
| `white-space` | ✅ | normal/nowrap |
| `word-break` | ❌ | 미지원 |
| `overflow-wrap` | ❌ | 미지원 |

## Transform / 애니메이션

| CSS 속성 | USS | 비고 |
|----------|-----|------|
| `transform: translate()` | ✅ | USS에서 `translate` 속성으로 분리 |
| `transform: rotate()` | ✅ | USS에서 `rotate` 속성으로 분리 |
| `transform: scale()` | ✅ | USS에서 `scale` 속성으로 분리 |
| `transform: skew()` | ❌ | 미지원 |
| `transform: matrix()` | ❌ | 미지원 |
| `transition` | ✅ | property/duration/timing-function/delay |
| `transition-timing-function` | ✅ | ease/linear/ease-in/ease-out/ease-in-out/cubic-bezier |
| `animation` | ❌ | 미지원. C# 코루틴 또는 실험적 API 필요 |
| `@keyframes` | ❌ | 미지원 |
| `will-change` | ❌ | 미지원 (UsageHints C# API로 대체) |

## 가시성 / 상호작용

| CSS 속성 | USS | 비고 |
|----------|-----|------|
| `visibility: visible` | ✅ | |
| `visibility: hidden` | ✅ | 공간 유지하며 숨김 |
| `display: none` | ✅ | 공간도 제거 |
| `cursor` | ✅ | 일부 커서 타입 지원 |
| `pointer-events: none` | ⚠️ | C#에서 `pickingMode = PickingMode.Ignore`로 설정 |
| `pointer-events: auto` | ⚠️ | `pickingMode = PickingMode.Position` |
| `user-select` | ❌ | 미지원 |
| `resize` | ❌ | 미지원 |

## 셀렉터 / 가상 클래스

| CSS 셀렉터 | USS | 비고 |
|-----------|-----|------|
| `.class` | ✅ | |
| `#id` | ✅ | USS에서 `name` 속성에 해당 |
| `element` 태그 셀렉터 | ✅ | |
| `.parent > .child` | ✅ | 직계 자손 |
| `.ancestor .descendant` | ✅ | 하위 요소 |
| `.a.b` 다중 클래스 | ✅ | |
| `:hover` | ✅ | |
| `:active` | ✅ | |
| `:focus` | ✅ | |
| `:disabled` | ✅ | |
| `:checked` | ✅ | |
| `:enabled` | ✅ | |
| `:root` | ✅ | |
| `::before` | ❌ | 미지원 |
| `::after` | ❌ | 미지원 |
| `::placeholder` | ❌ | 미지원 |
| `:nth-child()` | ❌ | 미지원 |
| `:first-child` | ❌ | 미지원 |
| `:last-child` | ❌ | 미지원 |
| `:not()` | ❌ | 미지원 |
| `[attr]` 속성 셀렉터 | ❌ | 미지원 |
| `*` 전체 셀렉터 | ✅ | |

## CSS 함수 / At-규칙

| CSS | USS | 비고 |
|-----|-----|------|
| `var(--name)` | ✅ | USS 변수 지원 |
| `calc()` | ❌ | 미지원. 고정값 또는 flex 비율로 대체 |
| `clamp()` | ❌ | 미지원 |
| `min()` / `max()` | ❌ | 미지원 |
| `rgb()` / `rgba()` | ✅ | |
| `hsl()` / `hsla()` | ❌ | 미지원. rgb로 변환 필요 |
| `url()` | ✅ | 에셋 경로 참조 |
| `@media` | ❌ | 미지원 |
| `@keyframes` | ❌ | 미지원 |
| `@import` | ✅ | TSS 테마 파일에서 지원 |
| `@font-face` | ❌ | 미지원. Unity 폰트 에셋 사용 |
