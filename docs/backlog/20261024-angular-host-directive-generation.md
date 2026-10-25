# Angular host directive generation

Status: Pending

## Context

Extend Geek CLI so its users can generate Angular host directives in projects both outside and inside Nx workspaces. Generated directives must be standalone, use the `host-directive` type, and have no selector. Both environments must be supported through direct CLI invocation, the interactive Angular wizard reachable from the general wizard, and MCP, with consistent behavior through the same shared service and command model.

Stack: Existing .NET 8 GeekCli and GeekCliServices solution, Angular CLI, and Nx Angular generators.

Expensive decisions: None.

## Backlog

1. As a Geek CLI user, I want to create an Angular host directive in a project outside Nx, so that I can reuse behavior through directive composition in my application. Status: Pending.
2. As a Geek CLI user, I want to create an Angular host directive in a project inside an Nx workspace, so that I can reuse behavior through directive composition in the selected project. Status: Pending.
