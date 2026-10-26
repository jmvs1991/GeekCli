# .NET DTO generation skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 5
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate DTOs through MCP, so that I can create the transfer models I need.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request a DTO or view DTO, then the assistant explains the prerequisites and inputs, applies the documented defaults, and asks for missing or ambiguous information before execution.
- [x] Given valid inputs, when the assistant requests generation, then I receive an outcome supported by the tool result, including the DTO variant requested and any confirmed generated artifacts.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment and available actionable information without claiming success or silently switching to CLI execution.

## Plan

- [x] Add one independent English-language skill to the existing installable catalog.
- [x] Describe when to use it, prerequisites, required inputs, defaults, and supported DTO variants and scopes.
- [x] Explain MCP invocation and communication of outcomes, errors, and unavailable tools.
- [x] Review content against the actual tool and shared service, document catalog availability, and update tracking statuses.

## Current implementation context

- MCP tool: `dotnet_dto`.
- Required parameters: `name` and `projectName`.
- Optional parameters: `scope`, defaulting to `basic`, and `view`, defaulting to `false`.
- The shared service selects a `geek-dto` or `geek-view-dto` template and resolves the scope suffix with `allowCorpCoCode: false`.
- Supported scope values, prerequisites, invocation, and results must be checked against the current implementation during skill authoring.

## Validation

Review the skill against the MCP tool and shared implementation for prerequisites, parameters, defaults, supported variants, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required.

## Out of scope

- Refactoring existing DTOs or migrating their mappings.
- Changes to templates, tools, shared services, commands, or installers.
- Skills for other tools.
