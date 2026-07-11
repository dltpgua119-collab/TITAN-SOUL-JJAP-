---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-tss.html
created: 2026-04-16
---

# Theme Style Sheet (TSS) & USS Best Practices

## TSS 개요

"Theme Style Sheet (TSS) files are regular USS files. UI Toolkit treats TSS as a distinct asset type and uses it for management purposes."

TSS는 여러 USS를 @import로 조합해 테마를 만드는 컨테이너.

## TSS 생성 방법
1. `Assets > Create > UI Toolkit > TSS Theme File` — 빈 파일 (기본 테마 import 필요)
2. `Assets > Create > UI Toolkit > Default Runtime Theme File` — 기본 테마 복사본

"To get all the default UI controls to work, you must import the default runtime theme, and then overwrite or add new styles to create your custom theme."

## TSS @import 문법

```uss
/* 기본 테마 */
@import url("unity-theme://default");

/* 절대 경로 */
@import url("/Assets/myFolder/myFile.uss");
@import url("project://database/Assets/myFolder/myFile.uss");

/* 상대 경로 */
@import url("../myFolder/myFile.uss");

/* 패키지 */
@import url("/Packages/com.unity.package.name/file-name.uss");
```

## TSS 적용

Inspector에서 TSS 에셋 선택 → Inherited Themes → 추가 또는:

```csharp
// Panel Settings에서 런타임 테마 전환
panelSettings.themeStyleSheet = darkTheme;
```

주의: "If you set a TSS for a Panel Setting asset, it doesn't make the TSS a global style for the whole project."

## 우선순위 규칙

현재 TSS 변수/스타일이 import된 스타일시트보다 우선.

---

# Apply Styles with C#

## 인라인 스타일 직접 설정
```csharp
button.style.backgroundColor = Color.red;
element.style.width = 200;
element.style.fontSize = 14;
```

## StyleSheet 파일 적용
```csharp
// 로드
var sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI/styles.uss");
// 또는
var sheet = Resources.Load<StyleSheet>("UI/styles");

// 적용 (하위 모든 요소에 적용됨)
element.styleSheets.Add(sheet);
```

"Style rules apply to the visual element and all its descendants, but don't apply to the parent or siblings of the element."

## style vs resolvedStyle

| 프로퍼티 | 설명 |
|----------|------|
| `element.style` | 인라인 스타일만 포함 (C# 또는 UXML에서 직접 설정한 값) |
| `element.resolvedStyle` | 모든 소스 합산 최종 계산값 (레이아웃 포함) |

```csharp
// 최종 계산된 높이 읽기
float h = element.resolvedStyle.height;
```

## 스타일 변경 감지
```csharp
// 레이아웃 변경 시 콜백
element.RegisterCallback<GeometryChangedEvent>(evt => {
    float newHeight = element.resolvedStyle.height;
});

// 주기적 확인 (스케줄러)
element.schedule.Execute(() =>
    Debug.Log(element.resolvedStyle.height)
).Every(100);
```

---

# USS Best Practices (성능 및 구조)

## 성능

- **인라인 스타일 최소화**: USS 파일 사용이 메모리 효율적
- **셀렉터 복잡도**: 성능 비용 = `N1(클래스 수) × N2(USS 파일 수)`
- **:hover 주의**: 마우스 이동마다 계층 재스타일링 유발

## 셀렉터 설계

```uss
/* ✅ 자식 셀렉터 (성능 좋음) */
.menu > .menu__item { }

/* ❌ 유니버설 + 복잡 계층 (성능 나쁨) */
.container > .panel > * { }

/* ✅ BEM 단일 클래스 (권장) */
.menu__item--disabled { }
```

## 커스텀 컨트롤 가이드라인
- 생성자에서 `AddToClassList()`로 USS 클래스 부착
- 자식 요소에 설명적인 클래스 할당
- 상태 변화 시 `AddToClassList()`/`RemoveFromClassList()` 동적 전환
- 커스텀 클래스는 충돌 방지를 위해 접두사 사용
