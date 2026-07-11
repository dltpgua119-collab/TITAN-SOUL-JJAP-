---
type: snippet
topic: unity-ui-toolkit
lang: cs/uxml
tags: [unity, ui-toolkit, custom-controls, data-binding, bindable]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-bindable-control.md]
---

# 바인딩 가능한 커스텀 컨트롤 스니펫

> `BindableElement` + `INotifyValueChanged<T>` 를 구현해 SerializedObject 바인딩을 지원하는 커스텀 컨트롤 패턴.

## 데이터 소스

```cs
[CreateAssetMenu(menuName = "UIToolkitExamples/TextureAsset")]
public class TextureAsset : ScriptableObject
{
    public Texture2D texture;
}
```

## 커스텀 컨트롤 구현 (TexturePreviewElement)

```cs
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

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

    // 이벤트 없이 내부 상태만 갱신 — 다른 서브 컨트롤도 SetValueWithoutNotify 사용
    public void SetValueWithoutNotify(Object newValue)
    {
        m_Value = newValue as Texture2D;
        m_Preview.image = m_Value;
        m_ObjectField.SetValueWithoutNotify(m_Value); // 중복 ChangeEvent 방지
    }

    // 값 변경 시 ChangeEvent<Object> 발송
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
<ui:UXML xmlns:ui="UnityEngine.UIElements"
         xmlns:example="UIToolkitExamples"
         editor-extension-mode="True">
    <example:TexturePreviewElement binding-path="texture" />
</ui:UXML>
```

`binding-path` 속성이 `TextureAsset.texture` 직렬화 필드와 자동 연결.

## Editor 클래스

```cs
[CustomEditor(typeof(TextureAsset))]
public class TextureAssetEditor : Editor
{
    [SerializeField] VisualTreeAsset m_VisualTree;

    public override VisualElement CreateInspectorGUI()
        => m_VisualTree.CloneTree();
}
```

## 핵심 패턴 요약

| 패턴 | 이유 |
|------|------|
| 서브 컨트롤에 `SetValueWithoutNotify()` 사용 | 중복 `ChangeEvent` 방지 |
| `ChangeEvent<T>.GetPooled()` + `using` | 이벤트 객체 Pool 재활용 |
| `evt.target = this` 설정 | 이벤트 발원지를 내 컨트롤로 지정 |

## 의존성
- `UnityEngine.UIElements`
- `Unity.Properties` (런타임 바인딩 시)

## 관련 페이지
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]] — [UxmlElement], BaseField<T>
- [[wiki/unity-ui-toolkit/data-binding-overview|데이터 바인딩 개요]] — MVVM, INotifyBindablePropertyChanged
- [[wiki/unity-ui-toolkit/data-binding-runtime|Runtime Data Binding 설정하기]] — [CreateProperty] 패턴

## 출처
- `raw/unity-ui-toolkit-bindable-control.md`
