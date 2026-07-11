---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/data-binding.html
created: 2026-04-16
---

# Data Binding in UI Toolkit (E-Book Chapter)

## 핵심 개념
Runtime data binding은 UI 요소를 앱 데이터에 직접 연결. MVVM 아키텍처 구현. 수동 동기화 코드 제거, 데이터 변경 시 UI 자동 갱신.

## 데이터 소스 설정
모든 C# 오브젝트(ScriptableObject, MonoBehaviour, 커스텀 클래스)가 데이터 소스 가능.

```csharp
[SerializeField, DontCreateProperty]
int m_Value;

[CreateProperty]  // 컴파일 타임 바인딩 코드 생성, 런타임 Reflection 제거
public int Value
{
    get => m_Value;
    set => m_Value = value;
}
```

## UXML 바인딩 문법
```xml
<ui:Label text="Placeholder" name="health-label">
    <Bindings>
        <ui:DataBinding property="text" data-source-path="PlayerName" 
                        binding-mode="ToTarget" />
    </Bindings>
</ui:Label>
```

## C# 바인딩 설정
```csharp
var label = new Label();
label.SetBinding("text", new DataBinding()
{
    dataSource = playerData,
    dataSourcePath = new PropertyPath("PlayerName"),
    bindingMode = BindingMode.ToTarget
});
```

## 바인딩 모드
- **TwoWay** (기본): 양방향 데이터 흐름
- **ToTarget**: 소스 → UI 단방향
- **ToSource**: UI → 소스 단방향
- **ToTargetOnce**: 일회성 초기화

## 변경 추적 인터페이스
- **INotifyBindablePropertyChanged**: 프로퍼티 수준 변경 추적, 영향받은 바인딩만 갱신
- **IDataSourceViewHashProvider**: 버전 해싱으로 전체 데이터 소스 변경 감지

## 고급 패턴

### Unresolved Bindings (UXML + 런타임 소스 할당)
```xml
<ui:Label data-source-type="PlayerData, Assembly" name="player-name">
    <Bindings>
        <ui:DataBinding property="text" data-source-path="PlayerName"/>
    </Bindings>
</ui:Label>
```
```csharp
// 런타임에 데이터 소스 할당
label.dataSource = playerData;
```

### ListView 바인딩
```csharp
m_ListView.dataSource = m_TeamData;
m_ListView.SetBinding("itemsSource", new DataBinding
{
    dataSourcePath = new PropertyPath("Players")
});
```

## 성능 최적화
- 값 타입 박싱 최소화 (중복 바인딩 감소)
- 업데이트 트리거를 요소 요구사항에 맞게 조정
- INotifyBindablePropertyChanged로 프로퍼티 수준 변경 추적
- 복잡한 데이터 계층을 플랫 구조로 통합
- 비용 큰 연산은 바인딩 대신 캐시
