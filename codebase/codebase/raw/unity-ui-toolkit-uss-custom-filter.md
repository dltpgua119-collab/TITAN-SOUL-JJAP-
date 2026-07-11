---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/create-custom-swirl-filter.html
created: 2026-05-03
---

# Create a Custom Swirl Filter

## Overview

This Unity manual page explains how to build a custom visual filter that applies a swirl distortion effect to UI elements. The tutorial uses a pre-built shader and material alongside a `FilterFunctionDefinition` asset to define parameters and bindings.

## Key Concepts

The approach involves:
- Creating a filter asset that specifies parameters (Angle and Radius)
- Binding those parameters to shader properties (`_Angle` and `_Radius`)
- Applying the filter through the UI Builder or USS code

## Prerequisites

Developers should understand:
- UXML fundamentals
- USS styling
- USS filter mechanics

## Step-by-Step Setup

### 1. Prepare Assets

Create a `SwirlFilter` folder in `Assets/` and download these files from the GitHub repository (https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-custom-swirl-filter):
- Swirl.shader
- Swirl.mat

### 2. Create FilterFunctionDefinition Asset

Right-click in the SwirlFilter folder → **Create > UI Toolkit > Filter Function Definition** → Rename to `SwirlFilter`

### 3. Configure in Inspector

**Filter Name:** Set to `swirl`

**Parameters:** Add two float-type parameters:
- Parameter 1: `Angle` (controls rotation)
- Parameter 2: `Radius` (controls effect radius)

**Passes:** Assign `Swirl.shader` as the material

**Parameter Bindings:** Create two bindings:
- Index 0 → Property `_Angle`
- Index 1 → Property `_Radius`

### 4. Create UXML and USS Files

**SwirlFilterExample.uss:**

```css
.outside {
    flex-grow: 1;
    position: absolute;
    height: 207px;
    width: 234px;
    top: 46px;
    left: 27px;
    background-color: rgb(255, 0, 0);
}

.inside {
    flex-grow: 1;
    position: absolute;
    height: 75px;
    width: 100px;
    top: 46px;
    left: 27px;
    background-color: rgb(0, 255, 247);
}
```

**SwirlFilterExample.uxml:**

```xml
<engine:UXML xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:engine="UnityEngine.UIElements" xmlns:editor="UnityEditor.UIElements" noNamespaceSchemaLocation="../UIElementsSchema/UIElements.xsd" editor-extension-mode="False">
    <Style src="SwirlFilterExample.uss" />
    <engine:VisualElement class="outside">
        <engine:VisualElement class="inside" />
    </engine:VisualElement>
</engine:UXML>
```

### 5. Apply Filter in UI Builder

1. Double-click `SwirlFilterExample.uxml`
2. **StyleSheets panel** → **+** → **Add Existing USS** → Select `SwirlFilterExample.uss`
3. **Hierarchy panel** → Select parent VisualElement
4. **Inspector** → **Inline Styles > Filter** → Click **Add(+)**
5. **Function dropdown** → Select `Custom`
6. **Definition** → `SwirlFilter`
7. Set **Angle**: `58.9`
8. Set **Radius**: `2.3`

### 6. Save to USS (Optional)

Add class name `.filter-effect` in Style Class List, then select "Extract Inlined Style to New Class." This generates:

```css
.filterEffect {
    filter: filter("SwirlFilter/SwirlFilterFunction.asset" 58.9 2.3);
}
```

## References

- USS Properties Documentation (../UIE-uss-properties.html)
- Complete Example Files (GitHub): https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-custom-swirl-filter
