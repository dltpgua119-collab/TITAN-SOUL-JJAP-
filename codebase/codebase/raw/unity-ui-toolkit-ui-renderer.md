---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-generate-2d-visual-content.html
created: 2026-04-16
---

# UI Renderer — 2D 비주얼 콘텐츠 생성

## 두 가지 API

- **Painter2D API**: HTML Canvas 스타일 경로 기반 벡터 드로잉 (권장)
- **Mesh API**: 정점/인덱스 직접 할당 (고급)

## Painter2D API

`MeshGenerationContext.painter2D`로 접근.

### 기본 패턴

```csharp
class MyElement : VisualElement {
    public MyElement() {
        generateVisualContent += OnGenerateVisualContent;
    }

    void OnGenerateVisualContent(MeshGenerationContext mgc) {
        var painter = mgc.painter2D;
        painter.fillColor = Color.red;

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

### 주요 메서드

| 메서드 | 기능 |
|--------|------|
| `BeginPath()` | 새 경로 시작 |
| `MoveTo(pos)` | 펜 이동 |
| `LineTo(pos)` | 직선 |
| `Arc(center, radius, startAngle, endAngle)` | 호 |
| `ArcTo(pos1, pos2, radius)` | 선분 간 호 |
| `BezierCurveTo(ctrl1, ctrl2, end)` | 3차 베지어 곡선 |
| `QuadraticCurveTo(ctrl, end)` | 2차 베지어 곡선 |
| `ClosePath()` | 경로 닫기 |
| `Fill(FillRule)` | 채우기 |
| `Stroke()` | 외곽선 |

### 선 스타일

```csharp
painter.lineWidth = 10.0f;
painter.strokeColor = Color.white;
painter.lineJoin = LineJoin.Round;
painter.lineCap = LineCap.Round;
```

### 구멍 있는 도형 (FillRule)

```csharp
// 외부 사각형
painter.BeginPath();
painter.MoveTo(new Vector2(10, 10));
painter.LineTo(new Vector2(300, 10));
painter.LineTo(new Vector2(300, 150));
painter.LineTo(new Vector2(10, 150));
painter.ClosePath();

// 내부 구멍
painter.MoveTo(new Vector2(150, 50));
painter.LineTo(new Vector2(175, 75));
painter.LineTo(new Vector2(150, 100));
painter.LineTo(new Vector2(125, 75));
painter.ClosePath();

painter.Fill(FillRule.OddEven);  // 또는 FillRule.NonZero
```

## Mesh API

```csharp
void OnGenerateVisualContent(MeshGenerationContext mgc) {
    var mesh = mgc.Allocate(4, 6);  // 4 vertices, 6 indices

    mesh.SetNextVertex(new Vertex() {
        position = new Vector3(0, 0, Vertex.nearZ), tint = Color.red });
    mesh.SetNextVertex(new Vertex() {
        position = new Vector3(100, 0, Vertex.nearZ), tint = Color.red });
    mesh.SetNextVertex(new Vertex() {
        position = new Vector3(100, 100, Vertex.nearZ), tint = Color.red });
    mesh.SetNextVertex(new Vertex() {
        position = new Vector3(0, 100, Vertex.nearZ), tint = Color.red });

    // 삼각형 두 개로 사각형
    mesh.SetNextIndex(0); mesh.SetNextIndex(1); mesh.SetNextIndex(2);
    mesh.SetNextIndex(0); mesh.SetNextIndex(2); mesh.SetNextIndex(3);
}
```
