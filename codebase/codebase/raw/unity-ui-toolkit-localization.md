---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/localization.html
created: 2026-04-16
---

# Localization in UI Toolkit (E-Book Chapter)

## 핵심 설정
Unity 6: Localization 패키지가 UI Toolkit에 직접 통합.

## 설정 단계
1. **설치**: Package Manager에서 Localization 패키지 추가
2. **로케일 정의**: Locale Generator로 언어 추가 (en, fr, es 등), 기본 로케일 지정
3. **테이블 생성**: String Tables(텍스트), Asset Tables(스프라이트/텍스처)
4. **UXML 설계**: UI Builder에서 UI 구성
5. **바인딩**: Inspector에서 text/backgroundImage 등을 String/Asset Table에 연결

## 핵심 API
```csharp
// 로케일 변경
Locale locale = LocalizationSettings.AvailableLocales.GetLocale("en");
LocalizationSettings.SelectedLocale = locale;

// 로케일 변경 이벤트 구독
LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

// 동적 생성된 요소에 런타임 바인딩
element.SetBinding("text", localizationBinding);
```

## 고급 기능
- **Smart Strings**: 플레이스홀더, 복수형, 조건부 포맷
- **StringChanged 이벤트**: 렌더링 전 번역 내용 수정 (드롭다운, 포맷 텍스트)
- **데이터 가져오기/내보내기**: Google Sheets, CSV로 비개발자 협업

## 에셋 로컬라이제이션
텍스처, 스프라이트, 폰트, 프리팹을 Asset Tables로 로컬라이즈.
`style.backgroundImage` 등 요소 프로퍼티에 바인딩.

## 모범 사례
- FlexBox 컨테이너 + 자동 크기 조정으로 언어별 텍스트 길이 변화 수용
- 대규모 프로젝트: 여러 String Table로 콘텐츠 구성
- 개발 중: Game View Locale 드롭다운으로 테스트
- 참고: Dragon Crashers 샘플 프로젝트
