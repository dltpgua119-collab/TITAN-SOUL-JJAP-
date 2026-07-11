---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/get-started-with-ui-shader-graph.html
created: 2026-05-03
---

# Get Started with UI Shader Graph

## Overview

This tutorial demonstrates creating custom shaders for UI elements using Shader Graph in Unity 6.3 LTS. The example builds a shader with gradient effects applied to button components.

## Prerequisites

Developers should be familiar with:
- Shader Graph
- Materials
- UI Builder
- Button elements

**Important note:** "UI Shader Graph only works with URP (Universal Render Pipeline)."

## Creating a Shader with Gradient Effect

### Steps 1-4: Initial Setup
1. Create a project using any URP template
2. Right-click in Assets folder → **Create** > **Shader Graph** > **URP** > **UI Shader Graph**
3. Name the asset `MyCustomShader`
4. Double-click to open in Shader Graph editor

### Steps 5-9: Building the Graph
5. Select **Create Node** > **UI** > **Render Type Branch**
6. Select **Create Node** > **UV** > **UV Distortion**
7. Connect UV Distortion's **UV** output to Render Type Branch's **Solid** input
8. Connect Render Type Branch's **Color** output to Fragment's **Base Color** input
9. Connect Render Type Branch's **Alpha** output to Fragment's **Alpha** input

The Main Preview window displays results on SDF and Bitmap text backgrounds.

10. Save the shader

## Applying the Shader to UI Elements

### Material Creation
1. Right-click Assets → **Create** > **Material**
2. Name it `MyCustomMaterial`
3. In Inspector, select `MyCustomShader` from the **Shader** dropdown

### UI Builder Assignment
4. Open **Window** > **UI Toolkit** > **UI Builder**
5. Drag a **Button** element from Library panel into Hierarchy
6. In Inspector's **Material** dropdown, select `MyCustomMaterial`

The Viewport displays the button with applied custom shader effects.

**Important:** Custom shaders affect selected elements and all child elements throughout the hierarchy.

## Additional Resources

- Introduction to UI Shader Graph
- Shader Graph documentation: https://docs.unity3d.com/Packages/com.unity.shadergraph@17.4/manual/index.html
- `-unity-material` property reference
