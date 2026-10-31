# Geek API specialist

Status: Pending

## Context

Create `geek-api-specialist` to help API maintainers build functionality in existing API solutions. Define conventions together with the user one section at a time, starting with Connection and advancing only after the current section is agreed.

Stack: Installable Codex agent distributed through Geek CLI, using the eleven existing Geek skills listed below.

Expensive decisions: None additional.

## Backlog

1. As a Geek CLI user, I want to install and invoke `geek-api-specialist` and receive a welcome message with its name and version, so that I can identify the agent I am using. Status: Pending.
2. As an API maintainer, I want to progressively agree on Connection conventions and apply them to a feature, so that its context, entities, and repositories follow my project's conventions. Requires: #1. Status: Pending.
3. As an API maintainer, I want to agree on Services conventions and apply them to a feature, so that its services follow my project's format. Requires: #2. Status: Pending.
4. As an API maintainer, I want to agree on unit test conventions and apply them to a feature, so that I can verify its behavior. Requires: #3. Status: Pending.
5. As an API maintainer, I want to agree on optional RestClient conventions and apply them when relevant, so that I can provide a client for my API. Requires: #4. Status: Pending.
6. As an API maintainer, I want to agree on IntegrationTest conventions and apply them to a feature, so that I can verify its integration. Requires: #5. Status: Pending.
7. As an API maintainer, I want to agree on Cache conventions and apply them to a feature, so that its caching follows my project's conventions. Requires: #6. Status: Pending.
8. As an API maintainer, I want to agree on API project conventions and expose a complete feature, so that its endpoints are consistent with the preceding sections. Requires: #7. Status: Pending.

## Agreed context

- Work on existing API solutions; creating solutions from scratch is outside the agreed scope.
- Connection contains the database context, entities, and read/write repositories.
- Test projects contain unit tests, Services projects contain API services, and the optional RestClient project contains the API client.
- IntegrationTest projects contain integration tests, and Cache projects manage caching.
- The API project has a business name such as Sales or Catalog; these names are examples.
- Define and review each section before moving to the next one. Detailed conventions remain to be agreed during those iterations.
- TDD is not required for writing the agent definition. The test-generation skills remain available for the API functionality it builds.
- Evaluate whether the agent's size and responsibilities justify splitting it into specialist subagents with an orchestrator. The architecture decision is still pending.
- Analyze an agent versioning approach so users can identify the version in use. The version scheme and initial version are still pending.
- Include a welcome message identifying the active agent and its version.

## Available skills

The agent may use these existing skill definitions:

- `GeekCli/Skills/geek-db-scaffold/SKILL.md`
- `GeekCli/Skills/geek-dotnet-api-unit-test/SKILL.md`
- `GeekCli/Skills/geek-dotnet-cache/SKILL.md`
- `GeekCli/Skills/geek-dotnet-controller/SKILL.md`
- `GeekCli/Skills/geek-dotnet-dto/SKILL.md`
- `GeekCli/Skills/geek-dotnet-read/SKILL.md`
- `GeekCli/Skills/geek-dotnet-resource/SKILL.md`
- `GeekCli/Skills/geek-dotnet-service/SKILL.md`
- `GeekCli/Skills/geek-dotnet-sp/SKILL.md`
- `GeekCli/Skills/geek-dotnet-unit-test-service/SKILL.md`
- `GeekCli/Skills/geek-dotnet-write/SKILL.md`
