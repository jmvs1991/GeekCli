# Angular service generation skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 13
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate Angular services, so that I can incorporate them into my project.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request an Angular service, then the assistant explains prerequisites, clarifies the target workspace and generation mode, asks for missing or ambiguous inputs, and applies supported defaults.
- [x] Given valid inputs for either supported generation mode, when I request generation, then I receive an outcome supported by the tool response, identifying the requested service, target location, and any confirmed generated artifacts.
- [x] Given an unavailable tool, missing prerequisite, invalid input, or failed execution, when I request generation, then the assistant explains the impediment and available actionable details without claiming success or executing generation commands directly.

## Plan

- [x] Add an independent English-language Angular service skill to the existing installable catalog.
- [x] Describe prerequisites, mode selection, required inputs, defaults, workspace context, and destination conventions.
- [x] Make MCP the exclusive generation path: collect inputs, invoke the corresponding MCP tool, and communicate confirmed outcomes and errors; report unavailable MCP without falling back to direct CLI execution.
- [x] Review the skill against the current tool and shared service, document its availability, and update tracking statuses.

## Current implementation context

- Proposed skill name: `geek-ngx-service`; MCP tool: `ngx_service`.
- Required input: `name`. Optional inputs: `nx`, defaulting to `false`, and nullable `project` and `folder`. In Nx mode, a nonblank name, project, and project-relative folder are required.
- The assistant invokes `ngx_service` through MCP exclusively for generation. Angular CLI and Nx are internal execution details of the tool. Do not instruct the assistant to run `ng`, `npx nx`, or Geek CLI generation commands directly.
- Internally, the tool launches Windows `cmd`. With `nx=false`, the shared service invokes `ng g s <name>` in the MCP process working directory; `project` and `folder` are unused in this mode.
- With `nx=true`, the shared service invokes `npx nx generate @nx/angular:service "<folder>/<name>" --project="<project>" --type=service`. Describe Windows command availability, Node.js, and the relevant installed workspace tooling as prerequisites.
- Nx execution requires the MCP process working directory to contain `nx.json`. The shared validator rejects missing required Nx inputs and parent directory segments in the folder. It does not verify project existence, project tags, or folder ownership; do not claim that it does.
- The Nx destination folder is project-relative. The shared service normalizes folder separators and trims leading and trailing slashes before appending the supplied name; it does not explicitly convert names or folder segments to PascalCase or kebab-case. Do not copy the component or page destination conventions into this skill.
- The tool exposes no module, working-directory, output-directory, or provider-scope parameter. Do not invent these inputs or promise unverified generated filenames, provider registration, or tests.
- The response contains `ExitCode` and captured `Output`. Handle invocation exceptions and nonzero exit codes, and report generated files only when confirmed by output or file inspection.

## Validation

Review the skill against the current MCP contract and shared implementation for prerequisites, modes, parameters, defaults, destination conventions, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required. No CLAUDE.md was found in the repository file listing; repository AGENTS.md instructions and existing skill refinements provide the local context.

## Out of scope

- Implementing business logic, HTTP integrations, provider configuration, or application-specific tests in generated services.
- Changes to generators, MCP tools, shared services, CLI commands, wizards, or installers.
- Skills for other tools.
