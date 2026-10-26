---
name: geek-dotnet-cache
description: Use the Geek CLI dotnet_cache MCP tool to generate a .NET cache template for an entity.
---

# Generate a .NET cache

Use this skill when the user asks to generate a Geek .NET cache from an installed template.

## Prerequisites and inputs

- The `dotnet_cache` MCP tool and .NET SDK must be available. The Geek cache template for the selected scope must already be installed and usable in the MCP process environment.
- The tool requires `name` (the entity or template name) and `projectName` (the target project name). Infer these from the request and workspace when clear; ask if either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, and `corp-co`. Do not offer `corp-co-code`; cache generation does not support it.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming the required inputs, scope, and target workspace, call `dotnet_cache` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects `geek-cache`, `geek-cache-corp`, or `geek-cache-corp-co` for `basic`, `corp`, or `corp-co`, respectively, and passes the requested name and project name to the template. It does not expose an output-directory parameter. Do not promise specific generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated files only when confirmed by the output or file inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful available details without claiming success.
- If `dotnet_cache` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
