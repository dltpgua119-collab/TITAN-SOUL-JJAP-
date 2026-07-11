---
type: concept
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, runtime, panel-settings, world-space, translate]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-runtime-ui-advanced.md]
---

# Runtime UI 고급 — 이동, Panel Settings, World Space

> 런타임에 UI 요소를 이동시킬 때는 `style.translate` + `UsageHints.DynamicTransform`을 사용한다. 레이아웃 재계산 없이 변환 단계만 업데이트해 성능을 최적화한다.

## 요소 이동 — 권장 패턴

```cs
// 초기화 시 한 번 설정
m_NpcNameTag.usageHints = UsageHints.DynamicTransform;

// Update()에서 매 프레임 위치/스케일 적용
void Update()
{
    var pos = GetCameraSpaceLocation(m_UITransform);
    m_NpcNameTag.style.translate = new Translate(pos.x, pos.y);

    float scale = 1f / distance * m_ScaleMultiplier;
    m_NpcNameTag.style.scale = new Scale(new Vector2(scale, scale));
}
```

**왜 `style.translate`인가?**
- `style.left`/`style.top` → 레이아웃 재계산 발생 → 비쌈
- `style.translate` → 변환(transform) 단계만 업데이트 → 저렴함
- `DynamicTransform` hint → GPU 레이어 최적화

---

## 좌표계 변환 (월드 → UI)

```cs
Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);
float uiX = screenPoint.x / Screen.width * containerRect.width;
float uiY = (1 - screenPoint.y / Screen.height) * containerRect.height;
element.style.translate = new Translate(uiX, uiY);
```

---

## Runtime Panel Settings 주요 속성

| 속성 | 설명 |
|------|------|
| **Sort Order** | 여러 패널 간 렌더링 순서 (높을수록 앞) |
| **Scale Mode** | `Constant Physical Size` / `Scale With Screen Size` / `Constant Pixel Size` |
| **Reference Resolution** | Scale With Screen Size 기준 해상도 |
| **Target Texture** | RenderTexture (World Space UI용) |
| **Theme Style Sheet** | 패널 전체 TSS 테마 |
| **Dynamic Atlas Settings** | 자동 아틀라싱 최소/최대 크기, 필터 |

---

## World Space UI

3D 공간에 UI를 배치하는 방법 (NPC 이름표, 빌보드 등):

1. **Camera Space**: Panel Settings → Scale Mode = Scale With Screen Size
2. **World Space**: Panel Settings → Target Texture → RenderTexture → 3D 메시에 적용

---

## 런타임 이벤트 시스템

- Input System 패키지 또는 Legacy Input Manager 모두 지원
- 씬에 `EventSystem` 컴포넌트 필요
- Panel Settings에서 Event System Mode 설정

---

## 런타임 성능 고려사항

| 최적화 | 방법 |
|--------|------|
| 이동 요소 | `UsageHints.DynamicTransform` 설정 |
| 정적 요소 | `UsageHints.None` (기본값) |
| 빈번한 텍스처 변경 | `UsageHints.DynamicColor` |
| 여러 UIDocument | Sort Order로 렌더링 순서 명시 |

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/runtime-ui|Runtime UI 구현]] — UIDocument+MonoBehaviour 기본 패턴
- [[wiki/unity-ui-toolkit/uss-transitions|USS Transitions & Transform]] — 애니메이션 트랜지션
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — UsageHints 상세

## 출처
- `raw/unity-ui-toolkit-runtime-ui-advanced.md`
