# Angular service generation without Nx

Related backlog: [Angular service generation](../../backlog/20261024-angular-service-generation.md)

Story: 1
Status: Completed

## Story

As a GeekCli user, I want to generate Angular services in projects without Nx through the CLI, interactive wizard, or MCP, so that I can create services using the same workflow as pages and components.

## Acceptance criteria

- [x] Given an Angular project without Nx, requesting service generation with a name creates the corresponding files according to Angular CLI configuration and defaults.
- [x] Equivalent inputs through the CLI, wizard, and MCP produce equivalent results; the wizard shows a summary and requests confirmation before execution.
- [x] When the name is missing or generation fails, each execution path reports an error and an unsuccessful execution result.

## Implementation tasks

- [x] Add a shared service that validates the name, executes `ng generate service <name>`, and propagates process output and the exit code through the existing shared execution and output abstractions.
- [x] Expose `geek-cli ngx service <name>` with command/settings classes and branch registration, and expose the discoverable MCP tool `NgxService(name)`, both using the same command model and shared service.
- [x] Register the shared service in dependency injection for CLI and MCP execution.
- [x] Add service generation to the Angular wizard, reachable from the general wizard, following existing summary, confirmation, and Back navigation patterns.
- [x] Manually verify equivalent inputs through all three paths in a disposable Angular project, including missing names, wizard cancellation, generator failure, output, generated files, and execution results.
- [x] Update README command structure and examples for direct CLI, interactive, and MCP service generation.
- [x] Build the solution with `dotnet build .\GeekCliSolution.sln` and the executable with `dotnet build .\GeekCli\GeekCli.csproj -c Release`.

## Scope and defaults

- Request only the service name and use the current directory, following the existing Angular workflow.
- Delegate naming, destinations, generated files, and default options to Angular CLI and project configuration.
- Keep execution and common validation in `GeekCliServices`; CLI, wizard, and MCP adapters collect inputs and present results.
- Keep interactive prompts in the wizard and preserve stdout for the MCP protocol.
- Wizard cancellation must not invoke the generator or create files.
- Report any execution path that cannot be verified and explain why.

## Out of scope

- Nx support; this belongs to story 2.
- Additional generation options.
- Automated tests and automated change validation; functional verification is manual, alongside the required builds.
