# Angular service generation in Nx

Related backlog: [Angular service generation](../../backlog/20261024-angular-service-generation.md)

Story: 2
Status: Completed
Approval: Pending

## Story

As a GeekCli user, I want to generate Angular services in a selected application or library within an Nx workspace through the CLI, interactive wizard, or MCP, so that I can create services in the project and destination folder I need.

## Acceptance criteria

- [x] Given an Nx workspace, requesting a service with a name, project, and project-relative folder creates the service at that location with the `.service` suffix, without requiring an NgModule.
- [x] Equivalent inputs through the CLI, wizard, and MCP produce equivalent results; the wizard supports selecting applications and libraries tagged `tech:angular`, shows a summary, and allows cancellation without generating files.
- [x] Missing required inputs, folders containing `..` segments, execution outside the Nx workspace root, and generator failures produce an error and an unsuccessful execution result; invalid inputs are rejected before generation.

## Implementation tasks

- [x] Extend the service CLI command with `--nx`, `--project`, and `--folder`, and the MCP tool with `nx=false`, `project=null`, and `folder=null`, reusing the shared command model and service.
- [x] Centralize validation and, in Nx mode, execute the equivalent of `npx nx generate @nx/angular:service Services/test --project=core --type=service`, preserving the provided folder and delegating naming to the generator.
- [x] Integrate Nx service generation into the existing wizard, reusing Angular project selection and requesting the name and project-relative folder, with a summary and confirmation.
- [x] Update README documentation for the CLI, wizard, and MCP execution paths.
- [x] Have the user manually verify generation in applications and libraries through all three paths, nested folders, invalid inputs, cancellation, generator failures, and regression without Nx.
- [x] Have the user manually build the solution with `dotnet build .\GeekCliSolution.sln` and the executable with `dotnet build .\GeekCli\GeekCli.csproj -c Release`.

## Assumptions and scope

- The reference command works in the target workspace, as confirmed. Do not add Nx version detection or compatibility variants.
- In Nx mode, the name, project, and project-relative folder are required. Run from the workspace root. Leave other options to the generator configuration.
- Preserve existing behavior without Nx. Do not modify page or component generation.
- The user will perform manual verification and builds. The agent must not run generators, tests, or builds.
- Do not add extra generation options or automated test infrastructure.
- Acceptance criteria remain unchecked until the user confirms validation. Keep the plan pending until implementation starts, and do not mark it completed until every acceptance criterion and implementation task is checked.
