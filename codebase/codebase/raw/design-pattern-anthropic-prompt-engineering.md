---
topic: design-pattern
original_type: url
source_url: https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/overview
created: 2026-04-17
---

# Prompt Engineering Overview — Anthropic Docs

## 사전 조건

이 가이드를 사용하기 전 필요한 것:
1. 사용 사례에 대한 성공 기준 명확한 정의
2. 해당 기준을 경험적으로 테스트하는 방법
3. 개선하고 싶은 첫 번째 초안 프롬프트

## 언제 프롬프트 엔지니어링을 하는가

프롬프트 엔지니어링으로 제어 가능한 성공 기준에 집중한다. 모든 실패 eval이 프롬프트 엔지니어링으로 해결되는 것은 아님. 예: 지연 시간과 비용은 다른 모델 선택으로 더 쉽게 개선 가능.

## 주요 프롬프트 엔지니어링 기법 (링크된 가이드 기준)

Claude 공식 "Prompting best practices" 페이지에서 다루는 기법들:
- **명확성과 예시**: 태스크를 명확하게 정의하고 예시 제공
- **XML 구조화**: XML 태그로 입력/출력 구조화
- **역할 프롬프팅**: LLM에게 역할 부여
- **사고 과정(Thinking)**: 추론 단계를 명시적으로 포함
- **프롬프트 체이닝**: 복잡한 태스크를 연결된 단계로 분해
- **Zero-shot / Few-shot**: 예시 없이 또는 예시와 함께 태스크 수행
- **Chain-of-Thought (CoT)**: 단계별 추론 유도

## 도구

- **Claude Console**: 프롬프트 생성기, 템플릿·변수, 프롬프트 개선기
- **GitHub 인터랙티브 튜토리얼**: https://github.com/anthropics/prompt-eng-interactive-tutorial
- **Google Sheets 튜토리얼**: 인터랙티브 스프레드시트 형식

## 핵심 인사이트

프롬프트 엔지니어링은 eval 기반 개발 사이클의 일부임:
1. 성공 기준 정의
2. eval 작성
3. 프롬프트 반복 개선
4. eval로 검증
