---
type: concept
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, renderer, vector-graphics, painter2d, mesh-api]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-ui-renderer.md]
---

# UI Renderer — 2D 커스텀 비주얼 콘텐츠

> `generateVisualContent` 콜백에서 Painter2D API 또는 Mesh API로 VisualElement에 커스텀 벡터 그래픽/메시를 그린다.

## Painter2D API (권장)

HTML Canvas에서 영감받은 경로 기반 드로잉. `MeshGenerationContext.painter2D`로 접근.

### 기본 패턴

```cs
class MyElement : VisualElement
{
    public MyElement()
    {
        generateVisualContent += OnGenerateVisualContent;
    }

    void OnGenerateVisualContent(MeshGenerationContext mgc)
    {
        var painter = mgc.painter2D;
        painter.fillColor = Color.cyan;

        painter.BeginPath();
        painter.MoveTo(Vector2.zero);
        painter.LineTo(new Vector2(layout.width, 0));
        painter.LineTo(new Vector2(layout.width, layout.height));
        painter.LineTo(new Vector2(0, layout.height));
        painter.ClosePath();
        painter.Fill();
    }
}
```

### 경로 메서드

| 메서드 | 기능 |
|--------|------|
| `BeginPath()` | 새 경로 시작 |
| `MoveTo(pos)` | 펜 이동 (선 없음) |
| `LineTo(pos)` | 직선 |
| `Arc(center, r, start, end)` | 호 |
| `ArcTo(p1, p2, r)` | 선분 간 호 |
| `BezierCurveTo(c1, c2, end)` | 3차 베지어 |
| `QuadraticCurveTo(ctrl, end)` | 2차 베지어 |
| `ClosePath()` | 경로 닫기 |
| `Fill(FillRule)` | 채우기 |
| `Stroke()` | 외곽선 그리기 |

### 선 스타일

```cs
painter.lineWidth = 10f;
painter.strokeColor = Color.white;
painter.lineJoin = LineJoin.Round;
painter.lineCap = LineCap.Round;
```

### 아크 (원, 부채꼴)

```cs
painter.BeginPath();
painter.Arc(new Vector2(100, 100), 50f, 0f, 360f); // 완전한 원
painter.Fill();
```

### 구멍 있는 도형

```cs
// 외부 사각형 + 내부 마름모 → 도넛 모양
painter.BeginPath();
// 외부
painter.MoveTo(new Vector2(10, 10));
painter.LineTo(new Vector2(300, 10));
painter.LineTo(new Vector2(300, 150));
painter.LineTo(new Vector2(10, 150));
painter.ClosePath();
// 구멍
painter.MoveTo(new Vector2(150, 50));
painter.LineTo(new Vector2(175, 75));
painter.LineTo(new Vector2(150, 100));
painter.LineTo(new Vector2(125, 75));
painter.ClosePath();

painter.Fill(FillRule.OddEven);
```

`FillRule.OddEven` — 경로 교차 횟수로 내부 판별  
`FillRule.NonZero` — 방향 포함 교차로 내부 판별

---

## Mesh API (고급)

정점/인덱스를 직접 할당해 삼각형 메시 생성.

```cs
void OnGenerateVisualContent(MeshGenerationContext mgc)
{
    var mesh = mgc.Allocate(4, 6); // 4정점, 6인덱스

    mesh.SetNextVertex(new Vertex {
        position = new Vector3(0, 0, Vertex.nearZ), tint = Color.red });
    mesh.SetNextVertex(new Vertex {
        position = new Vector3(100, 0, Vertex.nearZ), tint = Color.red });
    mesh.SetNextVertex(new Vertex {
        position = new Vector3(100, 100, Vertex.nearZ), tint = Color.red });
    mesh.SetNextVertex(new Vertex {
        position = new Vector3(0, 100, Vertex.nearZ), tint = Color.red });

    // 삼각형 2개로 사각형
    mesh.SetNextIndex(0); mesh.SetNextIndex(1); mesh.SetNextIndex(2);
    mesh.SetNextIndex(0); mesh.SetNextIndex(2); mesh.SetNextIndex(3);
}
```

**주의**: `Vertex.nearZ` 사용 필수 — UI Toolkit 클리핑 레이어와 일치.

---

## 언제 어떤 API를

| 상황 | API |
|------|-----|
| 원/호/선/곡선 등 벡터 도형 | Painter2D |
| 진행률 링, 커스텀 게이지 | Painter2D |
| UV 텍스처 맵핑, 복잡한 메시 | Mesh API |
| 세밀한 성능 제어 필요 | Mesh API |

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]] — VisualElement 상속 기반
- [[wiki/unity-ui-toolkit/performance-optimization|성능 최적화]] — 렌더링 배칭

## 출처
- `raw/unity-ui-toolkit-ui-renderer.md`
