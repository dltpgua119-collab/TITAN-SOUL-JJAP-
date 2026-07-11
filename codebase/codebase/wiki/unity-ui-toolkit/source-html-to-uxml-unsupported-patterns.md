---
type: source
topic: unity-ui-toolkit
lang: uss/cs
tags: [css, uss, 미지원, 우회, 변환]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/html-to-uxml-unsupported-patterns.md
---

# 미지원 CSS 패턴 및 우회 방법 소스

## 핵심 내용

USS 미지원 CSS 패턴 12가지(box-shadow, text-shadow, @keyframes, ::before/::after, filter, @media, calc(), z-index, text-decoration, hsl(), border-style, pointer-events)별 우회 구현 방법.

## 주요 인사이트

- box-shadow → VisualElement 중첩 또는 border로 단순화
- @keyframes → USS transition(단순) 또는 C# 코루틴(복잡) 또는 실험적 Animation API
- ::before/::after → C#에서 자식 VisualElement 삽입 (Insert(0, el))
- filter → 미리 처리된 텍스처 에셋 또는 커스텀 셰이더
- calc() → flex-grow로 남은 공간 활용 또는 C# GeometryChangedEvent 계산
- pointer-events:none → `pickingMode = PickingMode.Ignore`
- text-decoration → Rich Text Tags(`<u>`, `<s>`) 또는 border 구분선

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uss-workarounds|미지원 CSS 패턴 USS 우회 방법]]

## 원문 경로
`raw/html-to-uxml-unsupported-patterns.md`
