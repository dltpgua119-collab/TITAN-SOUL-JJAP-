---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/layouts.html
created: 2026-04-16
---

# UI Toolkit Layouts (E-Book Chapter)

## 핵심 개념
UI Toolkit은 **Yoga** (HTML/CSS Flexbox 구현 서브셋)로 요소 위치/크기를 자동 계산.
"Flexbox (or Flexible Box Layout) is a method for arranging items in rows or columns."

## Flexbox 장점
1. **반응형 UI** — 중첩 컨테이너가 해상도 변화에 자동 적응
2. **조직화된 복잡성** — 재사용 스타일로 수백 요소에 일관 적용
3. **분리된 설계** — UI 레이아웃이 코드 로직과 독립

## 주요 레이아웃 속성

### Direction & Wrap
- **flex-direction**: Row (가로) / Column (세로)
- **flex-wrap**: No Wrap / Wrap (여러 행/열에 분배)

### Sizing
- **width/height**: 요소 크기 (px, %, auto)
- **min/max width/height**: 확장·축소 제한

### Flex Properties
- **flex-basis**: grow/shrink 전 기본 크기
- **flex-grow**: 여백 분배 비율 (0=확장 안 함, 1=전부)
- **flex-shrink**: 컨테이너 부족 시 축소 비율 (0=유지, 1=축소)

### Alignment
- **align-items** (교차축): Start / Center / End / Stretch / Auto
- **justify-content** (주축): 자식 간격 분배
- **align-self**: 개별 요소 정렬 오버라이드

## 위치 모드

### Relative (기본값)
- 부모 Flexbox 규칙 따름
- 부모 크기/스타일 변화에 반응
- 영구적·복잡한 UI 구조에 적합

### Absolute
- 부모 컨테이너 기준 앵커
- flex 설정(Grow, Shrink, Margin) 무시
- Left/Top/Right/Bottom으로 앵커
- 팝업, 장식 요소, 동적 캐릭터 인디케이터에 적합

## 여백
- **Margin**: 테두리 외부 간격 (CSS 박스 모델)
- **Padding**: 테두리 내부 콘텐츠 간격

## 실용 워크플로우
1. 중첩 박스(컨테이너+자식)로 UI 목업
2. 부모 Direction(Row/Column) 설정
3. Flex Grow/Shrink로 반응성 구성
4. Justify Content/Align Items로 간격 조정
5. 플랫폼 확장성을 위해 % 단위 사용
6. 타겟 해상도에서 Game view 테스트

## Panel Settings Scale Modes
- **Constant Pixel Size**: 고정 픽셀 (선택적 스케일 팩터)
- **Constant Physical Size**: 기기 전반 물리적 크기 유지
- **Scale with Screen Size**: 기준 해상도로 동적 스케일링

## UXML as Prefab
UXML은 재사용 가능한 프리팹 역할 — 컴포넌트 기반 UI 아키텍처 지원.
