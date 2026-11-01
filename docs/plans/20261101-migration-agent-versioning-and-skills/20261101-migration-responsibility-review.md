# Migration responsibility review

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 3

Status: Completed

## Story

As a database maintainer, I want to break down the migration agent into functional blocks and assess which can become reusable skills, so that I can plan the necessary skill stories and a final agent redefinition based on the review.

## Acceptance criteria

- [x] Given the current migration agent, when I review its breakdown, then all its instructions are accounted for in functional blocks with a clear purpose and responsibility.
- [x] Given those blocks and the existing operation skills, when I review the assessment, then each block has a justified recommendation to remain in the agent, reuse an existing skill, or become a skill, with its inputs, deliverables, and dependencies identified where extraction is proposed.
- [x] Given the review findings, when they are recorded in the backlog, then existing skill stories are adjusted and additional skill stories are added only where justified, followed by a final agent redefinition story dependent on the required skill work; the proposed boundaries preserve the current workflow, confirmation requirements, plan coordination, and local development database execution restriction.

## Plan

- [x] Break down all current agent instructions into functional blocks.
- [x] Assess each block's cohesion, reuse potential, and overlap with existing operation skills.
- [x] Recommend what to retain, reuse, or extract, describing the boundaries and dependencies of extraction candidates.
- [x] Update existing skill stories and add any justified skill creation or adaptation stories, avoiding duplicate scope.
- [x] Define the final agent redefinition story and its dependencies, verify workflow preservation, and present the resulting review and backlog for approval before implementation.

## Functional breakdown and responsibility assessment

1. Initial greeting and agent version — retain in the agent; it depends on the installed definition's own metadata.
2. Request readiness gate — retain in the agent; confirm the repository and local development database are current before work.
3. Project discovery and initialization/update choice — retain in the agent; resolve the project family and migration target before selecting an operation.
4. Plan preparation and approval — retain in the agent; analyze requested behavior and existing code or SQL, then save and obtain approval for a plan.
5. One-part-at-a-time execution and progress tracking — retain in the agent; update verified plan progress and wait between approved parts.
6. Operation selection, parameter collection, and confirmation coordination — retain orchestration in the agent; delegate operation-specific parameters and confirmation rules to the selected skill.
7. Add a migration — reuse `geek-db-migration-add`; inputs and confirmation are defined by that skill, and its deliverable is a confirmed migration-add tool result.
8. Remove the latest migration — reuse `geek-db-migration-remove`; its deliverable is a confirmed removal result, distinct from database rollback.
9. Roll back a database to a migration — reuse `geek-db-migration-rollback`; its deliverable is a confirmed rollback result, subject to the agent's local-database restriction.
10. Generate SQL script templates — reuse `geek-db-script`; it owns operation parameters, output paths, template limitations, and generation reporting.
11. Local development database execution restriction — retain in the agent; verify the effective target and stop if local development cannot be established.
12. Migration attributes and applicable environments — include in the migration-file creation skill (backlog story #4). The skill applies `InitialMigration(true/false)` and `EnviromentMigration`, inspects only the selected schema project's precedents, and asks the user to choose applicable environments or provide values when no usable precedent exists. The agent retains coordination and the local development database execution restriction.
13. Create tables and their stored procedures — extract into two skills: a table-creation skill and a stored-procedure-creation skill. The agent decides that each new table requires at least three uses of the stored-procedure skill (Insert, Update, Delete). The table skill owns table structure conventions; the stored-procedure skill defines how to create an individual procedure. Both depend on `geek-db-script` for generated templates, and deliver reviewed artifacts for migration preparation.
14. Modify existing stored procedures and restore the prior definition in Down — extract into a dedicated skill. It depends on `geek-db-script` for templates and migration history for the prior definition; its deliverable includes a reviewed Down restoring that definition.
15. Prepare existing table alterations while leaving Up SQL to the user — extract into a dedicated skill. It depends on `geek-db-script`; its deliverable is generated alteration artifacts with forward SQL left for the user and reversal reviewed before use.
16. Tool result reporting and failure handling — retain common policy in the agent; operation skills report their specific outputs and paths.
17. Distinguish file preparation, migration removal, and database rollback — retain in the agent as a cross-cutting workflow rule, with operation-specific explanations in relevant skills.

Extraction boundaries: the migration-file creation skill (backlog story #4) owns migration attributes and environment selection. Four additional skills cover table creation, stored-procedure creation, stored-procedure modification, and table-alteration preparation. The agent retains workflow coordination, including invoking stored-procedure creation at least three times for each new table, as well as the local development database execution restriction. Existing add, remove, rollback, and script-generation operations continue to use their current skills. Skill stories depend on this review; final agent redefinition depends on every required skill story.

The backlog updates record these boundaries and dependencies. Skill implementation and agent redefinition remain out of scope for this review and require their own story execution.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Validate by directly comparing the proposal with the agent instructions and the four operation skill definitions, checking references and version consistency, and reviewing the documentation diff. No builds or generator execution are required for this documentation-only review.

## Out of scope

- Implementing the skill creation or adaptation stories resulting from this review.
- Implementing the final agent redefinition.
- Changing the agent behavior, version, installation, commands, or MCP tools.
- Generating migrations or scripts, or executing database operations.

## Approval

The user approved separate skills for table creation and stored-procedure creation under block 13, a stored-procedure modification skill under block 14, and a table-alteration preparation skill under block 15. The user confirmed the agent will decide to invoke stored-procedure creation for at least Insert, Update, and Delete for each new table. Migration attributes and environment selection from block 12 are assigned to the migration-file creation skill rather than a separate skill.
