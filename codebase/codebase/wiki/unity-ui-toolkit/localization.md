---
type: how-to
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, localization, i18n, l10n, string-table, locale]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-localization.md]
---

# UI Toolkit 로컬라이제이션 구현

> Unity Localization 패키지와 UI Toolkit을 연동해 다국어 UI를 구현하는 방법

## 전제 조건
- Unity 6 이상
- Package Manager에서 **Localization** 패키지 설치

## 단계

### 1. Localization 설정
```text
Project Settings > Localization > Create
```

### 2. 로케일 정의
```text
Locale Generator로 언어 추가
```
- 두 글자 코드: `en`, `ko`, `ja`, `fr`, `de` 등
- 기본 로케일 지정 (앱 시작 시 기본 언어)

### 3. 테이블 생성
```text
Window > Asset Management > Localization Tables
```
- **String Tables**: 텍스트 번역
- **Asset Tables**: 지역별 스프라이트·텍스처·폰트

### 4. UI Builder에서 바인딩
Inspector에서 UI 요소 선택 → 바인딩 추가:
- `text` 프로퍼티 → String Table 항목 연결
- `style.backgroundImage` → Asset Table 항목 연결

### 5. 런타임 언어 변경

```csharp
// 언어 변경
Locale locale = LocalizationSettings.AvailableLocales.GetLocale("ko");
LocalizationSettings.SelectedLocale = locale;

// 언어 변경 이벤트 구독
LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

private void OnLocaleChanged(Locale newLocale)
{
    Debug.Log($"Language changed to: {newLocale.LocaleName}");
}
```

### 6. 동적 생성 요소 런타임 바인딩

```csharp
// 코드로 생성한 요소에 로컬라이제이션 바인딩
var label = new Label();
label.SetBinding("text", new LocalizedString { TableReference = "UI", TableEntryReference = "welcome_message" });
root.Add(label);
```

## 고급 기능

### Smart Strings
플레이스홀더, 복수형, 조건부 포맷:
```text
"You have {count} {count:plural:item|items}"
→ "You have 1 item" / "You have 3 items"
```

### StringChanged 이벤트
번역 내용 렌더링 전 수정:
```csharp
localizedString.StringChanged += value => {
    myLabel.text = value.ToUpper(); // 예: 모두 대문자
};
```

### 외부 번역 파일 연동
- **Google Sheets**: 협업 번역 워크플로우
- **CSV**: 비개발자 외부 편집

## 에셋 로컬라이제이션
```csharp
// Asset Table에서 로컬라이즈된 스프라이트 로드
var localizedSprite = new LocalizedSprite { TableReference = "Icons", TableEntryReference = "flag" };
localizedSprite.AssetChanged += sprite => {
    flagImage.style.backgroundImage = new StyleBackground(sprite);
};
```

## 검증 방법
개발 중 Game View의 **Locale** 드롭다운으로 언어 전환 테스트.

## 주의사항
- FlexBox 컨테이너 + 자동 크기 조정으로 텍스트 길이 변화 수용
- 대규모 프로젝트: 여러 String Table로 구성
- Dragon Crashers 샘플 프로젝트 참고

## 관련 페이지
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]]
- [[wiki/unity-ui-toolkit/text-overview|텍스트 시스템]]

## 출처
- [Localization (E-Book)](https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/localization.html)
