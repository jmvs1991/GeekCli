# Angular directive generation

Status: Pending

## Context

Extend Geek CLI so its users can generate non-standalone Angular directives in projects both outside and inside Nx workspaces. Both environments must be supported through direct CLI invocation, the interactive Angular wizard reachable from the general wizard, and MCP, with consistent behavior through the same shared service and command model.

Stack: Existing .NET 8 GeekCli and GeekCliServices solution, Angular CLI, and Nx Angular generators.

Expensive decisions: None.

## Backlog

1. As a Geek CLI user, I want to create a non-standalone Angular directive in a project outside Nx, so that I can add reusable behavior to my application. Status: Pending.
2. As a Geek CLI user, I want to create a non-standalone Angular directive in a project inside an Nx workspace, so that I can add reusable behavior to the selected project. Status: Pending.
