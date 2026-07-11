---
type: source
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, uss, filter, filter-function-definition]
created: 2026-05-03
updated: 2026-05-03
source_path: raw/unity-ui-toolkit-uss-custom-filter.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/create-custom-swirl-filter.html
---

# 소스: Create a custom swirl filter

## 핵심 내용

`FilterFunctionDefinition` 자산이 USS `filter()` 함수의 인자(Index 0, 1, …)를 셰이더 프로퍼티(`_Angle`, `_Radius`)에 매핑한다. Filter Name(`swirl`), 파라미터 정의(float Angle/Radius), Passes(`Swirl.shader` 머티리얼), Parameter Bindings를 Inspector에서 설정 → UI Builder의 Inline Styles > Filter > Custom 으로 자산을 지정해 적용. Extract 기능으로 USS 클래스로 추출 가능.

## 주요 인사이트

- 픽셀 단위 후처리 필터의 공식 진입점 — 6.3 이전엔 워크어라운드만 있었음
- USS `filter:` 함수의 첫 인자는 자산 경로 문자열, 이후는 Parameter Bindings의 Index 순서
- Filter Name(`swirl`)과 USS 표기가 분리됨 — USS는 자산 경로로 참조
- 필터는 적용된 요소의 자식까지 영향

## 이 소스로 생성된 페이지

- [[wiki/unity-ui-toolkit/howto-uss-custom-filter|커스텀 USS 필터 만들기 (Swirl 예제)]]

## 원문 링크

- 공식 문서: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/create-custom-swirl-filter.html
- 예제 자산: https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-custom-swirl-filter
