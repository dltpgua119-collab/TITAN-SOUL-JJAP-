---
type: source
topic: unity-ui-toolkit
lang: multi
tags: [html, css, uxml, uss, 변환, 제약]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/html-to-uxml-overview.md
---

# HTML/CSS → UXML/USS 변환 레퍼런스 개요

## 핵심 내용

HTML/CSS로 제작한 웹 레이아웃을 Unity UI Toolkit(UXML/USS)으로 변환하기 위한 개요 문서.
변환 흐름 5단계와 USS의 핵심 제약, HTML/CSS 작성 10대 원칙을 정리한다.

## 주요 인사이트

- USS는 Flexbox, display:none 외 레이아웃 방식 전부 미지원
- @keyframes, ::before/::after, @media, z-index, filter, calc() 모두 미지원
- CSS 변수(var())는 USS에서 지원됨 (단, hex 아닌 rgb() 포맷 사용)
- 변환 전 HTML/CSS 단계에서 10대 원칙을 준수하면 변환 비용이 최소화됨

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/html-to-uxml-guide|HTML/CSS → UXML/USS 변환 가이드]]

## 원문 경로
`raw/html-to-uxml-overview.md`
