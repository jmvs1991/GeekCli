# .NET stored procedure generation skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 7
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate stored procedure classes, so that I can use them from my application.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request stored procedure classes, then the assistant explains prerequisites and required inputs, applies supported defaults, and asks for missing or ambiguous information before execution.
- [x] Given valid inputs, when I request generation, then the assistant invokes the corresponding MCP tool and reports an outcome supported by its result, including the requested scope and any confirmed generated artifacts.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment and available actionable information without claiming success or silently switching to CLI execution.

## Plan

- [x] Add one independent English-language skill to the existing installable catalog.
- [x] Describe usage conditions, prerequisites, required inputs, supported scopes, defaults, and target workspace context.
- [x] Explain MCP invocation and communication of confirmed outcomes, errors, and unavailable tools.
- [x] Review content against the current tool and shared service, document catalog availability, and update tracking statuses.

## Current implementation context

- MCP tool: `dotnet_sp`.
- Required parameters: `name` and `projectName`. Optional parameter: `scope`, defaulting to `basic`.
- Supported scopes for this operation are `basic`, `corp`, and `corp-co`, selecting `geek-sp`, `geek-sp-corp`, and `geek-sp-corp-co`, respectively. Although the common parser recognizes `corp-co-code`, this service rejects it.
- The service invokes a .NET template with the requested name and project name. It does not create or execute SQL stored procedures in a database.
- The tool exposes no working-directory, output-directory, operation, or encryption parameter. Do not invent such inputs or promise template artifacts that have not been verified.
- Generation depends on the MCP process working directory, the .NET SDK, and the corresponding installed Geek template.
- The result contains `ExitCode` and captured `Output`; invocation exceptions must also be handled without reporting success.

## Validation

Review the skill against the MCP contract and shared implementation for prerequisites, parameters, defaults, supported scopes, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required.

## Out of scope

- Refactoring existing stored procedure classes or migrating their business logic.
- Creating or executing SQL stored procedures in a database.
- Changes to templates, tools, shared services, commands, or installers.
- Skills for other tools.
