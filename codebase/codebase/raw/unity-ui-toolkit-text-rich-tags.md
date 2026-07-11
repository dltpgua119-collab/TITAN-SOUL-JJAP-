---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-supported-tags.html
created: 2026-04-16
---

# Rich Text Tags — 지원 태그 목록

## 기본 포맷

| 태그 | 기능 |
|------|------|
| `<b>text</b>` | 굵게 |
| `<i>text</i>` | 기울임 |
| `<u>text</u>` | 밑줄 |
| `<s>text</s>` | 취소선 |

## 색상 & 투명도

```
<color="red">red text</color>
<color=#FF0000>hex color</color>
<alpha=#FF>full opacity</alpha>
<mark=#ffff00aa>highlight</mark>
```

## 크기 & 간격

```
<size=24px>size in px</size>
<size=150%>size as percent</size>
<cspace=1em>character spacing</cspace>
<mspace=2.75em>monospace</mspace>
```

## 레이아웃 & 정렬

```
<br>  (줄바꿈)
<align="center">centered</align>  <!-- left, center, right, justified, flush -->
<indent=15%>indented</indent>
<line-indent=15%>line indent</line-indent>
<margin=5em>margin</margin>
<margin-left=5em>left margin only</margin-left>
```

## 고급 기능

```
<a href="https://unity.com">link text</a>   (최대 256자)
<link="ID">linked segment</link>
<sprite name="spriteName">   (스프라이트 삽입)
<font="Impact SDF">custom font</font>
<gradient="Light to Dark Green - Vertical">gradient</gradient>
<rotate="45">rotated text</rotate>
<voffset=1em>vertical offset</voffset>
```

## 대소문자 변환

```
<uppercase>ALL CAPS</uppercase>
<lowercase>all lowercase</lowercase>
<allcaps>SAME AS UPPERCASE</allcaps>
<smallcaps>SMALL CAPS</smallcaps>
```

## 특수 태그

```
<noparse><b>not parsed</b></noparse>   (태그 파싱 방지)
<style="H1">custom style</style>       (미리 정의된 스타일 적용)
```

## 주의사항

- `*` 표시 태그는 Advanced Text Generator(ATG)에서 제한적 지원
- 하이퍼링크(`<a>`)와 링크 ID(`<link>`)는 최대 256자 제한
