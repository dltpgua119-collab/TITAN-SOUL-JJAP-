---
type: how-to
topic: unity-ui-toolkit
lang: uxml/uss
tags: [unity, ui-toolkit, ui-builder, workflow, uxml, uss]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-ui-builder-workflow.md]
---

# UI Builder로 UI 만들기

> UI Builder의 시각적 편집 워크플로우. UXML 계층 구성 → Inspector 스타일링 → USS 추출 → 저장 6단계.

## 전제 조건

- Unity 2022.1+
- `Window > UI Toolkit > UI Builder` 메뉴 접근 가능

---

## 단계

### 1. UI Builder 열기 & UXML 생성

`Window > UI Toolkit > UI Builder` → 새 UXML 파일 생성

### 2. 계층 구성 (Hierarchy 패널)

Library 패널에서 Hierarchy로 드래그. 또는 컨트롤 더블클릭.

```text
[Library]
  └─ Built-in Controls
       ├─ VisualElement
       ├─ Label
       ├─ Button
       └─ ...
→ Hierarchy로 드래그
```

### 3. 요소 설정 (Inspector 패널)

Inspector에서 이름, 클래스, 속성, 스타일 설정:
- `name`: C#에서 Q<>() 검색에 사용
- `class`: USS 셀렉터 연결
- Flex Properties: 레이아웃 설정

**주요 레이아웃 설정:**

| 속성 | 값 | 효과 |
|------|-----|------|
| `flex-grow` | 1 | 가용 공간 채우기 |
| `flex-direction` | row | 자식 수평 배열 |
| `align-items` | center | 교차축 중앙 |
| `justify-content` | center | 주축 중앙 |

### 4. USS 스타일시트 생성

StyleSheets 패널 → `+` → 새 USS 파일 생성 → 셀렉터 추가.

### 5. 인라인 스타일 → USS 클래스 추출

Inspector의 인라인 스타일 우클릭 → **"Extract Inlined Styles to New Class"** → 클래스명 지정.

재사용 가능한 스타일은 USS로 분리해 유지보수성 향상.

### 6. 저장

`Ctrl+S` 또는 File > Save → UXML 파일 저장.

---

## 일반적인 실수

| 증상 | 원인 | 해결 |
|------|------|------|
| 배경색이 안 보임 | 알파값이 0 | Inspector에서 알파를 255로 설정 |
| 요소가 클릭 안 됨 | PickingMode.Ignore | PickingMode = Position으로 변경 |
| 스타일이 적용 안 됨 | USS 파일이 UXML에 연결 안 됨 | StyleSheets 패널에서 USS 추가 확인 |

---

## UI Toolkit Debugger 활용

런타임에 UI 계층 검사 및 스타일 임시 수정:

`Window > UI Toolkit > Debugger`

- **Pick Element**: Play 모드에서 요소 클릭 선택
- **Style Inspector**: 적용된 스타일 프로퍼티 실시간 확인
- **주의**: Debugger에서 수정한 스타일은 저장되지 않음 (임시만 적용)

---

## 관련 페이지
- [[wiki/unity-ui-toolkit/ui-builder|UI Builder 개요]] — 6개 패널 구성
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]] — 셀렉터와 속성
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]] — UXML 구조

## 출처
- `raw/unity-ui-toolkit-ui-builder-workflow.md`
