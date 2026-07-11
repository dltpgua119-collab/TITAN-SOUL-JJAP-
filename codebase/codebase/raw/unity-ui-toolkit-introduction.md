---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/introduction-ui-toolkit.html
created: 2026-04-16
---

# Introduction to UI Toolkit

## What is UI Toolkit?
UI Toolkit은 Unity Editor 및 런타임 게임/앱의 커스텀 UI를 개발하기 위한 프레임워크. 웹 개발자에게 친숙한 웹 기반 설계 원칙을 강조.

## Key Features

**Web-Inspired Architecture**: HTML/CSS/JS 워크플로와 유사하게 구조·스타일·동작을 분리.

**Retained Mode System**: 계층적 visual tree를 메모리에 유지하며, 상태 변경 시 자동으로 렌더링·업데이트.

**Data Binding**: 데이터 변경 시 UI 자동 갱신, 반응형 UI 패턴 (React/Angular 유사).

**Flexbox-Based Layout**: CSS Flexbox 모델로 반응형 디자인, 자동 요소 배치/크기 결정.

**Comprehensive Control Library**: 버튼, 토글, 리스트/트리뷰 등 표준 UI 컨트롤 제공, 커스터마이즈 가능.

## Core Components

**UXML**: HTML/XML에서 영감받은 마크업 언어. UI 구조 정의 및 재사용 가능한 템플릿 작성 (C# 방식보다 권장).

**USS**: 시각적 스타일 및 레이아웃 규칙 적용. CSS 속성 서브셋 지원 (C# 스타일링보다 권장).

**C# Scripts**: 동작, 사용자 인터랙션, 데이터 바인딩, 커스텀 컨트롤 개발 처리.

## Supporting Tools

- **UI Debugger**: 요소 계층 및 UXML/USS 구조 검사 (브라우저 DevTools 유사)
- **UI Builder**: UXML/USS 시각 편집 환경 (코딩 없이 UI 제작)
- **Sample Library**: Window > UI Toolkit > Samples에서 접근 가능한 내장 코드 예제

## Team Role Distribution
- 디자이너: UI Builder로 시각 디자인
- 개발자: 동작 구현 및 커스텀 기능
- 테크니컬 아티스트: 성능 최적화
- QA 테스터: 기능 검증
