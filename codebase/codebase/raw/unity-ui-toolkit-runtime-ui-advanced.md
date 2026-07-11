---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-move-elements-at-runtime.html
created: 2026-04-16
---

# Runtime UI 고급 — 요소 이동, Panel Settings, 성능

## 요소 이동 Best Practice

"The recommended best practice to move elements at runtime is to use `style.translate` and set the `DynamicTransform` usage hint on the moving element."

```csharp
// 이동할 요소에 UsageHints 설정
m_NpcNameTag.usageHints = UsageHints.DynamicTransform;

void Update()
{
    SetNameTagPositionAndScale();
}

void SetNameTagPositionAndScale()
{
    var cameraSpaceLocation = GetCameraSpaceLocation(m_UITransform);
    m_NpcNameTag.style.translate = new Translate(cameraSpaceLocation.x, cameraSpaceLocation.y);
    var scale = 1 / distance * m_ScaleMultiplier;
    m_NpcNameTag.style.scale = new Scale(new Vector2(scale, scale));
}
```

`DynamicTransform`은 레이아웃 dirty를 피하고 변환 단계만 업데이트해 성능 최적화.

## 좌표계 변환

- **픽셀 기반**: 직접 좌표값 사용
- **뷰포트 상대**: `screenPoint.x * containerSize.x` 변환

## World Space UI

런타임 패널을 3D 공간에 배치 (빌보드 UI, NPC 이름표 등):
- UIDocument의 Panel Settings에서 Render Mode: World Space Camera
- 또는 RenderTexture로 3D 오브젝트에 UI 텍스처 적용

## Runtime Panel Settings 주요 속성

| 속성 | 설명 |
|------|------|
| Sort Order | 여러 패널 간 렌더링 순서 |
| Scale Mode | Constant Physical Size / Scale With Screen Size / Constant Pixel Size |
| Reference Resolution | Scale With Screen Size 기준 해상도 |
| Target Texture | RenderTexture 타겟 (World Space UI 등) |
| Theme Style Sheet | 패널에 적용할 TSS |
| Dynamic Atlas Settings | 자동 아틀라싱 최소/최대 크기, 필터 |

## 런타임 이벤트 시스템

UI Toolkit은 Input System과 Legacy Input Manager 모두 지원.
`EventSystem` 컴포넌트가 씬에 있어야 포인터/키보드 이벤트 수신.

```csharp
// Input System 패키지 설치 시 자동 연동
// Panel Settings의 Event System Mode 설정 확인
```
