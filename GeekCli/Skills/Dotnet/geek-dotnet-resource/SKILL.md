---
name: geek-dotnet-resource
description: Use the Geek CLI dotnet_resource MCP tool to generate .NET message resource templates.
---

# Generate .NET message resources

Use this skill when the user asks to generate Geek .NET message resources for localized messages.

## Prerequisites and inputs

- The `dotnet_resource` MCP tool and .NET SDK must be available. The Geek resource template for the selected scope must already be installed and usable in the MCP process environment.
- The tool requires `name` and `projectName`. Infer them from the request and workspace when clear; ask if either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, `corp-co`, and `corp-co-code`. Clarify the scope when the request does not establish it and the intended template is ambiguous.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if unclear.

## Invoke the tool

After confirming the required inputs, scope, and target workspace, call `dotnet_resource` with `name`, `projectName`, and `scope`. The tool selects `geek-resource`, `geek-resource-corp`, `geek-resource-corp-co`, or `geek-resource-corp-co-code` for the corresponding scope and asks the .NET SDK to generate the template. Do not run `dotnet new` or another command as a fallback.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero.
- Describe generated files only when they are confirmed by the tool output or workspace inspection; do not promise specific template artifacts in advance.
- For a nonzero exit code, report that generation failed and include useful details from `Output`. If invocation raises an exception, explain the error using only details that are available; do not claim success.
- If the requested scope is unsupported, explain that only `basic`, `corp`, `corp-co`, and `corp-co-code` are accepted, then ask which supported scope to use. Do not invoke the tool with an unsupported value.
- If `dotnet_resource` is unavailable or execution fails, explain why generation could not be confirmed. Do not switch to direct CLI or another generation path.
