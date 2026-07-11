---
type: how-to
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, shader-graph, urp, ui-builder, tutorial]
created: 2026-05-03
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-shader-graph-tutorial.md]
---

# UI Shader Graph로 그라디언트 버튼 만들기

> URP 프로젝트에서 UI Shader Graph를 생성하고, Material을 만들어 UI Builder의 Button에 적용하는 입문 절차.

## 전제 조건

- **URP 템플릿 프로젝트** (UI Shader Graph는 URP 전용)
- Shader Graph, Material, UI Builder, Button 요소 기본 이해
- Unity 6.3 LTS 이상

## 단계

### 1. UI Shader Graph 자산 생성

`Assets/` 우클릭 → **Create > Shader Graph > URP > UI Shader Graph** → 이름 `MyCustomShader` → 더블클릭으로 Shader Graph 에디터 열기.

### 2. 노드 배치

| 추가 경로 | 노드 |
|---|---|
| `Create Node > UI > Render Type Branch` | Render Type Branch |
| `Create Node > UV > UV Distortion` | UV Distortion |

### 3. 와이어 연결

```text
UV Distortion.UV  ──→  Render Type Branch.Solid
Render Type Branch.Color  ──→  Fragment.Base Color
Render Type Branch.Alpha  ──→  Fragment.Alpha
```

Main Preview 창에 SDF/Bitmap 텍스트 배경 위 결과가 실시간 표시된다.

### 4. 셰이더 저장

상단 **Save Asset** 클릭.

### 5. Material 생성 및 셰이더 할당

1. `Assets/` 우클릭 → **Create > Material**
2. 이름 `MyCustomMaterial`
3. Inspector의 **Shader** 드롭다운에서 `MyCustomShader` 선택

### 6. UI Builder에서 적용

1. **Window > UI Toolkit > UI Builder** 열기
2. Library 패널에서 **Button** 을 Hierarchy로 드래그
3. Button 선택 상태에서 Inspector의 **Material** 드롭다운 → `MyCustomMaterial` 선택

Viewport에 커스텀 셰이더가 적용된 Button이 표시된다.

## 검증 방법

- Main Preview에서 SDF/Bitmap/텍스처/솔리드 4가지 미리보기가 모두 의도대로 보이는지 확인
- UI Builder Viewport에서 Button과 그 안 텍스트가 모두 셰이더의 영향을 받는지 (텍스트는 SDF Text 분기로 처리됨)
- 자식 요소가 의도치 않게 영향받는다면 머티리얼 적용 위치를 자식 노드로 옮기기

## 주의사항

- 머티리얼은 **선택한 요소와 모든 자식**에 전파됨 — 부모에 셰이더 적용 시 자식 텍스트도 영향받음
- URP 미적용 프로젝트에선 메뉴에 **URP > UI Shader Graph** 항목 자체가 보이지 않음
- 텍스트 가독성을 해칠 수 있는 효과(과한 UV 왜곡 등)는 SDF Text 입력을 별도로 처리해 보호

## 관련 페이지

- [[wiki/unity-ui-toolkit/concept-ui-shader-graph|UI Shader Graph 개요]] — Render Type Branch·기본 노드 설명
- [[wiki/unity-ui-toolkit/howto-uss-custom-filter|커스텀 USS 필터 (Swirl)]] — 픽셀 후처리 필터 작성법
- [[wiki/unity-ui-toolkit/howto-ui-builder|UI Builder 워크플로우]]

## 출처

- [[wiki/unity-ui-toolkit/source-shader-graph-tutorial|Get started with UI Shader Graph (Unity 6.3 Manual)]]
