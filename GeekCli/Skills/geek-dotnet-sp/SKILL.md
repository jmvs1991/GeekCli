---
name: geek-dotnet-sp
description: Use the Geek CLI dotnet_sp MCP tool to generate a .NET stored procedure class template.
---

# Generate a .NET stored procedure class

Use this skill when the user asks to generate a Geek .NET stored procedure class from an installed template.

## Prerequisites and inputs

- The `dotnet_sp` MCP tool and .NET SDK must be available. The Geek stored procedure template for the selected scope must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated type or template name) and `projectName` (the target project name). Infer them from the request and workspace when clear; ask if either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, and `corp-co`. Do not offer `corp-co-code`; stored procedure generation does not support it.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming the required inputs, scope, and target workspace, call `dotnet_sp` with `name`, `projectName`, and `scope`. Use MCP as the only generation path; do not run `dotnet new`, the CLI, or another command as a fallback.

The tool selects `geek-sp`, `geek-sp-corp`, or `geek-sp-corp-co` for `basic`, `corp`, or `corp-co`, respectively, and passes the requested name and project name to the template. It generates template files; it does not create or execute a stored procedure in a database. Do not promise specific generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report the requested scope and report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or workspace inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful available details without claiming success.
- If `dotnet_sp` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
