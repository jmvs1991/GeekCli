---
name: geek-dotnet-write
description: Use the Geek CLI dotnet_write MCP tool to generate a .NET write repository.
---

# Generate a .NET write repository

Use this skill when the user asks to generate a Geek .NET write repository from an installed template.

## Prerequisites and inputs

- The `dotnet_write` MCP tool and .NET SDK must be available. The Geek write repository template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated repository or template name), `dbSchema` (the database schema), and `contextName` (the database context name). Infer values from the request and workspace when clear; ask when any required value is missing or ambiguous.
- The tool does not accept `scope` or `view`; do not offer or infer those options for write repository generation.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming the required inputs and target workspace, call `dotnet_write` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects the `geek-write-storeprocedureful-transactionless` template and passes `name`, `dbSchema`, and `contextName` to it. Do not promise exact generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or file inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful details from the result without claiming success.
- If `dotnet_write` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
