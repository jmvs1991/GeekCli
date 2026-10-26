# Angular component and page generation skills

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 12
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want one skill per Angular component and page tool, so that I can generate them in the appropriate project.

## Acceptance criteria

- [x] Given the corresponding skill is installed and its MCP tool is available, when I request an Angular component or page, then the assistant explains prerequisites, clarifies the target workspace and generation mode, asks for missing or ambiguous inputs, and applies supported defaults.
- [x] Given valid inputs for either supported generation mode, when I request generation, then the assistant invokes `ngx_component` or `ngx_page` through MCP and reports an outcome supported by the response, identifying the requested resource, target location, and any confirmed generated artifacts or module updates.
- [x] Given an unavailable tool, invalid destination, missing prerequisite, or failed execution, when I request generation, then the assistant explains the impediment and available actionable details without claiming success or executing generation commands directly.

## Plan

- [x] Add two independent English-language skills to the existing installable catalog, one for Angular components and one for pages.
- [x] Describe prerequisites, mode selection, required inputs, defaults, and destination and module conventions for each operation.
- [x] Make MCP the exclusive generation path for each skill: collect inputs, invoke the corresponding MCP tool, and communicate confirmed outcomes and errors; if MCP is unavailable, report the impediment without falling back to direct CLI execution.
- [x] Review both skills against the current tools and shared services, document their availability, and update tracking statuses.

## Current implementation context

- Proposed skill names: `geek-ngx-component` and `geek-ngx-page`; MCP tools: `ngx_component` and `ngx_page`.
- The assistant invokes these MCP tools exclusively for generation. Angular CLI and Nx are internal execution details of the MCP tools, not alternative invocation paths for the skills. The skills must not instruct the assistant to run `ng`, `npx nx`, or Geek CLI generation commands directly.
- Both tools require `name`. Optional inputs are `nx`, defaulting to `false`, and nullable `project`, `folder`, and `module`. In Nx mode all three additional inputs are required. Neither tool exposes a working-directory parameter.
- Internally, both MCP tools launch `cmd`. With `nx=false`, the tool invokes Angular CLI (`ng`); with `nx=true`, it invokes `npx nx generate @nx/angular:component`. These commands describe existing tool behavior only. Describe Windows command availability, Node.js and the relevant installed workspace tooling as prerequisites.
- Angular CLI mode generates with `ng g c <name> --prefix ngx` for components and `ng g c <name> --type page` for pages. The other optional inputs are not used in this mode. Do not confuse an Angular CLI project with Angular standalone component generation; this mode does not explicitly set `--standalone`.
- Nx mode requires execution from a workspace root containing `nx.json`. Both operations convert the resource name to kebab-case and explicitly generate non-standalone components declared in an NgModule, with type `component` or `page` and selector `ngx-<name>-component` or `ngx-<name>-page`.
- For components, the tool describes a `tech:angular` application or library target. Inspect project configuration to resolve the appropriate destination: a workspace-relative folder under the application app root or library sourceRoot. The shared generator uses the supplied folder without PascalCase conversion and derives a relative module reference from the workspace-relative NgModule file path.
- For pages, the target is an Angular application under `apps/<project>/src/app`. The folder may be relative to that app root or include its workspace-relative prefix; remaining folder segments are converted to PascalCase. The module is passed as supplied, using a workspace-relative NgModule file path.
- Shared Nx validation rejects missing required inputs, parent directory segments in the folder, folders targeting another application under apps/, and execution outside a workspace root. It does not fully verify project tags, library ownership, or module existence; do not claim that it does.
- Both tools return `ExitCode` and captured `Output`. Handle invocation exceptions and nonzero exit codes, and report files and module updates only when confirmed by output or file inspection.

## Validation

Review both skills against the current MCP contracts and shared implementations for prerequisites, modes, parameters, defaults, destination conventions, invocation, results, and error handling. As agreed in the backlog, no TDD or installation testing is required. No CLAUDE.md was found in the repository file listing; repository AGENTS.md instructions and existing skill refinements provide the local context.

## Out of scope

- Implementing component or page business behavior, UI design, navigation, or routing beyond the generator's confirmed output.
- Changes to generators, MCP tools, shared services, CLI commands, wizards, or installers, including differences between adapter validation.
- Skills for other tools.
