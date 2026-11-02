# Stored procedure creation migration integration

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 10

Status: Pending

## Story

As a database maintainer, I want the stored-procedure creation skill to also request an existing migration and integrate the procedure and synonym script references, so that I can review their complete creation and reversal.

## Acceptance criteria

- [ ] Given the scripts already requested by the skill, when no existing migration has been supplied, then the skill requests that migration and leaves integration pending until it is provided.
- [ ] Given the four scripts and an existing migration, when stored-procedure creation is completed, then Up references procedure and synonym creation and Down references their respective removal, preserving existing migration operations.
- [ ] Given the updated skill, when its reusable integration template is consulted, then it shows all four references following the supplied example, with replaceable schema, folder, and filenames.

## Plan

- [ ] Add the requirement to request an existing migration while retaining the current script-request workflow.
- [ ] Include a reusable migration integration template alongside the existing procedure templates.
- [ ] Update the instructions to integrate all four references while preserving existing migration content and avoiding duplicate calls or constants.
- [ ] Validate instructions, template, and diff through direct review; TDD is not required because the repository has no test project.
- [ ] Keep this plan and the related backlog statuses consistent with verified implementation progress.

## Template contract

Extend `GeekCli/Skills/Db/geek-db-stored-procedure-create/SKILL.md` and bundle the integration pattern in its `templates/` directory. The template is an adaptation pattern for an existing migration, not a replacement migration class.

- Use `__FOLDER__` for the actual scripts folder and reuse existing project schema constants where applicable.
- In Up, reference the procedure through `CreateOrAlterSpFromSqlFile` with the procedure schema, then the synonym through `CreateOrAlterSynomFromSqlFile` with the dbo schema.
- In Down, reference the corresponding removal scripts through `RollbackSpFromSqlFile` and `RollbackSynomFromSqlFile`, respectively, preserving the same procedure-then-synonym order as the supplied example.
- Use the actual generated filenames: `CREATE_<SP>.sql` and `ROLLBACK_CREATE_<SP>.sql`. Procedure and synonym scripts share these filenames but reside in their respective script directories.
- Resolve schema, folder, and filename placeholders using the supplied artifacts and target-project precedents. Preserve migration attributes, metadata, and unrelated operations; do not duplicate matching references or constants.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite.

Review the skill, bundled template, and diff against the existing script generator. Check missing-migration handling, all four references with their actual schemas and filenames, preservation of existing migration operations, and reuse of existing matching calls and constants. Confirm the current script-request workflow and procedure-generation rules remain intact, and that integration is not reported complete while its migration is missing. For this instructions-and-template change, no builds or generator execution are required. Report any verification that cannot be performed and why.

## Out of scope

- Creating the migration or changing the script-request or script-generation workflow.
- Executing SQL or applying migrations to a database.
- Redefining the migration agent, which remains story #9.

## Refinement decisions

Story #10 is the target; story #9 remains dependent on it. The user clarified that the skill already requests the scripts: the additional input to request is the existing migration. Missing artifacts must be requested rather than generated through other skills. Preserve the example's extension-method spelling and operation order. This plan records the refinement only; skill and template implementation remain pending.
