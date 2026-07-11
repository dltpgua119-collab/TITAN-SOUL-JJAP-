---
type: concept
topic: unity-ui-toolkit
lang: cs
tags: [unity, ui-toolkit, uquery, query, visual-tree, search]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-uquery.md]
---

# UQuery

> Visual tree에서 UI 요소를 검색하는 jQuery/LINQ 기반 쿼리 시스템 — 메모리 할당 최소화 설계

## UQuery란?

"UQuery was inspired by JQuery and Linq, and is designed to limit dynamic memory allocation" — 모바일 성능 최적화 목적.

Visual tree(창 또는 패널의 모든 UI 요소 포함 오브젝트 그래프)에서 요소를 검색하는 확장 메서드 제공.

## 핵심 메서드

| 메서드 | 반환 | 용도 |
|--------|------|------|
| `Query<T>()` | `UQueryBuilder<T>` | 모든 매칭 요소 컬렉션 |
| `Q<T>()` | `T` | 첫 번째 매칭 요소 (단축형) |

## 검색 방법

### 이름으로 (`name` 속성)
```csharp
Button btn = root.Q<Button>("myButton");      // 이름 + 타입
root.Q("OK");                                  // 이름만 (VisualElement 반환)
root.Query("OK").ToList();                     // 모든 매칭 목록
```

### USS 클래스로
```csharp
root.Q(className: "yellow");                   // 첫 번째
root.Query(className: "yellow").ToList();       // 모든 매칭
```

### 타입으로
```csharp
root.Q<Button>();                              // 첫 번째 Button
root.Query<Button>().AtIndex(2);               // 세 번째 Button (0-based: index 2)
root.Query<Button>().ToList();                 // 모든 Button
```

## 쿼리 체이닝

```csharp
// 이름 + 클래스 + 타입 조합
Button btn = root.Query<Button>(className: "yellow", name: "OK").First();

// 계층적 검색: container2 하위의 "Cancel" Button
Button cancel = root.Query<VisualElement>("container2")
                    .Children<Button>("Cancel")
                    .First();

// 조건 필터
List<VisualElement> noTooltip = root.Query(className: "yellow")
    .Where(elem => elem.tooltip == "")
    .ToList();
```

## ForEach — 리스트 없이 처리

```csharp
// 툴팁 없는 모든 요소에 기본 툴팁 설정
root.Query().Where(elem => elem.tooltip == "")
    .ForEach(elem => elem.tooltip = "This is a tooltip!");

// 모든 Button에 이벤트 핸들러 등록
root.Query<Button>()
    .ForEach(btn => btn.RegisterCallback<ClickEvent>(OnButtonClick));
```

## 성능 모범 사례

```csharp
// ✅ 좋음 — 초기화 시 한 번만 쿼리 후 캐시
private Button _okButton;
private void OnEnable()
{
    _okButton = root.Q<Button>("ok");
}

// ❌ 나쁨 — 매 프레임 또는 이벤트마다 반복 쿼리
private void OnClick()
{
    root.Q<Button>("ok").text = "Clicked!"; // 매번 검색
}
```

```csharp
// ✅ 클로저에서 this 전체 캡처 금지
var button = root.Q<Button>("ok");
button.RegisterCallback<ClickEvent>(_ => button.text = "Clicked");

// ❌ this 전체 캡처 — 가비지 증가
button.RegisterCallback<ClickEvent>(_ => this._okButton.text = "Clicked");
```

## QueryState — 리스트 생성 없는 열거

```csharp
// 중간 List<T> 생성 없이 요소 열거
QueryState<Button> buttons = root.Query<Button>().Build();
foreach (var btn in buttons) {
    btn.SetEnabled(false);
}
```

## 조상 순회

UQuery는 하위 검색만 지원 — 조상 탐색은 `.parent` 체인으로 수동 처리:
```csharp
VisualElement ancestor = element.parent?.parent;
```

## 주의사항
- UQuery 결과는 초기화 시 캐시하고 재사용
- `ForEach`는 중간 리스트 없이 동작 → 메모리 절약
- 클로저에서 `this` 전체 캡처 금지
- 조상 방향 검색은 `.parent` 수동 탐색

## 관련 페이지
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]]
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]

## 출처
- [Find visual elements with UQuery](https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-UQuery.html)
