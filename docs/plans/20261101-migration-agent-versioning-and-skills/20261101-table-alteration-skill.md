# Table alteration preparation skill

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 8

Status: Completed

## Story

As a database maintainer, I want to prepare existing table alterations through a reusable skill, so that I can review the change and its reversal within an already generated migration.

## Acceptance criteria

- [x] Given an existing migration and a request to add a column, when the change is prepared, then Up integrates an already generated SQL script whose forward SQL I complete, and Down reverses the addition through `MigrationBuilder.DropColumn`.
- [x] Given another alteration supported by MigrationBuilder, such as adding a CHECK constraint, when the change is prepared, then I receive Up and Down implemented through MigrationBuilder without requiring SQL scripts.
- [x] Given an ambiguous implementation, insufficient information, or pending SQL, when I request preparation, then I receive the necessary clarification questions and the affected work remains pending.

## Plan

- [x] Define the reusable skill with the existing migration, target table, and requested change as inputs.
- [x] For column additions, request the existing Up script, leave its forward SQL to the user, and prepare the reversal through DropColumn.
- [x] For other changes, prioritize MigrationBuilder in Up and Down; ask the user when the approach is ambiguous.
- [x] Integrate the change and present its reversal for review, preserving migration attributes and unrelated operations.
- [x] Validate instructions and examples covering column additions, CHECK constraints, and incomplete cases.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Validate by directly reviewing instructions, references, and the diff against the three acceptance scenarios. Confirm that adding a column requires an existing Up SQL script and uses DropColumn in Down; adding a CHECK constraint uses MigrationBuilder in both directions; and ambiguous or incomplete work remains pending. For documentation-only changes, no builds or generator execution are required. Report any verification that cannot be performed and why.

Validation results (direct skill review):

- Confirmed column additions require an existing Up SQL script, leave forward SQL for the user, and reverse through `MigrationBuilder.DropColumn`.
- Confirmed CHECK constraints use `AddCheckConstraint` and `DropCheckConstraint` without SQL scripts; missing or ambiguous details and unsafe reversals prompt clarification and remain pending.
- Confirmed migration attributes and unrelated operations must be preserved. `git diff --check` passed; no build or generator execution was required for this documentation-only change.

## Out of scope

- Generating migrations or SQL scripts.
- Authoring the user's forward SQL.
- Executing database changes.
- Redefining the migration agent; that remains story #9.
