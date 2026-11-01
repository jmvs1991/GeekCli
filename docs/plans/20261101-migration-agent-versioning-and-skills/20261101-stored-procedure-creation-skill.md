# Stored procedure creation skill

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 6

Status: Completed

## Story

As a database maintainer, I want to complete an existing SQL file through a reusable skill, so that an individual stored procedure follows the established Geek SQL conventions.

## Acceptance criteria

- [x] Given an existing SQL file previously generated through `geek-db-script` and a selected table, schema, and operation, when I request an individual procedure, then that file contains the procedure named `SP_{{TABLE_NAME}}_{{OPERATION}}`, preserving the exact table spelling and using the agreed TRY/CATCH structure.
- [x] Given an INSERT, UPDATE, DELETE, or user-defined operation, when I review the prepared procedure, then it follows the agreed parameter, primary-key, sequence, audit, and return rules; the skill asks which business fields are updatable and requests any undefined custom behavior.
- [x] Given a procedure request, when its validations are reviewed before writing SQL, then proposed business validations are presented for confirmation and I am always asked whether I want additional validations; only confirmed validations are included, and missing required inputs or precedents leave the affected work pending rather than invented or reported as completed.
- [x] Given a prepared procedure, when I review its SQL formatting, then each column, matching value, update assignment, and primary-key predicate condition is on its own line, with leading commas and `AND` continuations as in the supplied examples.

## Agreed rules

- Receive the path to an existing SQL file previously generated through `geek-db-script`. Do not invoke that skill or generate additional scripts or synonyms.
- Use `SP_{{TABLE_NAME}}_{{OPERATION}}` in the table's schema. Preserve the exact table name; do not copy the singular/plural mismatch in the supplied examples. Operations include INSERT, UPDATE, DELETE, and others defined by the user.
- All operations except INSERT receive the complete primary key appropriate to the table's scope. All operations except DELETE receive `@user`; DELETE does not receive it.
- INSERT does not receive the table's code field. Generate it internally with `SP_PR_SEQUENCE_INSERT`, using the actual table and code-column names, after all approved validations and immediately before insertion to avoid consuming unused sequence values.
- All operations except DELETE return the affected record through a SELECT filtered by the complete primary key. Explicitly enumerate every table column; never use `SELECT *`.
- Ask which business fields may be updated. Any procedure that updates table data, including a custom operation, must always set `[UPDATE_DATE] = @date`, `[UPDATE_DATEL] = @datel`, and `[UPDATE_USER] = @user`. Obtain `@date` from `SYSUTCDATETIME()` and calculate `@datel` as numeric `yyyyMMdd`, following the supplied example.
- DELETE deletes directly using the complete primary key, without a prior existence check and without a returned SELECT.
- Every procedure, including custom operations, must use the TRY/CATCH body shown in the supplied examples. In CATCH, declare `@RC` and the error fields, populate message, line, number, severity, state, and procedure using the corresponding `ERROR_*` functions and `ISNULL` defaults, call `SP_PR_ERRORS_INSERT` only when `@severity <> 12`, and propagate with `RAISERROR(@error_message, @severity, @statusCatch)`. Preserve the shown argument order and respect the target project's signature and scope; ask if necessary evidence is missing.
- Never assume business validations or copy them automatically from examples. Present any suggested validations with their reason, always ask whether the user wants additional validations, and include only those confirmed. The existence check shown in the UPDATE example also requires confirmation; it is not a default requirement.
- For readability, put each column on its own line in `INSERT` column lists, matching `VALUES` lists, and returned `SELECT` lists. Put each `UPDATE` assignment and each primary-key predicate condition on its own line. Use leading commas for lists and leading `AND` for predicate continuations, matching the supplied examples. Keep INSERT columns and values in matching order.
- Resolve undefined custom-operation behavior with the user rather than inventing business logic. Use valid T-SQL comments (`--` or `/* ... */`) instead of the illustrative `//` comments in the INSERT example.

## Plan

- [x] Define the reusable skill to gather the existing SQL file, schema, table definition, operation, scope, and project precedents.
- [x] Specify confirmation of parameters, updatable business fields, proposed validations and their reasons, and any additional validations.
- [x] Add generic INSERT, UPDATE, and DELETE SQL templates based on the supplied procedures, with the shared TRY/CATCH pattern and target-project placeholders.
- [x] Document how to complete only the supplied existing file and report prepared work and any pending information.
- [x] Review each template against the supplied operation examples and review custom-operation, validation-confirmation, and missing-precedent handling without executing SQL against a database or requiring TDD or a new test project.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Review the generic INSERT, UPDATE, and DELETE templates against the supplied SQL examples, checking complete keys, explicit columns, one-column-per-line formatting, leading commas and `AND`s, matching INSERT column/value order, the DELETE exception for `@user`, sequence placement, audit fields, and shared error handling. Review custom-operation, validation-confirmation, missing-file, and missing-input handling. Report any verification that cannot be performed and why. Do not execute SQL against a database, create a test project, or require TDD. For this documentation-only change, review the diff; no builds or generator execution are required.

## Out of scope

- Coordinating the three procedure invocations required for each new table; this remains the agent's responsibility in story #9.
- Creating tables, modifying existing procedures, or preparing existing table alterations.
- Redefining or versioning the migration agent.
- Applying migrations or executing generated SQL against a database.
- Generating project migration SQL files, synonym artifacts, or Down scripts, or invoking `geek-db-script`.
- Adding unconfirmed business logic or validations, or new CLI/MCP operations.

## Approval

The user approved this implementation, requested generic operation templates based on supplied SQL examples, and added the one-column-per-line readability rule. Implementation is complete; template markers require adaptation to confirmed project details.
