# Angular service generation

Status: Pending

## Context

Add an Angular service generation command to GeekCli, supporting standalone Angular projects and Angular applications and libraries within Nx workspaces, following the existing page and component workflows. Both environments must be supported through direct CLI invocation, the interactive wizard, and MCP.

Stack: .NET 8, using the existing GeekCli executable and GeekCliServices shared service library, with Angular CLI and Nx generators.

Expensive decisions: none.

Current Nx command provided as a reference: `npx nx generate @nx/angular:service Services/test --project=core --type=service`.

## Backlog

1. Status: Pending — As a GeekCli user, I want to generate Angular services in projects without Nx through the CLI, interactive wizard, or MCP, so that I can create services using the same workflow as pages and components.
2. Status: Pending — As a GeekCli user, I want to generate Angular services in a selected application or library within an Nx workspace through the CLI, interactive wizard, or MCP, so that I can create services in the project and destination folder I need.
