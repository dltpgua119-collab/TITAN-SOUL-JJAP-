---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, uxml, load, instantiate]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-uxml-loading.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manage-asset-reference.html
---

# 소스: UXML/USS 로드 및 인스턴스화

## 핵심 내용
UXML(VisualTreeAsset), USS(StyleSheet)를 C#에서 로드하는 4가지 방법: SerializeField(권장), AssetDatabase(에디터), Addressables(런타임), Resources(비권장). Instantiate() vs CloneTree() 인스턴스화 패턴.

## 주요 인사이트
- SerializeField가 가장 단순하고 권장됨
- Resources.Load는 빌드 크기 증가로 프로덕션 비권장
- Instantiate()는 TemplateContainer 반환, CloneTree()는 parent에 직접 통합
- 인스턴스화 직후 UQuery로 요소 접근 가능

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/snippet-load-uxml|UXML/USS 로드 및 인스턴스화 패턴]]

## 원문 링크
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-reference-other-files-from-uxml.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-manage-asset-reference.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-LoadingUXMLcsharp.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Controls.html
