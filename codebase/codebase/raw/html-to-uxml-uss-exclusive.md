---
topic: unity-ui-toolkit
original_type: md
created: 2026-04-17
---

# USS 전용 속성 레퍼런스 (-unity-*)

CSS에는 없고 USS에서만 존재하는 속성들이다. Unity UI Toolkit 고유 기능을 스타일로 제어할 때 사용한다.

## 폰트 관련

### `-unity-font`
Unity Font 에셋 참조. CSS `font-family`의 대체재.
```uss
.label { -unity-font: resource("Fonts/Roboto"); }
```

### `-unity-font-definition`
TextCore FontAsset(SDF 렌더링) 참조. `-unity-font` 대신 사용.
```uss
.label { -unity-font-definition: resource("Fonts/Roboto SDF"); }
```

### `-unity-font-style`
폰트 굵기+기울임. CSS `font-weight` + `font-style`의 통합 대체재.

| 값 | 설명 |
|----|------|
| `normal` | 기본 |
| `bold` | 굵게 |
| `italic` | 기울임 |
| `bold-and-italic` | 굵게 + 기울임 |

```uss
.title { -unity-font-style: bold; }
.quote { -unity-font-style: italic; }
```

## 텍스트 정렬

### `-unity-text-align`
수평·수직 정렬 9방향. CSS `text-align`의 대체재 (수직 정렬 포함).

| 값 | 위치 |
|----|------|
| `upper-left` | 좌상단 |
| `upper-center` | 상단 중앙 |
| `upper-right` | 우상단 |
| `middle-left` | 좌중앙 |
| `middle-center` | 정중앙 |
| `middle-right` | 우중앙 |
| `lower-left` | 좌하단 |
| `lower-center` | 하단 중앙 |
| `lower-right` | 우하단 |

```uss
.centered { -unity-text-align: middle-center; }
.left     { -unity-text-align: middle-left; }
```

## 배경 이미지 스케일

### `-unity-background-scale-mode`
배경 이미지 크기 조정 방식. CSS `background-size`의 대체재.

| 값 | CSS 대응 | 설명 |
|----|----------|------|
| `stretch-to-fill` | `100% 100%` | 비율 무시하고 늘림 |
| `scale-and-crop` | `cover` | 비율 유지, 채움 (잘릴 수 있음) |
| `scale-to-fit` | `contain` | 비율 유지, 완전히 들어옴 |

```uss
.banner {
  background-image: url("project://Assets/banner.png");
  -unity-background-scale-mode: scale-and-crop;
}
```

### `-unity-background-image-tint-color`
배경 이미지에 색상 곱셈(tint) 적용. 흰색 아이콘 재사용에 유용.
```uss
.icon {
  background-image: url("project://Assets/icon-white.png");
  -unity-background-image-tint-color: rgb(33, 150, 243);
}
```

## 9-슬라이스 (스프라이트 분할)

### `-unity-slice-left / -right / -top / -bottom`
배경 이미지를 9개 영역으로 분할. 크기가 바뀌어도 모서리가 왜곡되지 않는다.
```uss
.button {
  background-image: url("project://Assets/button-bg.png");
  -unity-slice-left: 12;
  -unity-slice-right: 12;
  -unity-slice-top: 12;
  -unity-slice-bottom: 12;
}
```

### `-unity-slice-scale`
슬라이스 DPI 배율값 (기본 1.0).

## 텍스트 외곽선

### `-unity-text-outline-width` / `-unity-text-outline-color`
텍스트 외곽선. CSS `text-shadow`의 근사 대체재.
```uss
.outlined {
  color: white;
  -unity-text-outline-width: 1.5px;
  -unity-text-outline-color: rgb(0, 0, 0);
}
```

## 텍스트 오버플로우 위치

### `-unity-text-overflow-position`
말줄임표(`...`) 위치 지정.

| 값 | 설명 |
|----|------|
| `end` | 끝 (기본값) |
| `start` | 앞 |
| `middle` | 중간 |

```uss
.filename {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  -unity-text-overflow-position: middle;
}
```

## 오버플로우 클립 기준

### `-unity-overflow-clip-box`
`overflow: hidden` 클리핑 기준 박스.

| 값 | 설명 |
|----|------|
| `padding-box` | 패딩 영역까지 포함 (기본값) |
| `content-box` | 콘텐츠 영역만 |

## 단락 간격

### `-unity-paragraph-spacing`
여러 줄 텍스트에서 단락 간 추가 간격 (px).
```uss
.body-text { -unity-paragraph-spacing: 8px; }
```

## USS 전용 Transform 속성

CSS `transform`을 개별 속성으로 분리. transition 개별 지정이 쉬워진다.

```uss
/* CSS: transform: translate(20px, 10px) rotate(45deg) scale(1.2) */
.element {
  translate: 20px 10px;
  rotate: 45deg;
  scale: 1.2 1.2;
}
```

transition과 결합:
```uss
.card {
  translate: 0 0;
  scale: 1 1;
  transition: translate 0.2s ease, scale 0.2s ease;
}
.card:hover {
  translate: 0 -4px;
  scale: 1.02 1.02;
}
```

## UsageHints (C# 전용)

CSS `will-change`의 대체재. USS 속성이 아니라 C# API로만 설정.
```csharp
element.usageHints = UsageHints.DynamicTransform; /* 자주 이동 */
element.usageHints = UsageHints.DynamicColor;     /* 자주 색상 변경 */
container.usageHints = UsageHints.GroupTransform; /* 자식 전체 레이어 */
```
