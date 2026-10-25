# Subagent installation

Related backlog: [AI skills and subagents installation](../../backlog/20261024-ai-skills-agents-installation.md)

Story: 2
Status: Completed

## Story

As a Geek CLI user, I want to install a subagent from the included catalog through the CLI or interactive wizard, choosing project-level or user-level scope, so that I can use specialized agents in Codex.

This delivery provides a working installer with an initially empty catalog. Actual subagents will be defined and added later. By explicit user decision, subagent installation must not be available through MCP, extending the installation exception already agreed for skills.

## Acceptance criteria

- [x] Given a subagent available in the catalog, when I install it through the CLI or wizard with the scope and, for project scope, the project path, then its complete definition is installed in the corresponding destination and I receive confirmation including that location.
- [x] Given a subagent already installed at the destination, when I try to install it again, then the existing installation is preserved and the conflict is reported; if I explicitly authorize replacement, I receive the version included in the catalog.
- [x] Given an empty catalog or an invalid request, when I attempt installation, then I receive an explanatory message without changes to the destination; when the catalog is empty, the wizard allows me to go back.

## Implementation tasks

- [x] Add an initially empty subagent catalog bundled with the tool package, allowing agents to be added without changing the installer.
- [x] Implement a shared service and command model to validate inputs, resolve destinations, and install or replace only the selected agent.
- [x] Integrate the direct command and operation selection into the AI wizard, reachable from the general wizard.
- [x] Run the required builds and verify packaging; after the user installs the updated CLI version, verify the installed CLI and wizard in disposable environments using the validation scenarios below.
- [x] Update documentation, the MCP exception, and backlog statuses; retain this refinement in English according to repository conventions.

## Interfaces and agreed decisions

- Direct CLI: `geek-cli ai agent install <name> --scope <project|user> [--project-path <path>] [--overwrite]`.
- Scope is required. Project scope requires an existing project directory. User scope does not accept a project path.
- Each catalog entry is a standalone TOML file whose filename matches the agent name.
- Project destination: `<projectPath>/.codex/agents/<name>.toml`.
- User destination: `~/.codex/agents/<name>.toml`, with `~` resolved to the user's home directory.
- These destinations follow the [official OpenAI subagents documentation](https://learn.chatgpt.com/docs/agent-configuration/subagents).
- The wizard offers a choice between installing skills and subagents, displays a summary, and requests confirmation before installation or replacement.
- Existing installations are preserved unless replacement is explicitly authorized through `--overwrite` or wizard confirmation.
- CLI and wizard use the same shared service and command model, with common validation in the service library.
- MCP must not expose subagent installation.
- Functional verification was run against the installed GeekCli 2.5.0 CLI. Its `GeekCli.dll` SHA-256 matched the Release assembly from this checkout. The generated NuGet package metadata reports version 2.4.0.

## Validation

- [x] Use a temporary test subagent that is excluded from the published catalog.
- [x] Verify installation through the installed direct CLI and wizard for both project and user scope in disposable environments, isolating the user destination.
- [x] Verify equivalent CLI and wizard inputs produce equivalent contents, messages, and execution results.
- [x] Verify conflicts preserve existing installations and explicit replacement installs the bundled definition without changing other agents.
- [x] Verify empty-catalog behavior, invalid inputs, and write failures; invalid requests must not change the destination.
- [x] Review MCP registrations to confirm no subagent installation adapter was added; MCP execution is outside this story's verification scope.
- [x] Run `dotnet build .\GeekCliSolution.sln` from the repository root.
- [x] Run `dotnet build .\GeekCli\GeekCli.csproj -c Release` from the repository root.
- [x] Run `dotnet pack .\GeekCli\GeekCli.csproj -c Release` and verify catalog resources are packaged correctly without shipping the temporary test agent.
- [x] Record validation results, leaving unexecuted checks pending and explaining why they could not be completed.

Validation record: The installed GeekCli 2.5.0 passed direct CLI install for project and user scope, project conflict preservation, explicit replacement, unrelated-agent preservation, empty catalog, invalid scope/name/project path, and a blocked-write scenario. Its `GeekCli.dll` SHA-256 matched the Release assembly built from this checkout. The interactive AI wizard passed project and user installs with the expected summary and confirmation; the general wizard returned to its area menu from the empty subagent catalog. Temporary catalog entries and destination files were removed. Source inspection confirmed that no subagent installation MCP adapter was added; MCP execution was not tested because it is outside this story's scope.

Mark this plan as Completed only after every acceptance criterion, implementation task, and validation item is checked.

## Out of scope

- Defining or including actual subagents.
- Downloading agents, installing their dependencies, or executing them.
- Changing the general Codex configuration.
- Independent commands for listing, updating, or uninstalling agents.
