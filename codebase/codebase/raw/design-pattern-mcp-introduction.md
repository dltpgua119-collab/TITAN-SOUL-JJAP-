---
topic: design-pattern
original_type: url
source_url: https://modelcontextprotocol.io/introduction
created: 2026-04-17
---

# Model Context Protocol (MCP) — Introduction & Architecture

## MCP란?

MCP(Model Context Protocol)는 AI 애플리케이션을 외부 시스템에 연결하기 위한 오픈소스 표준.

AI 애플리케이션(Claude, ChatGPT 등)이 다음에 연결 가능:
- 데이터 소스 (로컬 파일, 데이터베이스)
- 도구 (검색 엔진, 계산기)
- 워크플로우 (특화된 프롬프트)

USB-C 포트 비유: 전자기기에 표준화된 연결 방식을 제공하듯, MCP는 AI 애플리케이션에 외부 시스템 연결 표준을 제공.

## MCP 아키텍처

### 참여자 (Participants)

클라이언트-서버 아키텍처:
- **MCP Host**: AI 애플리케이션 (Claude Code, Claude Desktop 등)
- **MCP Client**: MCP 서버에 대한 연결을 유지하는 컴포넌트 (Host가 Server마다 하나씩 생성)
- **MCP Server**: MCP 클라이언트에 컨텍스트를 제공하는 프로그램

예시: VS Code가 Host → Sentry MCP Server 연결 시 Client 인스턴스 생성 → Filesystem Server 추가 연결 시 별도 Client 인스턴스 생성.

### 레이어 (Layers)

**데이터 레이어 (Data Layer)**:
- JSON-RPC 2.0 기반 통신 프로토콜
- 라이프사이클 관리
- 핵심 프리미티브: tools, resources, prompts, notifications

**전송 레이어 (Transport Layer)**:
- STDIO 전송: 로컬 프로세스 간 표준 입출력 스트림 (네트워크 오버헤드 없음)
- Streamable HTTP 전송: HTTP POST + Server-Sent Events (원격 서버, OAuth 인증)

## 핵심 프리미티브 (Primitives)

### 서버가 노출하는 프리미티브
- **Tools**: AI가 호출할 수 있는 실행 함수 (파일 작업, API 호출, DB 쿼리)
- **Resources**: 컨텍스트 정보 제공 데이터 소스 (파일 내용, DB 레코드, API 응답)
- **Prompts**: 재사용 가능한 상호작용 템플릿 (시스템 프롬프트, few-shot 예시)

각 프리미티브는 탐색(`*/list`), 검색(`*/get`), 실행(`tools/call`) 메서드를 가짐.

### 클라이언트가 노출하는 프리미티브
- **Sampling**: 서버가 클라이언트의 AI 앱에서 LLM 완성을 요청
- **Elicitation**: 서버가 사용자에게 추가 정보 요청
- **Logging**: 서버가 디버깅·모니터링을 위해 클라이언트에 로그 메시지 전송

### 유틸리티 프리미티브
- **Tasks (실험적)**: 장기 실행 작업을 위한 내구성 있는 실행 래퍼

## 라이프사이클 (Lifecycle)

1. **초기화**: 클라이언트가 `initialize` 요청 전송 → 프로토콜 버전 협상, 기능 발견
2. **준비**: 클라이언트가 `notifications/initialized` 전송
3. **탐색**: 클라이언트가 `tools/list`, `resources/list` 등으로 사용 가능한 기능 조회
4. **실행**: `tools/call`로 도구 실행
5. **알림**: 서버가 상태 변경 시 `notifications/tools/list_changed` 등 전송

## JSON-RPC 2.0 메시지 예시

```json
// 도구 실행 요청
{
  "jsonrpc": "2.0",
  "id": 3,
  "method": "tools/call",
  "params": {
    "name": "weather_current",
    "arguments": {
      "location": "Seoul",
      "units": "metric"
    }
  }
}

// 도구 스키마 예시 (tools/list 응답)
{
  "name": "weather_current",
  "title": "Weather Information",
  "description": "Get current weather for any location",
  "inputSchema": {
    "type": "object",
    "properties": {
      "location": { "type": "string", "description": "City name or coordinates" },
      "units": { "type": "string", "enum": ["metric", "imperial", "kelvin"] }
    },
    "required": ["location"]
  }
}
```

## 광범위한 에코시스템 지원

Claude, ChatGPT, VS Code, Cursor, MCPJam 등 다양한 클라이언트·서버에서 지원.
