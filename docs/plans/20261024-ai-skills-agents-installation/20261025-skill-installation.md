# Skill installation

Related backlog: [AI skills and subagents installation](../../backlog/20261024-ai-skills-agents-installation.md)

Story: 1
Status: Completed

## Story

As a Geek CLI user, I want to install a skill from the included catalog through the CLI or interactive wizard, choosing project-level or user-level scope, so that I can use its instructions in Codex.

This delivery provides a working installer with an initially empty catalog. Actual skills will be defined and added later. By explicit user decision, skill installation must not be available through MCP; this overrides the backlog's three-path requirement for this operation only.

## Acceptance criteria

- [x] Given a catalog containing an available skill, when I install it through the CLI or wizard with the scope and, for project scope, the project path, then its complete contents are installed in the corresponding destination and I receive confirmation including that location.
- [x] Given a skill already present at the destination, when I try to install it again, then the existing installation is preserved and the conflict is reported; if I explicitly authorize replacement, the destination contains the complete bundled version.
- [x] Given an empty catalog or an invalid request, when I attempt installation, then I receive an explanatory message without changes to the destination; when no skills are available, the wizard allows me to go back.

## Implementation tasks

- [x] Add an initially empty catalog based on skill folders bundled in the tool package, allowing instructions and supporting resources to be added without changing the installer.
- [x] Implement a shared service and command model to validate inputs, resolve destinations, and install or replace only the selected skill.
- [x] Expose installation through the AI CLI branch and an AI wizard reachable from the general wizard, without registering an MCP installation tool.
- [x] Validate CLI and wizard behavior with a temporary skill: both scopes, nested resources, conflicts, replacement, an empty catalog, invalid inputs, and write failures. Confirm MCP does not expose skill installation, run both required builds, and verify the tool package includes catalog resources.
- [x] Update README usage and catalog-authoring instructions, and update the backlog to reflect the MCP exception for skill installation. Keep story and backlog statuses accurate as implementation progresses.

## Interfaces and agreed decisions

- Direct CLI: `geek-cli ai skill install <name> --scope <project|user> [--project-path <path>] [--overwrite]`.
- Scope is required. For `project`, the project path is required and must identify an existing directory. The project path is not accepted for `user` scope.
- Project destination: `<projectPath>/.agents/skills/<name>`.
- User destination: `~/.agents/skills/<name>`, with `~` resolved to the user's home directory.
- These destinations follow the [official Codex skill discovery documentation](https://learn.chatgpt.com/docs/build-skills).
- The catalog content is bundled with each Geek CLI version; installation requires no downloads.
- Install the entire skill folder, including instructions and supporting resources.
- The wizard offers available skills, requires scope selection, and requests explicit authorization before replacement, following existing summary, confirmation, and Back navigation patterns.
- Without explicit replacement authorization, an existing destination is preserved and installation returns an error. Invalid requests and write failures also return errors.
- An empty catalog is valid and must produce an explanatory message without creating destination directories.
- A temporary skill is used only for validation and must not ship in the published catalog.
- CLI and wizard use the same command model and shared service. Common validation and installation belong in the service library; prompts remain in the wizard.
- MCP must not expose this installation operation. This decision does not define MCP availability for the separate subagent story.

## Validation

- Use a disposable workspace and an isolated user home for installation checks.
- Verify equivalent inputs through CLI and wizard produce equivalent contents, messages, and execution results.
- Verify replacement removes obsolete contents of the selected skill while leaving other skills untouched.
- Verify missing or invalid scope, unknown skill names, missing or nonexistent project paths, and project paths supplied with user scope fail before writing.
- Verify the MCP tool list contains no skill installation tool.
- Run from the repository root:
  - `dotnet build .\GeekCliSolution.sln`
  - `dotnet build .\GeekCli\GeekCli.csproj -c Release`
  - `dotnet pack .\GeekCli\GeekCli.csproj -c Release` when checking bundled catalog resources.
- Report any verification that cannot be completed and explain why.

## Out of scope

- Defining or including actual skills.
- Installing skills through MCP.
- Installing subagents; that story will be refined separately.
- Remote downloads, dependency installation, or execution of skill scripts.
- Independent commands for listing, updating, or uninstalling skills.
