---
type: concept
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, uxml, uss, architecture]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-introduction.md, raw/unity-ui-toolkit-main.md]
---

# UI Toolkit 소개

> Unity Editor 및 런타임 UI 개발을 위한 웹 기반 프레임워크 — UXML(구조) + USS(스타일) + C#(동작)

## 무엇인가?

UI Toolkit은 "UI 개발을 위한 기능, 리소스, 도구의 모음"으로, Unity Editor 확장과 런타임 게임/앱 UI 모두를 개발할 수 있다.

웹 개발자에게 친숙한 설계 원칙 기반 — HTML/CSS/JS 워크플로와 유사하게 **구조·스타일·동작을 분리**.

## 핵심 특징

| 특징 | 설명 |
|------|------|
| **Retained Mode** | 계층적 visual tree를 메모리에 유지. 상태 변경 시 자동 렌더링·업데이트 |
| **Data Binding** | 데이터 변경 시 UI 자동 갱신 (React/Angular 유사 반응형 패턴) |
| **Flexbox Layout** | CSS Flexbox 모델로 반응형 레이아웃, 자동 요소 배치/크기 결정 |
| **Control Library** | 버튼·토글·리스트뷰 등 표준 UI 컨트롤 제공, 커스터마이즈 가능 |

## 핵심 컴포넌트

### UXML
- HTML/XML에서 영감받은 마크업 언어
- UI 구조 정의 및 재사용 가능한 템플릿 작성
- **C# 방식보다 권장** — 분리와 재사용성 향상

```xml
<ui:UXML xmlns:ui="UnityEngine.UIElements">
    <ui:Label text="Hello World" />
    <ui:Button text="Click Me" name="myButton"/>
</ui:UXML>
```

### USS (Unity StyleSheets)
- CSS 속성 서브셋 지원
- 시각적 스타일 및 레이아웃 규칙 적용
- **C# 스타일링보다 권장** — 관심사 분리

```uss
.my-button {
    background-color: #4CAF50;
    color: white;
    padding: 8px 16px;
}
```

### C# Scripts
- 동작 처리, 사용자 인터랙션
- 데이터 바인딩 설정
- 커스텀 컨트롤 개발

## 보조 도구

| 도구 | 역할 |
|------|------|
| **UI Builder** | UXML/USS 시각 편집 환경 (코딩 없이 UI 제작) |
| **UI Debugger** | 요소 계층 및 UXML/USS 구조 검사 (브라우저 DevTools 유사) |
| **Sample Library** | Window > UI Toolkit > Samples에서 내장 코드 예제 접근 |

## 팀 역할 분배
- **디자이너**: UI Builder로 시각 디자인
- **개발자**: 동작 구현 및 커스텀 기능
- **테크니컬 아티스트**: 성능 최적화
- **QA 테스터**: 기능 검증

## 주요 기능 영역
1. **Visual Development** — UI Builder로 UXML/USS 에셋 그래픽 편집
2. **Structural Foundation** — UXML 마크업 또는 C# 코드로 UI 구성
3. **Styling System** — USS로 시각적 표현
4. **Interactivity** — 사용자 입력·포인터·드래그앤드롭 이벤트
5. **Rendering** — Unity 그래픽 레이어 위 커스텀 렌더링
6. **Data Integration** — 프로퍼티와 UI 컨트롤 바인딩
7. **Typography** — 폰트 에셋 및 텍스트 스타일링
8. **Quality Assurance** — 테스팅·디버깅 도구

## 관련 페이지
- [[wiki/unity-ui-toolkit/ui-systems-comparison|UI 시스템 비교]]
- [[wiki/unity-ui-toolkit/visual-tree|Visual Tree]]
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]]

## 출처
- [Introduction to UI Toolkit](https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/introduction-ui-toolkit.html)
- [UI Toolkit Main Page](https://docs.unity3d.com/6000.3/Documentation/Manual/UIElements.html)
