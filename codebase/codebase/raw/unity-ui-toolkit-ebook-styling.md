---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/styling.html
created: 2026-04-16
---

# UI Toolkit Styling Best Practices (E-Book Chapter)

## USS 셀렉터 우선순위 (높→낮)
1. **인라인 스타일** — 모든 것 오버라이드
2. **ID 셀렉터** (`#id`) — 가장 구체적인 USS
3. **클래스 셀렉터** (`.className`)
4. **C# 타입 셀렉터** (`Button`, `Label`)

동일 우선순위 → USS 파일 내 순서(아래 항목 우선).

## 셀렉터 유형 및 용도

| 셀렉터 | 문법 | 용도 |
|--------|------|------|
| Type | `Button` | 특정 C# 타입 전체 스타일 |
| ID/Name | `#title` | 이름 프로퍼티로 고유 타겟 |
| Class | `.smallFont` | Class List로 재사용 스타일 |
| Direct Child | `#title > Label` | 직계 자식만 |
| Any Depth | `#title Label` | 임의 깊이 하위 요소 |
| Pseudo-class | `Button:hover` | 요소 상태별 스타일 |

## USS 변수 모범 사례
- 반복 값(색상, 크기, 폰트)은 변수로 추출
- 변수는 셀렉터 수준 스코프 (다른 셀렉터의 변수 참조 불가)
- Unity 6.1+: UI Builder에서 직접 편집 가능
- 지원 타입: float, color, string, 에셋 참조, dimensions, enums

## 성능 고려사항
"Avoid overly broad selectors (especially those ending in * or targeting generic Unity classes like .unity-button). Deep child selectors can potentially slow down performance."

## 트랜지션 & 애니메이션
Inspector 속성으로 구성:
- **Property**: 애니메이션 대상 (기본: "all")
- **Duration**: 길이 (초/밀리초, 0 이상)
- **Easing Function**: 애니메이션 커브
- **Delay**: 시작 전 지연

의사 클래스(`:hover`, `:active`, `:focus`) → 스타일 변경 시 자동 트랜지션 트리거.

## 동적 스타일 전환
```csharp
visualElement.RemoveFromClassList("common");
visualElement.AddToClassList("legendary");
```

## Theme Style Sheets (TSS)
멀티 테마 지원 (계절, 라이트/다크, 캐릭터별):
- 기존 테마에서 상속 → 변경되지 않은 셀렉터 재사용
- 새 USS 파일에서 수정된 속성만 오버라이드
- 누락 셀렉터는 부모 테마로 폴백
- Panel Settings Inspector에서 런타임에 테마 할당

**워크플로우**: TSS 생성 → USS 파일 추가 → 셀렉터 복사/커스터마이즈 → UI Builder 드롭다운에서 미리보기
