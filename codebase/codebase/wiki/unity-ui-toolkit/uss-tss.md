---
type: concept
topic: unity-ui-toolkit
lang: uss/cs
tags: [unity, ui-toolkit, uss, tss, theming, csharp-styling, best-practices]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-uss-tss.md]
---

# USS TSS & 스타일 적용 (C#)

> TSS(Theme Style Sheet)는 여러 USS를 @import로 묶는 테마 컨테이너. C# `element.style` / `element.styleSheets`로 런타임에 스타일을 적용할 수 있다.

---

## Theme Style Sheet (TSS)

"TSS files are regular USS files. UI Toolkit treats them as a distinct asset type for management purposes."

TSS는 USS와 동일한 문법을 쓰지만, 여러 USS를 조합해 테마를 구성하는 데 사용한다.

### TSS 생성

- `Assets > Create > UI Toolkit > Default Runtime Theme File` — Unity 기본 테마 복사본 (권장 시작점)
- `Assets > Create > UI Toolkit > TSS Theme File` — 빈 파일 (기본 테마 import 직접 추가 필요)

> 기본 컨트롤이 올바르게 작동하려면 반드시 기본 런타임 테마를 import 후 오버라이드해야 한다.

### @import 문법

```uss
/* 기본 Unity 테마 */
@import url("unity-theme://default");

/* 절대 경로 */
@import url("/Assets/UI/my-theme.uss");
@import url("project://database/Assets/UI/my-theme.uss");

/* 상대 경로 */
@import url("../shared/base.uss");

/* 패키지 */
@import url("/Packages/com.mycompany.ui/theme.uss");
```

### TSS 적용

**Inspector:** TSS 에셋 선택 → Inherited Themes → 파일 추가

**C#:** Panel Settings에서 런타임 전환

```cs
// 런타임 테마 전환
panelSettings.themeStyleSheet = darkTheme;
```

**우선순위:** 현재 TSS에 정의된 변수/스타일이 import된 것보다 우선.

---

## C#으로 스타일 적용

### 인라인 스타일

```cs
element.style.backgroundColor = Color.red;
element.style.width = Length.Percent(100);
element.style.fontSize = 14;
element.style.display = DisplayStyle.None;
```

### StyleSheet 파일 로드 & 적용

```cs
// 에디터
var sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI/styles.uss");

// 런타임
var sheet = Resources.Load<StyleSheet>("UI/styles");

// 적용 (요소 + 모든 하위에 적용)
element.styleSheets.Add(sheet);
```

### style vs resolvedStyle

| 프로퍼티 | 반환 내용 |
|----------|-----------|
| `element.style` | 인라인 스타일만 (직접 설정한 값, 미설정이면 undefined) |
| `element.resolvedStyle` | 모든 소스 합산 최종 계산값 (레이아웃 포함, 읽기 전용) |

```cs
// 실제 렌더링 크기 확인
float w = element.resolvedStyle.width;
float h = element.resolvedStyle.height;
```

### 스타일 변경 콜백

```cs
// 레이아웃 재계산 완료 후 호출
element.RegisterCallback<GeometryChangedEvent>(evt => {
    float newHeight = element.resolvedStyle.height;
    Debug.Log($"New height: {newHeight}");
});

// 주기적 폴링 (스케줄러)
element.schedule.Execute(() =>
    Debug.Log(element.resolvedStyle.height)
).Every(100);
```

---

## USS 작성 Best Practices

### 성능

| 규칙 | 이유 |
|------|------|
| USS 파일 사용 > 인라인 스타일 | 인라인은 요소당 오버헤드 |
| `:hover` 최소화 | 마우스 이동마다 계층 재스타일링 |
| 유니버설 셀렉터(`*`) 회피 | 모든 요소에 매칭 시도 |

### 셀렉터 설계

```uss
/* ✅ 자식 셀렉터 — 명확한 대상 */
.menu > .menu__item { }

/* ❌ 깊은 하위 + 유니버설 — 성능 저하 */
.container > .panel > * { }

/* ✅ BEM 단일 클래스 — 가장 빠름 */
.menu__item--disabled { opacity: 0.4; }
```

### 커스텀 컨트롤 패턴

```cs
public class MyControl : VisualElement {
    public MyControl() {
        AddToClassList("my-control");          // 기본 클래스
        var icon = new VisualElement();
        icon.AddToClassList("my-control__icon"); // 자식 클래스
        Add(icon);
    }

    public void SetActive(bool active) {
        // 상태 전환
        EnableInClassList("my-control--active", active);
    }
}
```

## 관련 페이지
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] — 셀렉터, 속성 개요
- [[wiki/unity-ui-toolkit/uss-variables|USS Variables]] — 변수 선언 및 내장 변수
- [[wiki/unity-ui-toolkit/uss-naming-conventions|USS 네이밍 컨벤션 (BEM)]] — 클래스 이름 구조
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — 배칭, UsageHints

## 출처
- `raw/unity-ui-toolkit-uss-tss.md`
