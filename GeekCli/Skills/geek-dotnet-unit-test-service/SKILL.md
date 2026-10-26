---
name: geek-dotnet-unit-test-service
description: Use the Geek CLI dotnet_unit_test_service MCP tool to generate a .NET service unit test template.
---

# Generate .NET service unit tests

Use this skill when the user asks to generate Geek .NET service unit tests from an installed template.

## Prerequisites and inputs

- The `dotnet_unit_test_service` MCP tool and .NET SDK must be available. The `geek-unittest-entity-service-paginationful-filterless` Geek template for the selected scope must be installed and usable in the MCP process environment.
- The tool requires `name` (the generated test name) and `projectName` (the target project name). Infer them from the request and workspace when clear; ask if either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, and `corp-co`. Do not use `corp-co-code`; the shared service does not support it.
- The tool has no working-directory or output-directory parameter. Generation runs in the MCP process working directory; clarify the target workspace if it is not clear.

## Invoke the tool

After confirming the required inputs, scope, and target workspace, call `dotnet_unit_test_service` with `name`, `projectName`, and `scope` when provided. Use the MCP tool as the only generation path; do not silently switch to CLI execution.

The tool selects `geek-unittest-entity-service-paginationful-filterless` for `basic`, or appends `-corp` or `-corp-co` for those scopes. It passes `name` and `projectName` to the template. Do not promise exact generated paths unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report generation success only when `ExitCode` is zero; identify generated artifacts only when confirmed by output or workspace inspection. Generation does not run the generated tests, so do not claim they passed.
- For a nonzero exit code or invocation error, explain that generation failed and provide useful details from the result.
- If the MCP tool is unavailable or an input is unsupported, explain the impediment and actionable next step. Do not claim success or silently fall back to CLI execution.
