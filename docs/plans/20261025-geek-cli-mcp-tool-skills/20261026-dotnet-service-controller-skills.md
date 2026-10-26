# .NET service and controller generation skills

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 10
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want one skill per .NET service and controller tool, so that I can generate my application's operations.

## Acceptance criteria

- [x] Given the corresponding skill is installed and its MCP tool is available, when I request a service or controller for an entity or view, then the assistant explains prerequisites, asks for missing or ambiguous inputs, and applies the supported defaults for the requested operation.
- [x] Given valid inputs, when I request generation, then I receive an outcome supported by the tool response, identifying the requested service or controller variant and any confirmed generated artifacts.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment and available actionable details without claiming success or silently switching to CLI execution.

## Plan

- [x] Add two independent English-language skills to the existing installable catalog, one for services and one for controllers.
- [x] Describe usage conditions, prerequisites, target workspace context, and each operation's required inputs, supported variants, and defaults.
- [x] Explain invocation of the corresponding MCP tool and communication of confirmed outcomes, errors, and unavailable tools.
- [x] Review both skills against the current tools and shared services, document their availability, and update tracking statuses.

## Current implementation context

- Proposed skill names: `geek-dotnet-service` and `geek-dotnet-controller`.
- `dotnet_service` requires `name` and `projectName`; optional parameters are `scope`, defaulting to `basic`, and `view`, defaulting to `false`.
- Supported service scopes are `basic`, `corp`, `corp-co`, and `corp-co-code`. Service templates follow `geek-entity-service-paginationful-filterless` or `geek-view-service-paginationful-filterless`, with the corresponding scope suffix.
- `dotnet_controller` requires `name`, `projectName`, and `codeField`; its only optional parameter is `view`, defaulting to `false`. It has no scope parameter.
- Controller templates are `geek-api-paginationful-filterless` and `geek-view-api-paginationful-filterless`. The supplied `codeField` is passed to the template; do not invent a default.
- Generation requires the .NET SDK and the corresponding installed Geek template. Execution uses the MCP process working directory; neither tool exposes a working-directory or output-directory parameter.
- Both tools return `ExitCode` and captured `Output`. Explain invocation exceptions and nonzero exit codes, and identify generated paths only when confirmed by output or file inspection.

## Validation

Review both skills against the current MCP contracts and shared implementations for prerequisites, parameters, defaults, supported variants, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required. No CLAUDE.md was found in the repository file listing; repository AGENTS.md instructions and existing skill refinements provide the local context.

## Out of scope

- Refactoring existing services or controllers, or implementing application-specific business operations beyond template generation.
- Changes to templates, MCP tools, shared services, CLI commands, wizards, or installers.
- Skills for other tools.
