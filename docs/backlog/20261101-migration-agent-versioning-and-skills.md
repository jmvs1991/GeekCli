# Migration agent versioning and skills

Status: In progress

## Context

Help database maintainers identify the version of the Geek migration agent and use its migration workflows with responsibilities clearly divided between the agent and reusable skills. Correct the agent's references to skill sources while preserving support for skills installed at user or project scope.

Stack: Existing Geek CLI agent definition in TOML, Markdown skills, and Geek CLI MCP tools.

Expensive decisions: None. No new multi-organization, permissions, offline, or multilingual requirements.

Scope: Version only the agent, independently of skills and generated migrations. Review responsibility boundaries before extracting workflows; retain plan coordination and the local development database execution restriction in the agent, and reuse the existing operation skills.

## Planning and validation

Every implementation plan generated for any story in this backlog must explicitly state that TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for these stories. Validate each change using the applicable repository checks and direct review of agent instructions, skill definitions, references, and version consistency.

Story #4 extends the existing `geek-db-migration-add` skill; its completed plan is `docs/plans/20261101-migration-agent-versioning-and-skills/20261101-migration-file-attributes.md`. Agent redefinition remains reserved for story #9.

## Backlog

1. As a database maintainer, I want to identify the migration agent's version, so that I know which revision I am using. Status: Completed.
2. As a database maintainer, I want the agent to reference the correct skill sources and recognize skills available at user or project scope, so that it can use its installed dependencies without confusing source paths with installation paths. Status: Completed.
3. As a database maintainer, I want the migration agent's instructions assigned to functional responsibility blocks, so that reusable skill boundaries and the final agent responsibilities are explicit. Status: Completed.
4. As a database maintainer, I want to create migration files through a reusable skill, so that generated migrations include the correct initial/update and environment attributes based on the selected schema project and its precedents. The skill must use `EntityFrameworkMigration.DataAnnotations`, `[InitialMigration(true)]` for initialization or `[InitialMigration(false)]` for updates, and `EnviromentMigration` with that exact spelling; it must present existing environment values for user selection and ask rather than inventing values when no usable precedent exists. Requires: #3. Status: Completed.
5. As a database maintainer, I want to create tables through a reusable skill, so that new tables follow the established Geek schema conventions in the reviewed migration workflow. Requires: #3 and #4. Status: Completed.
6. As a database maintainer, I want to create an individual stored procedure through a reusable skill with generic INSERT, UPDATE, and DELETE templates, so that procedures follow the established Geek SQL conventions. The agent will invoke this skill at least for Insert, Update, and Delete for each new table. Requires: #3 and #4. Status: Completed.
7. As a database maintainer, I want to modify existing stored procedures through a reusable skill, so that I can review the change and the previous definition restored by Down. Requires: #3 and #4. Status: Completed.
8. As a database maintainer, I want to prepare existing table alterations through a reusable skill, so that I can supply the forward SQL and review its reversal within the existing migration workflow. Requires: #3 and #4. Status: Completed.
9. As a database maintainer, I want the migration agent redefined to coordinate the new and existing skills according to the approved responsibility review, so that I can use the complete migration workflow while retaining plan coordination and the local development database execution restriction in the agent. Requires: #3, #4, #5, #6, #7, and #8. Status: Pending.

Story #3 is completed in `docs/plans/20261101-migration-agent-versioning-and-skills/20261101-migration-responsibility-review.md`. Stories #4–#8 cover migration-file creation and attributes, table creation, stored-procedure creation, stored-procedure modification, and table-alteration preparation. The agent will coordinate at least three stored-procedure-skill invocations (Insert, Update, Delete) for every new table. Stories #5–#8 depend on migration-file creation (#4); final agent redefinition #9 depends on all required skill stories and remains last. Existing add, remove, rollback, and SQL script-generation skills are reused where applicable without duplicating their operation scope.
