---
type: source
topic: unity-ui-toolkit
lang: cs/uss
tags: [unity, ui-toolkit, performance, batching, ebook]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-ebook-performance.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/optimizing-performance.html
---

# 소스: Optimizing Performance (E-Book Chapter)

## 핵심 내용
4대 업데이트 메커니즘, 배칭 최적화, 8-텍스처 제한, Sprite/Dynamic Atlas, 마스킹 전략, 애니메이션(translate>width), UsageHints, 가시성 제어(display>opacity), Source Generation 바인딩, 프로파일링 도구.

## 주요 인사이트
- display=None이 opacity=0보다 성능 효율적
- [GeneratePropertyBag]+[CreateProperty]로 Reflection 제거
- GroupTransform UsageHint: 부모 1개 설정으로 자식 전체 GPU 전파
- CSS 클래스 전환 중 애니메이션 금지 → 인라인 스타일 업데이트

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/optimizing-performance.html
