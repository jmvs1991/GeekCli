# .NET service and API test generation skills

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 11
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want one skill per service and API test tool, so that I can generate the corresponding tests.

## Acceptance criteria

- [x] Given the corresponding skill is installed and its MCP tool is available, when I request service or API test generation, then the assistant explains prerequisites, asks for missing or ambiguous inputs, and applies supported defaults.
- [x] Given valid inputs, when I request generation, then I receive an outcome supported by the tool response, identifying the requested test type and scope and any confirmed generated artifacts, without claiming the generated tests have passed unless separately verified.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment and available actionable details without claiming success or silently switching to CLI execution.

## Plan

- [x] Add two independent English-language skills to the existing installable catalog, one for service tests and one for API tests.
- [x] Describe usage conditions, prerequisites, target workspace context, required inputs, supported scopes, and defaults for each operation.
- [x] Explain invocation of the corresponding MCP tool and communication of confirmed generation outcomes and errors, distinguishing generation from test execution.
- [x] Review both skills against the current tools and shared services, document their availability, and update tracking statuses.

## Current implementation context

- Proposed skill names: `geek-dotnet-unit-test-service` and `geek-dotnet-api-unit-test`.
- `dotnet_unit_test_service` requires `name` and `projectName`.
- `dotnet_api_unit_test` requires `name`, `projectName`, `codeField`, `serviceInterface`, `dtoName`, `responseName`, `contextTestBase`, and `endpoint`. Do not invent defaults for these required inputs.
- Both tools accept optional `scope`, defaulting to `basic`. Supported scopes are `basic`, `corp`, and `corp-co`; the shared services reject `corp-co-code` even though the common parser recognizes it.
- Service test templates use `geek-unittest-entity-service-paginationful-filterless` with the corresponding scope suffix. API test templates use `geek-unittest-api-paginationful-filterless` with the corresponding scope suffix.
- Neither tool exposes a `view` parameter, working-directory parameter, or output-directory parameter.
- Generation requires the .NET SDK and the corresponding installed Geek template, and runs in the MCP process working directory.
- Both tools return `ExitCode` and captured `Output`. Handle invocation exceptions and nonzero exit codes, and report generated paths only when confirmed by output or file inspection.
- The tools generate templates; they do not execute the generated tests.

## Validation

Review both skills against the current MCP contracts and shared implementations for prerequisites, parameters, defaults, supported scopes, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required. No CLAUDE.md was found in the repository file listing; repository AGENTS.md instructions and existing skill refinements provide the local context.

## Out of scope

- Writing application-specific test cases, running generated tests, or claiming application test coverage.
- Changes to templates, MCP tools, shared services, CLI commands, wizards, or installers.
- Skills for other tools.
