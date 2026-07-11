---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, migration, ugui]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-migration-ugui.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitioning-From-UGUI.html
---

# 소스: uGUI → UI Toolkit 마이그레이션

## 핵심 내용
Canvas→UIDocument+PanelSettings, UIBehaviour→VisualElement, GetComponentInChildren→Q<T>(), 직렬화 이벤트→코드 콜백, Prefab→UXML+커스텀 컨트롤, 수동 앵커→Flexbox.

## 주요 인사이트
- UI Toolkit 계층은 Hierarchy에 표시 안 됨 → Window > UI Toolkit > Debugger로 확인
- 혼용 시 키보드 네비게이션 제한
- 요소 참조: Inspector 직접 할당 불가 → OnEnable()에서 Q<T>() 사용

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/migration-overview|마이그레이션 가이드]]

## 원문 링크
https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitioning-From-UGUI.html
