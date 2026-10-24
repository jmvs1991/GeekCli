# Angular component generation in Nx workspaces

Related backlog: [Angular Nx support](../../backlog/20261023-angular-nx-support.md)

Story: 1
Status: In progress

## Story

As a GeekCli user, I want to generate components in a standalone Angular project or an Angular project within an Nx workspace, so that I can use the same command in both environments.

## Acceptance criteria

- [ ] Given a CLI or MCP invocation, when the boolean `nx` flag is false or omitted, component generation preserves the existing behavior, including the `ngx` prefix, and the destination project may be omitted, null, or empty.
- [ ] Given an invocation with `nx` enabled, the user provides a project, folder, component name, and required module. The generator targets `apps/<project>/src/app/<PascalCase folder>/<kebab-case component>`, sets `--standalone=false`, `--module`, `--prefix=ngx`, and `--type=component`, and uses the selector `ngx-<kebab-case component>-component`. A missing or unknown project or a missing module returns an error without generating files.
- [ ] Given an interactive wizard invocation, when the user indicates that the environment is an Nx workspace, its Angular projects are listed for destination selection, and generation produces equivalent results to CLI and MCP invocations with the same inputs.

## Plan

- [x] Add the boolean `nx` flag, defaulting to false, and Nx project, folder, component, and module inputs to the shared command model.
- [x] Centralize validation, naming transformations, selector construction, and component generation in the shared service.
- [ ] Expose equivalent inputs through CLI and MCP, without interactive prompts in either path.
- [ ] Extend the wizard to ask whether the environment is an Nx workspace and, if so, list its Angular projects for destination selection.
- [ ] Verify all three execution paths in disposable projects, including invalid inputs, generator failures, and existing standalone behavior; run the required solution and Release builds and update usage documentation.

## Out of scope

- Page generation, which belongs to the second backlog story.
- Automatic environment detection.
- Workspace creation or migration and dependency installation.
- Additional component customization options.
