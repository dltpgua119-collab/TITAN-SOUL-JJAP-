---
type: concept
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, ugui, imgui, comparison]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-compare.md]
---

# UI 시스템 비교: UI Toolkit vs uGUI vs IMGUI

> Unity의 세 가지 UI 시스템을 목적·역할·기능별로 비교

## 일반 권장 사항 (Unity 6.3)

| 컨텍스트 | 권장 | 대안 |
|---------|------|------|
| **Runtime** | uGUI (Unity UI) | UI Toolkit |
| **Editor** | UI Toolkit | IMGUI |

> UI Toolkit은 최종적으로 권장 UI 시스템이 될 예정이지만, 현재는 uGUI의 일부 기능이 부족.

## 역할별 적합성

| 역할 | UI Toolkit | uGUI | IMGUI |
|------|-----------|------|-------|
| 프로그래머 | ✅ | ✅ | ✅ |
| 테크니컬 아티스트 | 부분 | ✅ | ❌ |
| UI 디자이너 | ✅ | 부분 | ❌ |

UI 디자이너는 UI Toolkit의 문서 기반 접근법과 UI Builder를 통한 시각 편집 혜택이 큼.

## Runtime 사용 사례

### UI Toolkit 권장
- 다해상도 메뉴/HUD가 많은 집중적 UI 프로젝트
- World space UI 및 VR 앱
- 커스텀 셰이더·머티리얼
- 고급 유연한 레이아웃 필요 시

### uGUI 권장
- MonoBehaviour 참조가 쉬운 구조 필요
- 키프레임 애니메이션 활용
- 씬 내 직접 배치(In-scene authoring) 워크플로

## 주요 Runtime 기능 비교

| 기능 | UI Toolkit | uGUI |
|------|-----------|------|
| WYSIWYG 편집 | ✅ | ✅ |
| 씬 내 직접 배치 | ❌ | ✅ |
| 직렬화된 이벤트 | ❌ | ✅ |
| 데이터 바인딩 | ✅ | ❌ |
| 텍스처 없는 요소 | ✅ | ❌ |
| SVG 지원 | ✅ | ❌ |
| 오른쪽→왼쪽 언어 | ✅ | ❌ |

## Editor 개발

### UI Toolkit 강점
- 복잡한 에디터 도구 (재사용성·분리 향상)
- Property drawer
- 디자이너와의 협업

### IMGUI 적합
- 경량 API로 빠른 UI 렌더링
- 에디터 확장성 무제한 접근

## 개발 상태
- **UI Toolkit**: 활발히 개발 중, 새 기능 자주 추가
- **uGUI / IMGUI**: 안정적·검증된 시스템, 업데이트 드묾

## 관련 페이지
- [[wiki/unity-ui-toolkit/introduction|UI Toolkit 소개]]
- [[wiki/unity-ui-toolkit/migration-overview|마이그레이션 가이드]]

## 출처
- [Comparison of UI systems in Unity](https://docs.unity3d.com/6000.3/Documentation/Manual/UI-system-compare.html)
