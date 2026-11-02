# Stored procedure modification skill

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 7

Status: Pending

## Story

As a database maintainer, I want to modify an existing stored procedure through a reusable skill and integrate it into a generated migration, so that I can review the change and restore its previous definition through Down.

## Acceptance criteria

- [ ] Given a generated migration and existing Up/Down SQL files, when I confirm the proposed change, then I receive the modified procedure integrated into Up through `CreateOrAlterSpFromSqlFile`, using private string constants `__SCHEMA__` and `__FOLDER__` containing the actual schema and folder.
- [ ] Given migration history that establishes the procedure definition immediately before the change, when I review the reversal, then the Down SQL restores that definition and the migration's Down references that file using the same constants.
- [ ] Given a missing migration, SQL file, change requirement, or conclusive evidence of the previous definition, when I request modification, then I receive an explanation and a clarification request, without invented information or incomplete work reported as finished.

## Plan

- [ ] Define `geek-db-stored-procedure-modify`, receiving the generated migration, both existing SQL files, and the requested change.
- [ ] Include an integration template with private string constants `__SCHEMA__` and `__FOLDER__`, and `CreateOrAlterSpFromSqlFile` calls in Up and Down referencing their respective SQL files.
- [ ] Establish the previous definition from migration history and referenced SQL, present the proposed change and restoration for review, and complete the SQL files after confirmation.
- [ ] Adapt the template to actual values, reuse equivalent existing constants where applicable, and preserve migration attributes, metadata, and unrelated operations.
- [ ] Review instructions, template, references, reversal, and insufficient-information scenarios; update plan and backlog statuses only according to verified progress.

## Agreed template

Bundle this pattern with the skill as an adaptation template, not as a replacement migration class:

```csharp
private const string __SCHEMA__ = "<SCHEMA>";
private const string __FOLDER__ = "<ISSUE_FOLDER>";

/// <inheritdoc />
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateOrAlterSpFromSqlFile(__SCHEMA__, __FOLDER__, "<UP_SQL_FILENAME>");
}

/// <inheritdoc />
protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateOrAlterSpFromSqlFile(__SCHEMA__, __FOLDER__, "<DOWN_SQL_FILENAME>");
}
```

`Sales` in the supplied example denotes a schema, not a required identifier. Resolve every placeholder using the actual schema, script folder, and existing SQL filenames. Do not leave empty schema values or unresolved placeholders in completed work. Integrate the calls into the generated migration without replacing unrelated operations or duplicating matching declarations.

The skill receives a migration previously generated through `geek-db-migration-add` and SQL files previously generated through `geek-db-script`. Their generation remains coordinated outside this skill. Trace the latest applicable migrations and their SQL to identify the definition immediately preceding the proposed change; if the evidence is ambiguous or missing, ask for clarification and keep the affected work pending. Down must restore that definition rather than drop the procedure or repeat the new definition. Identify the historical precedent when presenting the restoration for review.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Validate by directly reviewing skill instructions, the bundled template, and the diff. Check a confirmed modification with a verifiable previous definition; matching schema, folder, and Up/Down SQL references; existing constants and unrelated migration operations; and missing or ambiguous migration, SQL, change details, or history. Confirm that pending work is not reported as complete and that no SQL execution or generation is introduced. For this instructions-and-template change, no builds or generator execution are required. Report any verification that cannot be performed and why.

## Out of scope

- Generating migrations or scripts, or invoking their generation tools from this skill.
- Executing SQL or applying migrations to a database.
- Redefining or versioning the migration agent; that remains story #9.
- Adding CLI commands, MCP tools, or unconfirmed business logic.

## Refinement decisions

The user selected existing SQL files as inputs and requested that the skill prepare the modified procedure. The user additionally required an already generated migration, integration of both SQL files through `CreateOrAlterSpFromSqlFile`, a private `__SCHEMA__` string constant instead of a schema identifier such as `Sales`, and a bundled integration template. This document records the requested plan; implementation has not started.
