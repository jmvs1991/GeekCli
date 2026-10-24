# Angular Nx support

Status: In progress

## Context

Adapt the existing `geek-cli ngx component` and `geek-cli ngx page` commands to work with both standalone Angular projects and Angular projects within an Nx workspace, through direct CLI invocation, the interactive wizard, and MCP.

Stack: .NET 8, using the existing GeekCli executable and GeekCliServices shared service library.

Expensive decisions: none.

## Backlog

1. Status: In progress — As a GeekCli user, I want to generate components in a standalone Angular project or an Angular project within an Nx workspace, so that I can use the same command in both environments.
2. Status: Pending — As a GeekCli user, I want to generate pages in a standalone Angular project or an Angular project within an Nx workspace, so that I can use the same command in both environments.
