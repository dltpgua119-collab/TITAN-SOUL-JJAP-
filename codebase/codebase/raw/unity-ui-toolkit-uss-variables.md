---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-USS-CustomProperties.html
created: 2026-04-16
---

# USS Variables (Custom Properties)

## 개요
"USS variables, also called custom properties, define values that you can reuse in other USS rules. You can create variables for any type of USS property."

## 선언 문법
변수 이름에 `--` 접두사:
```uss
--color-1: red;
```

## var() 사용
```uss
color: var(--color-1);
```

## 기본값 (Fallback)
```uss
color: var(--color-1, #FF0000);
```
"The UI system uses the default value when it can't resolve the variable."

## 실용 예시
```uss
:root {
  --color-1: blue;
  --color-2: yellow;
}

.paragraph-regular {
  color: var(--color-1);
  background: var(--color-2);
  padding: 2px;
}

.paragraph-reverse {
  color: var(--color-2);
  background: var(--color-1);
  padding: 2px;
}
```

## CSS와의 차이
- `var()` 함수 내 중첩 불가 (예: `rgb()` 내부 사용 불가)
- 변수 값에 수학 연산 불가

---

# Unity 내장 USS 변수

## 카테고리

### Metrics (`--unity-metrics-*`)
- `--unity-metrics-default-font_normal_size` — 12px (Professional) / 14px (Runtime)
- `--unity-metrics-single_line-height` — 18px
- `--unity-metrics-toolbar-height` — 21px

### Colors (`--unity-colors-*`)
테마별(Professional/Personal/Runtime) 색상 팔레트:
- 컨트롤 배경: `--unity-colors-button-background`, `--unity-colors-button-background-hover`
- 텍스트 색: `--unity-colors-button-text`
- 상태별 색: hover, pressed, focused, disabled

```uss
button {
    background-color: var(--unity-colors-button-background);
    color: var(--unity-colors-button-text);
}
button:hover {
    background-color: var(--unity-colors-button-background-hover);
}
```

### Font (`--unity-font-*`)
텍스트 렌더링용 패딩: `--unity-font-button-padding-bottom`, `--unity-font-standard-padding-bottom`

### Icons (`--unity-icons-*`)
내장 아이콘 에셋 참조:
```uss
background-image: url('var(--unity-icons-dropdown)');
```

## 장점
- Professional/Personal/Runtime 환경에서 자동 테마 적용
- 전역 변수 업데이트 → 전체 UI에 즉시 반영
