# Coding Inventory — 참조 전용 코딩 지식 위키

이 저장소는 **LLM이 탐색해서 다른 프로젝트 개발에 활용하는 참조 전용(read-only) 코딩 지식 위키**다.
이 위키에는 더 이상 지식을 추가하지 않는다. 오직 **읽고 참조**하는 용도로만 사용한다.

개발 중 "이걸 어떻게 쓰지?", "이 패턴이 뭐였지?", "이 에러 왜 나지?"의 근거 자료를 여기서 찾아,
그 지식을 **다른 프로젝트**에 적용하는 것이 목적이다.

---

## 세션 시작 시 읽는 순서

1. 이 파일 (`CLAUDE.md`) — 탐색 규칙
2. `index.md` — 전체 페이지 카탈로그(탐색 진입점)
3. 필요한 주제의 `wiki/{topic}/_overview.md` — 해당 주제 지도

---

## 다루는 주제 (topic)

- `unity-ui-toolkit` — Unity UI Toolkit (UXML/USS/C#, 데이터 바인딩, 커스텀 컨트롤, HTML/CSS → UXML/USS 변환)
- `design-pattern` — LLM 시대의 설계·에이전틱 워크플로우·프롬프트 엔지니어링·RAG·MCP 툴 사용 패턴

---

## 디렉토리 구조

```
/
├── CLAUDE.md            ← 이 파일 (탐색 규칙)
├── index.md             ← 전체 페이지 카탈로그
├── log.md               ← 과거 구축 활동 기록 (참고용)
├── raw/                 ← 원본 소스 스냅샷 (불변)
│   └── {slug}.md
└── wiki/
    └── {topic}/
        ├── _overview.md ← 주제 개요 + 페이지·소스 목록
        └── {slug}.md    ← 개별 페이지
```

- **`wiki/`**: 합성·구조화된 지식. 실제 참조 대상.
- **`raw/`**: wiki 페이지가 요약하기 전의 원본 소스 스냅샷. 근거 원문을 확인할 때만 연다.

---

## 탐색 방법

- `index.md`는 topic 섹션별로 페이지를 나열하며, 각 항목에 `type · lang` 태그와 한 줄 설명이 붙어 있다.
  페이지를 직접 열지 않고도 topic·type·lang으로 필터링할 수 있다.
  - 형식: `- [[wiki/{topic}/{slug}|제목]] — {type} · {lang} — 설명`
- 페이지 간 연결은 `[[경로|제목]]` (Obsidian 위키링크) 형식이다.
- **source 페이지는 `index.md`에 없다.** 원본 요약이 필요하면 `wiki/{topic}/_overview.md`의 Sources 목록에서 찾는다.
- 에러 해결이 필요하면 `type: error` 페이지를 우선 탐색한다.

### 페이지 type 의미 (필터 기준)

| type | 내용 |
|---|---|
| `concept` | 개념·API·동작 원리 |
| `how-to` | 단계별 절차 가이드 |
| `snippet` | 복사해서 바로 쓰는 코드 조각 |
| `error` | 에러 → 원인 → 해결책 |
| `source` | 원본 소스 요약 (index 미등록, `_overview`로만 접근) |
| `overview` | 주제 폴더 개요 (`_overview.md`) |

각 페이지 상단 YAML frontmatter의 `type`·`topic`·`lang`·`tags`·`sources`로 성격과 근거 소스를 파악한다.

---

## 규칙

- 이 위키는 **참조 전용**이다. 페이지·`index.md`·`log.md`·`raw/`를 수정하거나 새 지식을 추가하지 않는다.
- 여기서 얻은 지식은 **다른 프로젝트**에 적용한다. 이 저장소 자체를 개발 대상으로 삼지 않는다.
- 코드 블록의 언어 태그(` ```cs `, ` ```uss `, ` ```uxml ` 등)를 읽어 대상 언어를 판별한다.
