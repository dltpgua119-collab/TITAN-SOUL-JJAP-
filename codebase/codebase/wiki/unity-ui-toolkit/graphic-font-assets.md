---
type: concept
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, sprites, textures, fonts, atlas, performance]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-ebook-graphic-font.md]
---

# 그래픽 및 폰트 에셋 준비

> DCC 앱에서 PNG로 내보낸 뒤 Texture Atlas로 묶어 렌더링 배칭을 극대화한다. FontAsset을 사용하면 SDF 렌더링과 아틀라싱을 세밀하게 제어할 수 있다.

## 이미지 포맷 및 Sprite 설정

지원 포맷: PNG (권장), BMP, TIF, TGA, JPG, PSD

### Sprite Inspector 핵심 항목

| 설정 | 역할 |
|------|------|
| **Pixels Per Unit (PPU)** | 에셋 해상도와 일치시켜 정확한 크기 유지 (128px 에셋 → PPU 128) |
| **Mesh Type: Tight** | 불투명 픽셀만 감싸 overdraw 감소 (기본값) |
| **Sprite Mode: Multiple** | 텍스처 아틀라스 — Sprite Editor에서 슬라이스 |
| **Sprite Mode: Polygon** | 원형/다각형 아웃라인 최적화 |

---

## 텍스처 아틀라싱

### Sprite Atlas (Unity 기본 도구)

여러 텍스처를 단일 아틀라스로 자동 패킹. 플랫폼별 변형 지원. 주로 편집 단계에서 사용.

### Dynamic Atlas (UI Toolkit 자동)

Panel Settings에 설정한 기준에 따라 UI Toolkit이 pre-pass 단계에서 자동 아틀라싱:
- 최소/최대 텍스처 크기 설정
- 인벤토리 등 동적 생성 UI에 효과적
- 런타임 + 에디터 모두 작동

> Sprite Atlas와 Dynamic Atlas를 함께 쓰면 배칭 효율을 극대화할 수 있다.

---

## 폰트 에셋

| 타입 | 설명 |
|------|------|
| **Font (TTF/OTF)** | UI Builder가 FontAsset으로 자동 변환 |
| **FontAsset** (권장) | 커닝·베이스라인 조정, 아틀라스/캐릭터셋/해상도 직접 제어 |

FontAsset을 사용하면:
- 필요한 문자만 포함해 메모리 절감 (Font Subsetting)
- SDF(Signed Distance Field) 렌더링으로 스케일에 관계없이 선명한 텍스트

---

## 에셋 임포트 Best Practices

| 규칙 | 이유 |
|------|------|
| 4K 이상 최고 해상도로 시작 | 다운스케일은 가능, 업스케일은 품질 저하 |
| 업스케일 금지 | 픽셀레이션 유발 |
| PSD 직접 import | 저장 시 자동 새로고침 → 반복 디자인 편의 |
| Import Preset 사용 | 유사 에셋에 일관된 설정 자동 적용 |
| AssetPostProcessor 활용 | 배치 에셋 검사 및 설정 자동화 |

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/text-overview|텍스트 시스템]] — TextCore/SDF, 폰트 에셋 상세
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — 배칭, 아틀라스, UsageHints

## 출처
- `raw/unity-ui-toolkit-ebook-graphic-font.md`
