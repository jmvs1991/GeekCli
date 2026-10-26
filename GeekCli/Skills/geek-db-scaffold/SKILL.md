---
name: geek-db-scaffold
description: Use the Geek CLI db_scaffold MCP tool to generate an Entity Framework model from an existing database table.
---

# Scaffold a database table

Use this skill when the user wants to generate Entity Framework scaffolding for an existing database table.

## Identify the project and gather inputs

- Inspect the workspace and request to identify the target project. The tool has no project or working-directory parameter, so execution uses the MCP process working directory. If the intended project cannot be determined or that working directory may target a different project, clarify before proceeding.
- The `db_scaffold` MCP tool must be available. It requires four string parameters: `table`, `outputDir`, `connectionString`, and `provider`. There are no MCP defaults.
- Ask for any missing or ambiguous table, output directory, connection string, provider, or target project information. Do not invoke the tool until all four parameters and the target project context are clear.
- The provider must be `SqlServer` or `Postgres` (case-insensitive). The target project must have the .NET SDK, EF tooling, and the matching provider package available for `dotnet ef dbcontext scaffold`.
- Treat `outputDir` as the output directory passed to EF scaffolding; relative paths resolve from the MCP process working directory. Confirm the intended destination when its meaning is unclear.

## Invoke and report the result

- Once the inputs are clear, invoke `db_scaffold` with the confirmed `table`, `outputDir`, `connectionString`, and `provider`. Do not substitute CLI execution or another tool.
- The tool requests Entity Framework scaffolding for the selected table, using data annotations and the requested output directory. Describe this requested result without claiming specific files were created unless the tool output confirms them or you inspect the files.
- The tool has no force-overwrite option. If scaffolding encounters existing files, describe what happened only when the tool output or file inspection confirms it.
- The tool returns an `ExitCode` and captured `Output`. Report success only when the exit code is zero and the output supports that conclusion. Mention generated paths only when confirmed by the output or inspected files.
- For a nonzero exit code or tool error, explain that scaffolding failed and share useful details from the result. Do not report success or claim generated artifacts.
- If the provider is unsupported, explain that only `SqlServer` and `Postgres` are accepted and ask the user to choose a supported provider; do not invoke the tool with another provider value.
- If `db_scaffold` is unavailable, explain that the request cannot proceed because the MCP tool is unavailable. Do not silently switch to CLI execution.
