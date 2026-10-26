---
name: geek-db-migration-remove
description: Use the Geek CLI db_migration_remove MCP tool to remove the latest Entity Framework migration from a Geek schema project.
---

# Remove the latest Geek database migration

Use this skill when the user asks to remove the latest Entity Framework migration from a Geek database schema project.

## Prerequisites and parameters

- The `db_migration_remove` MCP tool must be available in the assistant's current tool set.
- The tool requires `projectName`, a string identifying the Geek project.
- The optional `init` parameter is a boolean and defaults to `false`. `init=false` targets `<projectName>.SchemaUpdates`; `init=true` targets `<projectName>.SchemaInitialization`.
- Inspect the workspace and the user's request for `projectName` and the intended schema project. Present inferred values and ask the user to confirm them before execution. Ask for missing information. If multiple projects or schema targets are plausible, explain the ambiguity and ask which one to use; do not choose silently.
- Removing a migration changes project files. Do not execute until the user confirms the proposed project and `init` value.

## Invoke the tool

After confirmation, call only `db_migration_remove` with `projectName` and `init` (use `false` when the user has not requested schema initialization). The tool removes the latest migration from the selected schema project. Do not substitute CLI commands or another tool if this MCP tool is unavailable or execution fails.

## Report the result

The tool returns `ExitCode` and `Output`:

- If `ExitCode` is `0`, report that the tool completed successfully and summarize relevant details from `Output`. State that the latest migration was removed only when the result confirms it.
- If `ExitCode` is nonzero, explain that the operation failed and include useful details from `Output`; do not report success.
- If the output is empty or does not establish whether a migration was removed, state that the tool returned no confirmation. Do not infer that project files or database state changed.
- If `db_migration_remove` is unavailable, explain that you cannot remove the migration because the MCP tool is unavailable. Do not fall back to CLI commands or claim the operation ran.
