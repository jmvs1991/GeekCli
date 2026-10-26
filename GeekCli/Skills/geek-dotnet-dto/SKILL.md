---
name: geek-dotnet-dto
description: Use the Geek CLI dotnet_dto MCP tool to generate a .NET DTO or view DTO template.
---

# Generate a .NET DTO

Use this skill when the user asks to create a Geek .NET DTO or view DTO from an installed template.

## Prerequisites and inputs

- The `dotnet_dto` MCP tool and .NET SDK must be available. The requested Geek DTO template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated type or template name) and `projectName` (the target project name). Inspect the request and workspace to infer them; ask when either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported scopes are `basic`, `corp`, and `corp-co`. Do not offer `corp-co-code`; DTO templates do not support that scope.
- Optional `view` defaults to `false`. Use `false` for a regular DTO and `true` for a view DTO. Clarify the variant if the request does not make it clear.
- The tool has no working-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace before execution; clarify if they may point elsewhere.

## Invoke the tool

Once the required inputs, scope, variant, and target context are clear, call `dotnet_dto` with the confirmed values. Use the MCP tool as the only generation path. Do not run `dotnet new` or another command as a fallback.

The tool selects `geek-dto` when `view=false` and `geek-view-dto` when `view=true`, adding the selected scope suffix, then requests the named template for `projectName`. It does not expose an output-directory parameter. Do not promise exact generated filenames or files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or file inspection.
- For a nonzero exit code or tool error, explain that generation failed and include useful details from the result. Do not claim success.
- If `dotnet_dto` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
