# Migration file attributes and environment selection

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 4

Status: Completed

## Story

As a database maintainer, I want to create migration files through a reusable skill, so that generated migrations include the correct initialization/update and environment attributes for the selected schema project.

## Acceptance criteria

- [x] Given a selected schema project and confirmed parameters, when I request a migration, then the generated file includes `EntityFrameworkMigration.DataAnnotations` and `InitialMigration(true)` for initialization or `InitialMigration(false)` for updates.
- [x] Given migration precedents in the selected schema project, when I prepare the migration, then I am offered their environment values for selection and the file expresses my selection through `EnviromentMigration` using the project's syntax.
- [x] Given no usable environment precedents, when I prepare the migration, then I am asked for the necessary values without invention; if generation fails or cannot be confirmed, I receive the result and preparation remains pending.

## Plan

- [x] Extend `geek-db-migration-add`, preserving its parameters, confirmations, and MCP invocation.
- [x] Inspect only the selected initialization/update project and resolve environment values and syntax with the user.
- [x] Identify the confirmed generated migration and complete its attributes while preserving generated content.
- [x] Report prepared files and pending work, distinguishing generation from database application.
- [x] Review instructions and references and validate initialization, updates, missing precedents, and generation failures.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Validate the skill structure, compare instructions with the existing MCP adapter and migration service, walk through the acceptance scenarios, and review the documentation diff. This is an instruction-only change: no builds or generator execution are required.

Validation results (instruction review, not live migration execution):

- Reviewed initialization and update scenarios: each selects only its own schema project, retains the confirmed MCP inputs, and specifies the corresponding boolean attribute.
- Reviewed environment selection: existing values and syntax are offered for explicit selection; missing precedents require user input without borrowing from the other project or inventing syntax.
- Reviewed failure paths: unavailable tools, nonzero results, and unconfirmed creation do not lead to attribute edits or completion claims. Ambiguous generated files and incomplete edits are reported as pending.
- Compared the MCP adapter and shared service: the four parameters and `init=false` default are unchanged; the service targets `Migrations/<issue>` in the selected project. The package already includes the skill through its content glob. Agent source references remain valid and its version remains unchanged.
- Reviewed the diff and manually checked frontmatter, naming, description, and absence of unfinished placeholders. `git diff --check` passed, with line-ending warnings only. The bundled `quick_validate.py` could not run because Python lacks PyYAML (`ModuleNotFoundError: yaml`); its checks were reviewed manually instead.
- No builds, CLI/wizard/MCP invocation, or database operations were performed because only skill instructions and documentation changed.

## Out of scope

- Redefining the agent or removing its duplicated instructions (story #9).
- Creating tables, procedures, or alterations.
- Applying migrations or changing commands, MCP tools, or services.
- Versioning skills or migrations.

## Approval

The user chose to extend the existing skill instead of creating a coordinator skill and requested implementation of the proposed plan.
