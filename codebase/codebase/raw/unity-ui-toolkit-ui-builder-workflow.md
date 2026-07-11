---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIB-getting-started.html
created: 2026-04-16
---

# UI Builder 워크플로우

## 핵심 6단계

1. 새 UI Document (UXML) 생성
2. Library에서 요소를 Hierarchy로 드래그해 계층 구성
3. Inspector에서 프로퍼티 설정
4. USS 스타일시트 생성 → 재사용 셀렉터 추가
5. 인라인 스타일 → USS 클래스로 추출
6. 저장

## 시작

`Window > UI Toolkit > UI Builder` 열기 → 새 UXML 파일 생성.

## 요소 추가

- Library 패널에서 Hierarchy로 드래그
- 또는 컨트롤 더블클릭으로 계층에 추가

## 주요 레이아웃 설정

```
flex-grow: 1        → 가용 공간 채우기
flex-direction: row → 자식 요소 수평 배열
align-items: center → 교차축 중앙 정렬
justify-content: center → 주축 중앙 정렬
```

## 스타일 관리

### USS 클래스 생성
StyleSheet 패널 → 새 USS 파일 생성 → "Add new selector" 필드에 셀렉터 입력.

### 인라인 스타일 → USS 클래스 추출
"Extract Inlined Styles to New Class" 기능으로 인라인 프로퍼티를 스타일시트 클래스로 자동 변환.

## 색상 주의사항

색상의 알파 기본값 = `0` (투명). 색상이 보이지 않을 때 알파를 `255`로 설정.

---

# UI Toolkit Debugger

## 열기

`Window > UI Toolkit > Debugger`

## 주요 기능

- UI 계층 실시간 확인
- 각 요소의 USS 셀렉터 목록
- Style Inspector로 스타일 프로퍼티 검사 및 실시간 수정
- 에러/경고 표시

## Pick Element

**런타임 요소 선택**: GameObject 선택 → Play 모드 진입 → **Pick Element** 버튼 클릭 → Canvas/Game view에서 요소 클릭.

## 주의사항

"Editing styles in the Debugger only changes the inline styles on the live elements themselves and the changes aren't saved anywhere and will be lost on the next UI regeneration."

→ Debugger의 스타일 수정은 임시. 영구 변경은 반드시 USS 파일에 직접 저장.

---

# Test UI & Live Reload

## UI Toolkit Live Reload

UXML/USS 파일 수정 시 Play 모드 재시작 없이 자동 갱신.
(Unity Editor 설정에서 활성화 가능)

## Profiler Markers

UI Toolkit 전용 프로파일러 마커로 성능 병목 추적:
- `UnityEngine.UIElements.Panel:RepaintTree`
- `UnityEngine.UIElements.Panel:UpdateSchedulers`
