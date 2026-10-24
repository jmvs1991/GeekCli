# GeekCli executable instructions

These instructions supplement the root `AGENTS.md` for files under `GeekCli`.

## Entry points and registration

- `Program.cs` selects MCP mode when the first argument is `mcp`; other invocations use the Spectre.Console.Cli application.
- `Infrastructure/ServiceCollectionExtensions.cs` owns shared service registration through `AddGeekCliServices`, CLI configuration through `RunGeekCli`, and MCP hosting through `RunGeekCliMcpServerAsync`.
- `Branches/*BranchExtension.cs` registers direct commands and branch defaults for `db`, `dotnet`, `ngx`, and `rx`.
- `Infrastructure/DependencyInjection/TypeRegistrar.cs` and `TypeResolver.cs` bridge Spectre.Console.Cli and Microsoft dependency injection. Follow this integration rather than constructing service implementations in adapters.

## Direct commands and settings

- Place command and settings classes under `Commands/<Area>/<Operation>` using the existing naming and visibility conventions.
- Settings define Spectre command arguments, options, descriptions, and input validation. Reuse the appropriate area-specific settings bases.
- Command classes map settings to `GeekCliServices` command models and delegate to injected service interfaces. Reuse `CommandBase<TSettings, TService, TCommand>` or the relevant area base, including `DotnetCommandExecutorBase`, where appropriate.
- Keep template selection, file generation, process arguments, and common business validation in the service library. Adapter validation may improve CLI errors but must not be the only enforcement of a shared rule.
- Register every new direct command in the appropriate branch extension, including its name and description. Wire any new branch into `RunGeekCli`.

## Interactive execution

- Add each new command to the corresponding `Commands/<Area>/Wizard/*WizardCommand.cs`, including its menu choice, input prompts, and execution dispatch.
- Wizards must construct the same service command model and invoke the same service interface as direct CLI and MCP adapters. Align prompt defaults and available choices with those adapters and shared rules.
- Follow existing summary, confirmation, and Back navigation patterns. Keep prompts inside the interactive path.
- The general wizard in `Commands/Wizard/GeneralWizardCommand.cs` delegates through area wizard interfaces. For a new area, wire its wizard interface, implementation, dependency injection, and general-wizard navigation so the command is reachable.

## MCP tools and output

- Add tools under `Tools/<Area>`, following existing public tool classes with `[McpServerToolType]`, `[McpServerTool]`, and descriptive metadata.
- Inject the same service interface used by the CLI and wizard. Expose equivalent inputs and defaults, map them to the shared command model, and reuse shared parsers and validation rules.
- Derive from `McpToolBase` and wrap service execution in `Capture` to return `McpToolResult` with its exit code and captured output.
- MCP hosting uses stdio and `WithToolsFromAssembly()` for discovery. Keep tool classes and methods discoverable by this registration.
- Preserve stdout exclusively for protocol traffic in MCP mode. Keep host console logging directed to stderr and send service messages through `CommandOutput`. Do not add prompts, banners, or direct console output to MCP tool execution.

## Completion requirements

Every new command requires its service/model, command/settings, branch registration, wizard integration, MCP tool, and shared dependency injection registration in `AddGeekCliServices`. Both hosts use that registration; follow existing service lifetimes.

For command changes, verify all three execution paths and their parameter/default/validation parity as required by the root instructions, and update the corresponding `README.md` examples. A working direct command alone does not complete a new command.
