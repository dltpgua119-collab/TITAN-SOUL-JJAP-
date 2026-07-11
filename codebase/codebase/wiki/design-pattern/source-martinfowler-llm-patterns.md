---
type: source
topic: design-pattern
lang: multi
tags: [llm, design-pattern, prompt-engineering, chain-of-thought]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/design-pattern-martinfowler-llm-patterns.md
source_url: https://martinfowler.com/articles/2023-chatgpt-xu-hao.html
---

# Martin Fowler: An example of LLM prompting for programming (Xu Hao)

## 핵심 내용

실제 개발 시나리오에서 ChatGPT를 사용해 self-tested 코드를 생성하는 방법. Xu Hao의 고급 프롬프트 엔지니어링 워크플로우를 Martin Fowler가 문서화.

## 주요 인사이트

1. **Generated Knowledge Prompting**: 코드 직접 요청 대신 "Don't generate code"로 먼저 마스터 플랜 요청 → 계획 정제 → 점진적 코드 생성
2. **CoT as Architecture Spec**: Chain-of-Thought 지시로 구현 전략을 프롬프트에 내장 — 아키텍처 패턴(MVVM)을 기대하는 추론 지시로 표현
3. **컨텍스트 재설정 전략**: 새 세션 시작 시 원래 전략 + 마스터 플랜으로 컨텍스트 재공급 → 분리 생성된 코드가 맞물리게 함
4. **마스터 플랜 패턴**: 큰 기능을 번호 태스크로 분해 → 코드 없이 계획 검증 → 태스크별 코드 생성
5. **보안 원칙**: 기밀 정보를 프롬프트에 절대 포함 금지

## 이 소스로 생성된 페이지
- [[wiki/design-pattern/concept-gof-vs-llm-era|GoF vs LLM 시대 패턴]]
- [[wiki/design-pattern/concept-prompt-engineering-patterns|프롬프트 엔지니어링 패턴]]
- [[wiki/design-pattern/howto-prompt-chaining|프롬프트 체이닝 구현]]

## 원문 링크
https://martinfowler.com/articles/2023-chatgpt-xu-hao.html
