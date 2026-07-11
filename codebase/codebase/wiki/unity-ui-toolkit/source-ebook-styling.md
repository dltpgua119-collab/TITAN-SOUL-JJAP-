---
type: source
topic: unity-ui-toolkit
lang: uss/cs
tags: [unity, ui-toolkit, uss, styling, selector, tss]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/unity-ui-toolkit-ebook-styling.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/styling.html
---

# 소스: UI Toolkit Styling Best Practices (E-Book 챕터)

## 핵심 내용
USS 셀렉터 우선순위(인라인 > ID > 클래스 > 타입), 셀렉터 유형 6가지, USS 변수 모범 사례, 트랜지션 Inspector 설정, 동적 클래스 전환, TSS 멀티테마 워크플로우.

## 주요 인사이트
- 셀렉터 우선순위: 인라인 스타일 > `#id` > `.class` > C# 타입 순; 동일 우선순위는 USS 파일 내 순서로 결정
- 광범위한 셀렉터(`*`, `.unity-button` 등) 남용 시 성능 저하 경고
- USS 변수 스코프는 셀렉터 레벨 — 다른 셀렉터 변수 참조 불가
- TSS로 멀티 테마(라이트/다크, 캐릭터별) 구현; 부모 테마에서 상속 후 변경 부분만 오버라이드

## 이 소스로 생성·갱신된 페이지
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]]

## 원문 링크
- https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/styling.html
