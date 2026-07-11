---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitioning-From-UGUI.html
created: 2026-04-16
---

# Migrate from uGUI to UI Toolkit

## 핵심 아키텍처 차이

| 측면 | uGUI | UI Toolkit |
|------|------|-----------|
| UI 계층 | Hierarchy에 개별 GameObject | Virtual visual tree (Hierarchy 미표시) |
| 계층 확인 | Hierarchy 창 | Window > UI Toolkit > Debugger |
| 레이아웃 | 수동 앵커/피벗 | 자동 Flexbox 레이아웃 |
| 이벤트 | Inspector에서 직렬화된 이벤트 | 런타임에 코드로 콜백 등록 |
| 구조 파일 | Prefab (레이아웃+스크립트) | UXML (레이아웃) + C# (로직) 분리 |

## 컴포넌트 대응표

| 목적 | uGUI | UI Toolkit |
|------|------|-----------|
| 루트 컨테이너 | Canvas + Canvas Scaler | UIDocument + PanelSettings |
| 기본 요소 클래스 | UIBehaviour | VisualElement |
| 요소 검색 | `GetComponentInChildren<T>()` | `rootVisualElement.Query<T>()` |
| 요소 참조 | Inspector에서 직접 할당 | 런타임 쿼리로만 해결 |

## 레이아웃 시스템 변화
- uGUI: 수동 앵커·피벗 배치
- UI Toolkit: 웹 기반 자동 레이아웃 (모든 요소에 VerticalLayoutGroup 적용 유사)
- 자동 레이아웃 비활성화: `IStyle.position = Position.Absolute`

## 이벤트 처리 변화
- uGUI: Inspector에서 직렬화된 이벤트 설정 (OnClick 등)
- UI Toolkit: 런타임 스크립팅으로만 콜백 등록

## 혼용 시 제한사항
- "키보드로 UI Toolkit과 uGUI 포커스 요소 간 자유 이동 불가" (수동 스크립트 개입 필요)
- 크로스 시스템 임베딩 및 통합 스타일링 어려움

## 마이그레이션 주의사항
- 여러 UIDocument가 동일한 PanelSettings 참조 가능 (성능 최적화)
- Editor UI: UIDocument 불필요, EditorWindow + CreateGUI() 사용
- 재사용성: Prefab 대신 UXML 템플릿 + 커스텀 컨트롤 패턴
