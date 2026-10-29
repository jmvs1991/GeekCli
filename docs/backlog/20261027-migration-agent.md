# Database migration agent

Status: Completed

## Context

Help a database maintainer prepare and complete database migrations through a reviewed plan executed one part at a time. Deliver an installable Codex agent in `GeekCli/Agents`, using the four existing Geek CLI database migration and script skills.

Stack: Codex agent definition in TOML, Geek CLI skills and MCP tools, Entity Framework migrations, and SQL Server.

Expensive decisions: No additional requirements. Corporation/company scope belongs to the database tables being migrated. Plans are presented in Spanish and saved in English.

The complete agreed requirements, examples, acceptance criteria, and implementation tasks are recorded in [Migration agent requirements and implementation plan](../plans/20261027-migration-agent/20261027-migration-agent.md). That document is the reference for the conventions shared by these stories.

## Backlog

1. As a database maintainer, I want to install the migration agent using Geek CLI, so that I can use it in my projects. Status: Completed.
2. As a database maintainer, I want to prepare an update or initialization migration through a plan that I approve and execute one part at a time, so that I can review each advance before continuing. Requires: #1. Status: Completed.
3. As a database maintainer, I want to create a table with its scope, audit fields, synonym, and insert, update, and delete procedures, so that I receive a complete structure following my project's conventions. Requires: #2. Status: Completed.
4. As a database maintainer, I want to receive templates for altering a table and a prepared reversal, so that I can write the change SQL myself and undo the change when necessary. Requires: #2. Status: Completed.
5. As a database maintainer, I want to change a stored procedure and review the definition restored by its rollback, so that reverting the migration recovers the previous state. Requires: #2. Status: Completed.
6. As a database maintainer, I want to remove the latest migration from the selected project, so that I can discard a migration that needs to be replaced. Requires: #2. Status: Completed.
7. As a database maintainer, I want to return the database to a specific migration, so that I can recover the required state. Status: Completed.

## Delivery decision

Start with one agent coordinating the four existing skills. Their operation boundaries already provide separation, while the agent retains the migration context, confirmations, and plan progress. Reconsider smaller coordinated subagents if implementation reveals an independent responsibility that warrants delegation; a multi-agent design is not currently required.

Proposed agent name: `geek-db-migration-specialist`. Expected artifact: `GeekCli/Agents/geek-db-migration-specialist.toml`.
