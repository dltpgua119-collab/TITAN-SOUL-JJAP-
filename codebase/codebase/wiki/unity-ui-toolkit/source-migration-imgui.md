---
type: source
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, migration, imgui]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-migration-imgui.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-IMGUI-migration.html
---

# 소스: IMGUI → UI Toolkit 마이그레이션

## 핵심 내용
즉시 모드(OnGUI) vs 보유 모드(VisualElement) 아키텍처 차이. 함수 대응표(OnGUI→CreateGUI 등). IMGUIContainer로 기존 IMGUI 임베딩. 점진적 마이그레이션 전략.

## 주요 인사이트
- IMGUIContainer 안에서는 VisualElement 중첩 불가
- CreateGUI()는 OnGUI()와 달리 한 번만 호출됨
- BeginDisabledGroup() → SetEnabled(false)로 전환

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/migration-imgui|IMGUI → UI Toolkit 마이그레이션]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-IMGUI-migration.html
