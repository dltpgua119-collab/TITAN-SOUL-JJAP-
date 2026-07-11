---
type: source
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, editor-window, inspector, runtime-ui, uidocument]
created: 2026-04-16
updated: 2026-04-16
source_path: raw/unity-ui-toolkit-editor-runtime-ui.md
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-HowTo-CreateEditorWindow.html
---

# 소스: Editor UI 및 Runtime UI 구현

## 핵심 내용
EditorWindow+CreateGUI()로 에디터 창, [CustomEditor]+CreateInspectorGUI()로 커스텀 Inspector, UIDocument+MonoBehaviour+OnEnable()으로 런타임 UI 설정.

## 주요 인사이트
- [SerializeField] 멤버 변수로 핫 리로드 시 상태 보존
- OnEnable()에서 UXML 로드 완료 → 이때 Q<T>()로 요소 접근
- OnDisable()에서 콜백 해제 필수 (메모리 누수 방지)
- 여러 UIDocument가 동일 PanelSettings 공유 가능 (성능 최적화)

## 이 소스로 생성된 페이지
- [[wiki/unity-ui-toolkit/editor-ui|Editor UI 구현]]
- [[wiki/unity-ui-toolkit/runtime-ui|Runtime UI 구현]]

## 원문 링크
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-HowTo-CreateEditorWindow.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-HowTo-CreateCustomInspector.html
- https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-get-started-with-runtime-ui.html
