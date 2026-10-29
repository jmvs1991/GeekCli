# Migration agent requirements and implementation plan

Related backlog: [Database migration agent](../../backlog/20261027-migration-agent.md)

Status: Completed

This document preserves the requirements agreed during the conversation on October 27, 2026. It specifies the future agent; creating this document does not mean the agent or its migration operations have been implemented.

## Deliverable and execution model

- Deliver a Codex agent under `GeekCli/Agents`, installable using the existing Geek CLI agent installer and its interactive wizard. Proposed filename: `geek-db-migration-specialist.toml`.
- Start with one coordinating agent, using the existing skills rather than separate worker agents. The initial assessment found no independent responsibility requiring a subagent.
- Support both a natural-language request for a change and existing code or SQL changes that need a migration completed.
- Analyze the request, prepare a plan, and complete the necessary code or SQL, except for table-alteration SQL, which the user writes.
- Present the plan in Spanish and save its contents in English under `docs/plans/` at the target project's root. Follow the target repository's documented layout when it defines additional nesting or naming rules. In this repository, plans belong under `docs/plans/<related-backlog-name>/`.
- Divide the plan into parts that can be completed separately, with Markdown task checkboxes. Wait for plan approval before execution, complete one part, update progress, and wait for the user's instruction before continuing.
- Preserve unfinished work as unchecked tasks. Do not mark a plan completed until every acceptance criterion and task is checked.

## Initial checks and migration parameters

1. At the start, ask whether the user has updated the repository and applied its latest migrations to the database. If not, stop and wait for confirmation that both are up to date before continuing. Do not silently apply outstanding migrations.
2. Verify that the target workspace contains a solution and the matching projects `<projectName>.Context`, `<projectName>.Manager`, `<projectName>.SchemaInitialization`, and `<projectName>.SchemaUpdates`.
3. Derive `projectName` from the name before `.SchemaInitialization` or `.SchemaUpdates`. If there are multiple plausible project families or the structure is incomplete, explain the issue and request clarification rather than guessing.
4. Ask whether the requested migration is an update or initialization. Use `init=false` for updates and `init=true` for initialization.
5. Propose a migration name based on the requested change using `UPPER_SNAKE_CASE`, for example `UPDATE_SP_TR_QUOTATION_CANCEL`.
6. Default `issue` to `TL_yyyyMMdd`, using the current date, unless the user supplies a different value. Example: `TL_20261027`.
7. Present operation parameters and honor the selected skill's confirmation requirements. Existing authorization and explicit values supplied by the user remain relevant; do not invent missing inputs.

## Required skills and execution boundaries

Use the existing skills:

- `GeekCli/Skills/geek-db-migration-add/SKILL.md`
- `GeekCli/Skills/geek-db-migration-remove/SKILL.md`
- `GeekCli/Skills/geek-db-migration-rollback/SKILL.md`
- `GeekCli/Skills/geek-db-script/SKILL.md`

Use each skill's corresponding MCP tool with its documented inputs and confirmation rules. Do not substitute manual file creation or CLI execution when a required MCP tool is missing or fails. Report failures or unavailable tools without claiming that an operation succeeded.

All SQL script files must originate from `geek-db-script`; the agent may then complete generated templates where allowed by the requirements below. Preserve the skill's generated Up/Down and synonym artifacts and report their actual paths. Script generation does not apply changes to a database. Some Down templates are empty and require completion; do not present them as finished rollbacks.

Removing the latest migration and returning the database to a specified migration use their respective skills. A stored procedure's Down script is distinct from invoking the database rollback tool.

## Generated migration attributes

- Include `using EntityFrameworkMigration.DataAnnotations;`.
- Add `[InitialMigration(true)]` to initialization migrations and `[InitialMigration(false)]` to update migrations.
- Add `EnviromentMigration`, preserving that exact spelling from the project convention.
- Inspect existing migrations in the selected schema project only: `SchemaInitialization` for initialization or `SchemaUpdates` for updates.
- Gather all environment values used there and propose them using the attribute syntax established by that project. The user must review and choose the environments applicable to the change. If no usable precedent exists, ask rather than invent environment values.

Example supplied for initialization:

```csharp
using EntityFrameworkMigration.DataAnnotations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IngSelMigration.SchemaInitialization.Migrations.TL_20261027
{
    [EnviromentMigration(Development)]
    [InitialMigration(true)]
    public partial class UPDATE_SP_TR_QUOTATION_CANCEL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
```

## Creating tables

Create tables using `MigrationBuilder` and the project's Schema Builder column extension methods. Define a public `__TABLE__` constant for the table name.

