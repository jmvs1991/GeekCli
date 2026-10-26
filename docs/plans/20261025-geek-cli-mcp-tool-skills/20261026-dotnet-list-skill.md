# Dotnet list skill

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 1
Status: Completed

## Story

As a Geek CLI user, I want a skill that lets my assistant use the existing `dotnet_list` MCP tool, so that I can discover the installed Geek .NET templates.

## Acceptance criteria

- [x] Given Geek CLI with the bundled skill, when I run `geek-cli ai skill install geek-dotnet-list` with a valid scope, then the skill is installed in the corresponding destination. The existing catalog, packaging, and installer path is reused.
- [x] Given the installed skill and an available MCP tool, when I request the installed Geek .NET templates, then the assistant invokes `dotnet_list` without parameters and communicates its result without inventing templates.
- [x] Given an unavailable MCP tool or a failed execution, when I request the list, then the assistant reports the impediment without falling back to CLI commands.

## Implementation tasks

- [x] Update the backlog context in English to state that all stories deliver skills for invoking existing MCP tools, located at `GeekCli/Skills/<skill-name>/SKILL.md` and installable through the existing command. State that validation focuses on skill quality and does not repeat testing of existing install commands.
- [x] Create `GeekCli/Skills/geek-dotnet-list/SKILL.md` in English, including its name, description, usage conditions, prerequisites, parameterless invocation, and interpretation of `ExitCode` and `Output`. Explain that it lists installed Geek templates and how to communicate no matches, execution errors, or tool unavailability.
- [x] Reuse the existing catalog, packaging, and installer without modifying contracts or C# code.
- [x] Update the README with the skill's availability and installation examples for user and project scopes.
- [x] Validate the skill content against the MCP tool signature, execution behavior, and result contract. Do not repeat CLI or wizard installation tests; those existing commands have already been tested.

## Validation

- Review the skill against the MCP tool signature, execution behavior, and result contract.
- Ensure its instructions cover successful results, no matches, execution failure, and MCP tool unavailability without inventing templates or falling back to CLI commands.
- Review the skill against the actual MCP tool signature and result contract; do not infer templates from documentation or other tools.
- No TDD is required.

## Scope and status

Installing templates, configuring MCP, generating files through other tools, and refactoring existing commands are outside this story's scope.

The backlog is In progress while other tool skills remain. This story is Completed after reviewing its instructions against the MCP contract; installation testing is excluded because the existing commands have already been tested.
