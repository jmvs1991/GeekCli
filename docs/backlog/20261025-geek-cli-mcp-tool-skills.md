# Geek CLI MCP tool skills

Status: Completed

## Context

Create one independent skill for each of the 23 current Geek CLI MCP tools so Geek CLI users can ask their assistant to use each tool correctly. Stories group related deliveries; each tool still receives its own skill.

Stack: English-language skills included in the existing Geek CLI catalog and installable through the existing CLI command and interactive wizard.

Expensive decisions: English-only skill content; no other requirements identified.

Each skill covers when to use its tool, prerequisites, parameters and defaults, invocation, and interpretation of results. Refactoring existing code is outside this backlog's scope.

TDD and installation testing are not required for writing these skills because the existing install commands have already been tested. Focus validation on skill quality: review each skill against its MCP tool's actual requirements, parameters, defaults, invocation, and results, and ensure it gives the assistant clear guidance for communicating outcomes and handling errors or unavailable tools. Apply this validation approach to every story in this backlog.

## Backlog

1. As a Geek CLI user, I want a skill to list the available .NET templates, so that I know what I can generate. Status: Completed.
2. As a Geek CLI user, I want one skill per tool for adding, removing, and rolling back migrations, so that I can manage my database changes. Status: Completed.
3. As a Geek CLI user, I want a skill to generate SQL scripts, so that I can obtain scripts for my schema changes. Status: Completed.
4. As a Geek CLI user, I want a skill to generate models from existing tables, so that I can incorporate my database into my project. Status: Completed.
5. As a Geek CLI user, I want a skill to generate DTOs, so that I can create the transfer models I need. Status: Completed.
6. As a Geek CLI user, I want one skill per read and write repository tool, so that I can generate access to my data. Status: Completed.
7. As a Geek CLI user, I want a skill to generate stored procedure classes, so that I can use them from my application. Status: Completed.
8. As a Geek CLI user, I want a skill to generate message resources, so that I can incorporate localized messages. Status: Completed.
9. As a Geek CLI user, I want a skill to generate caches, so that I can create the cache for my entities. Status: Completed.
10. As a Geek CLI user, I want one skill per .NET service and controller tool, so that I can generate my application's operations. Status: Completed.
11. As a Geek CLI user, I want one skill per service and API test tool, so that I can generate the corresponding tests. Status: Completed.
12. As a Geek CLI user, I want one skill per Angular component and page tool, so that I can generate them in the appropriate project. Status: Completed.
13. As a Geek CLI user, I want a skill to generate Angular services, so that I can incorporate them into my project. Status: Completed.
14. As a Geek CLI user, I want one skill per React context, React Native module, component, and screen tool, so that I can generate each resource according to my needs. Status: Completed.

## Tool coverage

| Story | Area | MCP tools (one skill per tool) |
| --- | --- | --- |
| 1 | Dotnet | `dotnet_list` |
| 2 | Db | `db_migration_add`, `db_migration_remove`, `db_migration_rollback` |
| 3 | Db | `db_script` |
| 4 | Db | `db_scaffold` |
| 5 | Dotnet | `dotnet_dto` |
| 6 | Dotnet | `dotnet_read`, `dotnet_write` |
| 7 | Dotnet | `dotnet_sp` |
| 8 | Dotnet | `dotnet_resource` |
| 9 | Dotnet | `dotnet_cache` |
| 10 | Dotnet | `dotnet_service`, `dotnet_controller` |
| 11 | Dotnet | `dotnet_unit_test_service`, `dotnet_api_unit_test` |
| 12 | Ngx | `ngx_component`, `ngx_page` |
| 13 | Ngx | `ngx_service` |
| 14 | Rx | `rx_context`, `rx_native_module`, `rx_native_component`, `rx_native_screen` |

Total: 23 independent skills covering 5 Db, 11 Dotnet, 3 Ngx, and 4 Rx tools.
