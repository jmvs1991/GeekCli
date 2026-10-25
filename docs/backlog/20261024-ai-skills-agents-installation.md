# AI skills and subagents installation

Status: Completed

## Context

Add an `ai` branch to Geek CLI so its users can individually install Codex skills and subagents from a catalog defined within the CLI. Users can choose project-level or global user-level installation. Both capabilities must be available through direct CLI invocation and an interactive AI wizard reachable from the general wizard, with consistent behavior through the same shared service and command model for each operation. Neither installation operation is exposed through MCP.

Stack: Existing .NET 8 GeekCli and GeekCliServices solution, and Codex.

Expensive decisions: None.

## Backlog

1. As a Geek CLI user, I want to install a skill from the included catalog, choosing project-level or user-level scope, so that I can use its instructions in Codex. Status: Completed.
2. As a Geek CLI user, I want to install a subagent from the included catalog, choosing project-level or user-level scope, so that I can use specialized agents in Codex. Status: Completed.

Decision: Skill and subagent installation are intentionally unavailable through MCP.
