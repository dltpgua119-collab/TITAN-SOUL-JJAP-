---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/graphic-and-font-assets-preparation.html
created: 2026-04-16
---

# Graphic and Font Assets Preparation (E-Book)

## 개요
UI Toolkit 그래픽 워크플로우: DCC 앱(Photoshop 등)에서 PNG로 내보낸 뒤 Texture Atlas로 묶어 런타임 효율 확보.

## 비트맵 이미지 & 스프라이트

지원 포맷: PNG, BMP, TIF, TGA, JPG, PSD (3D 프로젝트 Texture2D, 2D 프로젝트 Sprite)

### Sprite 설정 핵심
- **Pixels Per Unit (PPU)**: 의도한 해상도에 맞게 설정 (예: 128px 에셋 → PPU 128)
- **Mesh Type**: "Tight" 기본값 — 불투명 픽셀만 감싸 overdraw 감소
- **Sprite Mode**:
  - Single: 단일 이미지
  - Multiple: 텍스처 아틀라스 (Sprite Editor에서 슬라이스)
  - Polygon: 원형/다각형 아웃라인 최적화

## 폰트 에셋

1. **Font (TTF/OTF)**: UI Builder가 자동 FontAsset 변환
2. **FontAsset** (권장): 커닝·베이스라인 미세 조정, 아틀라스/캐릭터셋/해상도 직접 제어 → 메모리 절감

## 텍스처 아틀라싱 시스템

### Sprite Atlas
Unity 기본 아틀라싱 도구. 에셋을 단일 텍스처에 자동 패킹. 플랫폼별 변형 지원. 편집 시 주로 사용.

### Dynamic Atlas
Panel Settings 기준(최소/최대 텍스처 크기, 속성 필터)에 따라 UI Toolkit이 pre-pass 중 자동 아틀라싱.
런타임 + 에디터 모두 작동. 인벤토리 같은 동적 UI에 유용.

## Best Practices

- **해상도**: 4K 이상 최고 해상도로 시작 (다운스케일 가능, 업스케일은 품질 저하)
- **래스터 이미지**: 업스케일 금지, 소스에서 다운스케일
- **PSD 워크플로우**: PSD 직접 import → 저장 시 자동 새로고침
- **Preset**: 유사 에셋에 일관된 import 설정 자동 적용
- **AssetPostProcessor**: 배치 에셋 검사 및 설정 자동화
