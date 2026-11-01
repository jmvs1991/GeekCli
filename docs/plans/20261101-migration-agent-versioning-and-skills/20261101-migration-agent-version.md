# Migration agent version identification

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 1

Status: Completed

## Story

As a database maintainer, I want to identify the migration agent's version, so that I know which revision I am using.

## Acceptance criteria

- [x] Given an installed migration agent definition, when I inspect its metadata, then I find the initial version `0.1.0` in `MAJOR.MINOR.PATCH` format.
- [x] Given a new conversation with the migration agent, when it responds for the first time, then it displays its name and the version declared in its installed definition.
- [x] Given an installation through the direct CLI or interactive wizard at user or project scope, when I inspect the installed definition, then it preserves the distributed version.

## Implementation tasks

- [x] Declare the agent version as `0.1.0`, following the existing API agent convention.
- [x] Include the agent name and the version from its installed definition in its initial greeting, before the existing opening questions.
- [x] Document the initial version and how to identify it.
- [x] Verify metadata and greeting consistency; run the required builds, package the tool, and check both installation paths in disposable destinations.
- [x] Review the initial greeting in a new conversation and record validation results and any limitations.

Validation results: The user confirmed that the packaged CLI installation, CLI and interactive installation scenarios, and first-response greeting were verified successfully. Both required builds completed with zero errors; package and installation checks were confirmed by the user. Restore emitted NU1900 warnings because the configured private NuGet vulnerability indexes were unavailable, and the build reported existing compiler warnings.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Review the agent instructions, README documentation, and version consistency. Run `dotnet build .\GeekCliSolution.sln`, `dotnet build .\GeekCli\GeekCli.csproj -c Release`, and `dotnet pack .\GeekCli\GeekCli.csproj -c Release` during implementation. Inspect the packaged definition and verify CLI and interactive installation at both user and project scope in disposable destinations. Confirm that the first response displays the installed name and version before the existing opening questions. Report any scenario that could not be verified and why.

Creating this plan is documentation-only and does not require builds or generator execution. Keep this plan Pending until implementation begins, and mark it Completed only when every acceptance criterion and implementation task is checked.

## Out of scope

- Versioning skills or generated migrations.
- Correcting skill references or redistributing agent responsibilities.
- Adding commands, MCP tools, or automatic updates.
