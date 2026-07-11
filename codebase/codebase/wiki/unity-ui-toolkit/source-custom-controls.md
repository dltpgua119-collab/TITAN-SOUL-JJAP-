---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, custom-control, ebook]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-custom-controls.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/custom-controls.html
---

# 소스: Custom Controls (E-Book Chapter)

## 핵심 내용
[UxmlElement]/[UxmlAttribute] 어트리뷰트로 커스텀 컨트롤 등록/노출. 생성자 초기화 패턴. 적절한 기반 클래스 상속. SetValueWithoutNotify()로 무한 루프 방지.

## 주요 인사이트
- public partial 클래스 선언 필수
- 생성자에서 시각 구조 초기화 (Awake 없음)
- BaseField<T> 상속 시 값 변경 이벤트 자동 지원
- USS 트랜지션 + EnableInClassList로 상태 시각화

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/custom-controls.html