Use the scope specified in the request. If missing, ask whether the table has corporation and company plus a code, corporation plus a code, or a code only. Include `CORPORATION` and `COMPANY` only when appropriate to that scope.

Common columns are `ID`, the applicable scope columns, the business code, `CREATION_DATE`, `CREATION_DATEL`, `UPDATE_DATE`, `UPDATE_DATEL`, `CREATE_USER`, and `UPDATE_USER`. Use the matching extension methods, including `IdColumn`, `CorporationColumn`, `CompanyColumn`, `CodeColumn`, `CreationDateColumn`, `CreationDatelColumn`, `UpdateDateColumn`, `UpdateDatelColumn`, `CreateUserColumn`, and `UpdateUserColumn`.

The primary key combines the applicable scope columns and code. Create a unique index on `ID`, filtered by `[ID] IS NOT NULL`. Create the table synonym using `CreateSynom`. In Down, call `DropSynom` before `DropTable`.

User-provided example for a corporation/company/code table:

```csharp
public const string __TABLE__ = "MR_BRANDS";

protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(name: __TABLE__,
                                 schema: Catalog,
                                 columns: table => new
                                 {
                                     ID = table.IdColumn(),
                                     CORPORATION = table.CorporationColumn(),
                                     COMPANY = table.CompanyColumn(),
                                     CODBRAND = table.CodeColumn("CODBRAND"),
                                     CREATION_DATE = table.CreationDateColumn(),
                                     CREATION_DATEL = table.CreationDatelColumn(),
                                     UPDATE_DATE = table.UpdateDateColumn(),
                                     UPDATE_DATEL = table.UpdateDatelColumn(),
                                     CREATE_USER = table.CreateUserColumn(),
                                     UPDATE_USER = table.UpdateUserColumn()
                                 },
                                 constraints: table =>
                                 {
                                     table.PrimaryKey($"PK_{__TABLE__}", x => new { x.CORPORATION, x.COMPANY, x.CODBRAND });
                                 });

    migrationBuilder.CreateIndex(name: $"IX_{__TABLE__}_ID",
                                 table: __TABLE__,
                                 schema: Catalog,
                                 column: "ID",
                                 unique: true,
                                 filter: "[ID] IS NOT NULL");

    migrationBuilder.CreateSynom(__TABLE__, Catalog);
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropSynom(__TABLE__);
    migrationBuilder.DropTable(__TABLE__, Catalog);
}
```

## Stored procedures for new tables

- Create three procedures with every new table: Insert, Update, and Delete.
- Name them `SP_<EXACT_TABLE_NAME>_<OPERATION>`. For `MR_BRAND`, use `SP_MR_BRAND_INSERT`, `SP_MR_BRAND_UPDATE`, and `SP_MR_BRAND_DELETE`. Do not singularize or pluralize the actual table name.
- Create all three in the same schema as the table, each with its own synonym. Generate their script and synonym templates through `geek-db-script`.
- Follow the project's common `BEGIN TRY` / `BEGIN CATCH` pattern. The supplied example collects `ERROR_MESSAGE`, `ERROR_LINE`, `ERROR_NUMBER`, `ERROR_SEVERITY`, `ERROR_STATE`, and `ERROR_PROCEDURE`, logs through `SP_PR_ERRORS_INSERT` when severity is not 12, and raises the error with `RAISERROR`. Adapt corporation/company inputs to the table's scope and consult local examples for the appropriate error-logging signature.
- Every procedure that inserts or updates data must return the affected row with a SELECT listing every table column explicitly, filtered by the complete primary key. Never use `SELECT *`.
- Update procedures receive the primary key and update `UPDATE_USER`, `UPDATE_DATE`, and `UPDATE_DATEL` for each update.
- Delete procedures receive the primary key, delete directly without an existence check, and do not return a SELECT. Additional consistency checks are included only when required by the requested change.
- Corporation and company parameters depend on whether those columns exist in the table.

The supplied Insert example is `Sales.SP_TR_QUOTATION_INSERT` for `Sales.TR_QUOTATIONS`. It uses `SP_PR_SEQUENCE_INSERT` to obtain `CODQUOTATION`, takes the UTC timestamp with `SYSUTCDATETIME()`, derives a numeric `yyyyMMdd` value through SQL conversion, writes creation audit fields, and selects an explicit column list using `CORPORATION`, `COMPANY`, and `CODQUOTATION`. These details are a reference for following existing project patterns, not a requirement to copy quotation-specific fields, statuses, or business logic to unrelated tables. The sample omits declarations for some business parameters; it is not a complete executable template.

