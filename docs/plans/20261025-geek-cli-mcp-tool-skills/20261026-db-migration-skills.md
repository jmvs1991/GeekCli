# Database migration skills

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 2
Status: Completed

## Story

As a Geek CLI user, I want one independent skill per tool for adding, removing, and rolling back migrations through MCP, so that I can manage my database changes.

## Acceptance criteria

- [x] Given the corresponding skill is installed and its MCP tool is available, when I request adding, removing, or rolling back a migration, then the assistant presents inferred values for confirmation and asks for missing information before executing the operation.
- [x] Given confirmed parameters, when the assistant executes the operation, then it uses the corresponding MCP tool and communicates the result and relevant details without claiming changes that the result does not confirm.
- [x] Given an unavailable MCP tool or a failed execution, when I attempt the operation, then I receive an explanation of the impediment without alternative CLI commands being executed.

## Implementation tasks

- [x] Create three independent English-language skills in the existing catalog: `geek-db-migration-add`, `geek-db-migration-remove`, and `geek-db-migration-rollback`.
- [x] Document usage conditions, prerequisites, and parameters for each tool. `db_migration_add` requires `projectName`, `migrationName`, and `issue`; `db_migration_remove` requires `projectName`; `db_migration_rollback` requires `projectName` and `migrationName`. All three accept `init`, defaulting to `false`.
- [x] Describe workspace inspection and presentation of proposed values for confirmation. Explain that `init=false` selects `SchemaUpdates`, `init=true` selects `SchemaInitialization`, removal removes the latest migration, and rollback updates the database to the specified migration.
- [x] Review the instructions against the current MCP contracts and shared services, covering incomplete parameters, ambiguous inferences, success, errors, unavailable tools, and interpretation of `ExitCode` and `Output`.
- [x] Document the new skills and update backlog statuses, correcting the already completed story 1. Retain this refinement in English according to repository conventions.

## Agreed decisions

- Ask the user while showing inferred values; do not use inferred values automatically. Ask for information that cannot be determined from the workspace or request, and do not silently choose between ambiguous candidates.
- Present `init=false` as the default.
- Use only the corresponding MCP tool. Do not fall back to CLI commands if the tool is unavailable or execution fails.
- Reuse the existing catalog and installation behavior. No public API changes are required.

## Validation

Review each skill against its actual MCP tool signature, service behavior, and result contract. Confirm that guidance clearly covers prerequisites, parameters and defaults, confirmation of inferred values, missing information, invocation, successful results, failed executions, and tool unavailability.

No TDD, installation testing, or execution of real migrations is required for this content-only delivery. Mark this plan as Completed only when every acceptance criterion and implementation task is checked.

## Out of scope

- Changes to MCP tools, shared services, CLI commands, or installers.
- Skills for SQL scripts or other operations.
- TDD, installation tests, or real migration execution as validation of this delivery.
