---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, tutorial, editor-window]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-get-started.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-simple-ui-toolkit-workflow.html
---

# 소스: Get Started with UI Toolkit

## 핵심 내용
`SimpleCustomEditor` 커스텀 Editor 창을 UI Builder·UXML·C# 세 방법으로 만드는 튜토리얼. 이벤트 핸들러(ClickEvent) 연결 방법까지 포함.

## 주요 인사이트
- 세 방법 비교: UI Builder(시각), UXML(선언적), C#(프로그래매틱)
- `root.Query<Button>().ForEach(RegisterHandler)` 패턴으로 동적 핸들러 등록
- `VisualTreeAsset.Instantiate()` — UXML을 C#에서 불러오는 핵심 패턴

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-simple-ui-toolkit-workflow.html
