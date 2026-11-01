---
name: geek-dotnet-list
description: Use the Geek CLI dotnet_list MCP tool to discover installed Geek .NET templates when a user asks which Geek templates are available.
---

# List Geek .NET templates

Use this skill when the user asks to see, find, or confirm which Geek .NET templates are installed and available to generate.

## Prerequisites

- The `dotnet_list` MCP tool must be available in the assistant's current tool set.
- Do not install templates or configure MCP as part of listing them.

## Tool invocation

Call `dotnet_list` with no parameters. The tool does not accept arguments. Do not substitute shell commands, other MCP tools, documentation, or remembered template names if the tool is unavailable or fails.

## Interpret the result

The tool returns `ExitCode` and `Output`:

- When `ExitCode` is `0`, communicate the templates shown in `Output` faithfully. Do not add, infer, or rename templates. If the output indicates that there are no matching Geek templates, say that none were found.
- When `ExitCode` is nonzero, report that listing the Geek templates failed. Include useful error details from `Output` and do not present the output as a successful list.
- If `Output` is empty or does not clearly show any matching templates despite a successful exit, say that no matching Geek templates were reported. Do not invent results.
- If `dotnet_list` is unavailable, say that you cannot retrieve the installed Geek .NET templates because the MCP tool is unavailable. Do not fall back to CLI commands or claim a result.
