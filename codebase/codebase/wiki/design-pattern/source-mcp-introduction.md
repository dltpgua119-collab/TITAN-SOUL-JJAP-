---
type: source
topic: design-pattern
lang: multi
tags: [llm, design-pattern, mcp, tool-use, architecture]
created: 2026-04-17
updated: 2026-04-17
source_path: raw/design-pattern-mcp-introduction.md
source_url: https://modelcontextprotocol.io/introduction
---

# Model Context Protocol Introduction & Architecture

## 핵심 내용

MCP는 AI 애플리케이션을 외부 시스템에 연결하는 오픈소스 표준. USB-C 비유: 전자기기에 표준화된 연결 방식을 제공하듯, MCP는 AI 앱에 외부 시스템 연결 표준을 제공.

## 주요 인사이트

1. **클라이언트-서버 아키텍처**: Host(AI 앱) → Client(연결 관리) → Server(컨텍스트 제공). Host는 Server마다 별도 Client 인스턴스 생성
2. **3개 서버 프리미티브**: Tools(실행 함수), Resources(데이터 소스), Prompts(재사용 템플릿)
3. **2개 전송 메커니즘**: STDIO(로컬, 네트워크 오버헤드 없음) / Streamable HTTP(원격, OAuth 인증)
4. **동적 도구 발견**: `tools/list`로 사용 가능한 도구 동적 조회, `notifications/tools/list_changed`로 실시간 업데이트
5. **JSON-RPC 2.0 기반**: 표준화된 메시지 구조로 라이프사이클(초기화→발견→실행→알림) 관리

## 이 소스로 생성된 페이지
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]]
- [[wiki/design-pattern/snippet-tool-use-schema|툴 스키마 정의 예시]]

## 원문 링크
https://modelcontextprotocol.io/introduction
