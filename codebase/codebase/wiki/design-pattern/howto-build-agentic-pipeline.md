---
type: how-to
topic: design-pattern
lang: multi
tags: [llm, design-pattern, agentic, orchestrator, tool-use]
created: 2026-04-17
updated: 2026-04-17
sources: [raw/design-pattern-anthropic-building-effective-agents.md, raw/design-pattern-mcp-introduction.md]
---

# 에이전틱 파이프라인 구축

> LLM이 도구를 사용해 순차적으로 행동하는 에이전틱 파이프라인을 단계별로 구축하는 가이드.

## 언제 이 가이드를 쓰는가

- 단일 LLM 호출로 처리하기 어려운 복잡한 태스크를 자동화할 때
- LLM이 외부 시스템(파일, API, DB)과 상호작용해야 할 때
- 프롬프트 체이닝이나 오케스트레이터-워커 패턴 구현 시

## 전제 조건

- LLM API 기본 사용 경험 (Anthropic Claude, OpenAI 등)
- 구현할 태스크의 단계 분해 완료

---

## 단계

### 1. 태스크 분해 및 도구 목록 정의

먼저 LLM에게 태스크를 어떤 도구로 수행할지 결정.

```python
# 예시: 코드베이스 분석 파이프라인
tools = [
    {
        "name": "read_file",
        "description": "Read file contents. Use for examining existing code.",
        "input_schema": {
            "type": "object",
            "properties": {
                "path": {"type": "string", "description": "Absolute file path"}
            },
            "required": ["path"]
        }
    },
    {
        "name": "list_directory",
        "description": "List files in directory. Use to explore project structure.",
        "input_schema": {
            "type": "object",
            "properties": {
                "path": {"type": "string"}
            },
            "required": ["path"]
        }
    },
    {
        "name": "write_file",
        "description": "Write content to file. Use only when explicitly asked to create/modify files.",
        "input_schema": {
            "type": "object",
            "properties": {
                "path": {"type": "string"},
                "content": {"type": "string"}
            },
            "required": ["path", "content"]
        }
    }
]
```

---

### 2. 도구 실행 함수 구현

```python
import subprocess
from pathlib import Path

def execute_tool(tool_name: str, tool_input: dict) -> str:
    """도구를 실행하고 결과를 문자열로 반환"""
    match tool_name:
        case "read_file":
            path = Path(tool_input["path"])
            if not path.exists():
                return f"Error: File not found: {path}"
            return path.read_text(encoding="utf-8")
        
        case "list_directory":
            path = Path(tool_input["path"])
            if not path.is_dir():
                return f"Error: Not a directory: {path}"
            files = [str(p) for p in path.iterdir()]
            return "\n".join(files)
        
        case "write_file":
            path = Path(tool_input["path"])
            path.parent.mkdir(parents=True, exist_ok=True)
            path.write_text(tool_input["content"], encoding="utf-8")
            return f"Successfully wrote to {path}"
        
        case _:
            return f"Error: Unknown tool: {tool_name}"
```

---

### 3. 오케스트레이터 루프 구현

```python
import anthropic

client = anthropic.Anthropic()

def run_agent(task: str, tools: list, max_iterations: int = 10) -> str:
    messages = [{"role": "user", "content": task}]
    
    for iteration in range(max_iterations):
        response = client.messages.create(
            model="claude-sonnet-4-6",
            max_tokens=4096,
            tools=tools,
            messages=messages
        )
        
        # 도구 사용 없이 완료
        if response.stop_reason == "end_turn":
            final_text = next(
                block.text for block in response.content
                if hasattr(block, "text")
            )
            return final_text
        
        # 도구 호출 처리
        if response.stop_reason == "tool_use":
            # 어시스턴트 응답을 대화에 추가
            messages.append({"role": "assistant", "content": response.content})
            
            # 각 도구 호출 실행
            tool_results = []
            for block in response.content:
                if block.type == "tool_use":
                    result = execute_tool(block.name, block.input)
                    tool_results.append({
                        "type": "tool_result",
                        "tool_use_id": block.id,
                        "content": result
                    })
            
            # 도구 결과를 대화에 추가
            messages.append({"role": "user", "content": tool_results})
        else:
            break
    
    return "Max iterations reached"
```

---

### 4. 오류 복구 추가

```python
def run_agent_with_retry(task: str, tools: list, max_retries: int = 3) -> str:
    for attempt in range(max_retries):
        try:
            return run_agent(task, tools)
        except anthropic.RateLimitError:
            wait_time = 2 ** attempt  # 지수 백오프
            time.sleep(wait_time)
        except anthropic.APIError as e:
            if attempt == max_retries - 1:
                raise
            continue
    raise RuntimeError("All retries exhausted")
```

---

### 5. Eval 추가

```python
def evaluate_agent_output(task: str, output: str, criteria: list[str]) -> dict:
    """LLM을 평가자로 사용 (G-Eval 방식)"""
    eval_prompt = f"""
태스크: {task}
출력: {output}

다음 기준으로 평가해 (각 항목 0-1 점수):
{chr(10).join(f"- {c}" for c in criteria)}

JSON으로 반환: {{"scores": {{}}, "overall": 0.0, "feedback": ""}}
"""
    response = client.messages.create(
        model="claude-haiku-4-5-20251001",
        max_tokens=500,
        messages=[{"role": "user", "content": eval_prompt}]
    )
    return json.loads(response.content[0].text)
```

---

## 검증 방법

1. 단순한 태스크부터 시작해 파이프라인이 올바르게 종료되는지 확인
2. 도구 오류 케이스 테스트 (파일 없음, API 실패 등)
3. max_iterations에 도달하는 케이스 확인 및 조정
4. Eval로 출력 품질 측정

## 관련 페이지
- [[wiki/design-pattern/concept-agentic-patterns|에이전틱 워크플로우 패턴]]
- [[wiki/design-pattern/concept-tool-use-pattern|툴 사용 패턴 (MCP)]]
- [[wiki/design-pattern/snippet-tool-use-schema|툴 스키마 정의 예시]]

## 출처
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
- [[wiki/design-pattern/source-mcp-introduction|Model Context Protocol Introduction]]
