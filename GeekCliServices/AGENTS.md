# GeekCliServices library instructions

These instructions supplement the root `AGENTS.md` for files under `GeekCliServices`.

## Service contracts and models

- Organize features under `Services/<Area>/<Operation>`, with service interfaces, implementations, and command records in the appropriate `Models` folder.
- Follow `ICommandService<TCommand>` and its `int RunProcess(string processToRun, TCommand command)` contract. Feature interfaces such as `IDbScriptService` specialize this contract; implementations supply the shared execution behavior.
- Reuse existing model bases such as `DbCommandBase`, `DotnetTemplateCommandBase`, `DotnetProjectTemplateCommandBase`, and `DotnetDbTemplateCommandBase` where applicable. Follow nearby immutable record and `init` property conventions.
- Command models represent execution inputs shared by the CLI, wizard, and MCP tool. Keep them independent of executable-project settings, command classes, and MCP result types.
- Never add a project reference or code dependency on `GeekCli`. Keep prompting, CLI registration, MCP metadata, and host dependency injection in the executable.

## Shared execution and validation

- Centralize execution, normalization, defaults, and common validation in services or shared rules/parsers in this library so every entry point receives the same behavior.
- Validate common requirements before writing files or starting external processes. CLI settings validation and wizard choices do not protect callers that invoke the service directly.
- Reuse existing helpers such as `DbScriptRules`, `DbScriptTypeParser`, and `DotnetScopeParser` when relevant. Keep supported values and conditional requirements consistent across adapters.
- Return meaningful exit codes and preserve external-process failures. Do not make service execution depend on interactive input.

## File generation

- Reuse `ScaffoldingServiceBase<TCommand>` and area bases such as `RxServiceBase<TCommand>` for features that follow their scaffolding pattern. The base captures the working directory, creates directories, and provides file helpers; subclasses implement feature generation through `Execute`.
- Use `Path.Combine` and the existing feature's working-directory and naming rules when resolving output paths.
- Preserve the target feature's overwrite behavior. `ScaffoldingServiceBase.CreateFile` skips existing files, while `DbScriptService` writes its SQL scripts directly. Do not silently replace one policy with another.
- Keep generated folder structures, filenames, template content, and path-segment handling consistent with the relevant feature. Report generated or skipped artifacts through `CommandOutput`.

## External processes and output

- Reuse `ExternalProcessServiceBase<TCommand>` for existing process-based patterns: build arguments in `BuildArgs`, redirect stdout and stderr, use `UseShellExecute = false` and `CreateNoWindow = true`, and propagate the process exit code.
- Reuse `DotnetCommandServiceBase` for .NET template execution, including `RunDotnet`, `BuildTemplateArgs`, option helpers, scope suffixes, and quoting. Follow `NgxServiceBase` for Angular process conventions.
- Keep command-line construction in services, using the existing quoting helpers for user-supplied values rather than duplicating process construction in CLI or MCP adapters.
- Send operational messages and child-process output through `CommandOutput.Info` and `CommandOutput.Error`. Its default sink writes to stderr, while MCP uses `CommandOutput.Push` with a buffering sink to capture output per invocation. Do not bypass this abstraction with direct stdout writes or Spectre UI output.

## Integration and verification

A new service-backed command is complete only when `GeekCli` registers the service and exposes direct CLI, wizard, and MCP adapters for the same model and service. Coordinate changes to inputs, defaults, supported values, or validation across all three paths.

Follow the root build and verification instructions for implementation changes, verify equivalent behavior through each execution path, and update affected `README.md` examples. Keep this library reusable without invoking the executable project.
