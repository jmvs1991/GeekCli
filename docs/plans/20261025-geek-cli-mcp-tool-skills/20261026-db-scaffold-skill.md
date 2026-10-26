# Database model scaffolding skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 4
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate models from existing tables through MCP, so that I can incorporate my database into my project.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request model generation from an existing table, then the assistant identifies the target project, explains the prerequisites and required inputs, and asks for missing or ambiguous information before execution.
- [x] Given valid inputs, when the assistant invokes model generation, then I receive an outcome supported by the tool result and a description of the resulting Entity Framework scaffolding, without unverified claims about generated files.
- [x] Given an unavailable MCP tool, unsupported provider, or failed execution, when I request model generation, then the assistant explains the impediment and any actionable information available without reporting success or silently switching to CLI execution.

## Plan

- [x] Add one independent English-language skill to the existing installable catalog.
- [x] Describe when to use it, project and database prerequisites, required parameters, supported providers, and output destination behavior.
- [x] Explain MCP invocation and communication of successful outcomes, errors, and unavailable tools.
- [x] Review the content against the current tool and shared service, document catalog availability, and update tracking statuses.

## Current implementation context

- MCP tool: `db_scaffold`; all four parameters are required, with no MCP defaults: `table`, `outputDir`, `connectionString`, and `provider`.
- Supported provider values are `SqlServer` and `Postgres`, matched case-insensitively.
- The shared service invokes Entity Framework scaffolding through `dotnet ef dbcontext scaffold`, with a table filter, data annotations, and the requested output directory.
- Execution inherits the MCP process working directory. The tool offers no project or working-directory parameter.
- Results contain `ExitCode` and captured `Output`; exceptions can also surface as tool errors. Exact artifacts must be confirmed from output or inspected files.
- The current invocation does not include a force-overwrite option.

## Validation

Review skill content against the MCP contract, shared model, provider resolution, process arguments, and result handling. Review project prerequisites and artifact claims against the behavior actually supported. As agreed in the backlog, no TDD, installation testing, or real database scaffolding is required for this content-only story.

## Out of scope

- Changes to tools, shared services, commands, or installers.
- Entity or repository refactoring after generation.
- Skills for other tools.
