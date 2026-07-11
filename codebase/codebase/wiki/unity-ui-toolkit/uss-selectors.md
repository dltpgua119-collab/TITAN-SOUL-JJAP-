---
type: concept
topic: unity-ui-toolkit
lang: uss
tags: [unity, ui-toolkit, uss, selectors, pseudo-class, specificity]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-uss-selectors.md]
---

# USS 셀렉터 레퍼런스

> USS 셀렉터로 visual tree의 요소를 타겟팅해 스타일을 적용한다. CSS와 거의 동일한 문법을 사용한다.

## 셀렉터 타입 요약

### Type 셀렉터 — 요소 타입 매칭

```uss
Button { background-color: grey; }
Label { color: white; }
```

### Name 셀렉터 — `#name` 매칭

```uss
#submit-button { background-color: green; }
```

C#에서 name 설정: `element.name = "submit-button";`

### Class 셀렉터 — `.class` 매칭

```uss
.primary-button { background-color: blue; }
.disabled { opacity: 0.5; }
```

C#에서 클래스 추가: `element.AddToClassList("primary-button");`

### Universal 셀렉터 — `*` 전체 매칭

```uss
* { margin: 0; padding: 0; }
```

**성능 주의**: 복잡한 계층과 결합하면 모든 요소 매칭 시도 → 느림.

### Descendant 셀렉터 — 임의 깊이 하위

```uss
.panel Label { color: white; }
#container Button { border-radius: 4px; }
```

### Child 셀렉터 — 직계 자식 (`>`)

```uss
.menu > .menu__item { padding: 4px; }
```

Descendant보다 빠름 — 직계 자식만 확인.

### Multiple 셀렉터 — 복합 조건

```uss
/* Button이면서 .primary 클래스이면서 :hover 상태 */
Button.primary:hover { background-color: darkblue; }
```

### Selectors List — 쉼표로 공유

```uss
Button, Toggle, Slider {
    border-radius: 4px;
}
```

---

## Pseudo-classes (의사 클래스)

| 의사 클래스 | 적용 상태 |
|-------------|-----------|
| `:hover` | 포인터가 올려진 상태 |
| `:active` | 클릭/누른 상태 |
| `:focus` | 포커스 상태 |
| `:checked` | 선택된 상태 (Toggle, RadioButton) |
| `:enabled` | 활성화 상태 |
| `:disabled` | 비활성화 상태 (`SetEnabled(false)`) |
| `:root` | visual tree 루트 요소 |
| `:first-child` | 첫 번째 자식 |
| `:last-child` | 마지막 자식 |

```uss
Button:hover { background-color: #666; }
Toggle:checked { border-color: green; }
.item:disabled { opacity: 0.4; }

/* 커스텀 pseudo-state (C# AddToClassList로 설정) */
.card--selected { border-color: yellow; }
```

---

## 우선순위 (Specificity)

높음 → 낮음:

| 우선순위 | 셀렉터 |
|----------|--------|
| 1 (최고) | 인라인 스타일 (`element.style.*`) |
| 2 | `#name` 셀렉터 |
| 3 | `.class` 셀렉터, 의사 클래스 |
| 4 | Type 셀렉터 (`Button`, `Label`) |
| 5 (최저) | Universal 셀렉터 (`*`) |

동일 우선순위: USS 파일 내 **나중에 정의된 규칙** 우선.
USS 파일 순서: 나중에 추가된 파일이 우선.

---

## 성능 가이드라인

```uss
/* ✅ 가장 빠름 — 단일 클래스 */
.menu__item--disabled { opacity: 0.4; }

/* ✅ 빠름 — 자식 셀렉터 */
.menu > .menu__item { }

/* ⚠️ 느림 — 깊은 하위 셀렉터 */
.container .panel .sub-panel Label { }

/* ❌ 가장 느림 — 유니버설 + 복잡 계층 */
.container > * > * { }
```

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] — 셀렉터 사용법 개요
- [[wiki/unity-ui-toolkit/uss-naming-conventions|USS 네이밍 컨벤션 (BEM)]] — BEM으로 단일 클래스 설계
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — 배칭과 셀렉터 비용

## 출처
- `raw/unity-ui-toolkit-uss-selectors.md`
