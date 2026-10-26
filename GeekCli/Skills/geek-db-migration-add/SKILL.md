---
name: geek-db-migration-add
description: Use the Geek CLI db_migration_add MCP tool to add an Entity Framework migration to a Geek schema project.
---

# Add a Geek database migration

Use this skill when the user asks to create or add an Entity Framework migration for a Geek database schema project.

## Prerequisites and parameters

- The `db_migration_add` MCP tool must be available in the assistant's current tool set.
- The tool requires `projectName`, `migrationName`, and `issue` as strings. `projectName` identifies the Geek project, `migrationName` is the requested migration name, and `issue` identifies the output folder beneath `Migrations`.
- The optional `init` parameter is a boolean and defaults to `false`. `init=false` targets `<projectName>.SchemaUpdates`; `init=true` targets `<projectName>.SchemaInitialization`.
- Inspect the workspace and the user's request for these values. Present inferred values and ask the user to confirm them before execution. Ask for any missing value. If a value has multiple plausible candidates, explain the ambiguity and ask which one to use; do not choose silently.
- Do not run the migration operation until all required values are known and the user confirms the proposed parameters, including the `init` choice.

## Invoke the tool

After confirmation, call only `db_migration_add` with `projectName`, `migrationName`, `issue`, and `init` (use `false` when the user has not requested schema initialization). Do not substitute CLI commands or another tool if this MCP tool is unavailable or execution fails.

## Report the result

The tool returns `ExitCode` and `Output`:

- If `ExitCode` is `0`, report that the tool completed successfully and summarize relevant details from `Output`. Claim a migration was added only when the result confirms it.
- If `ExitCode` is nonzero, explain that the operation failed and include useful details from `Output`; do not report success.
- If the output is empty or does not establish whether a migration was added, state that the tool returned no confirmation. Do not infer that files or database state changed.
- If `db_migration_add` is unavailable, explain that you cannot add the migration because the MCP tool is unavailable. Do not fall back to CLI commands or claim the operation ran.
