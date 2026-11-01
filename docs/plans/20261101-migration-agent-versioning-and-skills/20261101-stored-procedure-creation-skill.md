# Stored procedure creation skill

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 6

Status: Pending

## Story

As a database maintainer, I want to complete an existing SQL file through a reusable skill, so that an individual stored procedure follows the established Geek SQL conventions.

## Acceptance criteria

- [ ] Given an existing SQL file previously generated through `geek-db-script` and a selected table, schema, and operation, when I request an individual procedure, then that file contains the procedure named `SP_{{TABLE_NAME}}_{{OPERATION}}`, preserving the exact table spelling and using the agreed TRY/CATCH structure.
- [ ] Given an INSERT, UPDATE, DELETE, or user-defined operation, when I review the prepared procedure, then it follows the agreed parameter, primary-key, sequence, audit, and return rules; the skill asks which business fields are updatable and requests any undefined custom behavior.
- [ ] Given a procedure request, when its validations are reviewed before writing SQL, then proposed business validations are presented for confirmation and I am always asked whether I want additional validations; only confirmed validations are included, and missing required inputs or precedents leave the affected work pending rather than invented or reported as completed.

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
- Resolve undefined custom-operation behavior with the user rather than inventing business logic. Use valid T-SQL comments (`--` or `/* ... */`) instead of the illustrative `//` comments in the INSERT example.

## Plan

- [ ] Define the reusable skill and collect the existing SQL file, schema, table definition, operation, scope, and project precedents.
- [ ] Resolve parameters and business fields, present proposed validations, ask about additional validations, and confirm which to include.
- [ ] Complete the existing SQL using the agreed conventions, approved validations, and shared TRY/CATCH structure.
- [ ] Present the prepared procedure and any pending information, preserving agent ownership of workflow coordination.
- [ ] Review instructions and verify representative operations without executing SQL against a database or requiring TDD or a new test project.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Review representative INSERT, UPDATE, DELETE, and custom-operation SQL in disposable existing files, checking complete keys, explicit columns, the DELETE exception for `@user`, sequence placement, update audit fields, and the shared error-handling structure. Verify that suggested validations and validations from examples are included only after confirmation, and that the skill always asks about additional validations. Check missing-file and missing-input handling and confirm that no script generation or database execution is requested. Report any verification that cannot be performed and why. For this documentation-only plan update, review the diff; no builds or generator execution are required.

## Out of scope

- Coordinating the three procedure invocations required for each new table; this remains the agent's responsibility in story #9.
- Creating tables, modifying existing procedures, or preparing existing table alterations.
- Redefining or versioning the migration agent.
- Applying migrations or executing generated SQL against a database.
- Generating SQL files, synonym artifacts, or Down scripts, or invoking `geek-db-script`.
- Adding unconfirmed business logic or validations, or new CLI/MCP operations.

## Approval

The user agreed to the refined plan and requested this update, including explicit confirmation of business validations. Implementation remains pending; this update does not implement the skill.
