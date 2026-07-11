---
type: source
topic: unity-ui-toolkit
lang: uxml/cs
tags: [unity, ui-toolkit, uxml, template, stylesheet]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-uxml-writing.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-WritingUXMLTemplate.html
---

# 소스: UXML 기초 (작성, 스타일 추가, 재사용)

## 핵심 내용
UXML은 XML 기반 UI 구조 선언 언어. 네임스페이스(engine:/editor:/ui:), 공통 속성(name/class/tabindex 등), 외부 USS 파일 참조, Template+Instance 재사용 패턴, AttributeOverrides 커스터마이즈.

## 주요 인사이트
- UXML `class`/`name`/`style` 오버라이드 불가 — 스타일 변경은 USS 셀렉터로
- 인라인 스타일보다 외부 USS 파일 참조 권장
- `xsi:noNamespaceSchemaLocation`으로 IDE 자동완성 활성화
- `VisualTreeAsset.Instantiate()`가 C# 로드의 핵심 패턴

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]

## 원문 링크
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-UXML.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-WritingUXMLTemplate.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-add-style-to-uxml.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-reuse-uxml-files.html
