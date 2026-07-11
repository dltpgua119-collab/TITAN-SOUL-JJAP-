---
type: source
topic: design-pattern
lang: multi
tags: [llm, design-pattern, agentic, anthropic]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/design-pattern-anthropic-building-effective-agents.md
source_url: https://www.anthropic.com/engineering/building-effective-agents
---

# Anthropic: Building Effective Agents

## 핵심 내용

Anthropic이 수많은 LLM 에이전트 팀과 작업한 경험에서 도출한 공식 가이드. "가장 성공적인 구현은 복잡한 프레임워크나 특화 라이브러리를 사용하지 않았다. 대신 단순하고 조합 가능한 패턴으로 구축했다."

## 주요 인사이트

1. **워크플로우 vs 에이전트 구분**: 워크플로우는 사전 정의된 코드 경로, 에이전트는 LLM이 자신의 프로세스와 도구 사용을 동적으로 지시
2. **7가지 에이전틱 패턴 분류**: Augmented LLM → 프롬프트 체이닝 → 라우팅 → 병렬화 → 오케스트레이터-워커 → 평가자-최적화자 → 자율 에이전트
3. **단순성 원칙**: 가장 단순한 해결책부터 시작. 복잡성은 지연 및 비용과 교환됨
4. **ACI (Agent-Computer Interface)**: 도구 설계가 프롬프트만큼 중요. SWE-bench 팀은 도구 최적화에 프롬프트보다 더 많은 시간을 투자
5. **실제 적용 사례**: 고객 지원(명확한 성공 기준), 코딩 에이전트(자동화된 테스트로 검증 가능)

## 이 소스로 생성된 페이지
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]]
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]]
- [[wiki/design-pattern/concept-gof-vs-llm-era|GoF vs LLM 시대 패턴]]
- [[wiki/design-pattern/howto-build-agentic-pipeline|에이전틱 파이프라인 구축]]

## 원문 링크
https://www.anthropic.com/engineering/building-effective-agents
