# Angular pipe generation

Status: Pending

## Context
Extend Geek CLI so its users can generate non-standalone Angular pipes in projects both outside and inside Nx workspaces. Projects outside Nx require only the pipe name; projects inside Nx allow choosing the name, destination, and declaring NgModule. Both stories cover direct CLI invocation, the Angular wizard reachable from the general wizard, and MCP, with consistent behavior through the same shared service and command model.

Stack: Existing .NET 8 GeekCli and GeekCliServices solution, Angular CLI, and Nx Angular generators.

Expensive decisions: None.

The Nx generator path must end with the pipe name, appended to the supplied destination folder. For example, a pipe named `example` in `apps/ing-sel-crm/src/app/Shared/Pipes` uses `--path=apps/ing-sel-crm/src/app/Shared/Pipes/example`.

Both generators support disabling standalone generation with `--standalone=false`. For Nx projects, the declaring NgModule can be selected with `--module`. Outside Nx, Geek CLI accepts only the pipe name and leaves destination and module resolution to Angular CLI. Generator options were verified against the official documentation; no generator execution was performed.

References:
- [Nx Angular pipe generator](https://nx.dev/docs/technologies/angular/generators#pipe)
- [Angular CLI pipe generator](https://angular.dev/cli/generate/pipe)

## Backlog
1. As a Geek CLI user, I want to create a non-standalone Angular pipe in a project outside Nx by providing only its name, so that I can use it in my module-based application. Status: Pending.
2. As a Geek CLI user, I want to create a non-standalone Angular pipe in a project inside an Nx workspace, choosing its name, destination, and declaring module, so that I can use it in my module-based workspace project. Status: Pending.
