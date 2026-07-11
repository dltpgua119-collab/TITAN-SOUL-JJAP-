---
topic: design-pattern
original_type: url
source_url: https://martinfowler.com/articles/2023-chatgpt-xu-hao.html
created: 2026-04-17
---

# An example of LLM prompting for programming — Martin Fowler / Xu Hao

**Published**: 13 April 2023 | **Updated**: 20 April 2023

## 개요

Xu Hao가 ChatGPT를 사용해 self-tested 코드를 생성하는 방법을 Martin Fowler가 문서화. 실제 개발 시나리오에 적용된 고급 프롬프트 엔지니어링 기법 시연.

## 핵심 프롬프팅 기법

### Chain of Thought Prompting
프롬프트 내에 구현 전략을 내장해 LLM의 추론 과정을 가이드. 원하는 아키텍처 패턴을 "ChatGPT가 따를 Chain of Thought 지시"로 설명.

### Generated Knowledge Prompting
최종 코드를 즉시 요청하는 대신 먼저 계획을 요청. "먼저 LLM에게 문제에 대한 유용한 정보를 생성하게 한 다음, 그 정보를 LLM에 다시 피드해 최종 결과물 생성."

## 초기 컨텍스트 프롬프트 구조

시작 프롬프트에 포함된 내용:
- **기술 스택 명시**: TypeScript, React, Redux, Konva.js, Vitest
- **아키텍처 패턴**: MVVM (두 가지 뷰모델 타입: 공유·로컬)
- **구현 가이드라인**: Redux slices, hooks, 테스트 패턴을 다루는 5가지 번호 전략
- **테스트 패턴**: `describe` 블록, 데이터 기반 테스트, 인터페이스 기반 모킹 3가지 규칙

## 실용적 워크플로우

1. **포괄적 가이드라인으로 컨텍스트 확립**
2. **코드 없이 마스터 플랜 요청** ("Don't generate code" 사용)
3. **그룹화와 명명을 위한 추가 프롬프트로 플랜 정제**
4. **특정 마스터 플랜 태스크를 위해 점진적으로 코드 생성**
5. **대상 재작성을 통한 반복적 개선**

## 실제 예시: Awareness Layer 기능

원격 사용자 인식(커서, 이름, 상태)을 표시하는 기능 구현 워크스루.

마스터 플랜은 12개의 번호 태스크를 포함. ChatGPT는 초기에 코드 없이 태스크를 제공.

생성된 코드 포함:
- awareness 상태를 관리하는 Redux slices
- 상태 변화를 캡슐화하는 ViewModel 인터페이스
- Konva 레이어가 있는 컴포넌트 구조

## 컨텍스트 윈도우 한계

토큰 한계에서 세 가지 도전:
1. **불완전한 생성**: "continue" 프롬프트로 해결
2. **치명적 오류**: 새 대화 시작 필요
3. **컨텍스트 손실**: 원래 전략과 마스터 플랜으로 컨텍스트 재설정

"컨텍스트의 chain of thought가 별도 세션에서 생성된 코드가 맞물리는 데 결정적이었다."

## 핵심 교훈

- 프롬프트 구성이 LLM 출력 품질에 크게 영향
- 마스터 플랜을 번호 항목으로 분해하면 관리 용이
- 아키텍처 가이드라인으로 대화 시작 → 추론 요청 → 결합된 결과물 생성
- LLM을 "주니어 파트너처럼" 대하면 효과적인 반복 가능

## 보안 고려사항

"잠재적으로 기밀일 수 있는 것은 절대 프롬프트에 넣지 말 것. 보안 위험이 됨."
