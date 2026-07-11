---
topic: unity-ui-toolkit
original_type: url
source_url: https://docs.unity3d.com/6000.3/Documentation/Manual/ui-systems/introduction-to-ui-shader-graph.html
created: 2026-05-03
---

# Introduction to UI Shader Graph

## Overview

UI Shader Graph enables creation of custom shaders for UI elements with capabilities including:

- Custom button effects (glow, blur, interactive color changes)
- Text rendering (crisp, scalable text with outlines)
- Animated UI elements (gradient and texture animations)
- Color processing (grayscale, sepia, color correction)

**Important limitation**: UI Shader Graph creates shaders for rendering UI mesh elements directly, not custom filters or post-processing effects on render targets.

## UI-Specific Rendering

The Shader Graph editor for UI includes:

- **Fragment node**: Inputs for Base Color and Alpha
- **Vertex node**: Vertex-level processing
- **Main Preview window**: Real-time visual feedback showing before/after states
- **No Shader section**: Example graphs (texture, gradient, SDF text, Bitmap text) as baselines
- **With Shader section**: Same examples demonstrating shader effects

## Single Shader for Multiple Render Types

UI Toolkit meshes contain triangles for various render types (text, solid colors, textures, gradients) within a single shader for optimal CPU performance.

### Render Type Branch Node

This specialized node allows defining different behaviors for each render type within one shader. Each input port connects to a specific render type:

| Render Type | Applies To | Preview Appearance |
|-------------|-----------|-------------------|
| Solid | Solid color backgrounds and borders | Background/border of SDF and Bitmap graphs |
| Texture | Texture graphics | Texture graph |
| SDF Text | Signed Distance Fields fonts | SDF text in SDF graphs |
| Bitmap Text | Bitmap fonts | Bitmap text in Bitmap graphs |
| Gradient | Vector graphics | Gradient graph |

**Tip**: Use **Create** > **Shader Graph** > **From Template** > **UI** to add Render Type Branch automatically.

### Render Type Node

Outputs the current render type being processed, enabling conditional logic for render-type-specific effects.

## Default Nodes

UI Shader Graph provides default nodes for each render type:

- Default Solid (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/default-solid-node.html)
- Default Texture (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/default-texture-node.html)
- Default Gradient (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/default-gradient-node.html)
- Default Bitmap Text (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/default-bitmap-text-node.html)
- Default SDF Text (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/default-sdf-text-node.html)

**Performance note**: Leave unconnected inputs to use default values rather than connecting default nodes, as this optimizes branching efficiency.

## Customizing Render Types

Modify default node outputs before connecting to Render Type Branch node. Examples include color multiplication, UV distortion, and effect chaining.

## UI-Specific Input Nodes

- Element Texture UV (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/element-texture-uv-node.html): Texture coordinates for sampling
- Element Layout UV (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/element-layout-uv-node.html): UV coordinates within element's layout rectangle
- Element Texture Size (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/element-texture-size-node.html): Assigned texture size

## Sample Element Texture Node

The Sample Element Texture (https://docs.unity3d.com/Packages/com.unity.shadergraph@17.3/manual/sample-element-texture-node.html) node samples textures at specific UV coordinates for complex visual effects or texture manipulation.

**Note**: The sampled texture is the element's currently assigned texture (font texture, background image, Image element source, or texture from custom rendering code).

## Related Resources

- Get started with UI Shader Graph (get-started-with-ui-shader-graph.html)
- Introduction to font assets (../UIE-font-asset.html)
- Work with vector graphics (work-with-vector-graphics.html)
