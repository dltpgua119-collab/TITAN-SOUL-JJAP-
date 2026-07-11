---
topic: design-pattern
original_type: url
source_url: https://www.anthropic.com/engineering/building-effective-agents
created: 2026-04-17
---

# Building Effective Agents — Anthropic Engineering

## Introduction

Anthropic has worked extensively with teams implementing LLM agents across industries. Their research shows "the most successful implementations weren't using complex frameworks or specialized libraries. Instead, they were building with simple, composable patterns."

## What are Agents?

The post distinguishes between two categories of agentic systems:

- **Workflows**: Systems where LLMs and tools operate through predefined code paths
- **Agents**: Systems where LLMs dynamically direct their own processes and tool usage

## When to Use Agents

The guidance emphasizes starting with the simplest solution possible. "Agentic systems often trade latency and cost for better task performance." Workflows suit well-defined tasks, while agents handle situations requiring flexibility and model-driven decision-making.

## Building Blocks and Patterns

### 1. Augmented LLM
The basic building block combining retrieval, tools, and memory capabilities.

### 2. Prompt Chaining
Decomposing tasks into sequential steps where each LLM call processes prior outputs. Ideal for situations where "the task can be easily and cleanly decomposed into fixed subtasks."

### 3. Routing
Classifying inputs and directing them to specialized downstream tasks.

### 4. Parallelization
Running tasks simultaneously through:
- **Sectioning**: Independent subtasks run in parallel
- **Voting**: Multiple attempts for increased confidence

### 5. Orchestrator-Workers
A central LLM dynamically breaks down tasks and delegates to worker LLMs, suited for unpredictable subtask requirements.

### 6. Evaluator-Optimizer
One LLM generates responses while another provides feedback in loops.

### 7. Agents (Autonomous)
Autonomous systems that "plan and operate independently" with potential returns to humans for guidance.

## Framework Considerations

Frameworks mentioned include the Claude Agent SDK, Strands Agents by AWS, Rivet, and Vellum. The post advises developers to "start by using LLM APIs directly" since many patterns require only minimal code, and to thoroughly understand any framework's underlying mechanics.

## Tool Development (Agent-Computer Interface)

Key recommendations:
- Minimize formatting overhead for the model
- Include example usage and edge cases in definitions
- Test extensively with varied inputs
- Design tools to reduce user error (the "poka-yoke" principle)

The SWE-bench implementation devoted "more time optimizing tools than the overall prompt," discovering that requiring absolute filepaths eliminated model mistakes with relative paths.

## Real-World Applications

**Customer Support** — Combines conversation with action, featuring clear success criteria and feedback loops.

**Coding Agents** — Leverage verifiable outputs through automated testing and objective quality measurement.

## Core Principles

1. **Simplicity** in design
2. **Transparency** in planning steps
3. Careful **tool documentation and testing**

The conclusion emphasizes that effective implementation means "building the _right_ system for your needs," not the most sophisticated one.
