---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, tool-use, mcp, aci]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-mcp-introduction.md, raw/design-pattern-anthropic-building-effective-agents.md]
---

# 툴 사용 패턴 (MCP)

> LLM이 외부 시스템과 상호작용하는 방법. ACI(Agent-Computer Interface) 설계가 프롬프트 설계만큼 중요하다.

## 핵심 개념

**함수 호출 (Function Calling)**: LLM이 특정 함수를 호출해야 할 때 구조화된 출력을 생성하고, 애플리케이션이 그것을 실행한 후 결과를 LLM에 반환.

**MCP (Model Context Protocol)**: Anthropic이 주도하는 도구 사용 표준화. USB-C 비유 — AI 앱과 외부 시스템 연결의 표준 인터페이스.

## MCP 아키텍처

```text
[MCP Host (AI 앱)]
  ├── [MCP Client 1] ──── [MCP Server A: 파일시스템]
  ├── [MCP Client 2] ──── [MCP Server B: 데이터베이스]
  └── [MCP Client 3] ──── [MCP Server C: Sentry (원격)]
```

- **Host**: AI 애플리케이션 (Claude Code, VS Code 등)
- **Client**: 서버별 1:1 연결 관리 컴포넌트
- **Server**: 도구·리소스·프롬프트를 노출하는 프로그램 (로컬/원격 모두 가능)

## 서버가 노출하는 3가지 프리미티브

| 프리미티브 | 설명 | 예시 |
|---|---|---|
| **Tools** | LLM이 호출하는 실행 함수 | 파일 작업, API 호출, DB 쿼리 |
| **Resources** | 컨텍스트 정보 제공 데이터 소스 | 파일 내용, DB 레코드, API 응답 |
| **Prompts** | 재사용 가능한 상호작용 템플릿 | 시스템 프롬프트, few-shot 예시 |

## 툴 스키마 설계 (ACI)

좋은 도구 스키마의 요소:
1. **명확한 이름**: `get_weather` not `weather`
2. **상세한 description**: 도구가 하는 일, 언제 사용하는지, 엣지 케이스
3. **명확한 inputSchema**: JSON Schema로 타입·필수·기본값 정의

```json
{
  "name": "get_file_contents",
  "description": "Read the contents of a file. Use this when you need to inspect existing code. Returns error if file does not exist.",
  "inputSchema": {
    "type": "object",
    "properties": {
      "file_path": {
        "type": "string",
        "description": "Absolute path to the file (e.g., /home/user/project/main.py). Relative paths are NOT supported."
      }
    },
    "required": ["file_path"]
  }
}
```

## ACI 설계 원칙 (Anthropic)

1. **포맷 오버헤드 최소화**: 모델이 처리해야 할 불필요한 구조 줄이기
2. **예시와 엣지 케이스 포함**: 도구 정의에 사용 예시와 주의사항 명시
3. **다양한 입력으로 철저히 테스트**: LLM은 예상치 못한 인수로 도구 호출 가능
4. **poka-yoke 원칙**: 사용자 오류를 줄이도록 도구 설계
   - 예: 상대 경로 오류 방지를 위해 절대 경로만 허용

> SWE-bench 팀은 "전체 프롬프트보다 도구 최적화에 더 많은 시간을 투자했다"

## MCP 통신 흐름

```text
1. 초기화: initialize 요청 → 프로토콜 버전 협상 + 기능 발견
2. 탐색: tools/list → 사용 가능한 도구 목록
3. 실행: tools/call → 도구 실행 결과 반환
4. 알림: notifications/tools/list_changed → 도구 목록 변경 알림 (실시간)
```

**전송 방식**:
- **STDIO**: 로컬 프로세스 간 (네트워크 오버헤드 없음, 단일 클라이언트)
- **Streamable HTTP**: 원격 서버 (OAuth 인증, 다중 클라이언트 지원)

## 주의사항

- 도구 실행은 실제 부작용(파일 수정, API 호출)이 발생 — 충분한 권한 검사 필요
- LLM은 도구를 예상치 못한 순서·조합으로 호출할 수 있음
- 도구 오류 처리를 명확하게 — LLM이 오류에서 복구할 수 있도록

## 관련 페이지
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]]
- [[wiki/design-pattern/snippet-tool-use-schema|툴 스키마 정의 예시]]

## 출처
- [[wiki/design-pattern/source-mcp-introduction|Model Context Protocol Introduction]]
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
