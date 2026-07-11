---
type: source
topic: unity-ui-toolkit
lang: uss/cs
tags: [uss, -unity-, 전용속성, 폰트, 배경, transform]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/html-to-uxml-uss-exclusive.md
---

# USS 전용 속성 레퍼런스 소스

## 핵심 내용

CSS에 없는 USS 전용 `-unity-*` 속성 전체 레퍼런스.
폰트·텍스트 정렬·배경 스케일·9-슬라이스·텍스트 외곽선·오버플로우 위치·단락 간격·Transform 분리 속성 포함.

## 주요 인사이트

- `-unity-font`: Font 에셋 참조 (CSS font-family 대체)
- `-unity-font-definition`: TextCore FontAsset 참조 (SDF 렌더링)
- `-unity-font-style`: bold/italic/bold-and-italic (숫자 weight 미지원)
- `-unity-text-align`: 9방향 정렬 (수직 포함, CSS text-align 대체)
- `-unity-background-scale-mode`: stretch-to-fill/scale-and-crop/scale-to-fit
- `-unity-background-image-tint-color`: 이미지 색상 곱셈 tint
- `-unity-slice-*`: 9-슬라이스 경계 px
- `-unity-text-outline-*`: 텍스트 외곽선 (text-shadow 근사 대체)
- `-unity-text-overflow-position`: 말줄임표 위치 (end/start/middle)
- `translate`, `rotate`, `scale`: CSS transform을 USS에서 분리한 개별 속성
- `UsageHints`: C# 전용 성능 힌트 (will-change 대체)

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uss-exclusive-properties|USS 전용 속성 (-unity-*)]]

## 원문 경로
`raw/html-to-uxml-uss-exclusive.md`
