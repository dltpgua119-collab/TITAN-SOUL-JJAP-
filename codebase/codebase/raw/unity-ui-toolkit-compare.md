---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UI-system-compare.html
created: 2026-04-16
---

# Comparison of UI systems in Unity

## Overview
UI Toolkit is intended to become the recommended UI system for developing UI in projects. However, it currently lacks certain features supported by uGUI and IMGUI.

## General Recommendations (Unity 6.3)

| Context | Recommendation | Alternative |
|---------|----------------|-------------|
| Runtime | uGUI (Unity UI) | UI Toolkit |
| Editor | UI Toolkit | IMGUI |

## Role-Based Suitability

| Role | UI Toolkit | uGUI | IMGUI |
|------|-----------|------|-------|
| Programmer | ✅ | ✅ | ✅ |
| Technical Artist | Partial | ✅ | ❌ |
| UI Designer | ✅ | Partial | ❌ |

UI designers benefit from UI Toolkit's document-based approach and can use the UI Builder to visually edit their UI.

## Runtime Use Cases

**UI Toolkit 권장:**
- 다해상도 메뉴/HUD (집중적 UI 프로젝트)
- World space UI 및 VR 앱
- 커스텀 셰이더 및 머티리얼
- 고급 유연한 레이아웃

**uGUI 권장:**
- MonoBehaviour 참조 쉬운 구조
- 키프레임 애니메이션
- 씬 내 직접 배치 워크플로

## Key Runtime Feature Comparison

| Feature | UI Toolkit | uGUI |
|---------|-----------|------|
| WYSIWYG authoring | ✅ | ✅ |
| In-scene authoring | ❌ | ✅ |
| Serialized events | ❌ | ✅ |
| Data binding system | ✅ | ❌ |
| Textureless elements | ✅ | ❌ |
| SVG support | ✅ | ❌ |
| Right-to-left language | ✅ | ❌ |

## Editor Development

**UI Toolkit 강점:**
- 복잡한 에디터 도구 (재사용성·분리 향상)
- Property drawer
- 디자이너 협업

**IMGUI 적합:**
- 경량 API로 빠른 UI 렌더링
- 에디터 확장성 무제한 접근

## 개발 상태
UI Toolkit은 활발히 개발 중, 새 기능 자주 추가. uGUI/IMGUI는 안정적이나 업데이트 드묾.
