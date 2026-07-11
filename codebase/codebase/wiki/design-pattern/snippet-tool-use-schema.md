---
type: snippet
topic: design-pattern
lang: json
tags: [llm, design-pattern, tool-use, mcp, json-schema]
created: 2026-04-17
updated: 2026-04-17
sources: [raw/design-pattern-mcp-introduction.md, raw/design-pattern-anthropic-building-effective-agents.md]
---

# 툴 스키마 정의 예시

> JSON Schema 기반 MCP / Anthropic 툴 스키마 정의. 좋은 도구 정의는 LLM이 올바르게 사용하도록 유도한다.

## 코드

### Anthropic 함수 호출 스키마

```python
tools = [
    {
        "name": "read_file",
        "description": (
            "Read the contents of a file at the specified path. "
            "Use this to inspect existing code before making changes. "
            "Returns file contents as a string. "
            "Raises FileNotFoundError if the file does not exist."
        ),
        "input_schema": {
            "type": "object",
            "properties": {
                "path": {
                    "type": "string",
                    "description": (
                        "Absolute path to the file "
                        "(e.g., /home/user/project/src/main.py). "
                        "Relative paths are NOT supported."
                    )
                }
            },
            "required": ["path"]
        }
    },
    {
        "name": "execute_bash",
        "description": (
            "Run a bash command. "
            "Use for running tests, building projects, or checking outputs. "
            "Do NOT use for destructive operations (rm -rf, git push --force). "
            "Returns stdout and stderr."
        ),
        "input_schema": {
            "type": "object",
            "properties": {
                "command": {
                    "type": "string",
                    "description": "The bash command to execute"
                },
                "working_dir": {
                    "type": "string",
                    "description": "Absolute path to working directory. Defaults to project root."
                }
            },
            "required": ["command"]
        }
    },
    {
        "name": "search_code",
        "description": (
            "Search for a pattern in the codebase using regex. "
            "Use to find function definitions, usages, or imports. "
            "Returns file paths and line numbers of matches."
        ),
        "input_schema": {
            "type": "object",
            "properties": {
                "pattern": {
                    "type": "string",
                    "description": "Regex pattern to search for"
                },
                "file_glob": {
                    "type": "string",
                    "description": "Glob pattern to filter files (e.g., '**/*.py'). Optional.",
                    "default": "**/*"
                }
            },
            "required": ["pattern"]
        }
    }
]
```

---

### MCP 서버 툴 정의 (Python SDK)

```python
from mcp.server import Server
from mcp.types import Tool, TextContent

server = Server("my-server")

@server.list_tools()
async def list_tools() -> list[Tool]:
    return [
        Tool(
            name="get_weather",
            title="Weather Information",
            description=(
                "Get current weather for a location. "
                "Returns temperature, conditions, and humidity. "
                "Use metric units for most locations."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "location": {
                        "type": "string",
                        "description": "City name or 'lat,lon' coordinates"
                    },
                    "units": {
                        "type": "string",
                        "enum": ["metric", "imperial"],
                        "default": "metric",
                        "description": "Temperature units"
                    }
                },
                "required": ["location"]
            }
        )
    ]

@server.call_tool()
async def call_tool(name: str, arguments: dict) -> list[TextContent]:
    if name == "get_weather":
        location = arguments["location"]
        units = arguments.get("units", "metric")
        # 실제 날씨 API 호출
        result = fetch_weather(location, units)
        return [TextContent(type="text", text=result)]
    raise ValueError(f"Unknown tool: {name}")
```

---

### 좋은 설명 vs 나쁜 설명 비교

```python
# 나쁜 예 — LLM이 언제/어떻게 사용할지 알기 어려움
{
    "name": "file_op",
    "description": "File operations",
    "input_schema": {"type": "object", "properties": {"f": {"type": "string"}}}
}

# 좋은 예 — 언제 사용하는지, 제약 조건, 예시 포함
{
    "name": "read_file",
    "description": (
        "Read a file's contents. "
        "Use BEFORE modifying any file to understand existing code. "
        "Use absolute paths only (/home/user/project/file.py, not ./file.py). "
        "Returns error if file doesn't exist — check with list_directory first."
    ),
    "input_schema": {
        "type": "object",
        "properties": {
            "path": {
                "type": "string",
                "description": "Absolute file path"
            }
        },
        "required": ["path"]
    }
}
```

## 사용 방법

1. `name`: 동사_명사 형태로 명확하게 (`read_file`, `search_code`, `send_email`)
2. `description`: "Use this when..." 형태로 용도 명시 + 제약 조건 + 금지 사항
3. 파라미터 설명에 구체적 예시 값 포함
4. 선택적 파라미터에 `default` 값 항상 포함

## 의존성

- Anthropic Python SDK: `pip install anthropic`
- MCP Python SDK: `pip install mcp`
- MCP JSON Schema 사양: https://modelcontextprotocol.io/specification/latest

## 관련 페이지
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]]
- [[wiki/design-pattern/howto-build-agentic-pipeline|에이전틱 파이프라인 구축]]

## 출처
- [[wiki/design-pattern/source-mcp-introduction|Model Context Protocol Introduction]]
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
