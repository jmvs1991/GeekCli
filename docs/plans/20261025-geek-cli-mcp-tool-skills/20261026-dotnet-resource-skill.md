# .NET message resource generation skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 8
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate message resources, so that I can incorporate localized messages.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request message resource generation, then the assistant explains prerequisites, asks for missing or ambiguous inputs, and applies supported defaults.
- [x] Given valid parameters, when I request generation, then the assistant invokes `dotnet_resource` and reports the outcome and any generated files confirmed by the response.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment without claiming success or silently switching to CLI execution.

## Plan

- [x] Add the independent English-language `geek-dotnet-resource` skill to the existing installable catalog.
- [x] Document usage conditions, .NET SDK and Geek template prerequisites, and the MCP process working directory.
- [x] Describe required `name` and `projectName` parameters and optional `scope`, defaulting to `basic`, with supported values `basic`, `corp`, `corp-co`, and `corp-co-code`.
- [x] Explain MCP invocation and communication of `ExitCode`, `Output`, exceptions, and results with insufficient evidence to confirm generated files.
- [x] Review content against the implementation, document catalog availability, and update tracking statuses.

## Current implementation context

- MCP tool: `dotnet_resource`.
- Required parameters: `name` and `projectName`. Optional parameter: `scope`, defaulting to `basic`.
- Supported scopes select `geek-resource`, `geek-resource-corp`, `geek-resource-corp-co`, and `geek-resource-corp-co-code`, respectively.
- Generation depends on the MCP process working directory, the .NET SDK, and the corresponding installed Geek template.
- The tool exposes no language, working-directory, or output-directory parameter. Do not invent such inputs or promise template artifacts that have not been verified.
- The result contains `ExitCode` and captured `Output`; invocation exceptions must also be handled without reporting success.

## Validation

Review the skill against the MCP contract and shared implementation for prerequisites, parameters, defaults, all four supported scopes, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required.

## Out of scope

- Refactoring existing message resources.
- Translating messages.
- Changes to tools, templates, shared services, commands, or installers.
- Adding language or output-directory parameters.
- Skills for other tools.
