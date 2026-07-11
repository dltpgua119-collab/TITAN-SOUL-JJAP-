---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-bind-custom-control.html
created: 2026-04-16
---

# 바인딩 가능한 커스텀 컨트롤 (BindableElement + INotifyValueChanged)

## 핵심 인터페이스

- `BindableElement`: 바인딩 가능 UI 요소 기본 클래스
- `INotifyValueChanged<T>`: 값 변경 알림 인터페이스

## ScriptableObject 데이터

```csharp
[CreateAssetMenu(menuName = "UIToolkitExamples/TextureAsset")]
public class TextureAsset : ScriptableObject
{
    public Texture2D texture;
}
```

## 커스텀 컨트롤 구현

```csharp
[UxmlElement]
public partial class TexturePreviewElement : BindableElement, INotifyValueChanged<Object>
{
    public static readonly string ussClassName = "texture-preview-element";
    Image m_Preview;
    ObjectField m_ObjectField;
    Texture2D m_Value;

    public TexturePreviewElement()
    {
        AddToClassList(ussClassName);
        
        m_Preview = new Image();
        Add(m_Preview);
        
        m_ObjectField = new ObjectField();
        m_ObjectField.objectType = typeof(Texture2D);
        m_ObjectField.RegisterValueChangedCallback(OnObjectFieldValueChanged);
        Add(m_ObjectField);
    }

    void OnObjectFieldValueChanged(ChangeEvent<Object> evt) => value = evt.newValue;

    // SetValueWithoutNotify: 내부 상태 갱신, 이벤트 없음
    public void SetValueWithoutNotify(Object newValue)
    {
        m_Value = newValue as Texture2D;
        m_Preview.image = m_Value;
        m_ObjectField.SetValueWithoutNotify(m_Value); // 중복 이벤트 방지
    }

    // value: 변경 시 ChangeEvent 발송
    public Object value
    {
        get => m_Value;
        set
        {
            if (value == this.value) return;
            var previous = this.value;
            SetValueWithoutNotify(value);
            using (var evt = ChangeEvent<Object>.GetPooled(previous, value))
            {
                evt.target = this;
                SendEvent(evt);
            }
        }
    }
}
```

## UXML에서 바인딩

```xml
<example:TexturePreviewElement binding-path="texture" />
```

`binding-path`로 직렬화 필드 이름과 자동 연결.

## 핵심 패턴
- `SetValueWithoutNotify()`가 내부 상태를 갱신 → 다른 컨트롤(ObjectField 등)은 `SetValueWithoutNotify()` 사용 (중복 이벤트 방지)
- `value` setter는 `ChangeEvent<T>`를 Pool에서 꺼내 발송
