---
type: concept
topic: unity-ui-toolkit
lang: multi
tags: [unity, ui-toolkit, ui-builder, uxml, uss, visual-editor]
created: 2026-04-16
updated: 2026-06-11
sources: [raw/unity-ui-toolkit-ui-builder.md]
---

# UI Builder

> UXML과 USS를 코딩 없이 시각적으로 생성·편집하는 Unity 내장 에디터 도구

## 무엇인가?

"UI Builder lets you visually create and edit UI assets, such as UI Documents (.uxml), and StyleSheets (.uss)."

코딩 없이 UI 레이아웃·스타일을 디자인하고, 변경 사항의 UXML/USS 코드를 실시간으로 미리볼 수 있는 시각 편집 환경.

## 인터페이스 구성

```text
┌─────────────────────────────────────────────────────┐
│ Toolbar (메뉴, 테마 선택, 미리보기)                  │
├──────────┬──────────────────────────┬────────────────┤
│StyleSheets│         Viewport         │   Inspector    │
│          │      (Canvas)            │                │
│Hierarchy │                          │                │
│          │                          │                │
├──────────┴──────────────────────────┤                │
│          Library                    │                │
│  Standard | Project                 │                │
└─────────────────────────────────────┴────────────────┘
│  UXML Preview  │  USS Preview                         │
└──────────────────────────────────────────────────────┘
```

### StyleSheets 패널
- USS 파일 추가·재정렬·제거
- USS 셀렉터 생성·수정
- 여러 요소/문서에 스타일 분배

### Hierarchy 패널
- 현재 문서의 요소 계층 트리뷰 표시
- UXML 정의 요소 + Template 인스턴스 표시
- 요소 이름(name) 또는 C# 타입으로 표시
- 드래그로 요소 재정렬

### Library 패널
| 탭 | 내용 |
|----|------|
| **Standard** | 기본 스타일의 내장 UI 요소 |
| **Project** | 커스텀 `.uxml` 에셋 + `[UxmlElement]` C# 요소 |

→ 드래그앤드롭 또는 더블클릭으로 Hierarchy/Canvas에 인스턴스화

### Viewport (Canvas)
- UXML 출력을 실시간으로 표시하는 부동 크기 조절 가능 캔버스
- 팬·줌 내비게이션
- Game 창 크기에 맞추기 가능

### Inspector 패널
선택 대상에 따라 동적 변경:
- **요소 선택**: UXML 속성, 인라인 스타일
- **USS 셀렉터 선택**: 스타일 프로퍼티
- **캔버스**: 크기·배경 설정

### Code Preview 패널
- **UXML Preview**: 편집 내용 → UXML 코드 실시간 생성
- **USS Preview**: 편집 내용 → USS 코드 실시간 생성

## 선택적 패키지
- `com.unity.vectorgraphics` — VectorImage 배경 지원
- `com.unity.2d.sprite` — 2D 스프라이트 배경 지원

## 워크플로우
1. Window > UI Toolkit > UI Builder 열기
2. Library에서 요소를 Hierarchy/Canvas로 드래그
3. Inspector에서 속성·스타일 설정
4. StyleSheets 패널에서 USS 파일 연결
5. UXML Preview로 생성된 코드 확인
6. 저장 → .uxml/.uss 파일로 자동 저장

## 주의사항
- UI Builder로 만든 파일은 일반 UXML/USS — 코드에서도 동일하게 사용 가능
- 커스텀 컨트롤(`[UxmlElement]`)은 Library > Project 탭에 자동 등록
- 테마 선택으로 다양한 USS 테마 미리보기 가능

## 관련 페이지
- [[wiki/unity-ui-toolkit/uxml-basics|UXML 기초]]
- [[wiki/unity-ui-toolkit/uss-basics|USS 기초]]
- [[wiki/unity-ui-toolkit/custom-controls|커스텀 컨트롤]]
- [[wiki/unity-ui-toolkit/howto-get-started|UI Toolkit 시작하기]]

## 출처
- [UI Builder](https://docs.unity3d.com/6000.3/Documentation/Manual/UIBuilder.html)
- [Interface overview](https://docs.unity3d.com/6000.3/Documentation/Manual/UIB-interface-overview.html)
