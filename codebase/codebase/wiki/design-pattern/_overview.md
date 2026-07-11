---
type: overview
topic: design-pattern
lang: multi
tags: [llm, design-pattern, agentic, prompt-engineering, architecture]
created: 2026-04-17
updated: 2026-06-11
---

# LLM 친화적 디자인 패턴

> LLM이 개발 워크플로우에 참여할 때 유리한 코드 구조·시스템 설계·프롬프트 패턴의 집합

## 이 주제에서 다루는 것

GoF 패턴은 인간의 유지보수·확장성을 위해 설계되었다. LLM이 코드베이스를 탐색하고, 수정하고, 생성하는 워크플로우에서는 다른 패턴이 유리하다. 이 토픽은 다음 질문에 답한다:

- LLM 친화적 패턴이란 무엇인가? GoF 패턴과 어떻게 다른가?
- LLM이 연속된 행동을 취하는 시스템의 표준 패턴은?
- 어떤 코드 구조가 LLM 환각 위험을 낮추는가?
- 프롬프트를 GoF처럼 이름 붙인 재사용 패턴으로 분류할 수 있는가?
- LLM 시스템이 프로덕션 스케일로 성장할 때 적용하는 아키텍처 패턴은?

## 페이지 목록

### Concept
- [[wiki/design-pattern/concept-gof-vs-llm-era|GoF vs LLM 시대 패턴]] — concept · multi — GoF 패턴과 LLM 친화적 패턴의 차이, 새로 등장한 패턴
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]] — concept · multi — 프롬프트 체이닝·라우팅·병렬화·오케스트레이터-워커·자율 에이전트
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]] — concept · multi — MCP 아키텍처, 함수 호출 패턴, ACI 설계
- [[wiki/design-pattern/concept-prompt-engineering-patterns|프롬프트 엔지니어링 패턴]] — concept · multi — zero-shot·few-shot·CoT·XML 구조화·역할 프롬프팅
- [[wiki/design-pattern/concept-rag-pattern|RAG 패턴]] — concept · multi — RAG 아키텍처, 청킹 전략, 검색 품질, fine-tuning과의 비교
- [[wiki/design-pattern/concept-llm-friendly-code|LLM 친화적 코드 구조]] — concept · multi — LLM 가독성 vs 인간 가독성, 코드베이스 구조화 원칙
- [[wiki/design-pattern/concept-hallucination-reduction|환각 감소 패턴]] — concept · multi — 환각 위험을 낮추는 코드 구조·시스템 패턴 (Guardrails, Evals, Defensive UX)

### How-To
- [[wiki/design-pattern/howto-structure-code-for-llm|LLM 보조 개발을 위한 코드베이스 구조화]] — how-to · multi — CLAUDE.md 작성, 모듈 경계, 네이밍 전략
- [[wiki/design-pattern/howto-build-agentic-pipeline|에이전틱 파이프라인 구축]] — how-to · multi — 도구 정의, 오케스트레이터 루프, 오류 복구, eval 추가
- [[wiki/design-pattern/howto-prompt-chaining|프롬프트 체이닝 구현]] — how-to · multi — 복잡한 태스크 분해, 출력 전달, 단계별 오류 처리

### Snippet
- [[wiki/design-pattern/snippet-chain-of-thought-prompt|CoT 프롬프트 템플릿]] — snippet · md — XML 태그 기반 Chain-of-Thought 프롬프트 구조
- [[wiki/design-pattern/snippet-tool-use-schema|툴 스키마 정의 예시]] — snippet · json — JSON Schema 기반 MCP/Anthropic 툴 스키마

## Sources

- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]] — 에이전틱 워크플로우 패턴 공식 가이드
- [[wiki/design-pattern/source-eugeneyan-llm-patterns|Eugene Yan: LLM Patterns]] — RAG·Guardrails·Evals·Caching 패턴 카탈로그
- [[wiki/design-pattern/source-anthropic-prompt-engineering|Anthropic Prompt Engineering Overview]] — 프롬프트 엔지니어링 패턴 공식 문서
- [[wiki/design-pattern/source-mcp-introduction|Model Context Protocol Introduction]] — MCP 툴 사용 아키텍처
- [[wiki/design-pattern/source-martinfowler-llm-patterns|Martin Fowler: ChatGPT Test Automation Patterns]] — GoF vs LLM 시대 패턴 비교
- [[wiki/design-pattern/source-azure-genai-gateway|Azure GenAI Gateway]] — 프로덕션 스케일 LLM 아키텍처 패턴
- [[wiki/design-pattern/source-simonwillison-llm-code|Simon Willison: Using LLMs for Code]] — LLM 가독성 높은 코드 구조

## 관련 주제
- [[wiki/unity-ui-toolkit/_overview|Unity UI Toolkit]] — UI 설계 패턴 (GoF 패턴 일부 적용)
