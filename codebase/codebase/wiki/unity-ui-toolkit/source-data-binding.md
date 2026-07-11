---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, data-binding, mvvm, ebook]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-data-binding.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/data-binding.html
---

# 소스: Data Binding (E-Book Chapter)

## 핵심 내용
MVVM 아키텍처, [CreateProperty]+[GeneratePropertyBag]로 컴파일 타임 바인딩 코드 생성, UXML 선언적 바인딩, TwoWay/ToTarget/ToSource/ToTargetOnce 바인딩 모드, INotifyBindablePropertyChanged/IDataSourceViewHashProvider 변경 추적.

## 주요 인사이트
- Unresolved Binding으로 UXML과 런타임 데이터 소스 분리 가능
- [GeneratePropertyBag]으로 Reflection 제거 → 성능 향상
- INotifyBindablePropertyChanged = 프로퍼티 수준 세밀한 추적
- ListView도 SetBinding으로 itemsSource 바인딩 가능

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/data-binding.html
