---
name: geek-db-migration-rollback
description: Use the Geek CLI db_migration_rollback MCP tool to update a Geek schema database to a specific migration.
---

# Roll back a Geek schema database

Use this skill when the user asks to roll a Geek schema database back or update it to a specific Entity Framework migration.

## Prerequisites and parameters

- The `db_migration_rollback` MCP tool must be available in the assistant's current tool set.
- The tool requires `projectName` and `migrationName`, both strings. `projectName` identifies the Geek project; `migrationName` is the target migration to which the database will be updated.
- The optional `init` parameter is a boolean and defaults to `false`. `init=false` targets `<projectName>.SchemaUpdates`; `init=true` targets `<projectName>.SchemaInitialization`.
- Inspect the workspace and the user's request for these values. Present inferred values and ask the user to confirm them before execution. Ask for missing information. If the migration name, project, or schema target has multiple plausible candidates, explain the ambiguity and ask which one to use; do not choose silently.
- Updating a database can change its schema and data. Do not execute until the user confirms the proposed project, target migration, and `init` value.

## Invoke the tool

After confirmation, call only `db_migration_rollback` with `projectName`, `migrationName`, and `init` (use `false` when the user has not requested schema initialization). The tool updates the selected schema database to the specified migration. Do not substitute CLI commands or another tool if this MCP tool is unavailable or execution fails.

## Report the result

The tool returns `ExitCode` and `Output`:

- If `ExitCode` is `0`, report that the tool completed successfully and summarize relevant details from `Output`. State that the database was updated to the target migration only when the result confirms it.
- If `ExitCode` is nonzero, explain that the operation failed and include useful details from `Output`; do not report success.
- If the output is empty or does not establish whether the database was updated, state that the tool returned no confirmation. Do not infer that database state changed.
- If `db_migration_rollback` is unavailable, explain that you cannot roll back the database because the MCP tool is unavailable. Do not fall back to CLI commands or claim the operation ran.
