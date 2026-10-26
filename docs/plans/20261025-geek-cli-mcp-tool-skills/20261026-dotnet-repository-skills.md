# .NET repository generation skills

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 6
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want one skill per read and write repository tool, so that I can generate access to my data.

## Acceptance criteria

- [x] Given the skills are installed and their MCP tools are available, when I request a read or write repository, then the assistant selects the corresponding skill, explains prerequisites and inputs, applies supported defaults, and asks for missing or ambiguous information before execution.
- [x] Given valid inputs, when I request generation, then the assistant invokes the corresponding MCP tool and reports an outcome supported by its result, including the requested repository variant and any confirmed generated artifacts.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment and available actionable information without claiming success or silently switching to CLI execution.

## Plan

- [x] Add two independent English-language skills to the existing installable catalog, one per repository tool.
- [x] Describe prerequisites, target workspace context, required inputs, and the variants and defaults supported by each tool.
- [x] Explain MCP invocation and communication of confirmed outcomes, errors, and unavailable tools.
- [x] Review both skills against their tools and shared services, document catalog availability, and update tracking statuses.

## Current implementation context

- MCP tools: `dotnet_read` and `dotnet_write`.
- Both require `name`, `dbSchema`, and `contextName`.
- Read additionally accepts `scope`, defaulting to `basic`, and `view`, defaulting to `false`.
- Read selects `geek-read-paginationful-filterless-entity` or `geek-read-paginationful-filterless-view`, with the scope suffix resolved using `allowCorpCoCode: true`.
- Write selects `geek-write-storeprocedureful-transactionless` and exposes neither scope nor view parameters.
- Neither tool exposes a working-directory parameter. Skill authoring must account for the MCP process working directory and the required .NET SDK and Geek templates.
- Supported scope spellings, invocation, prerequisites, and results must be checked against the current implementation during skill authoring.

## Validation

Review each skill against its MCP tool and shared implementation for prerequisites, parameters, defaults, supported variants, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required.

## Out of scope

- Refactoring entities, database contexts, or existing repositories.
- Changes to templates, tools, shared services, commands, or installers.
- Skills for other tools.
