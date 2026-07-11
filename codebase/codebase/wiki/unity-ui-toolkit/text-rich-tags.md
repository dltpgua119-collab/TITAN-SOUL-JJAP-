---
type: concept
topic: unity-ui-toolkit
lang: cs/uss
tags: [unity, ui-toolkit, text, rich-text, tags, markup]
created: 2026-04-16
updated: 2026-04-16
sources: [raw/unity-ui-toolkit-text-rich-tags.md]
---

# Rich Text Tags 레퍼런스

> Label, Button 등 텍스트 요소에서 `<tag>` 마크업으로 인라인 스타일을 적용한다.

## 활성화 방법

```cs
label.enableRichText = true; // 기본값 true
```

---

## 기본 포맷

```xml
<b>굵게</b>
<i>기울임</i>
<u>밑줄</u>
<s>취소선</s>
```

---

## 색상 & 투명도

```xml
<color="red">named color</color>
<color=#FF0000>hex color</color>
<color=#FF000080>hex + alpha</color>
<alpha=#80>반투명</alpha>
<mark=#ffff00aa>형광 하이라이트</mark>
```

---

## 크기 & 간격

```xml
<size=24px>픽셀 크기</size>
<size=150%>퍼센트 크기</size>
<cspace=1em>문자 간격 넓힘</cspace>
<mspace=2.75em>고정폭 간격</mspace>
```

---

## 레이아웃 & 정렬

```xml
<br>                           줄바꿈
<align="center">중앙 정렬</align>   <!-- left / center / right / justified / flush -->
<indent=15%>들여쓰기</indent>
<line-indent=15%>줄 들여쓰기</line-indent>
<margin=5em>여백</margin>
<margin-left=5em>왼쪽 여백</margin-left>
```

---

## 링크 & 스프라이트

```xml
<!-- 하이퍼링크 (최대 256자) -->
<a href="https://unity.com">Unity 공식 사이트</a>

<!-- 링크 ID (클릭 콜백 등록용) -->
<link="item_01">아이템 이름</link>

<!-- 스프라이트 임베드 -->
<sprite name="star_icon">
<sprite index="3">
```

---

## 폰트 & 효과

```xml
<font="Impact SDF">폰트 변경</font>
<gradient="Light to Dark Green - Vertical">그라디언트</gradient>
<rotate="45">45도 회전</rotate>
<voffset=1em>수직 오프셋</voffset>
```

---

## 대소문자 변환

```xml
<uppercase>대문자 변환</uppercase>
<lowercase>소문자 변환</lowercase>
<smallcaps>SMALL CAPS</smallcaps>
```

---

## 특수 태그

```xml
<!-- 태그 파싱 방지 (코드 표시 등) -->
<noparse><b>파싱 안 됨</b></noparse>

<!-- 미리 정의된 스타일 적용 (TextStyleSheet에 정의) -->
<style="H1">제목 스타일</style>
```

---

## 링크 클릭 콜백

```cs
label.text = "Click <link=\"action\">here</link> to continue";
label.RegisterCallback<PointerUpLinkTagEvent>(evt => {
    if (evt.linkID == "action") HandleAction();
});
```

---

## 주의사항

- `*` 표시 태그는 Advanced Text Generator에서 제한적 지원
- `<a>`와 `<link>` 태그 최대 256자 제한
- `enableRichText = false` 설정 시 태그가 텍스트로 표시됨

## 관련 페이지
- [[wiki/unity-ui-toolkit/text-overview|텍스트 시스템]] — TextCore/SDF, 폰트 에셋, OS 이모지

## 출처
- `raw/unity-ui-toolkit-text-rich-tags.md`
