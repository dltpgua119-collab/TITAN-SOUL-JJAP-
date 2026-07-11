---
topic: unity-ui-toolkit
original_type: md
created: 2026-04-17
---

# HTML 요소 → UXML 요소 대응표

## 구조 요소

| HTML | UXML | 비고 |
|------|------|------|
| `<div>` | `<VisualElement>` | 기본 컨테이너 |
| `<section>` | `<VisualElement>` | 의미적 구분 없음, 이름 속성으로 구분 |
| `<article>` | `<VisualElement>` | 동일 |
| `<main>` | `<VisualElement>` | 동일 |
| `<header>` | `<VisualElement>` | 동일 |
| `<footer>` | `<VisualElement>` | 동일 |
| `<nav>` | `<VisualElement>` | 동일 |
| `<aside>` | `<VisualElement>` | 동일 |
| `<span>` | `<VisualElement>` 또는 `<Label>` | 인라인 개념 없음. 텍스트면 Label |
| `<ul>` / `<ol>` | `<VisualElement>` + 자식 | ListView로 대체 권장 (항목 많을 때) |
| `<li>` | `<VisualElement>` 또는 `<Label>` | |
| `<table>` | `<VisualElement>` (Flexbox 행/열) | table display 미지원, Flexbox로 재현 |
| `<tr>` / `<td>` | `<VisualElement>` | |
| `<fieldset>` | `<GroupBox>` | 테두리+레이블 컨테이너 |
| `<details>` / `<summary>` | `<Foldout>` | 접기/펼치기 |
| `<hr>` | `<VisualElement>` (높이 1px, 배경색 지정) | 구분선 직접 구현 |

## 텍스트 요소

| HTML | UXML | 비고 |
|------|------|------|
| `<h1>` ~ `<h6>` | `<Label>` | font-size로 크기 구분, 시맨틱 없음 |
| `<p>` | `<Label>` | white-space: normal로 줄바꿈 |
| `<span>` | `<Label>` | 인라인 배치는 row Flexbox로 구현 |
| `<strong>` / `<b>` | `<Label>` | `-unity-font-style: bold` |
| `<em>` / `<i>` | `<Label>` | `-unity-font-style: italic` |
| `<a>` | `<Label>` + `RegisterCallback<ClickEvent>` | 하이퍼링크 직접 구현 |
| `<br>` | `\n` in Label text | 또는 별도 Label 요소 |
| `<pre>` / `<code>` | `<Label>` | 고정폭 폰트 에셋 지정 |
| `<blockquote>` | `<VisualElement>` + `<Label>` | 들여쓰기 + 좌측 border로 구현 |

## 폼 / 입력 요소

| HTML | UXML | 비고 |
|------|------|------|
| `<input type="text">` | `<TextField>` | |
| `<input type="password">` | `<TextField>` | `isPasswordField="true"` |
| `<input type="number">` | `<IntegerField>` / `<FloatField>` | |
| `<input type="checkbox">` | `<Toggle>` | |
| `<input type="radio">` | `<RadioButton>` | |
| `<input type="range">` | `<Slider>` | |
| `<input type="button">` | `<Button>` | |
| `<input type="submit">` | `<Button>` | |
| `<input type="color">` | `<ColorField>` | Editor 전용 |
| `<textarea>` | `<TextField>` | `multiline="true"` |
| `<select>` | `<DropdownField>` | |
| `<option>` | DropdownField의 choices 목록 | C#으로 choices 리스트 설정 |
| `<button>` | `<Button>` | |
| `<label>` | `<Label>` | for 연결 기능 없음 |
| `<form>` | `<VisualElement>` | form 제출 개념 없음, C#으로 처리 |
| `<progress>` | `<ProgressBar>` | |
| `<input type="file">` | 없음 | C# File 다이얼로그 직접 구현 |

## 미디어 / 그래픽 요소

| HTML | UXML | 비고 |
|------|------|------|
| `<img>` | `<Image>` | `src` 대신 C#에서 Texture2D/Sprite 할당 |
| `<svg>` | `<Image>` + VectorImage | Unity VectorImage 에셋 사용 |
| `<canvas>` | `<IMGUIContainer>` | IMGUI로 직접 드로잉 |
| `<video>` | `<Image>` + VideoPlayer | C#으로 VideoPlayer 제어 |
| `<audio>` | 없음 | C# AudioSource로 처리 |
| `<picture>` | `<Image>` | 해상도 대응은 C#으로 |
| `<figure>` | `<VisualElement>` | |
| `<figcaption>` | `<Label>` | |

## 인터랙티브 요소

| HTML | UXML | 비고 |
|------|------|------|
| `<dialog>` | `<VisualElement>` (overlay) | position: absolute + 전체 화면 |
| `<tooltip>` (title attr) | `<Tooltip>` 속성 | `tooltip="..."` 속성으로 설정 |
| `<details>` | `<Foldout>` | |
| 드래그 앤 드롭 | `Manipulator` (C#) | PointerDown/Move/Up 이벤트 조합 |
| `<datalist>` | `<DropdownField>` | |

## 복합 UI 패턴

| 웹 패턴 | Unity 대응 | 구현 방식 |
|---------|-----------|---------|
| 모달 다이얼로그 | `<VisualElement>` (position: absolute, 전체 크기) | z-order는 트리 맨 마지막에 배치 |
| 탭 UI | `<VisualElement>` + `<Button>` 목록 | 탭 클릭 시 C#으로 display 전환 |
| 아코디언 | `<Foldout>` 또는 커스텀 구현 | |
| 캐러셀 / 슬라이더 | `<ScrollView>` + C# 제어 | |
| 무한 스크롤 리스트 | `<ListView>` | virtualization 내장 |
| 툴팁 | `tooltip` 속성 | |
| 드롭다운 메뉴 | `<DropdownField>` 또는 커스텀 | |
| 토스트 / 알림 | `<VisualElement>` + USS transition | position: absolute, 애니메이션 |
| 그리드 카드 레이아웃 | `<VisualElement>` (flex-wrap: wrap) | CSS Grid 대신 Flexbox wrap 사용 |

## 지원하지 않는 HTML 요소

| HTML | 사유 | 대안 |
|------|------|------|
| `<iframe>` | 웹 콘텐츠 임베딩 개념 없음 | 없음 |
| `<script>` | C#이 스크립트 역할 | C# 코드 |
| `<style>` (인라인) | USS 파일로 분리 | .uss 파일 |
| `<link>` | USS 파일 참조는 C#/UXML에서 | UXML `<Style>` 태그 |
| `<meta>` | 웹 메타 개념 없음 | 없음 |
| `<map>` / `<area>` | 이미지 맵 없음 | C# PointerEvent로 구현 |
| `<embed>` / `<object>` | 없음 | 없음 |

## UXML 전용 요소 (HTML 대응 없음)

| UXML | 용도 |
|------|------|
| `<MinMaxSlider>` | 최솟값·최댓값 범위 슬라이더 |
| `<BoundsField>` | 3D 바운딩 박스 입력 (Editor) |
| `<CurveField>` | AnimationCurve 편집기 (Editor) |
| `<ObjectField>` | Unity 에셋 참조 필드 (Editor) |
| `<ListView>` | 가상화 스크롤 리스트 |
| `<TreeView>` | 계층형 트리 리스트 |
| `<TabbedView>` | 탭 컨테이너 (실험적) |
| `<IMGUIContainer>` | IMGUI 코드 임베딩 |
| `<TemplateContainer>` | UXML 템플릿 인스턴스 |
