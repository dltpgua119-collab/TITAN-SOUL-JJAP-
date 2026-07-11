---
type: concept
topic: design-pattern
lang: multi
tags: [llm, design-pattern, llm-friendly-code, code-structure]
created: 2026-04-17
updated: 2026-06-11
sources: [raw/design-pattern-simonwillison-llm-code.md, raw/design-pattern-eugeneyan-llm-patterns.md]
---

# LLM 친화적 코드 구조

> LLM이 코드를 탐색·수정·생성할 때 성능을 높이는 코드베이스 구조화 원칙. "인간 가독성"과 겹치지만 LLM만의 추가 고려사항이 있다.

## 인간 가독성 vs LLM 친화성

| 원칙 | 인간 가독성 | LLM 친화성 |
|---|---|---|
| 네이밍 | 명확한 의미 전달 | **동일** — 훈련 데이터에 패턴이 많을수록 유리 |
| 함수 크기 | 단일 책임, 작게 | **동일** — 컨텍스트 윈도우 내에 완결된 단위 |
| 주석 | 왜(Why)를 설명 | LLM은 코드 자체를 잘 이해 — 과도한 주석 불필요 |
| 라이브러리 선택 | 팀 친숙도 | **훈련 데이터에 많이 등장하는 안정적 라이브러리 우선** |
| 모듈 경계 | 응집도·결합도 | **파일 단위로 자기완결적인 모듈** — LLM이 파일 전체를 컨텍스트로 받음 |
| 타입 정보 | 코드 문서화 | **강력한 타입 주석** — LLM의 추론 grounding에 직접 기여 |

## LLM 친화적 코드의 핵심 원칙

### 1. "Boring Technology" 원칙

LLM은 훈련 데이터에 많이 등장한 패턴을 더 잘 이해한다.

```python
# LLM 친화적 (표준적이고 잘 알려진 패턴)
from pathlib import Path
import json

def load_config(path: Path) -> dict:
    with open(path) as f:
        return json.load(f)

# 덜 친화적 (비주류 라이브러리, 복잡한 패턴)
from mycompany.utils import SpecialFileLoader
config = SpecialFileLoader.load_yaml_with_env_substitution(path)
```

**적용**: 실험적·비주류 라이브러리보다 검증된 표준 라이브러리 선호.

---

### 2. 자기완결적 파일 단위 모듈

LLM은 파일(또는 파일들의 집합)을 컨텍스트로 받는다. 한 파일이 너무 많은 외부 의존성을 가지면 컨텍스트 전달이 어려워진다.

```text
# 좋은 구조: 기능별 자기완결 모듈
src/
  auth/
    models.py      ← User, Session 모델 + 관련 유틸리티
    service.py     ← 인증 로직 (models만 의존)
    routes.py      ← HTTP 핸들러 (service만 의존)

# 나쁜 구조: 순환 의존성, 전역 상태 혼재
src/
  utils.py         ← 모든 것이 여기 있음
  main.py          ← utils에서 무작위로 import
```

---

### 3. 명시적 함수 시그니처 + 타입 힌트

```python
# LLM 친화적: 타입이 완전한 함수 시그니처
async def download_db(
    url: str,
    max_size_bytes: int = 5 * 1024 * 1024,
    timeout_seconds: float = 30.0
) -> pathlib.Path:
    """Download database from URL. Raises ValueError if size exceeds limit."""

# 덜 친화적
def download(**kwargs):
    ...
```

LLM에게 함수를 요청할 때도 이 형식이 유효:
```text
Write a Python function with this signature:
async def download_db(url: str, max_size_bytes: int = 5 * 1024 * 1024) -> pathlib.Path

Behavior: [상세 사양]
```

---

### 4. 컨텍스트 파일 (CLAUDE.md / AGENTS.md)

LLM 에이전트에게 코드베이스를 설명하는 전용 파일.

```markdown
# CLAUDE.md

## 프로젝트 구조
- src/api/ — FastAPI 엔드포인트
- src/services/ — 비즈니스 로직
- src/models/ — SQLAlchemy 모델

## 코딩 컨벤션
- 모든 비동기 함수에 async/await 사용
- 타입 힌트 필수
- 테스트는 tests/ 디렉토리, pytest 사용

## 금지 사항
- globals() 사용 금지
- 중앙 utils.py에 로직 추가 금지
```

---

### 5. 테스트 작성 의무화

LLM이 생성한 코드를 검증하는 유일한 방법. 자동화된 테스트가 있으면 LLM의 코드 생성 → 테스트 → 수정 루프가 가능해짐.

```text
LLM 코드 생성
    ↓
테스트 실행 (실패)
    ↓
LLM에게 오류 메시지 전달
    ↓
LLM 코드 수정
    ↓
테스트 통과
```

## LLM 컨텍스트 관리

LLM에게 코드를 제공하는 방법:

```bash
# 전체 코드베이스를 단일 컨텍스트로 — codebase Q&A에 유용
files-to-prompt . -c | llm -m gemini-2.0-pro -s 'architectural overview'

# 관련 파일만 선택해 제공
cat src/auth/models.py src/auth/service.py | claude "인증 로직 리뷰해줘"
```

## 주의사항

- LLM은 훈련 컷오프 이후 라이브러리의 breaking change를 모를 수 있음 — 최신 버전 API 사용 시 공식 문서 링크 제공
- 컨텍스트가 많을수록 좋지 않음 — 관련 파일만 선택적으로 제공
- LLM 출력은 반드시 실행·테스트로 검증

## 관련 페이지
- [[wiki/design-pattern/howto-structure-code-for-llm|LLM 보조 개발을 위한 코드베이스 구조화]]
- [[wiki/design-pattern/concept-hallucination-reduction|환각 감소 패턴]]

## 출처
- [[wiki/design-pattern/source-simonwillison-llm-code|Simon Willison: Using LLMs for Code]]
- [[wiki/design-pattern/source-eugeneyan-llm-patterns|Eugene Yan: LLM Patterns]]
