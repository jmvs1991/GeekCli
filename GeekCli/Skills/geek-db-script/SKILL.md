---
name: geek-db-script
description: Use the Geek CLI db_script MCP tool to generate SQL script templates for a Geek schema project.
---

# Generate Geek database SQL scripts

Use this skill when the user asks to prepare SQL script templates for a Geek schema project.

## Prerequisites and parameters

- The `db_script` MCP tool must be available in the assistant's current tool set.
- The tool requires `projectName`, `schema`, `type`, and `issue` as strings. `projectName` identifies the Geek project, `schema` is the target schema, and `issue` names the issue folder beneath the scripts directories.
- `type` accepts `query`; `modifysp`, `modifystoredprocedure`, `altersp`, `alterstoredprocedure`; `createsp`, `createstoredprocedure`; `modifytable`, `altertable`; `createtable`; `createview`; or `modifyview`, `alterview`. Matching is case-insensitive and accepts spaces, hyphens, or underscores within these values.
- `objectName` is required for every type except `query`. For a query it is optional; when supplied, it is used for the filename token instead of `issue`.
- The optional `init` parameter is a boolean and defaults to `false`.
- Inspect the workspace and request for these values. Present inferred parameters for confirmation before execution. Ask for missing values and clarify ambiguous project, schema, type, issue, object name, or initialization target instead of choosing silently.
- Do not invoke the tool until all required values are known and the user confirms the proposed parameters, including the `init` choice.

## Destination and generated files

- With `init=false`, generation targets `<projectName>.SchemaUpdates`; with `init=true`, it targets `<projectName>.SchemaInitialization`. Paths are relative to the MCP process working directory.
- The service creates an Up and a Down script beneath `Scripts/<schema>/Up|Down/<operation-folder>/<issue>/`. The operation folder is `Queries`, `Sp`, `Tables`, or `Views`, according to `type`.
- The Up filename is `<CREATE|ALTER|QUERY>_<token>.sql`; the Down filename is `ROLLBACK_<CREATE|ALTER|QUERY>_<token>.sql`. The token is `objectName` when supplied, otherwise `issue` (the latter applies to queries).
- Create operations (`createsp`, `createstoredprocedure`, `createtable`, and `createview`) also generate Up and Down synonym scripts under `Scripts/dbo/Up|Down/Synonyms/<issue>/`.
- Matching output paths are overwritten. Use the paths reported by the tool's `Output` when summarizing generated files; do not invent or normalize returned paths.

## Explain the generated templates

- Every Up script is a starting template with SQL that still requires review and completion. Describe its TODO placeholders accurately; do not present it as a finished migration.
- Query and modify operations currently generate empty Down scripts. Create stored procedure, table, and view operations generate Down scripts with statements to drop the created object.
- For create operations, the synonym Up script replaces an existing dbo synonym of the same name before creating it for the schema object; the synonym Down script drops that synonym if it exists.
- Explain which Up and Down paths were generated and identify any empty Down scripts. SQL script generation only writes template files; never say that database changes were applied.

## Invoke the tool and report the outcome

- After confirmation, call only `db_script` with the confirmed `projectName`, `schema`, `type`, `issue`, `init`, and `objectName` values. Do not substitute a CLI command or another tool.
- On `ExitCode` 0, report successful template generation only when `Output` confirms it, and list the generated paths present in that output. If output is empty or does not confirm paths, say the result did not confirm which files were generated.
- On a nonzero `ExitCode`, explain that generation failed and include useful details from `Output`; do not report success.
- Invalid parameters or execution failures may be returned as MCP tool errors instead of an `McpToolResult`. Explain the error without claiming files were generated.
- If `db_script` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to CLI execution.
