# Angular page generation in Nx workspaces

Related backlog: [Angular Nx support](../../backlog/20261023-angular-nx-support.md)

Story: 2
Status: Completed
Approval: Approved

## Story

As a GeekCli user, I want to generate pages in a standalone Angular project or an Angular project within an Nx workspace, so that I can use the same command in both environments.

## Acceptance criteria

- [x] Given a standalone Angular project, when page generation is invoked with Nx mode disabled or omitted, then the existing page generation behavior and defaults are preserved without requiring Nx inputs.
- [x] Given an Nx workspace, when the user provides the destination Angular application, folder, page name, and declaring module with Nx mode enabled, then a page is generated in that destination with the folder formatted as PascalCase and the page name formatted as kebab-case; missing required inputs, a library destination, or another invalid destination return an error without generating files.
- [x] Given equivalent inputs, when page generation runs through direct CLI, the interactive wizard, or MCP, then all three paths produce equivalent artifacts and execution results; the page wizard lists only Angular applications from the Nx workspace and excludes libraries.

## Plan

- [x] Reuse the shared `StringExtension` string formatters for page and component naming without changing component behavior.
- [x] Extend page generation through the direct CLI with Nx mode, destination inputs, and the page generator type.
- [x] Add common validation for required Nx inputs and Angular application destinations, rejecting libraries and invalid folders without generating files.
- [x] Expose equivalent inputs and defaults through MCP.
- [x] Extend the page wizard to select Nx mode and an Angular application, excluding libraries, collect destination inputs, and confirm the generation summary.
- [x] Manually verify all three execution paths in disposable workspaces, including standalone behavior, rejection of library destinations, invalid inputs, and generator failures; run the required solution and Release builds.
- [x] Update usage documentation and track completion of this story and its backlog.

## Out of scope

- Adding a unit test framework or automated tests; validation for this story is manual, as in the previous story.
- Automatic environment detection.
- Workspace creation or migration and dependency installation.
- Automatic route registration or additional page customization options.
- Changes to existing component generation behavior.
- Page generation in Angular libraries; pages are supported only in Angular applications.

## Related component requirement

Pages must target Angular applications only. Components may target both Angular applications and Angular libraries, including shared component libraries.

Component library generation is tracked as [Story 3](../../backlog/20261023-angular-nx-support.md) with its own [implementation plan](20261024-nx-component-library-generation.md). This remains separate from page generation; pages are supported only in Angular applications.
