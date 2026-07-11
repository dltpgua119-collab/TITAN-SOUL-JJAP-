---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/text.html
created: 2026-04-16
---

# Text Rendering in UI Toolkit (E-Book Chapter)

## 핵심 기술
UI Toolkit은 **TextCore** (TextMesh Pro 후속)을 사용. SDF(Signed Distance Field) 기술로 변형/확대 시에도 선명한 렌더링.

## 폰트 에셋 준비
- TTF/OTF → Unity 폰트 에셋으로 변환 필요
- **생성**: Create > Text Core > Font Asset > SDF
- Face Info (간격/스케일링), Generation Settings, Atlas/Material, Fallback Fonts 설정

### 실용 가이드라인
- ASCII만 사용: 512×512 아틀라스, 패딩 5가 대부분 폰트에 충분
- 폰트 변형: Create > Text Core > Font Asset Variant (별도 아틀라스 없이 Face Info 독립)

## 리치 텍스트
UI Builder Extra Settings에서 활성화. 런타임 텍스트 포맷팅:
- `<b>굵게</b>`, `<i>기울임</i>`, `<color=red>색상</color>`
- `<size=20>크기</size>`, `<sprite index=0>` 스프라이트 임베드

## 고급 효과
- **그라디언트**: Create > Text Core > Gradient Color → `<gradient="assetName">text</gradient>`
- **스프라이트/이모지**: 스프라이트 시트 임포트 → `<sprite name="identifier">`
- **OS 이모지**: "Dynamic OS" 아틀라스 — iOS(Apple Emoji), Android(Noto Color Emoji), 빌드 크기 감소

## Text Style Sheets
텍스트 많은 앱: Assets > Text Core > Text Stylesheet로 재사용 스타일 중앙화.

## 성능 고려사항
- 아틀라스 해상도 최소화
- 이모지는 OS 폰트 사용 (임베드 대신)
- 아틀라스 채우기 모드 고려 (정적 vs 동적)
- 동적 폰트 사용 시 "Clean Dynamic Data On Build" 활성화
