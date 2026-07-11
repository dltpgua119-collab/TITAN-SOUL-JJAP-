---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, uquery, query]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-uquery.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-UQuery.html
---

# 소스: Find Visual Elements with UQuery

## 핵심 내용
jQuery/LINQ 기반 visual tree 검색 시스템. Q<T>() (첫 번째), Query<T>() (컬렉션), 이름·클래스·타입·조건 체이닝, ForEach로 중간 리스트 없는 처리, QueryState로 메모리 최적화.

## 주요 인사이트
- 초기화 시 캐시, 매번 쿼리 금지
- ForEach가 ToList()보다 메모리 효율적
- 조상 방향 탐색은 UQuery 미지원 → .parent 체인 수동 탐색
- 클로저에서 this 전체 캡처 금지

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uquery|UQuery]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-UQuery.html
