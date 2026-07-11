---
type: source
topic: unity-ui-toolkit
lang: uxml/cs
tags: [html, uxml, 요소, 대응표, 변환]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/html-to-uxml-element-mapping.md
---

# HTML 요소 → UXML 요소 대응표 소스

## 핵심 내용

HTML 모든 카테고리(구조·텍스트·폼·미디어·인터랙티브)의 요소를 UXML 컨트롤과 1:1 매핑.
지원하지 않는 HTML 요소 목록과 UXML 전용 요소(HTML 대응 없음)도 포함.

## 주요 인사이트

- `<div>`, `<section>`, `<header>` 등 구조 요소는 모두 `<VisualElement>`로 대응
- `<h1>`~`<h6>`, `<p>`, `<span>`은 `<Label>`로 대응 (시맨틱 없음)
- `<select>` → `<DropdownField>`, `<input type="range">` → `<Slider>` 등 폼 요소는 직접 대응 컨트롤 존재
- `<iframe>`, `<script>`, `<map>`, `<embed>` 등은 대응 요소 없음
- `<ListView>`, `<TreeView>`, `<Foldout>`, `<MinMaxSlider>` 등은 UXML 전용

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/html-to-uxml-elements|HTML → UXML 요소 대응표]]

## 원문 경로
`raw/html-to-uxml-element-mapping.md`
