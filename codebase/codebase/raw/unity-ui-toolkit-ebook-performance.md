---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/optimizing-performance.html
created: 2026-04-16
---

# UI Toolkit Performance Optimization (E-Book Chapter)

## 핵심 성능 원칙
업데이트 메커니즘 최소화, 드로우 콜 감소.

## visual tree 4대 업데이트 메커니즘

| 메커니즘 | 비용 | 트리거 |
|---------|------|--------|
| Style resolution | 중첩 계층에서 높음 | 클래스/스타일 변경 |
| Layout 재계산 | 잦은 업데이트 시 비쌈 | 크기/위치 수정 |
| Vertex buffer 업데이트 | 리소스 집약적 | 지오메트리 변경 |
| Rendering state 변경 | CPU 오버헤드 증가 | 텍스처/마스크 전환 |

## 배칭 최적화
동일 GPU 요구사항(셰이더, 텍스처, 메시, GPU 파라미터)을 가진 요소 그룹화.
텍스트 요소 사이 이미지 하나만 삽입해도 배치 분할 발생.

### Vertex Budget
Panel Settings에서 Vertex Budget 조정 → 버퍼 단편화 방지. 복잡한 UI는 20,000+ 권장.

## 텍스처 관리
- **8-텍스처 제한**: uber shader가 최대 8개 텍스처를 하나의 드로우 콜로 렌더링. 초과 시 배치 분할.
- **Dynamic Texture Atlas**: 여러 텍스처 전환을 통합. Sprite Atlas(정적) + Dynamic Atlas(런타임 변경) 조합 권장.
- `RuntimePanelUtils.ResetDynamicAtlas(panelName)` — 단편화된 아틀라스 리셋

## 마스킹 전략

| 마스크 유형 | 특성 | 권장 |
|-----------|------|------|
| 직사각형 마스크 | 셰이더 기반, 스텐실 없음, 배치 분할 없음 | 가능하면 사용 |
| 복잡 마스크(둥근 모서리) | 스텐실 버퍼, 최대 7단계 중첩, 배치 분할 | 최소화 |

## 애니메이션 최적화

```csharp
// ❌ 비쌈 (레이아웃 재계산 트리거)
element.style.width = new Length(100, LengthUnit.Pixel);
element.style.left = new Length(50, LengthUnit.Pixel);

// ✅ GPU 효율 (레이아웃 재계산 우회)
element.style.translate = new Translate(50, 0);
element.style.scale = new Scale(1.2f, 1.2f);
element.style.rotate = new Rotate(45);
```

### Usage Hints
```csharp
element.usageHints = UsageHints.DynamicTransform;   // 개별 요소 이동
parentContainer.usageHints = UsageHints.GroupTransform; // 그룹 GPU 전파
```

## 가시성 제어 전략

| 방법 | 렌더 | 레이아웃 | 용도 |
|------|------|---------|------|
| `opacity = 0` | O | O | 트랜지션 |
| `visible = false` | X | O | 임시 숨김 |
| `display = None` | X | X | 잦은 토글 |
| `RemoveFromHierarchy()` | X | X | 드문 요소 |

권장: 성능 중요 토글에 `DisplayStyle.None`; 드문 다이얼로그에 제거.

## Runtime Data Binding 최적화

```csharp
[GeneratePropertyBag]  // 컴파일 타임 프로퍼티 백 생성 (반사 제거)
public class CharacterData : ScriptableObject, 
    INotifyBindablePropertyChanged, 
    IDataSourceViewHashProvider
{
    private long _version;
    [SerializeField, DontCreateProperty] string _characterName;
    
    [CreateProperty]  // 프로퍼티별 컴파일 타임 바인딩 코드 생성
    public string CharacterName {
        get => _characterName;
        set {
            _characterName = value;
            _version++;
            propertyChanged?.Invoke(this, 
                new BindablePropertyChangedEventArgs(nameof(CharacterName)));
        }
    }
    
    public long GetViewHashCode() => _version;
    public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
}
```

## 피해야 할 것
- 애니메이션 중 잦은 CSS 클래스 전환 (대신 인라인 스타일)
- 배치당 8개 이상 텍스처
- 스텐실 마스크 7단계 이상 중첩
- 레이아웃 프로퍼티(width, height, position) 애니메이션
- `opacity` 기반 가시성 (대신 `display: None`)
- Reflection 기반 데이터 바인딩 (대신 source generation)
- 프로파일링 없는 vertex buffer 과할당

## 프로파일링 도구
- **Unity Profiler**: 전체 성능 지표
- **UI Toolkit Debugger**: 요소 검사 + Dynamic Atlas Viewer
- **Frame Debugger**: 드로우 콜 분석
- **SetPanelChangeReceiver**: 세밀한 변경 추적 (에디터/개발 빌드 전용)

## Unity 6 성능 향상
- 이벤트 디스패치 속도 2배
- 클래식 요소의 Jobified 지오메트리 생성
- 텍스트 생성 병렬화
- 연산 캐싱으로 깊은 계층 레이아웃 성능 향상
