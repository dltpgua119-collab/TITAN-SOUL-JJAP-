---
topic: unity-ui-toolkit
original_type: md
created: 2026-04-17
---

# HTML/CSS → UXML/USS 변환 레퍼런스 개요

## 목적

이 문서 세트는 HTML/CSS로 제작한 웹 레이아웃을 Unity UI Toolkit(UXML/USS)으로 변환하기 위한 참조 자료다.
웹사이트 프로토타입을 HTML/CSS로 먼저 설계할 때 USS 호환 범위를 사전에 인지하고, 변환 불가 패턴을 피하거나 우회 방법을 적용하는 것이 목표다.

## 변환 흐름

```
HTML/CSS 프로토타입
        │
        ▼
[1] 요소 대응 확인 (01-element-mapping)
    HTML 태그 → UXML 컨트롤 매핑
        │
        ▼
[2] 레이아웃 검토 (03-layout-conversion)
    Grid/Float 사용 여부 확인 → Flexbox로 재설계
        │
        ▼
[3] CSS 속성 필터링 (02-css-property-support)
    미지원 속성 제거 또는 표시
        │
        ▼
[4] 미지원 패턴 처리 (04-unsupported-patterns)
    box-shadow, @keyframes 등 우회 방법 적용
        │
        ▼
[5] USS 전용 속성 추가 (05-uss-exclusive)
    -unity-* 속성으로 Unity 고유 스타일 지정
        │
        ▼
UXML/USS 완성
```

## 핵심 제약 요약

| 항목 | 웹(CSS) | USS(Unity) |
|------|---------|-----------|
| 레이아웃 | Flexbox, Grid, Block, Float | **Flexbox만** |
| position | static, relative, absolute, fixed, sticky | **relative, absolute만** |
| display | flex, grid, block, inline, none 등 | **flex, none만** |
| 애니메이션 | @keyframes + animation | **transition만** (keyframe은 C# 필요) |
| 가상 요소 | ::before, ::after | **미지원** (C#으로 자식 요소 추가) |
| 미디어 쿼리 | @media | **미지원** (C#으로 처리) |
| z-index | 지원 | **미지원** (트리 순서로 제어) |
| 필터 | filter: blur, brightness 등 | **미지원** |
| 스크립트 | JavaScript | **C# (RegisterCallback)** |

## HTML/CSS 작성 시 준수 사항 (변환 최소화 원칙)

1. **레이아웃은 Flexbox만 사용** — `display: grid`, `float`, `display: inline-block` 금지
2. **position은 relative/absolute만** — `fixed`, `sticky` 금지
3. **z-index 대신 DOM 순서로 레이어 제어** — 나중에 오는 요소가 위에 렌더링
4. **@keyframes 대신 transition 활용** — 복잡한 애니메이션은 별도 표시
5. **::before / ::after 미사용** — 필요한 경우 실제 자식 요소로 대체
6. **box-shadow, text-shadow, filter 미사용** — 대안 설계 필요
7. **calc(), clamp() 미사용** — 고정값 또는 flex 비율로 대체
8. **@media 미사용** — 단일 해상도 기준으로 설계
9. **CSS 변수(var())는 사용 가능** — USS도 CSS 변수 지원
10. **이미지는 background-image 또는 img 태그** — USS의 `-unity-background-scale-mode`로 대응 가능

## 버전 정보

- Unity 버전 기준: Unity 6 (UI Toolkit 정식 지원)
- CSS 기준: CSS3 표준
