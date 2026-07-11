---
type: concept
topic: unity-ui-toolkit
lang: uss
tags: [unity, ui-toolkit, uss, selector, property, variable, specificity]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-ebook-styling.md]
---

# USS 기초

> CSS에서 영감받은 Unity StyleSheets — 셀렉터·속성·변수로 UI 요소의 시각적 스타일 정의

## USS란?

"USS files are text files inspired by Cascading Style Sheets (CSS) from HTML."

CSS의 문법을 따르되 Unity에 맞게 오버라이드·커스터마이즈. VisualElement(visual tree의 노드)를 타겟으로 스타일 적용.

## 셀렉터 유형 및 우선순위

우선순위 (높→낮):
1. **인라인 스타일** — 모든 것 오버라이드
2. **ID/Name 셀렉터** (`#title`)
3. **클래스 셀렉터** (`.small-font`)
4. **타입 셀렉터** (`Button`, `Label`)

동일 우선순위 → USS 파일 내 순서 (아래 항목 우선).

> 셀렉터 타입 전체 목록과 Specificity 상세 규칙 → [[wiki/unity-ui-toolkit/uss-selectors|USS 셀렉터 레퍼런스]] 참조

```uss
/* 타입 셀렉터 — 모든 Button */
Button {
    background-color: #4A4A4A;
}

/* 이름 셀렉터 — name="title"인 요소 */
#title {
    font-size: 24px;
    color: white;
}

/* 클래스 셀렉터 — class에 "small-font" 포함 */
.small-font {
    font-size: 12px;
}

/* 직계 자식 */
#title > Label {
    color: grey;
}

/* 임의 깊이 하위 */
#title Label {
    margin-top: 4px;
}

/* 의사 클래스 — hover 상태 */
Button:hover {
    background-color: #666666;
}

Button:active {
    scale: 0.95 0.95;
}
```

## 주요 USS 속성

```uss
.my-element {
    /* 크기 */
    width: 200px;
    height: 50px;
    min-width: 100px;
    max-width: 400px;

    /* 여백 */
    margin: 8px;
    padding: 12px 16px;

    /* 배경 */
    background-color: rgba(0, 0, 0, 0.8);
    border-radius: 8px;
    border-width: 1px;
    border-color: #FFFFFF;

    /* 텍스트 */
    color: white;
    font-size: 14px;
    -unity-font-style: bold;
    -unity-text-align: middle-center;

    /* 레이아웃 */
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
}
```

## USS 변수 (Custom Properties)

```uss
/* 변수 정의 — :root 또는 특정 셀렉터에서 */
:root {
    --color-primary: #4A90D9;
    --color-danger: #E74C3C;
    --spacing-base: 8px;
    --font-size-body: 14px;
}

/* 변수 사용 */
.button--primary {
    background-color: var(--color-primary);
    padding: var(--spacing-base);
    font-size: var(--font-size-body);
}

.button--danger {
    background-color: var(--color-danger);
}
```

**변수 스코프**: 셀렉터 수준 — 다른 셀렉터의 변수 참조 불가.

## 트랜지션 (Transitions)

```uss
/* 호버 시 배경색 트랜지션 */
.my-button {
    background-color: #4A4A4A;
    transition-property: background-color, scale;
    transition-duration: 0.2s, 0.1s;
    transition-timing-function: ease-in-out;
}

.my-button:hover {
    background-color: #666666;
}

.my-button:active {
    scale: 0.95 0.95;
}
```

## Theme Style Sheets (TSS)

다크/라이트 모드, 계절, 캐릭터별 테마 지원:

```text
default.tss ← 기본 테마
    └─ overrides-dark.uss (다크 모드 오버라이드)
    └─ overrides-seasonal.uss (계절 테마 오버라이드)
```

```csharp
// Panel Settings에서 런타임 테마 전환
panelSettings.themeStyleSheet = darkTheme;
```

## 동적 스타일 전환 (C#)

```csharp
// 클래스 추가/제거로 스타일 전환
element.AddToClassList("button--legendary");
element.RemoveFromClassList("button--common");

// 조건부 클래스 토글
element.EnableInClassList("item--selected", isSelected);

// 인라인 스타일 직접 설정
element.style.backgroundColor = Color.red;
element.style.fontSize = 18;
```

## 성능 주의사항

```uss
/* ❌ 성능 저하 — 넓은 셀렉터 */
* { color: white; }
.unity-button { background: grey; }

/* ❌ 성능 저하 — 깊은 자식 셀렉터 */
.container .panel .sub-panel Label { ... }

/* ✅ 권장 — 구체적인 클래스 셀렉터 */
.hud__health-label { color: white; }
```

## 주의사항
- USS는 CSS의 서브셋 — 모든 CSS 속성 지원 안 됨
- `-unity-` 접두사 속성은 Unity 전용 확장
- 변수는 선언한 셀렉터 스코프 내에서만 유효
- 인라인 스타일이 항상 USS 오버라이드

## 관련 페이지
- [[wiki/unity-ui-toolkit/uss-layout-engine|레이아웃 엔진 (Flexbox)]]
- [[wiki/unity-ui-toolkit/uss-naming-conventions|USS 네이밍 컨벤션 (BEM)]]
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]]

## 출처
- [Introduction to USS](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-about-uss.html)
- [Styling (E-Book)](https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/styling.html)
