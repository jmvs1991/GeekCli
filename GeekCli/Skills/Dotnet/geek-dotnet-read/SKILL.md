---
name: geek-dotnet-read
description: Use the Geek CLI dotnet_read MCP tool to generate a .NET read repository for an entity or view.
---

# Generate a .NET read repository

Use this skill when the user asks to generate a Geek .NET read repository from an installed template.

## Prerequisites and inputs

- The `dotnet_read` MCP tool and .NET SDK must be available. The selected Geek read repository template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated repository or template name), `dbSchema` (the database schema), and `contextName` (the database context name). Infer values from the request and workspace when clear; ask when any required value is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, `corp-co`, and `corp-co-code`.
- Optional `view` defaults to `false`. Use `false` for an entity repository and `true` for a view repository. Clarify the requested variant when it is unclear.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming the required inputs, scope, variant, and target workspace, call `dotnet_read` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects `geek-read-paginationful-filterless-entity` when `view=false` or `geek-read-paginationful-filterless-view` when `view=true`, then appends the selected scope suffix (`-corp`, `-corp-co`, or `-corp-co-code`; `basic` adds no suffix). It passes `name`, `dbSchema`, and `contextName` to the template. Do not promise exact generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or file inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful details from the result without claiming success.
- If `dotnet_read` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
