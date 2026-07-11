---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-Transitions.html
created: 2026-04-16
---

# USS Transitions & Transform

## USS Transitions
"USS transitions are similar to CSS transitions. A USS transition changes property values over a given duration."

### 핵심 속성
| 속성 | 역할 |
|------|------|
| `transition-property` | 애니메이션할 USS 속성 지정 |
| `transition-duration` | 애니메이션 길이 |
| `transition-timing-function` | 이징 곡선 |
| `transition-delay` | 시작 지연 |

### USS 예시
```uss
.my-button {
    transition-property: color, rotate;
    transition-duration: 0.2s;
    transition-timing-function: ease-in-out;
    color: white;
    background-color: #4A4A4A;
}

.my-button:hover {
    color: yellow;
    background-color: #666666;
}

.my-button:active {
    rotate: 5deg;
    scale: 0.95 0.95;
}
```

### C# 구현
```csharp
element.style.transitionProperty = new List<StylePropertyName> { "rotate", "color" };
element.style.transitionDuration = new List<TimeValue> { 
    new(0.2f, TimeUnit.Second) 
};
element.style.transitionTimingFunction = new List<EasingFunction> { 
    EasingMode.EaseInOut 
};
element.style.transitionDelay = new List<TimeValue> { 0 };
```

### 주의사항
- "The first frame in a scene has no previous state." → 첫 프레임 후 트랜지션 시작
- 단위 일치 필수: `left` 기본값 `auto` → 트랜지션 시 `0px` 명시
- 레이아웃 프로퍼티(width, height) 트랜지션 → 레이아웃 재계산 비쌈 → translate/scale 사용
- 이징 함수: ease, ease-in, ease-out, linear, ease-in-sine, ease-in-bounce 등

---

# USS Transform Properties

## 개요
"Applying transform to an element reduces recalculations because it doesn't change the layout of other elements in the hierarchy."

## 핵심 Transform 속성

| 속성 | 역할 |
|------|------|
| `transform-origin` | 회전/스케일 기준점 (피벗) |
| `translate` | 위치 이동 (px 또는 %) |
| `scale` | 크기 조정 (음수 = 반전) |
| `rotate` | 회전 (deg, grad, rad, turn) |

## USS 예시
```uss
.card {
    transform-origin: center;
    transition-property: rotate, scale;
    transition-duration: 0.3s;
}

.card:hover {
    rotate: 5deg;
    scale: 1.05;
}

.card--flipped {
    rotate: 180deg;
    scale: -1 1;  /* 가로 반전 */
}
```

## C# 구현
```csharp
// 변환 기준점 설정
element.style.transformOrigin = new TransformOrigin(
    Length.Percent(50), Length.Percent(50));

// 이동 (레이아웃 재계산 없음)
element.style.translate = new Translate(Length.Percent(10), 50);

// 스케일
element.style.scale = new Scale(new Vector2(1.2f, 1.2f));

// 회전
element.style.rotate = new Rotate(Angle.Degrees(45));
```

## 변환 적용 순서
Scale → Rotate → Translate

## 사용 권장
- 정적 레이아웃 정의: position/width/height 사용
- 애니메이션: translate/scale/rotate 사용 (레이아웃 재계산 없음)
