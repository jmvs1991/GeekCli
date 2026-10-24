# Angular component generation in Nx workspaces

Related backlog: [Angular Nx support](../../backlog/20261023-angular-nx-support.md)

Story: 1

## Story

As a GeekCli user, I want to generate components in a standalone Angular project or an Angular project within an Nx workspace, so that I can use the same command in both environments.

## Acceptance criteria

- [ ] Given a CLI or MCP invocation, when the boolean `nx` flag is false or omitted, then component generation preserves the existing behavior, including the `ngx` prefix, and the destination project may be omitted, null, or empty.
- [ ] Given an invocation with `nx` enabled, when a valid Angular project in the workspace is specified, then the component is generated in that project. If the destination is null, empty, or does not exist, an error is returned and no files are generated.
- [ ] Given an interactive wizard invocation, when the user indicates that the environment is an Nx workspace, then its Angular projects are listed for destination selection, and generation produces equivalent results to CLI and MCP invocations with the same inputs.

## Plan

- Add the boolean `nx` flag, defaulting to false, and an optional destination project to the shared command model.
- Centralize validation and adapt component generation to the explicitly selected environment in the shared service.
- Expose equivalent inputs through CLI and MCP, without interactive prompts in either path.
- Extend the wizard to ask whether the environment is an Nx workspace and, if so, list its Angular projects for destination selection.
- Verify all three execution paths in disposable projects, including invalid inputs, generator failures, and existing standalone behavior; run the required solution and Release builds and update usage documentation.

## Out of scope

- Page generation, which belongs to the second backlog story.
- Automatic environment detection.
- Workspace creation or migration and dependency installation.
- Additional component customization options.
