---
name: geek-db-scaffold
description: Use the Geek CLI db_scaffold MCP tool to generate an Entity Framework model from an existing database table.
---

# Scaffold a database table

Use this skill when the user wants to generate Entity Framework scaffolding for an existing database table.

## Identify the project and gather inputs

- Inspect the workspace and request to identify the target project. The tool has no project or working-directory parameter, so execution uses the MCP process working directory. If the intended project cannot be determined or that working directory may target a different project, clarify before proceeding.
- For Connection work in an existing API solution, scaffold only from a local database. Never scaffold against test, staging, production, or another non-local database. If the database environment is unclear, ask before invoking the tool.
- For Connection work, verify that the MCP process working directory is the Connection project root, alongside `Connection.csproj`, before invoking the tool. The `outputDir` parameter selects the generated file destination; it does not change the process working directory. If the working directory cannot be verified, do not invoke the tool.
- For Connection work, place table entities in the matching schema's `Entities` folder and use its namespace. Database table names commonly start with `MR_` or `TR_`; remove that prefix and use the singular PascalCase entity name when that matches the target solution (for example, `MR_BRANDS` becomes `BrandEntity` in `Brand.entity.cs`). Do not assume a particular schema name.
- Preserve all data annotations produced by scaffolding. Do not add or remove annotations. If an annotation appears missing, report it for review rather than guessing.
- Select the entity base class and interface according to the table's key scope: no corporation uses `Entity` and `IEntity`; corporation uses `EntityWithCorporation` and `IEntityWithCorporation`; corporation and company use `EntityWithCorporationAndCompany` and `IEntityWithCorporationAndCompany`. Do not redeclare properties inherited from the selected base, including corporation/company keys and common audit fields. Check that primary-key mapping matches the actual table and inherited keys.
- Each entity must expose its static `Builder` action, and the schema's `<Schema>Context` at the schema folder root must register every entity's builder in `OnModelCreating`. Verify that the context derives from `DbContext`, accepts `DbContextOptions<<Schema>Context>`, configures the schema's default database schema, and uses the target solution's established collation. The agreed example is `Modern_Spanish_CI_AS`; inspect the target and ask if its collation is missing or ambiguous.
- After scaffolding, inspect the resulting context and entities for the expected paths, filenames, namespaces, builders, annotations, base types, and key mappings. Report missing or ambiguous conventions for user review; do not infer fixes.
- The `db_scaffold` MCP tool requires four string parameters: `table`, `outputDir`, `connectionString`, and `provider`. It also accepts optional `startupProject` and `project` strings, both defaulting to `null`.
- Ask for any missing or ambiguous table, output directory, connection string, or provider information. Use `startupProject` when EF Core needs a separate startup project, and `project` when the EF Core target project should be specified. These map to `dotnet ef --startup-project` and `dotnet ef --project`, respectively; omit either parameter when it is not needed.
- The provider must be `SqlServer` or `Postgres` (case-insensitive). The target project must have the .NET SDK, EF tooling, and the matching provider package available for `dotnet ef dbcontext scaffold`.
- Treat `outputDir` as the output directory passed to EF scaffolding; relative paths resolve from the MCP process working directory. Confirm the intended destination when its meaning is unclear.

## Invoke and report the result

- Once the inputs are clear, invoke `db_scaffold` with the confirmed required parameters and include `startupProject` and/or `project` only when needed. Do not substitute CLI execution or another tool.
- The tool requests Entity Framework scaffolding for the selected table, using data annotations and the requested output directory. Describe this requested result without claiming specific files were created unless the tool output confirms them or you inspect the files.
- The tool has no force-overwrite option. If scaffolding encounters existing files, describe what happened only when the tool output or file inspection confirms it.
- The tool returns an `ExitCode` and captured `Output`. Report success only when the exit code is zero and the output supports that conclusion. Mention generated paths only when confirmed by the output or inspected files.
- For a nonzero exit code or tool error, explain that scaffolding failed and share useful details from the result. Do not report success or claim generated artifacts.
- If the provider is unsupported, explain that only `SqlServer` and `Postgres` are accepted and ask the user to choose a supported provider; do not invoke the tool with another provider value.
- If `db_scaffold` is unavailable, explain that the request cannot proceed because the MCP tool is unavailable. Do not silently switch to CLI execution.
