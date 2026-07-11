---
topic: design-pattern
original_type: url
source_url: https://simonwillison.net/2025/Mar/11/using-llms-for-code/
created: 2026-04-17
---

# Here's how I use LLMs to help me write code — Simon Willison

**Published**: 11 March 2025

## 핵심 원칙

### 적절한 기대치 설정

LLM은 "고급 자동완성" — 토큰 시퀀스를 예측. "과잉 자신감 있는 페어 프로그래밍 도우미"로 프레임화: 빠른 조회와 지루한 태스크에 뛰어남. 실수를 하므로 맹목적 신뢰가 아닌 인간 감독 필요.

### 훈련 컷오프 날짜 고려

모델은 특정 지식 컷오프를 가짐. 라이브러리·breaking changes 친숙도에 크게 영향. 권장사항: 훈련 데이터에 많이 등장하는 안정적이고 잘 확립된 라이브러리 선택 ("boring technology" 원칙).

### 컨텍스트가 핵심

가장 중요한 기술은 대화 컨텍스트 관리 — 스레드 내 모든 메시지. 새 대화 시작 시 컨텍스트가 0으로 초기화됨. Claude Projects, Cursor, VS Code Copilot은 코드 저장소·파일 통합으로 컨텍스트 확장.

### 옵션 요청

연구 단계에서 구현 접근법 탐색: "Rust에 어떤 HTTP 라이브러리가 있나?" 핵심 요구사항이 달성 가능한지 증명하는 프로토타입 구축 필수.

### 정확히 무엇을 할지 지시

프로덕션 코드에서 "권위주의" 모드: LLM을 "디지털 인턴"처럼 대우. 함수 시그니처가 있는 상세 사양 제공이 우수한 결과를 낳음.

**예시 프롬프트 구조**:
```
Write a Python function with this signature:
async def download_db(url, max_size_bytes=5 * 1025 * 1025): -> pathlib.Path

[동작, 오류 처리, 유효성 검사에 대한 상세 사양]
```

### 모든 것을 테스트

테스트는 필수 — 코드는 배포 전 실행 검증 필요. 중요한 배포 책임에 수동 QA 필요.

### 대화임을 기억

초기 출력 거부는 실패가 아닌 반복. 리팩터링 요청 ("반복되는 코드를 함수로 분리해줘")이 조수 측 좌절 없이 작동.

### 코드를 실행하는 도구 사용

안전한 샌드박스 환경:
- **ChatGPT Code Interpreter**: 격리된 Kubernetes VM의 Python 실행
- **Claude Artifacts**: HTML/JavaScript/CSS 앱을 위한 잠긴 iframe 샌드박스
- **Claude Code**: Anthropic의 에이전트 구현
- **Cursor/Windsurf**: 에디터 통합 에이전트 (주의 필요)
- **Aider**: 오픈소스 선도 구현 (최근 80%+ 자체 작성)

### Vibe-coding for Learning

Andrej Karpathy가 만든 용어. 정확성보다 빠른 반복이 목표: diff를 읽지 않고 "Accept All", 오류 메시지 복사-붙여넣기. 모델 능력과 한계에 대한 직관 구축 가속화.

## 상세 예시: Claude Code Colophon 프로젝트

실제 프로젝트 시연 — 도구 생성 기록을 문서화하는 색인된 목차 페이지 생성.

**gather_links.py 초기 단계**:
- 프롬프트: "커밋 기록에서 HTML 파일 확인 후 URL을 JSON 구조로 추출하는 Python 스크립트 구축"
- 비용: $0.61 | 시간: 17분 18초

**GitHub Actions 자동화** (두 번째 세션):
- Python 환경 설정
- 자동화된 스크립트 실행 (gather_links.py → build_colophon.py)
- 저장소 커밋 없이 생성된 파일 게시
- 비용: $0.18 | 시간: 10분 18초

**인간 개입 지점**: 동시 배포 충돌 인식 → GitHub Pages 소스를 수동으로 "Deploy from branch"에서 "GitHub Actions"로 전환. LLM 능력이 한계에 닿을 때 인간 전문성 필요성 시연.

## 핵심 이점

### 속도가 야망을 가능하게 함

개발 가속화는 프로젝트 실현 가능성을 변환. Colophon 프로젝트 — LLM 없이는 불가능했을 — 30분 미만에 완성. 속도는 기존 계획 가속화만이 아닌 이전에 정당화할 수 없었던 프로젝트를 가능하게 함.

### LLM이 기존 전문성을 증폭

25년+ 전문 코딩 경험에 크게 의존. 프롬프트 품질은 기술, 아키텍처 패턴, 합리적 성공 가능성에 대한 깊은 친숙도를 반영.

### 보너스: 코드베이스 질의응답

완전한 컨텍스트가 제공되면 LLM은 낯선 코드 분석에 뛰어남.

```bash
files-to-prompt . -c | llm -m gemini-2.0-pro-exp-02-05 \
  -s 'architectural overview as markdown'
```

## 핵심 결론

LLM은 숙련된 실무자를 위한 생산성 곱셈기로 작동하며, 자율 에이전트가 아님. 성공을 위해 필요한 것:
- 명확한 사양
- 끊임없는 테스트
- 반복 의지
- LLM 능력이 한계에 닿을 때 제어 재개 준비

진정한 가치는 시간 제약 내에서 더 야심찬 프로젝트를 가능하게 하는 가속화된 실행을 통해 나타남.
