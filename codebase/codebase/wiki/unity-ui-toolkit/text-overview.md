---
type: concept
topic: unity-ui-toolkit
lang: cs/uss
tags: [unity, ui-toolkit, text, font, textcore, sdf, rich-text, localization]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-text.md]
---

# 텍스트 시스템

> TextCore(TextMesh Pro 후속)와 SDF 기술로 고품질 텍스트 렌더링 — 폰트 에셋, 리치 텍스트, 이모지, 로컬라이제이션 지원

## 핵심 기술

UI Toolkit은 **TextCore**(TextMesh Pro 후속)을 사용.  
**SDF (Signed Distance Field)** 기술로 변형·확대 시에도 선명한 렌더링.

## 폰트 에셋

### 폰트 에셋 생성
```text
Create > Text Core > Font Asset > SDF
```

TTF/OTF → Unity 폰트 에셋(글리프·메트릭·렌더링 설정 포함).

### 주요 설정

| 설정 | 설명 |
|------|------|
| **Face Info** | 간격·스케일링 조정 |
| **Generation Settings** | 소스 폰트 선택·렌더링 모드 |
| **Atlas and Material** | 텍스처 생성·크기 제어 |
| **Fallback Fonts** | 누락 글리프 보완 |
| **Ligature Tables** | 합자 문자 쌍 |

**실용 가이드**: ASCII만 사용 → 512×512 아틀라스, 패딩 5로 대부분 충분.

### 폰트 변형 (Variant)
```text
Create > Text Core > Font Asset Variant
```
별도 아틀라스 없이 Face Info만 독립적으로 관리.

## 리치 텍스트 태그

UI Builder Extra Settings에서 활성화:

```xml
<b>굵게</b>                          <!-- 굵게 -->
<i>기울임</i>                        <!-- 기울임꼴 -->
<color=#FF0000>빨간색</color>        <!-- 색상 -->
<size=20>큰 텍스트</size>            <!-- 크기 -->
<sprite index=0>                     <!-- 스프라이트 임베드 -->
<sprite name="star">                 <!-- 이름으로 스프라이트 -->
<gradient="myGradient">텍스트</gradient>  <!-- 그라디언트 -->
```

## 고급 효과

### 그라디언트
```text
Create > Text Core > Gradient Color
```
Resources 폴더에 배치 후 Text Settings에서 참조.

### 이모지 지원

**임베드 방식**: 스프라이트 시트 임포트 → 스프라이트 에셋 생성
```xml
<sprite name="emoji_smile">
```

**OS 이모지 (권장)**: "Dynamic OS" 아틀라스 채우기 모드
- iOS: Apple Emoji
- Android: Noto Color Emoji
- 빌드 크기 감소 (폰트 패키징 불필요)

## Text Style Sheets

텍스트 많은 앱에서 포맷 중앙화:
```text
Assets > Text Core > Text Stylesheet
```
여러 Label에서 동일한 스타일 재사용.

## 로컬라이제이션 연동

```csharp
// Localization 패키지로 런타임 언어 변경
LocalizationSettings.SelectedLocale = 
    LocalizationSettings.AvailableLocales.GetLocale("ko");
```

UI Builder Inspector에서 `text` 프로퍼티를 String Table에 직접 바인딩 가능.

## 성능 최적화

| 최적화 | 방법 |
|--------|------|
| 아틀라스 크기 최소화 | 필요한 글리프만 포함 |
| 이모지 | OS 폰트 사용 (임베드 대신) |
| 아틀라스 모드 | 정적 vs 동적 적절히 선택 |
| 빌드 정리 | "Clean Dynamic Data On Build" 활성화 |

## 관련 페이지
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] (텍스트 스타일 속성)
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]]

## 출처
- [Text (E-Book)](https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/text.html)
- [Work with text overview](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-work-with-text.html)
