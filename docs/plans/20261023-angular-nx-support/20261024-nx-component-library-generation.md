# Angular component generation in Nx libraries

Related backlog: [Angular Nx support](../../backlog/20261023-angular-nx-support.md)

Story: 3
Status: Completed
Approval: Approved

## Story

As a GeekCli user, I want to generate components in Angular libraries within an Nx workspace, so that I can create reusable UI components in shared libraries.

## Acceptance criteria

- [x] The component wizard lists Nx projects tagged `tech:angular`, including applications and libraries, while the page wizard remains limited to applications.
- [x] Component generation accepts a workspace-relative destination folder under an application's `src/app` or a library's `sourceRoot`, preserves the supplied path, and appends the kebab-case component name.
- [x] The declaring NgModule is supplied as a workspace-relative path including `.ts`; CLI, wizard, and MCP use the same command model and service behavior.
- [x] Invalid paths containing traversal segments fail before the generator creates files.
- [x] The service does not query Nx to rediscover the selected project's destination root; the wizard queries Nx to list and filter project choices.

## Plan

- [x] Include Angular libraries when discovering component projects by `tech:angular` tag.
- [x] Generate components using the supplied workspace-relative destination and module paths.
- [x] Keep project discovery in the wizard and remove the extra `nx show project` lookup from shared component execution.
- [x] Align direct CLI, wizard, MCP descriptions, and README examples with the folder and module path conventions.
- [x] Manually verify application and library generation, project listing, invalid-path rejection, and page wizard application-only filtering.
- [x] Build the solution and the GeekCli project in Release configuration.

## Out of scope

- Generating pages in Angular libraries.
- Adding an automated test framework; verification is manual in this repository.
