---
name: geek-dotnet-service
description: Use the Geek CLI dotnet_service MCP tool to generate a .NET service for an entity or view.
---

# Generate a .NET service

Use this skill when the user asks to generate a Geek .NET service from an installed template.

## Prerequisites and inputs

- The `dotnet_service` MCP tool and .NET SDK must be available. The selected Geek service template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated service or template name) and `projectName` (the target project name). Infer values from the request and workspace when clear; ask when either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, `corp-co`, and `corp-co-code`.
- Optional `view` defaults to `false`. Use `false` for an entity service and `true` for a view service. Clarify the requested variant when it is unclear.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming the required inputs, scope, variant, and target workspace, call `dotnet_service` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects `geek-entity-service-paginationful-filterless` when `view=false` or `geek-view-service-paginationful-filterless` when `view=true`, then appends the selected scope suffix (`-corp`, `-corp-co`, or `-corp-co-code`; `basic` adds no suffix). It passes `name` and `projectName` to the template. Do not promise exact generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or workspace inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful details from the result without claiming success.
- If `dotnet_service` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
