---
type: how-to
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, data-binding, runtime-binding, mvvm]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-data-binding-runtime.md]
---

# Runtime Data Binding 설정하기

> 데이터 소스 클래스에 `[CreateProperty]`를 붙이고 UXML 또는 UI Builder에서 바인딩을 선언하면, 소스 ↔ UI 간 자동 동기화가 이루어진다.

## 전제 조건

- Unity 2022.1+ (Runtime Binding API)
- `Unity.Properties` 패키지 설치

---

## 단계

### 1. 데이터 소스 클래스 작성

```cs
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [CreateProperty]
    public string playerName = "Hero";

    [CreateProperty]
    public int health = 100;

    [CreateProperty]
    public float score = 0f;
}
```

- `[CreateProperty]` → 해당 프로퍼티를 바인딩 가능으로 표시
- public 프로퍼티만 노출 가능

### 2. UXML에서 바인딩 선언

```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:Label name="name-label"
              data-source="PlayerData.asset"
              data-source-path="playerName">
        <Bindings>
            <ui:DataBinding property="text" binding-mode="ToTarget" />
        </Bindings>
    </ui:Label>

    <ui:ProgressBar name="health-bar"
                    data-source="PlayerData.asset"
                    data-source-path="health">
        <Bindings>
            <ui:DataBinding property="value" binding-mode="TwoWay" />
        </Bindings>
    </ui:ProgressBar>
</ui:UXML>
```

### 3. UI Builder 워크플로우

1. Inspector에서 UI 요소 선택
2. **Bindings > Data Source > Object**에 에셋 지정
3. **Data Source Path**에서 프로퍼티 선택
4. 우클릭 → **Add Binding**
5. **Binding Mode**와 **Update Trigger** 설정

---

## 바인딩 모드

| 모드 | 동작 | 사용 시점 |
|------|------|-----------|
| `TwoWay` (기본) | 소스 ↔ UI 양방향 | 편집 가능 입력 필드 |
| `ToTarget` | 소스 → UI 단방향 | 읽기 전용 표시 |
| `ToSource` | UI → 소스 단방향 | 입력만 받고 표시는 다른 경로 |
| `ToTargetOnce` | 소스 → UI 최초 1회 | 초기값만 적용 |

---

## 업데이트 트리거

| 트리거 | 동작 |
|--------|------|
| Every Frame | 매 프레임 강제 갱신 (성능 비용 주의) |
| On Change Detection | 변경 감지 시 갱신 (권장) |
| When Marked Dirty | `MarkDirty()` 호출 시에만 갱신 |

```cs
// 수동으로 갱신 요청
binding.MarkDirty();
```

---

## C#에서 바인딩 설정

```cs
// data-source를 C#에서 설정
var label = root.Q<Label>("name-label");
label.dataSource = playerData;
label.dataSourcePath = new PropertyPath(nameof(PlayerData.playerName));

// 바인딩 추가
var binding = new DataBinding {
    dataSourcePath = new PropertyPath(nameof(PlayerData.playerName)),
    bindingMode = BindingMode.ToTarget,
};
label.SetBinding("text", binding);
```

---

## 검증 방법

- 플레이 모드 진입 후 소스 값 변경 → UI 자동 갱신 확인
- UI Builder에서 Preview 모드로 바인딩 동작 확인
- `TwoWay` 모드: UI 값 변경 → 소스 ScriptableObject 인스펙터 값도 변경 확인

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]] — MVVM, [CreateProperty], INotifyBindablePropertyChanged
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]] — 바인딩 가능한 커스텀 컨트롤 패턴

## 출처
- `raw/unity-ui-toolkit-data-binding-runtime.md`
