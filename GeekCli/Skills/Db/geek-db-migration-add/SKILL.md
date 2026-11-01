---
name: geek-db-migration-add
description: Add a Geek Entity Framework migration through db_migration_add and complete its initialization and user-selected environment attributes.
---

# Add a Geek database migration

Use this skill when the user asks to create or add an Entity Framework migration for a Geek database schema project.

## Prerequisites and parameters

- The `db_migration_add` MCP tool must be available in the assistant's current tool set.
- The tool requires `projectName`, `migrationName`, and `issue` as strings. `projectName` identifies the Geek project, `migrationName` is the requested migration name, and `issue` identifies the output folder beneath `Migrations`.
- The optional `init` parameter is a boolean and defaults to `false`. `init=false` targets `<projectName>.SchemaUpdates`; `init=true` targets `<projectName>.SchemaInitialization`.
- Inspect the workspace and the user's request for these values. Present inferred values and ask the user to confirm them before execution. Ask for any missing value. If a value has multiple plausible candidates, explain the ambiguity and ask which one to use; do not choose silently.
- Do not run the migration operation until all required values are known and the user confirms the proposed parameters, including the `init` choice.

## Select applicable environments

Resolve the selected schema project from the confirmed `projectName` and `init`. Inspect existing migrations only in that project: `<projectName>.SchemaInitialization` for `init=true`, or `<projectName>.SchemaUpdates` for `init=false`. If the selected project cannot be identified unambiguously, ask before proceeding.

Gather the environment values used by `EnviromentMigration`, preserving that exact spelling. Present the available values and their established attribute syntax for the user to review and choose; do not select all environments or infer a selection from the current runtime environment. If there is no usable precedent, ask the user for the environment values and any syntax needed to express them. Do not borrow values from the other schema project or invent values or attribute syntax. Keep preparation pending until the selection and syntax are resolved.

Environment selection is part of this skill's preparation, not an additional MCP parameter. These attributes describe where a migration is applicable; they do not authorize database execution. When invoked by the migration agent, retain its local development database execution restriction.

## Invoke the tool

After confirmation, call only `db_migration_add` with `projectName`, `migrationName`, `issue`, and `init` (use `false` when the user has not requested schema initialization). Do not substitute CLI commands or another tool if this MCP tool is unavailable or execution fails.

## Complete the generated migration

Proceed only when the tool result confirms successful migration creation. Locate the generated migration class in the selected project's `Migrations/<issue>` folder using the confirmed name, tool output, and actual generated files. If the generated file cannot be identified unambiguously, report that uncertainty and ask rather than editing a guessed file or rerunning generation.

Complete the generated migration class with:

- `using EntityFrameworkMigration.DataAnnotations;`
- `[InitialMigration(true)]` for `init=true`, or `[InitialMigration(false)]` for `init=false`.
- `EnviromentMigration` attributes expressing only the user's chosen environments with the established or user-confirmed syntax.

Inspect existing declarations before editing so matching imports and attributes are not duplicated. Preserve the generated migration body, designer metadata, and model snapshot. Do not create a replacement migration manually. Review the resulting class and diff against the confirmed initialization choice and environment selection. If attribute preparation cannot be completed, report the generated file and the remaining work without claiming the migration is ready.

## Report the result

The tool returns `ExitCode` and `Output`:

- If `ExitCode` is `0`, report that the tool completed successfully and summarize relevant details from `Output`. Claim a migration was added only when the result confirms it.
- If `ExitCode` is nonzero, explain that the operation failed and include useful details from `Output`; do not report success.
- If the output is empty or does not establish whether a migration was added, state that the tool returned no confirmation. Do not infer that files or database state changed.
- If `db_migration_add` is unavailable, explain that you cannot add the migration because the MCP tool is unavailable. Do not fall back to CLI commands or claim the operation ran.

Report migration generation and attribute preparation separately. Include the actual prepared file path, initialization/update choice, selected environments, and any pending work. Claim preparation is complete only after verifying the attributes in the generated file. Creating and editing migration files does not apply the migration to a database; do not apply it as part of this skill.
