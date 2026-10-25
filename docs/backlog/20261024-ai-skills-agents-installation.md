# AI skills and subagents installation

Status: In progress

## Context

Add an `ai` branch to Geek CLI so its users can individually install Codex skills and subagents from a catalog defined within the CLI. Users can choose project-level or global user-level installation. Both capabilities must be available through direct CLI invocation, an interactive AI wizard reachable from the general wizard, and MCP, with consistent behavior through the same shared service and command model for each operation.

Stack: Existing .NET 8 GeekCli and GeekCliServices solution, and Codex.

Expensive decisions: None.

## Backlog

1. As a Geek CLI user, I want to install a skill from the included catalog, choosing project-level or user-level scope, so that I can use its instructions in Codex. Status: Completed.
2. As a Geek CLI user, I want to install a subagent from the included catalog, choosing project-level or user-level scope, so that I can use specialized agents in Codex. Status: Pending.

Decision: Skill installation is intentionally unavailable through MCP; this exception applies only to story 1 and does not define MCP availability for subagent installation.
