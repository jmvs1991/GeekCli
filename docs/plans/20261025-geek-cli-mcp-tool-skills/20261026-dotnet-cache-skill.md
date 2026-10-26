# .NET cache generation skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 9
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate caches, so that I can create the cache for my entities.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request cache generation, then the assistant explains prerequisites, asks for missing or ambiguous inputs, and applies supported defaults.
- [x] Given valid inputs, when I request generation, then the assistant invokes `dotnet_cache` and reports an outcome supported by the response, including the requested scope and any confirmed generated files.
- [x] Given an unavailable tool, unsupported input, or failed execution, when I request generation, then the assistant explains the impediment without claiming success or silently switching to CLI execution.

## Plan

- [x] Add the independent English-language `geek-dotnet-cache` skill to the existing installable catalog.
- [x] Describe usage conditions, prerequisites, required inputs, defaults, supported scopes, and target workspace context.
- [x] Explain MCP invocation and communication of confirmed outcomes, errors, and unavailable tools.
- [x] Review content against the current tool and shared service, document catalog availability, and update tracking statuses.

## Current implementation context

- MCP tool: `dotnet_cache`.
- Required parameters: `name` and `projectName`. Optional parameter: `scope`, defaulting to `basic`.
- Supported scopes are `basic`, `corp`, and `corp-co`, selecting `geek-cache`, `geek-cache-corp`, and `geek-cache-corp-co`, respectively. Although the common parser recognizes `corp-co-code`, this service rejects it.
- Generation requires the .NET SDK and the corresponding installed Geek template, and runs in the MCP process working directory.
- The tool exposes no working-directory, output-directory, cache-provider, or expiration parameter. Do not invent such inputs or promise unverified template artifacts.
- The response contains `ExitCode` and captured `Output`; invocation exceptions must also be handled without reporting success.

## Validation

Review the skill against the MCP contract and shared implementation for prerequisites, parameters, defaults, supported scopes, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required.

## Out of scope

- Refactoring existing caches or configuring cache providers and expiration policies.
- Changes to templates, tools, shared services, commands, or installers.
- Skills for other tools.
