# Migration agent versioning and skills

Status: Pending

## Context

Help database maintainers identify the version of the Geek migration agent and use its migration workflows with responsibilities clearly divided between the agent and reusable skills. Correct the agent's references to skill sources while preserving support for skills installed at user or project scope.

Stack: Existing Geek CLI agent definition in TOML, Markdown skills, and Geek CLI MCP tools.

Expensive decisions: None. No new multi-organization, permissions, offline, or multilingual requirements.

Scope: Version only the agent, independently of skills and generated migrations. Review responsibility boundaries before extracting workflows; retain plan coordination and the local development database execution restriction in the agent, and reuse the existing operation skills.

## Planning and validation

Every implementation plan generated for any story in this backlog must explicitly state that TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for these stories. Validate each change using the applicable repository checks and direct review of agent instructions, skill definitions, references, and version consistency.

## Backlog

1. As a database maintainer, I want to identify the migration agent's version, so that I know which revision I am using. Status: Completed.
2. As a database maintainer, I want the agent to reference the correct skill sources and recognize skills available at user or project scope, so that it can use its installed dependencies without confusing source paths with installation paths. Status: Completed.
3. As a database maintainer, I want to break down the migration agent into functional blocks and assess which can become reusable skills, so that I can plan the necessary skill stories and a final agent redefinition based on the review. Status: Pending.
4. As a database maintainer, I want to create tables and their Insert, Update, and Delete procedures through reusable skills, so that I receive the established database structure through the reviewed migration workflow. Requires: #3. Status: Pending.
5. As a database maintainer, I want to change stored procedures through a reusable skill, so that I can review both the change and the previous definition restored by Down. Requires: #3. Status: Pending.
6. As a database maintainer, I want to prepare table alterations through a reusable skill, so that I can supply the forward SQL and review its reversal within the existing migration workflow. Requires: #3. Status: Pending.
7. As a database maintainer, I want the migration agent redefined to coordinate the new and existing skills according to the approved responsibility review, so that I can use the complete migration workflow while retaining plan coordination and the local development database execution restriction in the agent. Requires: #3, #4, #5, #6, and any additional skill stories identified by #3. Status: Pending.

Story #3 may adjust stories #4–#6 and add justified skill creation or adaptation stories. Keep the agent redefinition story last, after all required skill work, and update its dependencies and numbering if stories are added.
