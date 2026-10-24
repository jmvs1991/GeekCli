# Repository instructions

## Solution and responsibilities

- `GeekCliSolution.sln` contains two .NET 8 projects with nullable reference types and implicit usings enabled.
- `GeekCli` is the executable, packaged as the `geek-cli` .NET tool. It owns Spectre.Console.Cli commands, interactive wizards, MCP tools, and dependency injection.
- `GeekCliServices` is the shared service library. It owns command models, validation rules, file generation, and external-process execution for database, .NET, Angular, React, and React Native workflows.
- Keep the dependency direction from `GeekCli` to `GeekCliServices`. The service library must not reference the executable project.
- Follow the additional instructions in each project's `AGENTS.md` when editing that project.

## Command areas

Use these area names consistently across commands, branches, wizards, MCP tools, and services:

- `Db` refers to database commands (CLI branch: `db`).
- `Dotnet` refers to .NET commands (CLI branch: `dotnet`).
- `Ngx` refers to Angular commands (CLI branch: `ngx`).
- `Rx` refers to React commands, including React Native (CLI branch: `rx`).

## C# conventions

- Follow nearby code: four-space indentation, braces on separate lines, block-scoped namespaces matching the folder hierarchy, and PascalCase type and public member names.
- Use camelCase parameters and local variables, and follow the surrounding private-field convention, commonly `_camelCase`.
- Keep feature files grouped by area and operation. Use `I...Service` interfaces, service implementations, and command records under `Models` following the existing feature structure.
- Reuse existing command, settings, model, and service base classes when appropriate. Keep nullable annotations accurate and dependencies explicit through constructor injection.
- Put reusable extension methods in the project's `ExtensionMethods` folder. String extension methods belong in a static `StringExtension` class and use the `this string` receiver.
- Preserve existing public command names, options, defaults, generated output, and exit behavior unless the requested change explicitly alters them.

## Required execution paths

Every new command must ship with all three execution paths:

1. Direct CLI invocation with registered command and settings classes.
2. Interactive execution through the appropriate wizard, reachable from the general wizard.
3. An MCP tool discoverable through `geek-cli mcp`.

All three paths must invoke the same shared service using the same command model. Maintain consistent parameter meanings, required inputs, defaults, validation, generated artifacts, and execution behavior across them. CLI option spelling and MCP parameter naming may follow their respective existing conventions.

Keep execution and common validation in `GeekCliServices`; adapters collect or parse inputs and present results. Interactive prompts belong in wizards and must not be required by direct CLI or MCP execution. Preserve stdout for the MCP protocol; use the shared output abstraction for service messages.

These requirements govern new commands and changes to command behavior. Do not retrofit unrelated existing commands solely to satisfy this document.

## Backlog workflow

- Save backlogs as Markdown files in `docs/backlog/` at the repository root.
- Use the filename format `yyyyMMdd-name.md`, where `yyyyMMdd` is the creation date and `name` describes the backlog.
- Present backlogs to the user in Spanish, and save their contents in English.
- Include a `Status` field in each backlog with one of these values: `Pending`, `In progress`, or `Completed` (presented in Spanish as `Pendiente`, `En progreso`, or `Terminado`). Set new backlogs to `Pending` and keep the status updated as work progresses.
- Include a separate `Status` field for each story within a backlog, using `Pending`, `In progress` (WIP), or `Completed`. Present these statuses in Spanish as `Pendiente`, `En progreso`, or `Terminado`. Set new stories to `Pending` and update each story's status as work progresses.
- Mark a backlog as `Completed` only when all of its stories and tasks are `Completed`.

## Implementation plans

- Save plans under `docs/plans/<related-backlog-name>/` at the repository root, where `<related-backlog-name>` is the related backlog's filename without the `.md` extension. For example, plans for `docs/backlog/20261023-angular-nx-support.md` belong in `docs/plans/20261023-angular-nx-support/`.
- Use the filename format `yyyyMMdd-name.md`, where `yyyyMMdd` is the plan's creation date and `name` describes the plan.
- Present plans to the user in Spanish, and save their contents in English.
- Represent plan acceptance criteria and implementation tasks as Markdown checkboxes. Mark a plan as `Completed` only when every acceptance criterion and plan task is checked (`[x]`); keep unchecked items (`[ ]`) for work that remains.

## Build and validation

Run these commands from the repository root when relevant to an implementation change:

```powershell
dotnet build .\GeekCliSolution.sln
dotnet build .\GeekCli\GeekCli.csproj -c Release
```

`Taskfile.yml` also provides `task geek-cli:build`. To create a tool package when needed:

```powershell
dotnet pack .\GeekCli\GeekCli.csproj -c Release
```

For future command additions or behavior changes:

- Verify direct CLI invocation, interactive execution, and MCP invocation with equivalent inputs. Check required and optional parameters, defaults, invalid inputs, output, generated artifacts or process arguments, and exit results.
- Exercise generators in a disposable workspace with the necessary external tools and templates available. Report any execution path that could not be verified and why.
- Update `README.md` command structure and usage examples to reflect the affected CLI, wizard, and MCP behavior.

For documentation-only changes, check instructions against the implementation and review the diff. No build or generator execution is required.
