# Database SQL script skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 3
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want a skill to generate SQL script templates through MCP, so that I can prepare scripts for my schema changes.

## Acceptance criteria

- [x] Given the skill is installed and its MCP tool is available, when I request SQL script generation, then the assistant presents inferred parameters for confirmation, explains the default destination, and asks for missing or ambiguous information before execution.
- [x] Given confirmed valid parameters, when the assistant generates the scripts, then I receive the confirmed generated paths and an explanation of the resulting Up/Down templates, any creation synonyms, and the SQL still requiring completion, without being told that database changes were applied.
- [x] Given an unavailable MCP tool, invalid inputs, or a failed execution, when I request script generation, then the assistant explains the impediment without reporting unconfirmed success or falling back to CLI execution.

## Plan

- [x] Add one independent English-language skill to the existing installable catalog.
- [x] Describe prerequisites, supported operations, required and conditional parameters, defaults, destination selection, and confirmation of inferred values.
- [x] Explain MCP invocation, generated artifacts, overwrite behavior, incomplete SQL, and communication of successful or failed outcomes.
- [x] Review the skill against the current tool and shared service, document its availability, and update the related tracking statuses.

## Agreed details

- Skill name: `geek-db-script`; MCP tool: `db_script`.
- Required parameters: `projectName`, `schema`, `type`, and `issue`. `objectName` is required except for `query`; when supplied for a query, it determines the filename token instead of `issue`.
- Optional `init` defaults to `false`: target `<projectName>.SchemaUpdates`; `true` targets `<projectName>.SchemaInitialization`. Paths are relative to the MCP process working directory.
- Supported operations: query, create/modify stored procedure, create/modify table, and create/modify view. Document accepted values against the actual parser.
- Follow the confirmed migration skills' interaction: inspect the workspace and request, present inferred values for confirmation, and ask for missing or ambiguous values before invocation.
- Generation writes Up and Down files and overwrites matching existing paths. Create operations also generate dbo synonym Up/Down files. Query and modify operations currently produce empty Down files; Up templates contain SQL to complete.
- Interpret `ExitCode` and `Output`, and handle tool errors. Report paths supported by returned output; never claim that SQL was applied to a database or that templates are complete migrations.

## Validation

Review content against the actual MCP contract, parser, rules, service behavior, and result contract. Cover every supported operation, conditional object names, destination defaults, output paths, overwrites, incomplete templates, errors, and unavailable tools. No TDD, installation tests, or real script generation is required for this content-only story.

## Out of scope

- Changes to tools, shared services, commands, or installers.
- Completing application-specific SQL or executing scripts against a database.
- Skills for other tools.
