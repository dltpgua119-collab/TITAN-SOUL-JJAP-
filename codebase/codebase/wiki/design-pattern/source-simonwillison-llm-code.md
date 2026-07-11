---
type: source
topic: design-pattern
lang: multi
tags: [llm, design-pattern, llm-friendly-code, workflow]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/design-pattern-simonwillison-llm-code.md
source_url: https://simonwillison.net/2025/Mar/11/using-llms-for-code/
---

# Simon Willison: Here's how I use LLMs to help me write code

## 핵심 내용

25년+ 경험을 가진 개발자가 LLM을 코드 작성 도구로 사용하는 실용적 워크플로우. "생산성 곱셈기"로서의 LLM — 자율 에이전트가 아닌 숙련된 실무자의 능력 증폭 도구.

## 주요 인사이트

1. **LLM 친화적 코드 구조 원칙**: 훈련 데이터에 많이 등장하는 안정적·잘 확립된 라이브러리 선택 ("boring technology"). 컨텍스트가 핵심 — 관련 파일·코드를 적극적으로 제공
2. **사양 중심 프롬프팅**: 함수 시그니처 + 상세 동작 사양으로 LLM에게 지시 — "무엇을 할지 정확히 말하라"
3. **인간 개입 지점 인식**: LLM 능력이 한계에 닿을 때 (동시 배포 충돌 같은 복잡한 인프라 결정) 제어 재개 준비
4. **Vibe-coding for Learning**: diff 읽지 않고 "Accept All" — LLM 능력·한계에 대한 직관 구축에 유용, 프로덕션 코드에는 부적합
5. **속도가 야망을 가능하게 함**: 30분 미만으로 이전에는 정당화할 수 없었던 프로젝트 완성

## 이 소스로 생성된 페이지
- [[wiki/design-pattern/concept-llm-friendly-code|LLM 친화적 코드 구조]]
- [[wiki/design-pattern/howto-structure-code-for-llm|LLM 보조 개발을 위한 코드베이스 구조화]]

## 원문 링크
https://simonwillison.net/2025/Mar/11/using-llms-for-code/
