---
type: concept
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, shader-graph, urp, render-type-branch]
created: 2026-05-03
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-shader-graph-intro.md]
---

# UI Shader Graph

> URP 전용. UI 요소 메시 자체에 적용되는 커스텀 셰이더를 Shader Graph로 제작하는 기능 (Unity 6.3+).

## 설명

UI Shader Graph는 VisualElement 메시 렌더링 단계에서 동작하는 셰이더를 시각적으로 만든다. uGUI의 `Image.material`처럼 머티리얼을 꽂는 형태가 아니라, UI Toolkit이 **하나의 머티리얼로 텍스트·솔리드·텍스처·그라디언트를 한꺼번에 그리는 구조**에 맞춰 분기(branch)를 짜는 방식이다.

**용도**:
- 버튼 글로우·블러·인터랙티브 컬러 변화
- 텍스트 외곽선·고품질 스케일링
- 그라디언트·텍스처 애니메이션
- 그레이스케일·세피아·컬러 보정

**한계**: UI 메시 *렌더링 셰이더* 만 만든다. 렌더 타깃 후처리(post-process)나 USS `filter()` 같은 별도 필터는 [[wiki/unity-ui-toolkit/howto-uss-custom-filter|FilterFunctionDefinition]] 경로를 써야 한다.

**전제 조건**: **URP(Universal Render Pipeline) 프로젝트만 지원**. Built-in/HDRP 미지원.

## 핵심 노드: Render Type Branch

UI Toolkit의 단일 머티리얼은 한 드로우 콜 안에 5종 렌더 타입의 트라이앵글을 섞어 보낸다. `Render Type Branch` 노드는 각 입력 포트에 타입별 처리를 연결해, 한 셰이더 그래프 안에서 분기 처리를 가능케 한다.

| 포트(Render Type) | 적용 대상 |
|---|---|
| Solid | 솔리드 색상 배경·테두리 |
| Texture | 텍스처 그래픽 (Image element 등) |
| SDF Text | SDF 폰트 텍스트 |
| Bitmap Text | Bitmap 폰트 텍스트 |
| Gradient | 벡터 그래픽 그라디언트 |

**자동 셋업**: `Create > Shader Graph > From Template > UI` 템플릿이 Render Type Branch를 미리 추가한다.

**별도 노드 — Render Type**: 현재 처리 중인 렌더 타입 enum을 출력. `Branch` 외에도 자체적으로 조건 분기를 짤 때 사용.

### 미연결 입력 = 기본값 사용 (성능 팁)

Render Type Branch의 입력을 **연결하지 않으면** 해당 타입은 기본 노드(Default Solid/Texture/Gradient/Bitmap Text/SDF Text)와 동등한 동작을 자동으로 사용한다. 기본 동작이 필요한 포트에 굳이 Default 노드를 연결하지 말 것 — 분기 효율이 떨어진다.

## UI 전용 입력 노드

| 노드 | 출력 |
|---|---|
| `Element Texture UV` | 텍스처 샘플링용 UV |
| `Element Layout UV` | 요소의 layout 사각형 내 정규화 UV |
| `Element Texture Size` | 현재 할당된 텍스처의 픽셀 크기 |
| `Sample Element Texture` | 지정 UV에서 요소 텍스처 샘플 (font texture, background image, Image source, 커스텀 렌더 텍스처 자동 선택) |

**중요**: `Sample Element Texture`가 가져오는 텍스처는 **요소가 그 시점에 그리고 있는 텍스처** 다 — 텍스트라면 폰트 atlas, Image라면 source. 별도 texture 슬롯이 아니다.

## Render Type 별 커스터마이즈 패턴

```text
[Default Texture] → [Multiply Color] → [Render Type Branch.Texture]
                                       ↑
[Default Solid]  → [UV Distortion]  → [Render Type Branch.Solid]
```

기본 노드 출력을 받아 색·UV·이펙트 체이닝 후 Branch에 다시 꽂는다.

## 적용 흐름 (요약)

1. `Create > Shader Graph > URP > UI Shader Graph` → 그래프 작성
2. Material 생성 후 위 셰이더 할당
3. UI Builder에서 VisualElement Inspector의 **Material** 드롭다운으로 머티리얼 지정
4. 셰이더는 **선택한 요소와 모든 자식**에 적용된다 (계승됨)

## 주의사항

- **URP 필수** — Built-in/HDRP 프로젝트에선 메뉴에 항목이 안 뜸
- 머티리얼은 자식 요소까지 전파되므로, 일부만 적용하려면 부모/자식 구조 분리 필요
- 한 셰이더가 여러 렌더 타입을 다루므로, 텍스트 가독성 등 타입별 시각적 영향을 항상 확인할 것
- 효과는 mesh-level이므로 USS `filter()` 같은 픽셀 후처리(블러 전체 영역에 번짐 등)는 표현 불가 → [[wiki/unity-ui-toolkit/howto-uss-custom-filter|커스텀 USS 필터]] 사용

## 관련 페이지

- [[wiki/unity-ui-toolkit/howto-ui-shader-graph-gradient|UI Shader Graph 그라디언트 만들기 (How-to)]]
- [[wiki/unity-ui-toolkit/howto-uss-custom-filter|커스텀 USS 필터 만들기 (FilterFunctionDefinition)]]
- [[wiki/unity-ui-toolkit/ui-renderer|UI Renderer (Painter2D / Mesh API)]] — 셰이더 없이 코드로 2D 비주얼 그리기
- [[wiki/unity-ui-toolkit/ui-systems-comparison|UI 시스템 비교]] — 셰이더·머티리얼 활용도 비교
- [[wiki/unity-ui-toolkit/uss-workarounds|미지원 CSS 패턴 USS 우회]] — 6.3 이전엔 filter 우회로 셰이더 권장

## 출처

- [[wiki/unity-ui-toolkit/source-shader-graph-intro|Introduction to UI Shader Graph (Unity 6.3 Manual)]]
