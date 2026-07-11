---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/UIE-ElementRef.html
created: 2026-04-16
---

# UXML 내장 요소 레퍼런스

## 기본 요소
| 요소 | 용도 |
|------|------|
| `VisualElement` | 모든 UI 요소의 기반 클래스 |
| `BindableElement` | 데이터 바인딩 지원 VisualElement |

## 텍스트 & 이미지 표시
| 요소 | 용도 |
|------|------|
| `Label` | 텍스트 표시 |
| `TextElement` | 텍스트 기반 요소 기반 클래스 |
| `Image` | 텍스처/스프라이트 표시 |

## 컨테이너 & 레이아웃
| 요소 | 용도 |
|------|------|
| `Box` | 시각적 컨테이너 |
| `GroupBox` | 관련 컨트롤 그룹화 (레이블 포함) |
| `ScrollView` | 스크롤 가능 영역 |
| `TwoPaneSplitView` | 크기 조정 가능한 2패널 레이아웃 |
| `HelpBox` | 정보 메시지 박스 |

## 버튼 & 액션
| 요소 | 용도 |
|------|------|
| `Button` | 클릭 가능한 버튼 |
| `RepeatButton` | 누르고 있으면 반복 발화 |

## 텍스트 입력
| 요소 | 용도 |
|------|------|
| `TextField` | 문자열 입력 |
| `IntegerField` | 정수 입력 |
| `FloatField` | 부동소수점 입력 |
| `DoubleField` | double 입력 |
| `LongField` | long 입력 |
| `UnsignedIntegerField` | uint 입력 |
| `UnsignedLongField` | ulong 입력 |

## 벡터 & 기하 필드
| 요소 | 용도 |
|------|------|
| `Vector2Field` / `Vector3Field` / `Vector4Field` | 벡터 입력 |
| `Vector2IntField` / `Vector3IntField` | 정수 벡터 입력 |
| `RectField` / `RectIntField` | 사각형 입력 |
| `BoundsField` / `BoundsIntField` | 경계 볼륨 입력 |
| `Hash128Field` | 해시 입력 |

## 선택 컨트롤
| 요소 | 용도 |
|------|------|
| `Toggle` | 체크박스 (bool) |
| `RadioButton` | 라디오 버튼 단일 항목 |
| `RadioButtonGroup` | 라디오 버튼 그룹 (상호 배타) |
| `DropdownField` | 드롭다운 목록 선택 |
| `EnumField` | enum 기반 선택 |
| `EnumFlagsField` | enum flags 다중 선택 |
| `ToggleButtonGroup` | 버튼 그룹 토글 |

## 슬라이더
| 요소 | 용도 |
|------|------|
| `Slider` | float 범위 슬라이더 |
| `SliderInt` | int 범위 슬라이더 |
| `MinMaxSlider` | 최소/최대값 슬라이더 |
| `ProgressBar` | 진행률 표시 (읽기 전용) |

## 목록 & 트리
| 요소 | 용도 |
|------|------|
| `ListView` | 가상화 목록 (스크롤 + 아이템 재활용) |
| `TreeView` | 계층형 트리 |
| `MultiColumnListView` | 다중 컬럼 목록 |
| `MultiColumnTreeView` | 다중 컬럼 트리 |

## 탭 & 팝업
| 요소 | 용도 |
|------|------|
| `TabView` / `Tab` | 탭 인터페이스 |
| `PopupWindow` | 플로팅 팝업 창 |
| `Foldout` | 접기/펴기 섹션 |

## Unity 전용 (Editor Only)
| 요소 | 용도 |
|------|------|
| `ColorField` | 색상 선택 |
| `GradientField` | 그라디언트 편집 |
| `CurveField` | 애니메이션 커브 편집 |
| `ObjectField` | 에셋/오브젝트 참조 |
| `PropertyField` | Inspector 스타일 프로퍼티 표시 |
| `InspectorElement` | 인스펙터 패널 내장 |
| `TagField` / `LayerField` / `LayerMaskField` | Unity 태그/레이어 선택 |
| `MaskField` / `Mask64Field` / `RenderingLayerMaskField` | 마스크 선택 |
| `IMGUIContainer` | 기존 IMGUI 코드 임베딩 |

## 툴바 (Editor Only)
`Toolbar`, `ToolbarButton`, `ToolbarMenu`, `ToolbarToggle`, `ToolbarSpacer`, `ToolbarSearchField`, `ToolbarBreadcrumbs`, `ToolbarPopupSearchField`

## UXML 구조 요소
| 요소 | 용도 |
|------|------|
| `TemplateContainer` | UXML 템플릿 인스턴스 컨테이너 |
| `Scroller` | 스크롤바 단독 컨트롤 |
