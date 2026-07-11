---
type: concept
topic: unity-ui-toolkit
lang: uss/cs
tags: [unity, ui-toolkit, uss, variables, custom-properties, theming]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-uss-variables.md]
---

# USS Variables (Custom Properties)

> `--variable-name: value` 로 정의하고 `var()` 로 재사용하는 USS 변수 시스템. 테마 색상·간격·폰트 크기를 중앙 관리할 때 사용한다.

## 선언 및 사용

```uss
:root {
    --color-primary: #4A90D9;
    --color-danger: #E74C3C;
    --spacing-base: 8px;
    --font-size-body: 14px;
}

.button--primary {
    background-color: var(--color-primary);
    padding: var(--spacing-base);
    font-size: var(--font-size-body);
}

.button--danger {
    background-color: var(--color-danger);
}
```

## 기본값 (Fallback)

변수를 해석할 수 없을 때 사용할 대체값:

```uss
color: var(--color-primary, #4A90D9);
```

## CSS와의 차이

- `var()` 내 중첩 불가 — `rgb(var(--r), ...)` 형태 지원 안 됨
- 변수 값에 수학 연산 불가 (`calc()` 제한적)
- 변수는 선언한 셀렉터 스코프 안에서만 상속

---

## Unity 내장 USS 변수

Unity는 Editor/Runtime 테마에서 자동 적용되는 `--unity-*` 변수를 제공한다.

### Metrics (`--unity-metrics-*`)

| 변수 | 값 |
|------|-----|
| `--unity-metrics-default-font_normal_size` | 12px (Pro) / 14px (Runtime) |
| `--unity-metrics-single_line-height` | 18px |
| `--unity-metrics-toolbar-height` | 21px |

```uss
.my-control {
    height: var(--unity-metrics-single_line-height);
}
```

### Colors (`--unity-colors-*`)

테마(Professional/Personal/Runtime)에 따라 자동 전환:

```uss
button {
    background-color: var(--unity-colors-button-background);
    color: var(--unity-colors-button-text);
}

button:hover {
    background-color: var(--unity-colors-button-background-hover);
}
```

상태별 변수 패턴: `--unity-colors-{element}-background-{state}`
- state: `hover`, `pressed`, `focused`, `disabled`

### Icons (`--unity-icons-*`)

```uss
.my-dropdown-arrow {
    background-image: url('var(--unity-icons-dropdown)');
}
```

### Font (`--unity-font-*`)

텍스트 렌더링 패딩 조정용: `--unity-font-button-padding-bottom`

---

## 장점

- Professional/Personal/Runtime 환경에서 자동 테마 적용
- 변수 하나 바꾸면 전체 UI에 즉시 전파
- 커스텀 컨트롤에서 내장 변수 활용 시 테마 자동 호환

## 주의사항

- `:root`에 선언해야 전체 트리에서 참조 가능
- 변수 이름은 `--kebab-case` 규칙 권장

## 관련 페이지
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] — 셀렉터, 속성 개요
- [[wiki/unity-ui-toolkit/uss-tss|USS TSS & 스타일 적용]] — 테마 시스템, C# 스타일 적용
- [[wiki/unity-ui-toolkit/uss-naming-conventions|USS 네이밍 컨벤션 (BEM)]] — 클래스 이름 구조

## 출처
- `raw/unity-ui-toolkit-uss-variables.md`
