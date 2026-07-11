---
type: snippet
topic: design-pattern
lang: md
tags: [llm, design-pattern, chain-of-thought, prompt-engineering]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-anthropic-prompt-engineering.md, raw/design-pattern-martinfowler-llm-patterns.md]
---

# CoT 프롬프트 템플릿

> XML 태그 기반 Chain-of-Thought 프롬프트 구조. Anthropic 권장 형식.

## 코드

### 기본 CoT 템플릿

```xml
<task>
다음 문제를 단계별로 풀어. 각 단계를 명확히 설명하고 최종 답을 마지막에 제시해.

[문제 내용]
</task>
```

---

### 역할 + CoT + XML 구조화 (권장 조합)

```xml
<role>
당신은 15년 경력의 소프트웨어 아키텍트입니다.
</role>

<context>
현재 시스템 상황: [시스템 설명]
기술 스택: [스택 정보]
</context>

<task>
다음 문제를 분석하고 해결책을 제시해.
코드 생성 전에 반드시 단계별 사고 과정을 보여줘.

[구체적인 문제]
</task>

<output_format>
1. 문제 분석 (2-3 문장)
2. 고려한 접근법 목록
3. 선택한 접근법과 근거
4. 구현 단계
5. 최종 코드
</output_format>
```

---

### Generated Knowledge + CoT (Xu Hao 패턴)

```text
## 1단계: 마스터 플랜 (코드 생성 없이)

기술 스택: TypeScript, React, Redux
아키텍처: MVVM

구현 가이드라인:
- Redux slice는 도메인별로 분리
- 컴포넌트는 ViewModel hook을 통해서만 상태 접근
- 모든 외부 데이터는 interface로 추상화

[기능 설명]을 구현하기 위한 마스터 플랜을 번호 목록으로 작성해.
코드는 생성하지 마.
```

```text
## 2단계: 개별 태스크 구현

위 마스터 플랜:
[1단계 출력 붙여넣기]

[구현 가이드라인 재공급]

태스크 3번을 구현해. 다른 태스크와 일관성을 유지해.
```

---

### Few-Shot CoT 템플릿

```text
다음 예시처럼 단계별로 분석해:

예시 1:
입력: [예시 입력]
사고 과정:
- 먼저 [A]를 확인
- 그 다음 [B]를 분석
- [A]와 [B]의 관계를 파악
결론: [결론]

예시 2:
입력: [예시 입력]
사고 과정:
- ...
결론: [결론]

이제 다음을 같은 방식으로 분석해:
입력: [실제 입력]
```

## 사용 방법

1. 태스크 복잡도에 따라 템플릿 선택
2. `[대괄호]` 부분을 실제 내용으로 교체
3. 역할은 구체적일수록 좋음 ("전문가"보다 "15년 경력의 SRE")
4. 출력 형식 지정은 검증 가능한 구조 생성에 유용

## 의존성

- 특정 라이브러리 불필요 — 모든 LLM API에 적용 가능
- Anthropic Claude는 XML 태그를 특히 잘 처리함

## 관련 페이지
- [[wiki/design-pattern/concept-prompt-engineering-patterns|프롬프트 엔지니어링 패턴]]
- [[wiki/design-pattern/howto-prompt-chaining|프롬프트 체이닝 구현]]

## 출처
- [[wiki/design-pattern/source-anthropic-prompt-engineering|Anthropic Prompt Engineering Overview]]
- [[wiki/design-pattern/source-martinfowler-llm-patterns|Martin Fowler: ChatGPT Test Automation Patterns]]
