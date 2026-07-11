---
type: how-to
topic: design-pattern
lang: multi
tags: [llm, design-pattern, llm-friendly-code, workflow]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-simonwillison-llm-code.md, raw/design-pattern-anthropic-building-effective-agents.md]
---

# LLM 보조 개발을 위한 코드베이스 구조화

> LLM(Claude Code, Copilot, Cursor 등)을 개발 도구로 사용할 때 최적의 결과를 얻기 위한 코드베이스 구조화 가이드.

## 언제 이 가이드를 쓰는가

- 새 프로젝트를 시작하면서 LLM 협업을 고려할 때
- 기존 코드베이스에 LLM 도구를 도입할 때
- LLM이 자꾸 잘못된 코드를 생성할 때

## 전제 조건

- LLM 코딩 도구 사용 경험 (Claude Code, Copilot, Cursor 등)
- 프로젝트의 기술 스택 결정 완료

---

## 단계

### 1. CLAUDE.md / AGENTS.md 작성

프로젝트 루트에 LLM 에이전트용 안내 파일을 만든다.

```markdown
# CLAUDE.md

## 프로젝트 개요
[한 단락으로 프로젝트 목적 설명]

## 디렉토리 구조
- src/api/ — FastAPI 엔드포인트 (라우터만, 비즈니스 로직 없음)
- src/services/ — 비즈니스 로직 (외부 의존성 추상화)
- src/models/ — SQLAlchemy 모델 + Pydantic 스키마
- tests/ — pytest 테스트

## 코딩 컨벤션
- Python 3.11+, 타입 힌트 필수
- 모든 비동기 함수에 async/await 사용
- 함수는 50줄 이하

## 금지 사항
- globals() 사용 금지
- 중앙 utils.py에 새 로직 추가 금지

## 테스트 실행
pytest tests/ -v
```

**핵심**: 금지 사항을 명시하면 LLM이 나쁜 패턴을 피할 수 있다.

---

### 2. 안정적인 라이브러리 선택

LLM이 잘 아는 라이브러리를 선택한다 ("boring technology").

```python
# 좋음: 훈련 데이터에 충분히 등장하는 표준 라이브러리
from pathlib import Path
import json
import asyncio
from typing import Optional

# 피하기: 최신 버전 API나 비주류 라이브러리
from cutting_edge_lib_v3.experimental import magic_function  # 2024년 출시
```

**기준**:
- GitHub Stars 1,000+ 이상 프로젝트
- 2년 이상 활발하게 유지보수됨
- 공식 문서가 풍부함

---

### 3. 함수 시그니처를 명시적으로 설계

```python
# 좋음: LLM에게 명확한 계약을 제공
async def send_notification(
    user_id: str,
    message: str,
    channel: Literal["email", "sms", "push"] = "email",
    priority: int = Field(ge=1, le=5, default=3)
) -> NotificationResult:
    """Send notification to user. Raises UserNotFoundError if user_id invalid."""

# 나쁨: LLM이 추측해야 함
def notify(u, m, **kwargs):
    ...
```

---

### 4. 자기완결적 모듈 경계 설계

각 모듈이 파일 하나(또는 폴더 하나)로 이해 가능하게:

```text
# 좋은 구조
auth/
  __init__.py
  models.py      # User, Session 정의
  service.py     # authenticate(), create_session()
  routes.py      # /login, /logout 엔드포인트

# 나쁜 구조 (LLM이 여러 파일을 동시에 이해해야 함)
models.py        # User, Product, Order 모두 섞임
utils.py         # 300줄의 헬퍼 함수들
main.py          # 모든 로직이 여기
```

---

### 5. 테스트 환경 구성

LLM이 코드를 생성하면 즉시 실행 가능한 테스트 환경:

```bash
# 빠른 피드백 루프
pytest tests/unit/ -v --tb=short    # 단위 테스트
pytest tests/integration/ -v        # 통합 테스트
```

테스트 파일도 자기완결적으로:
```python
# tests/test_auth_service.py
# 이 파일만 읽어도 테스트 목적이 명확해야 함
class TestAuthService:
    """인증 서비스 테스트: 로그인 성공/실패, 세션 만료"""
```

---

### 6. LLM에게 컨텍스트 제공하는 방법 표준화

```bash
# 특정 기능 요청 시 관련 파일을 함께 제공
# (Claude Code에서는 @ 참조로 파일 추가)
"src/auth/models.py 기반으로 비밀번호 재설정 기능을 service.py에 추가해줘"

# 코드베이스 질문 시
files-to-prompt src/ | claude "이 코드베이스의 인증 흐름을 설명해줘"
```

---

## 검증 방법

1. LLM에게 새 기능 추가를 요청해 첫 시도 성공률 확인
2. LLM이 금지된 패턴(globals, 중앙 utils)을 사용하는지 확인
3. LLM 생성 코드가 테스트를 통과하는지 확인

## 관련 페이지
- [[wiki/design-pattern/concept-llm-friendly-code|LLM 친화적 코드 구조]]
- [[wiki/design-pattern/howto-build-agentic-pipeline|에이전틱 파이프라인 구축]]

## 출처
- [[wiki/design-pattern/source-simonwillison-llm-code|Simon Willison: Using LLMs for Code]]
- [[wiki/design-pattern/source-anthropic-building-effective-agents|Anthropic: Building Effective Agents]]
