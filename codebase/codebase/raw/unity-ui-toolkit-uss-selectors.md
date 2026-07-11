---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-USS-Selectors.html
created: 2026-04-16
---

# USS Selectors 레퍼런스

## 셀렉터 타입

### 1. Type 셀렉터
요소 타입으로 매칭:
```uss
Button { background-color: grey; }
Label { color: white; }
```

### 2. Name 셀렉터
`name` 속성으로 매칭 (`#` 접두사):
```uss
#submit-button { background-color: green; }
#title-label { font-size: 24px; }
```

### 3. Class 셀렉터
USS 클래스로 매칭 (`.` 접두사):
```uss
.primary-button { background-color: blue; }
.disabled { opacity: 0.5; }
```

### 4. Universal 셀렉터
모든 요소 매칭 (`*`):
```uss
* { margin: 0; padding: 0; }
```
**주의**: 성능 비용 높음 — 복잡한 계층과 결합 금지.

### 5. Descendant 셀렉터
임의 깊이 하위 요소 매칭 (공백):
```uss
.panel Label { color: white; }
#container Button { border-radius: 4px; }
```

### 6. Child 셀렉터
직계 자식만 매칭 (`>`):
```uss
.menu > .menu__item { padding: 4px; }
```
Descendant보다 성능 좋음 (부분 매칭 가능).

### 7. Multiple 셀렉터
여러 조건 동시 만족 (공백 없이 연결):
```uss
Button.primary:hover { background-color: darkblue; }
/* Button이면서 primary 클래스이면서 hover 상태 */
```

### 8. Selectors List
같은 스타일 공유 (`,` 구분):
```uss
Button, Toggle, Slider {
    border-radius: 4px;
}
```

### 9. Pseudo-classes
특정 상태 타겟:

| 의사 클래스 | 조건 |
|-------------|------|
| `:hover` | 포인터 올려진 상태 |
| `:active` | 클릭/누른 상태 |
| `:focus` | 포커스 상태 |
| `:checked` | 체크된 상태 (Toggle 등) |
| `:enabled` | 활성화 상태 |
| `:disabled` | 비활성화 상태 |
| `:root` | visual tree 루트 |
| `:last-child` | 마지막 자식 |
| `:first-child` | 첫 번째 자식 |

```uss
Button:hover { background-color: #666; }
Toggle:checked { border-color: green; }
.item:disabled { opacity: 0.4; }
```

## 우선순위 (Specificity)

높음 → 낮음:
1. 인라인 스타일
2. `#name` 셀렉터
3. `.class` 셀렉터 / 의사 클래스
4. Type 셀렉터
5. Universal 셀렉터

동일 우선순위 → USS 파일 내 나중에 정의된 규칙 우선.
