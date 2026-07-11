---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIBuilder.html
created: 2026-04-16
---

# UI Builder

## 정의
"UI Builder lets you visually create and edit UI assets, such as UI Documents (.uxml), and StyleSheets (.uss)" — 코딩 없이 UXML/USS 생성·편집하는 시각 에디터.

## 인터페이스 구성

### StyleSheets 패널
- USS 파일 추가·재정렬·제거
- USS 셀렉터 생성·수정 (여러 요소/문서에 스타일 분배)

### Hierarchy 패널
- 현재 문서의 요소 계층 트리뷰
- UXML 정의 요소 + 런타임 생성 요소(Template 인스턴스) 표시
- 요소 선택·재정렬, 인스턴스화된 템플릿 열기, 스타일 클래스·스타일시트 확인
- 이름 속성 또는 C# 타입으로 요소 표시

### Library 패널
- 사용 가능한 UI 컴포넌트 목록
- **Standard 탭**: 기본 스타일의 내장 요소
- **Project 탭**: 커스텀 `.uxml` 에셋 + VisualElement 상속 C# 요소
- 드래그앤드롭 또는 더블클릭으로 인스턴스화

### Viewport (Canvas)
- "the output of a UI Document (UXML) on a floating resizable edit-time Canvas"
- 메뉴·설정·테마 선택·미리보기 툴바
- 팬·줌 내비게이션, 캔버스 수동 크기 조정 또는 Game 창 크기 맞춤

### Inspector 패널
선택에 따라 동적 변경:
- 요소 선택 시: 속성 및 UXML 프로퍼티
- USS 셀렉터 선택 시: 스타일 옵션
- 캔버스 설정 시: 크기·배경 커스터마이즈

### Code Preview 패널
- UXML Preview: 편집한 내용의 UXML 코드 자동 생성·표시
- USS Preview: 편집한 내용의 USS 코드 자동 생성·표시

## 선택적 패키지
- `com.unity.vectorgraphics` — VectorImage 배경
- `com.unity.2d.sprite` — 2D 스프라이트 에셋 배경
