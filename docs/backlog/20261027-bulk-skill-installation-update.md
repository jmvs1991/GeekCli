# Bulk skill installation and update

Status: Completed

## Context

Extend Geek CLI skill installation with bulk operations for users who want to install all missing skills or update all installed skills from the repository's bundled catalog. Preserve project and user scopes and exclude skills from external sources.

Stack: Existing .NET 8 solution.
Expensive decisions: No changes to the existing organization, permissions, offline/synchronization, or language behavior.

Both operations must be available through direct CLI invocation and the interactive wizard. By explicit user decision, skill installation and update must not be exposed through MCP; this overrides the repository's general three-path requirement for these operations.

The user generates and installs the version to be tested. Execution tests begin after the user explicitly indicates they may begin.

## Backlog

1. As a Geek CLI user, I want to install all catalog skills that are not yet installed in the selected scope, so that I can complete my installation in a single operation. Status: Completed.
2. As a Geek CLI user, I want to update all catalog skills already installed in the selected scope, so that I can obtain their versions bundled with Geek CLI in a single operation. Status: Completed.
