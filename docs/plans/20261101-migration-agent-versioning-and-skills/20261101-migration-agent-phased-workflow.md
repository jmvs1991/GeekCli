# Migration agent phased workflow

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 9

Status: Pending

## Story

As a database maintainer, I want `geek-db-migration-specialist` to coordinate the available skills through documented phases and atomic migrations, so that I can review the scope and control each change.

## Acceptance criteria

- [ ] Given a migration request, when the agent investigates it, then it presents the affected projects, changes, precedents, dependencies, and open questions, and records its findings without generating migrations.
- [ ] Given sufficient investigation findings, when the agent presents its plan, then it states the migration count, each migration's contents, dependencies, and reversal; table creation is always separate from its stored procedures, and any grouping of related changes is justified.
- [ ] Given an approved plan, when the agent executes and validates its parts, then it maintains separate documentation for each phase, records only verified progress, and presents results and pending work. All user-facing presentation is in Spanish and all saved documentation is in English.

## Implementation plan

- [ ] Declare all eight Db skills except scaffold, retain source-versus-installation distinctions, and delegate operation-specific rules to their skills while removing duplicated or conflicting agent instructions.
- [ ] Define Investigation: confirm repository and local migration readiness; identify projects, initialization versus update, existing changes, affected objects, precedents, prior definitions for Down, available environments, and required skills and tools. Resolve ambiguities needed for planning without generating migration artifacts.
- [ ] Define Planning: propose migration names, count, contents, dependencies, execution order, and reversal, then obtain approval. For each new table, use one migration for its table and synonym and another for its Insert, Update, and Delete procedures and their synonyms. Group other changes only when related and explain why.
- [ ] Define Execution and Final validation: execute one approved part at a time, honor skill-specific confirmations, record verified results, and wait for user instructions before continuing. Preserve the local development database execution restriction and verify the effective target before any database connection.
- [ ] Review skill references, agent version consistency, phase documentation, and representative workflows. TDD is not required because the repository has no test project; do not introduce a test project or a TDD prerequisite.

## Phase documentation contract

Store documentation in the target repository using this layout:

| Phase | Directory |
| --- | --- |
| Investigation | `docs/research/<related-backlog-name>/` |
| Planning | `docs/plans/<related-backlog-name>/` |
| Execution | `docs/execution/<related-backlog-name>/` |
| Final validation | `docs/validation/<related-backlog-name>/` |

Use the related backlog filename without `.md` as the shared directory identifier. If no backlog exists, use one descriptive work identifier consistently across the four directories.

Name every document `yyyyMMdd-<english-description>.md`, using its creation date. Create a document when its phase begins, link documents belonging to the same work, and update the corresponding document when advancing or resuming that phase. Do not create separate phase artifacts merely to implement this agent-definition change; the contract governs the agent's migration workflow.

Present all phases, findings, questions, plans, progress, and results to the user in Spanish. Save their documentation in English.

- Investigation records evidence, findings, dependencies, and open questions.
- Planning records acceptance criteria and implementation tasks as Markdown checkboxes, including the migration breakdown and approval boundary.
- Execution records actions, actual results, prepared artifacts, verified progress, and pending work.
- Final validation records checks, outcomes, limitations, and remaining work.

Mark the plan Completed only when every acceptance criterion and task is verified and checked. Leave unfinished work unchecked and do not treat empty generated templates or unconfirmed tool results as completed work.

## Required skills and responsibility boundaries

The agent must declare and coordinate these installed skills, whose sources are under `GeekCli/Skills/Db/`:

- `geek-db-migration-add`
- `geek-db-migration-remove`
- `geek-db-migration-rollback`
- `geek-db-script`
- `geek-db-table-create`
- `geek-db-table-alter`
- `geek-db-stored-procedure-create`
- `geek-db-stored-procedure-modify`

Exclude `geek-db-scaffold`. Agent installation does not imply skill installation; preserve support for user- and project-scoped installed skills and report missing required dependencies without substituting manual generation or CLI execution for required MCP operations.

Keep workflow coordination, readiness gates, plan approval, one-part-at-a-time execution, reporting, and the local development database restriction in the agent. Delegate migration attributes and environment selection, table conventions, procedure preparation, and table alteration behavior to their corresponding skills. Preserve required parameter confirmations and the distinction between file preparation, migration removal, and database rollback. Coordinate at least three procedure-creation skill invocations for each new table: Insert, Update, and Delete.

## Validation

TDD is not required because the repository has no test project. Do not add one for this story.

- [ ] Review the definition against all eight skill sources and the approved responsibility review; verify scaffold is excluded and conflicting legacy rules are removed.
- [ ] Review a new-table scenario: exactly two planned migrations for that table and its standard procedures, with table-first execution and procedure-first reversal.
- [ ] Review an existing-procedure modification: investigation identifies the prior definition and its source; Down restores that definition under the skill's confirmation rules.
- [ ] Review a table alteration requiring user-authored forward SQL: keep preparation pending until the user completes it, following the table-alter skill.
- [ ] Review missing skills or tools and unverifiable or non-local database targets: stop the affected operation and report the reason without claiming completion.
- [ ] Verify all four phase locations, date-prefixed English filenames, cross-links, English saved content, Spanish presentation, and accurate progress on resumption.
- [ ] Review the diff and agent metadata/banner version consistency. Run applicable repository checks if implementation changes extend beyond instruction and documentation files; documentation-only changes require no build or generator execution.

## Out of scope

- Changing CLI commands, MCP tools, or the internal rules of the Db skills.
- Generating or applying actual database migrations as part of this story's implementation.
- Introducing subagents, new test infrastructure, or automatic execution against non-local databases.

## Approval

The user selected four phases: Investigation, Planning, Execution, and Final validation. The user selected mandatory separation of table creation from its stored procedures and approved phase-based directories, Spanish presentation, English saved contents, and `yyyyMMdd-*.md` filenames. The user requested creation of this plan; agent implementation remains pending.
