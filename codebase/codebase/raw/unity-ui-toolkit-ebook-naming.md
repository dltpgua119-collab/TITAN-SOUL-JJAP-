---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/naming-conventions.html
created: 2026-04-16
---

# UI Toolkit Naming Conventions (E-Book Chapter)

## 핵심 권장: BEM (Block Element Modifier)

CSS/웹 개발 표준 BEM 네이밍 컨벤션 강력 권장.

## BEM 구조
형식: `block-name__element-name--modifier-name`
예시: `navbar-menu__shop-button--small`

### 구성 요소
1. **Block** (`block-name`): 최상위 UI 컴포넌트 (navbar, character-stats-panel 등). 일반 요소는 블록 생략 가능 (`button--small`).
2. **Element** (`element-name`): 블록의 자식 또는 부분. 예: `navbar-menu__shop-button`의 `shop-button`.
3. **Modifier** (`--modifier-name`): 변형 또는 상태 (pressed, selected, 크기 변형).

## 핵심 가이드라인
- **Kebab case** 사용 (하이픈 구분)
- 블록/요소 연결: 이중 언더스코어 `__`
- 수정자 연결: 이중 대시 `--`
- 허용 문자: 라틴 문자, 숫자, 대시만
- "가독성 우선, 간결성 차선"
- **의미론적** 네이밍 (프레젠테이션 기반 아님): `button--quit` ✅ vs `button--red` ❌
- 아트 에셋(스프라이트, 텍스처)까지 컨벤션 확장
- 멀티 프로젝트 환경: 클래스 충돌 방지를 위해 접두사 고려
- 생성자에서 `AddToClassList()`로 USS 클래스 적용

## 추가 팁
- 짧지만 설명적인 이름 유지
- 타입 이름보다 역할/관계 강조
- 변경 가능한 속성 기반 이름/수정자 지양
- 코드와 에셋 전반에 일관성 유지
