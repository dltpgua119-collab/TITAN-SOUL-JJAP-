---
type: source
topic: unity-ui-toolkit
lang: uss/cs
tags: [css, uss, 레이아웃, flexbox, grid, 변환]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/html-to-uxml-layout-conversion.md
---

# 레이아웃 변환 가이드 소스

## 핵심 내용

CSS Grid·Float·Block·Inline 레이아웃을 USS Flexbox로 변환하는 구체적 패턴.
position:fixed/sticky 대체, z-index 대체, gap 대체, @media 대체 패턴 포함.

## 주요 인사이트

- CSS Grid → Flexbox: 행 컨테이너 중첩 + flex-wrap:wrap으로 2차원 구현
- Float → `flex-direction: row`로 직접 대체 가능
- USS 기본 flex-direction이 column임에 주의 (CSS는 row)
- gap 미지원 → 자식 요소 margin으로 대체
- z-index → BringToFront()/SendToBack() C# API
- position:fixed → 루트의 직계 자식으로 position:absolute
- @media → GeometryChangedEvent + 클래스 전환

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/html-to-uxml-layout|레이아웃 변환 가이드 (CSS → USS Flexbox)]]

## 원문 경로
`raw/html-to-uxml-layout-conversion.md`
