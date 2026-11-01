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

1. As a database maintainer, I want to identify the migration agent's version, so that I know which revision I am using. Status: Pending.
2. As a database maintainer, I want the agent to reference the correct skill sources and recognize skills available at user or project scope, so that it can use its installed dependencies without confusing source paths with installation paths. Status: Pending.
3. As a database maintainer, I want a review of which migration responsibilities belong in the agent and which belong in skills, so that I can approve their separation while preserving the current workflow. Status: Pending.
4. As a database maintainer, I want to create tables and their Insert, Update, and Delete procedures through reusable skills coordinated by the agent, so that I receive the established database structure through the reviewed migration workflow. Requires: #3. Status: Pending.
5. As a database maintainer, I want to change stored procedures through a reusable skill coordinated by the agent, so that I can review both the change and the previous definition restored by Down. Requires: #3. Status: Pending.
6. As a database maintainer, I want to prepare table alterations through a reusable skill coordinated by the agent, so that I can supply the forward SQL and review its reversal within the existing migration workflow. Requires: #3. Status: Pending.
