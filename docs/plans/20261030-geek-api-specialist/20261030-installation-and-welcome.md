# Geek API specialist installation and welcome

Related backlog: [Geek API specialist](../../backlog/20261030-geek-api-specialist.md)

Story: 1 of the original backlog (installation and welcome, before the responsibility-block redefinition).
Status: Completed
Approval: Approved

Historical plan: This completed work is retained as implementation history. It does not correspond to story #1 of the redefined backlog, which covers the responsibility-block review. Its completed status and recorded validation remain unchanged.

## Story

As a Geek CLI user, I want to install and invoke `geek-api-specialist` and receive a welcome message with its name and version, so that I can identify the agent I am using.

## Acceptance criteria

- [x] Given Geek CLI includes `geek-api-specialist`, when I install it through the direct CLI or AI wizard for a project or my user profile, then the agent is available in the selected scope and I receive confirmation of its installation location.
- [x] Given `geek-api-specialist` is installed and available in Codex, when I invoke it, then its initial response welcomes me and identifies the agent as `geek-api-specialist` with the version of its installed definition.
- [x] Given an installation already exists, when I install it again without authorizing replacement, then the existing definition is preserved and the conflict is reported; when I explicitly authorize replacement and invoke the resulting installation, then its welcome identifies the bundled version I installed.

## Plan

- [x] Agree on the agent version scheme (`MAJOR.MINOR.PATCH`, independent of Geek CLI, initial `0.1.0`) and English ASCII welcome.
- [x] Add the minimal agent definition to the bundled catalog using the existing installation behavior and packaging.
- [x] Define the initial welcome using the installed agent's identity and version, leaving feature conventions for later stories.
- [x] Verify packaging, both installation paths and scopes, replacement behavior, and the welcome in Codex; run the required solution and Release builds. User confirmed all verifications passed.
- [x] Document installation, invocation, and the agreed versioning approach, and record validation results.

## Context checked

- The shared installer already discovers bundled TOML definitions and supports project and user scopes, conflict preservation, and explicit replacement.
- The AI wizard already discovers the bundled agent catalog. The package already includes agent definitions.
- The existing installation plan explicitly records the user's decision to exclude agent installation from MCP. This story reuses that installer and does not introduce a command or change its behavior.
- The existing agent definition inspected does not establish an agent-versioning convention.
- No `CLAUDE.md` was found in the repository file listing; repository context was reviewed through the root and executable `AGENTS.md` instructions, installer, existing agent, packaging, README, and installation plan.

## Out of scope

- Connection, Services, tests, RestClient, IntegrationTest, Cache, and API conventions or feature implementation.
- Deciding or implementing an orchestrator and specialist subagent architecture.
- Automatic installation of the eleven skills or changes to their definitions.
- New install, update, or uninstall commands, MCP installation, and changes to general Codex configuration.

## Suggestions (your call)

- Use an agent version independent of the Geek CLI package, with `MAJOR.MINOR.PATCH` and initial version `0.1.0`, so the welcome identifies the installed definition even when unrelated CLI features change. This is a proposal, not an agreed decision.
- Proposed welcome wording, subject to approval: "Soy geek-api-specialist, versión 0.1.0. Te ayudo a desarrollar funcionalidades en soluciones API existentes."

Validation results: User confirmed the installation, replacement, welcome, and build checks passed. The agent is `geek-api-specialist` version `0.1.0`; its installed welcome uses an ASCII banner and English greeting.
