---
name: geek-dotnet-controller
description: Use the Geek CLI dotnet_controller MCP tool to generate a .NET controller for an entity or view.
---

# Generate a .NET controller

Use this skill when the user asks to generate a Geek .NET controller from an installed template.

## Prerequisites and inputs

- The `dotnet_controller` MCP tool and .NET SDK must be available. The selected Geek controller template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated controller or template name), `projectName` (the target project name), and `codeField` (the field used by the generated controller). Infer values from the request and workspace when clear; ask when any required value is missing or ambiguous. Do not invent a `codeField` default.
- Optional `view` defaults to `false`. Use `false` for an entity controller and `true` for a view controller. Clarify the requested variant when it is unclear.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming all required inputs, the variant, and the target workspace, call `dotnet_controller` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects `geek-api-paginationful-filterless` when `view=false` or `geek-view-api-paginationful-filterless` when `view=true`. It passes `name`, `projectName`, and `codeField` to the selected template. The tool has no scope parameter. Do not promise exact generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or workspace inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful details from the result without claiming success.
- If `dotnet_controller` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
