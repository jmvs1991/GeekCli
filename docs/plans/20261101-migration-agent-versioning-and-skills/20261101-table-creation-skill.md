# Reusable table creation skill

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 5

Status: Pending

## Story

As a database maintainer, I want to create tables from a complete definition or a base structure through a reusable skill, so that new tables follow Geek conventions.

## Acceptance criteria

- [ ] Given a table creation request with missing information, when preparation begins, then I am asked for the table name, schema, actual code-column name, and scope, and I can choose to provide the complete definition or generate only the base structure.
- [ ] Given the required information, when the table is prepared, then columns appear in this order: `ID`, `CORPORATION` and `COMPANY` where applicable, code, additional columns, `CREATION_DATE`, `CREATION_DATEL`, `UPDATE_DATE`, `UPDATE_DATEL`, `CREATE_USER`, and `UPDATE_USER`; the primary key combines scope columns and code, and the table has a unique index on `ID` and its synonym.
- [ ] Given a prepared table, when I review its reversal, then it drops the synonym before dropping the table; the base structure is available as a template bundled with the skill.

## Plan

- [ ] Add `geek-db-table-create` with a reusable template and two modes: base structure or complete definition; ask only for missing information.
- [ ] Adapt the template using private constants `__TABLE_NAME__` and `__SCHEMA__`, actual values, and the project's column extensions; never use the illustrative `CODTEST` as a default code-column name.
- [ ] Prepare the table using `MigrationBuilder`, preserving the required column order, selected scope, and requested additional fields; do not invent business fields in base-structure mode.
- [ ] Prepare primary key `PK_<table>`, unique index `IX_<table>_ID` without a filter, and `CreateSynom`, `DropSynom`, and `DropTable` operations, preserving existing migration content.
- [ ] Review both modes, all three scopes, column order, and reversal; verify that the template is included in the package and correct the documented SQL-template dependency for this story.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Validate instructions, template, references, scenarios, and the diff through direct review. This instruction-only change does not require builds or generator execution. Check that unresolved information or ambiguous project conventions are raised with the user and remain pending rather than being invented.

## Out of scope

- Generating SQL scripts to create the table.
- Creating stored procedures, reserved for story #6.
- Redefining or versioning the agent, reserved for story #9.
- Creating the migration or changing its attributes.
- Executing database changes or adding commands, services, or MCP tools.

## Agreed assumptions

- The skill operates on a migration already prepared through `geek-db-migration-add`.
- `IdColumn()` defines an auto-incrementing, non-nullable `ID`; its unique index needs no filter.
- Supported scopes are corporation + company + code, corporation + code, or code only. `CORPORATION` and `COMPANY` are included only when the selected scope requires them.
- Use the matching `IdColumn`, `CorporationColumn`, `CompanyColumn`, `CodeColumn`, `CreationDateColumn`, `CreationDatelColumn`, `UpdateDateColumn`, `UpdateDatelColumn`, `CreateUserColumn`, and `UpdateUserColumn` extensions. Pass the actual code-column name to `CodeColumn`.
- Additional columns belong strictly between the code column and `CREATION_DATE`.
- The base structure is complete for the selected scope without requiring additional business columns.
- The agent retains workflow coordination. Table creation uses `MigrationBuilder` without redundant SQL templates; stored-procedure script generation remains outside this skill.
