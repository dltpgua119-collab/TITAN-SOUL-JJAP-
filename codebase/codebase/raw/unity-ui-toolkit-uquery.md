---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-UQuery.html
created: 2026-04-16
---

# Find Visual Elements with UQuery

## UQuery란?
"UQuery was inspired by JQuery and Linq, and is designed to limit dynamic memory allocation" — 모바일 성능 최적화 목적.
visual tree(창 또는 패널의 모든 UI 요소 포함 오브젝트 그래프)를 검색하는 확장 메서드 제공.

## 핵심 메서드

- `Query()` — 매칭되는 모든 요소 컬렉션 반환
- `Q()` — `Query<T>.First()` 단축형, 첫 번째 매칭만 반환

두 메서드 모두 내부적으로 `UQueryBuilder`로 선택 규칙 구성.

## 쿼리 셀렉터 문법

### 이름으로 검색
```csharp
root.Q("OK");                    // "OK"라는 이름의 첫 번째 요소
root.Query("OK").ToList();       // "OK" 이름인 모든 요소
```

### USS 클래스로 검색
```csharp
root.Q(className: "yellow");                 // 첫 번째 매칭
root.Query(className: "yellow").ToList();    // 모든 매칭
```

### 타입으로 검색
```csharp
root.Q<Button>();                // 첫 번째 Button
root.Query<Button>().AtIndex(2); // 세 번째 Button
```

## 쿼리 체이닝
```csharp
// 이름 + 클래스 + 타입 조합
root.Query<Button>(className: "yellow", name: "OK").First();

// 컨테이너 내 특정 자식
root.Query<VisualElement>("container2").Children<Button>("Cancel").First();

// 조건 필터링
root.Query(className: "yellow").Where(elem => elem.tooltip == "").ToList();
```

## ForEach 사용
```csharp
// 중간 리스트 생성 없이 직접 처리
root.Query().Where(elem => elem.tooltip == "")
    .ForEach(elem => elem.tooltip = "This is a tooltip!");
```

## 모범 사례
- 초기화 시 UQuery 결과를 캐시 (반복 쿼리 지양)
- 클로저에서 `this` 전체 캡처 금지 — 필요한 요소만 캡처
- 리스트 생성 없는 열거에는 `QueryState` 구조체 사용
- 조상 순회는 `.parent` 체인으로 수동 탐색
