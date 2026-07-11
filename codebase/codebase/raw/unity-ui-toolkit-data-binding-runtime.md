---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-get-started-runtime-binding.html
created: 2026-04-16
---

# Data Binding — Runtime Binding 상세

## 데이터 소스 클래스

```csharp
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu]
public class ExampleObject : ScriptableObject
{
    [CreateProperty]
    public string simpleLabel = "Hello World!";

    [CreateProperty]
    public int count = 0;
}
```

- `[CreateProperty]` → 해당 프로퍼티를 UI 바인딩 가능하게 표시
- public 프로퍼티만 노출됨

## UXML에서 바인딩

```xml
<engine:Label text="Label" 
    data-source="ExampleObject.asset" 
    data-source-path="simpleLabel">
    <Bindings>
        <engine:DataBinding property="text" binding-mode="ToTarget" />
    </Bindings>
</engine:Label>
```

- `data-source`: 에셋 인스턴스 참조
- `data-source-path`: 프로퍼티 이름
- `binding-mode`: 동기화 방향

## 바인딩 모드

| 모드 | 동작 |
|------|------|
| `TwoWay` (기본) | 소스 ↔ UI 양방향 동기화 |
| `ToTarget` | 소스 → UI (읽기 전용 UI) |
| `ToSource` | UI → 소스 (입력만) |
| `ToTargetOnce` | 한 번만 소스 → UI, 이후 dirty 표시 시에만 |

## 업데이트 트리거

| 트리거 | 동작 |
|--------|------|
| Every Frame | 매 프레임 갱신 |
| On Change Detection | 변경 감지 시 갱신 (불가능하면 매 프레임) |
| When Marked Dirty | `MarkDirty()` 호출 시에만 갱신 |

```csharp
// 수동으로 dirty 표시
binding.MarkDirty();
```

## UI Builder 워크플로우

1. Inspector에서 Label 선택
2. Bindings > Data Source > Object에 에셋 설정
3. Data Source Path로 프로퍼티 선택
4. 우클릭 → Add Binding
5. Binding Mode 및 Update Trigger 설정
