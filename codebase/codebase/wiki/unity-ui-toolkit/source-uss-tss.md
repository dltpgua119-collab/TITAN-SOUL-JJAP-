---
type: source
topic: unity-ui-toolkit
lang: uss/cs
tags: [unity, ui-toolkit, uss, tss, theming]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-uss-tss.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-tss.html
---

# 소스: TSS, C# 스타일 적용, USS Best Practices

## 핵심 내용
TSS(@import로 USS 조합하는 테마 컨테이너), C# `element.style` / `element.styleSheets` 런타임 스타일 적용, `resolvedStyle` vs `style` 구분, USS 성능 모범 사례.

## 주요 인사이트
- `panelSettings.themeStyleSheet`로 런타임 테마 전환 가능
- `resolvedStyle`은 읽기 전용 최종 계산값
- `:hover` 남용 시 마우스 이동마다 전체 계층 재스타일링
- BEM 단일 클래스 셀렉터가 가장 빠른 CSS 매칭

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uss-tss|USS TSS & 스타일 적용 (C#)]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-tss.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-apply-styles-with-csharp.html
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-USS-WritingStyleSheets.html
