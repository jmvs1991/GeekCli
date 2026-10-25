# Angular widget generation

Status: Pending

## Context

Add `geek-cli ngx widget` as a separate command branch alongside `component`, with equivalent generation behavior. Widgets are standalone, use `type=widget`, and have the selector `ngx-<kebab-case-name>-widget`, without requiring an NgModule. Support direct CLI invocation, the interactive wizard reachable from the general wizard, and MCP.

Stack: .NET 8, using the existing GeekCli executable and GeekCliServices shared service library, with Angular CLI and Nx generators.

Expensive decisions: none.

## Backlog

1. Status: Pending — As a GeekCli user, I want to generate widgets in Angular projects without Nx through the CLI, interactive wizard, or MCP, so that I can create widgets following the project's conventions.
2. Status: Pending — As a GeekCli user, I want to generate widgets in an application or library within an Nx workspace through the CLI, interactive wizard, or MCP, so that I can place them in the project and destination folder I need.
