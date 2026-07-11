---
type: source
topic: unity-ui-toolkit
lang: uss
tags: [css, uss, 지원여부, 속성, 변환]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/html-to-uxml-css-property-support.md
---

# CSS 속성 USS 지원 여부 레퍼런스 소스

## 핵심 내용

CSS 전체 속성을 카테고리별(박스 모델·레이아웃·색상·타이포그래피·Transform·가시성·셀렉터·함수)로 분류하고 USS 지원 여부를 ✅/⚠️/❌로 표기.

## 주요 인사이트

- 박스 모델(width, margin, padding, border-radius): 대부분 ✅
- 레이아웃: display:flex/none만 ✅, grid·float·inline 전부 ❌
- 배경: background-color/image ✅, gradient·filter·backdrop-filter ❌
- 타이포그래피: font-size/letter-spacing ✅, font-family·font-weight는 -unity-* 대체
- Transform: translate/rotate/scale ✅ (USS 개별 속성), @keyframes ❌
- 셀렉터: :hover/:focus/:active ✅, ::before/::after/:nth-child() ❌
- CSS 함수: var() ✅, calc()/clamp()/hsl() ❌

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/css-to-uss-support|CSS 속성 USS 지원 여부]]

## 원문 경로
`raw/html-to-uxml-css-property-support.md`