## Changing existing stored procedures

- Generate modification scripts through `geek-db-script` and complete the requested change following the common procedure conventions.
- Inspect the latest migrations affecting that specific procedure to identify its definition immediately before the proposed change.
- Prepare Down to restore that previous definition, rather than merely removing the procedure or copying the changed version.
- Present the proposed rollback for the user's verification that it restores the correct prior state. Ask for clarification if the previous definition cannot be established clearly.

## Altering existing tables

- Apply table alterations through SQL scripts, not `MigrationBuilder` in Up.
- Generate the script files through `geek-db-script`. The user writes the SQL for the table alteration; the agent must not silently complete that SQL on the user's behalf.
- Down may use `MigrationBuilder` to reverse the alteration. For example, adding a column uses user-written SQL in the generated Up script and `migrationBuilder.DropColumn(...)` in Down.
- Reflect user-owned SQL work in the plan and leave it pending until completed. Do not report an empty generated template as a completed change.

## Acceptance criteria

- [x] The packaged agent is available through the existing direct CLI and interactive installer, for project and user scope.
- [x] The agent enforces the initial repository/database confirmation, validates the required project family, asks for migration type, and uses the agreed parameter and attribute conventions.
- [x] The agent presents plans in Spanish, saves them in English under the target root's `docs/plans/`, and executes only one approved part at a time while preserving task status.
- [x] New-table workflows follow the Schema Builder, scope, primary key, ID index, synonym, and three-procedure conventions, including explicit-column SELECT behavior and the Delete exception.
- [x] Existing-procedure workflows prepare and present the previous definition for rollback review; table-alteration workflows leave forward SQL authoring to the user and permit MigrationBuilder reversals.
- [x] All migration operations and script-file generation use the required skills, with accurate reporting of failures, missing tools, generated templates, and completed work.

## Implementation tasks

- [x] Create `GeekCli/Agents/geek-db-migration-specialist.toml` using the supported agent-definition format and encode all requirements above.
- [x] Verify the bundled agent can access or clearly identify its four skill dependencies. Do not assume the agent installer automatically installs skills; dependency installation is not an agreed new CLI feature.
- [x] Validate the single-agent approach against the workflows; document any justified change to the initial recommendation before introducing coordinated subagents.
- [x] Update README examples and catalog descriptions to document the new agent, its dependencies, and installation.
- [x] Validate packaging and installation through the existing direct CLI and wizard in disposable destinations, covering project and user scope.
- [x] Review the agent's behavior against the scenarios below, recording any verification that could not be executed and why.
- [x] Update backlog story statuses and this plan's checkboxes to reflect actual completion.

## Validation scenarios

- [x] Repository/database not yet updated: the agent waits for confirmation before continuing.
- [x] Missing or ambiguous project structure: the agent explains the issue instead of guessing a project name.
- [x] Update and initialization requests: correct schema target, attributes, explicit type question, default issue, overridden issue, and UPPER_SNAKE_CASE name.
- [x] Environment discovery: only the selected schema project is inspected, and the proposed environments are presented for user review.
- [x] Both starting points: a change described in words and preexisting code or SQL changes.
- [x] Plan approval and execution: Spanish presentation, English file, partial completion, unchecked remaining tasks, and waiting between parts.
- [x] New tables at all three scopes: correct columns, primary key, filtered unique ID index, synonyms, and reversible table creation.
- [x] Generated Insert/Update/Delete scripts: exact table-based names, same schema, synonyms, common error handling, audit updates, explicit-column primary-key-filtered results, and no Delete SELECT or unsolicited existence check.
- [x] Stored procedure change: previous definition found in migration history, Down prepared, and user review requested; unclear history is surfaced.
- [x] Add-column change: skill-generated forward template left for user SQL and DropColumn reversal in Down.
- [x] Missing or failed MCP tool: no manual or CLI fallback and no unsupported success claim.
- [x] Run `dotnet build .\GeekCliSolution.sln`, `dotnet build .\GeekCli\GeekCli.csproj -c Release`, and `dotnet pack .\GeekCli\GeekCli.csproj -c Release` when implementing and packaging the agent; inspect that its TOML is included.
- [x] Install using `geek-cli ai agent install geek-db-migration-specialist --scope project --project-path <path>`, user scope, and the AI wizard; confirm the installed definition matches the bundled artifact.

No builds or generator execution are required for creating these documentation files. Implementation and runtime verification remain pending. Mark this plan Completed only when every acceptance criterion, implementation task, and validation scenario is checked.
