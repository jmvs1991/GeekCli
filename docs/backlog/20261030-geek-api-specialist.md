# Geek API specialist

Status: Pending

## Context

Help maintainers develop features in existing API solutions through `geek-api-specialist`, with clearly defined responsibility blocks and reusable skills where they provide value. Review those boundaries before expanding the agent or deciding which skills to create or adapt.

Stack: Existing Codex agent definition in TOML, Markdown skills, and Geek CLI MCP tools, distributed through Geek CLI.

Expensive decisions: None additional. No new multi-organization, permissions, offline/synchronization, or multilingual/multi-currency requirements.

## Backlog

1. As an API maintainer, I want to review the specialist's responsibilities by functional block and agree which remain in the agent, reuse existing skills, or justify new skills, so that responsibility boundaries are clear before expanding it. Status: Pending.
2. As an API maintainer, I want to develop a feature with the specialist coordinating the agreed skills, so that my solution's conventions are applied consistently. Requires: #1 and the required skill stories resulting from that review. Status: Pending.
3. As a Geek CLI user, I want to receive suggested text for my project's `AGENTS.md` when installing the specialist, so that I can specify when API requests should be delegated to it. Status: Pending.

## Agreed context

- Work on existing API solutions; creating solutions from scratch is outside the agreed scope.
- Connection contains the database context, entities, and read/write repositories.
- Test projects contain unit tests, Services projects contain API services, and the optional RestClient project contains the API client.
- IntegrationTest projects contain integration tests, and Cache projects manage caching.
- The API project has a business name such as Sales or Catalog; these names are examples.
- Agree on conventions progressively with the user. The areas above are context for the responsibility review rather than mandatory story boundaries or a fixed implementation sequence.
- Story #1 determines the final responsibility blocks and whether each should remain in the agent, reuse an existing skill, or justify creating or adapting a skill. Add or adapt skill stories only when that review justifies them, avoiding duplicate responsibilities.
- Story #2 is the final agent redefinition and depends on every required skill story resulting from the review. Update its dependencies when those stories are added.
- TDD is not required for writing the agent definition. The test-generation skills remain available for the API functionality it builds.
- The existing API agent already declares version `0.1.0` and includes a welcome message identifying its name and version; these are existing behavior rather than new pending features.
- Agent installation already supports displaying suggested `AGENTS.md` text from an accompanying recommendation file. Story #3 supplies the API specialist's recommendation through that mechanism without automatically modifying the target project's `AGENTS.md`.
- Use the migration specialist as a reference for responsibility separation, skill reuse, and installation recommendations. Do not automatically transfer its database-specific restrictions to the API specialist.

## Completed work from the original backlog

The original installation and welcome story is completed and retained in its [historical plan](../plans/20261030-geek-api-specialist/20261030-installation-and-welcome.md). It established installation, versioning, and the initial welcome. That plan's original story #1 is distinct from the responsibility-review story #1 above; its completed status and validation remain valid.

The current stories will receive new plans when refined.

## References

- Responsibility review and skill extraction approach: [Migration agent versioning and skills](20261101-migration-agent-versioning-and-skills.md).
- Existing API specialist: `GeekCli/Agents/Dotnet/geek-api-specialist.toml`.
- Migration specialist and installation recommendation: `GeekCli/Agents/Db/`.

- Existing skill sources to assess for reuse: `GeekCli/Skills/Db/` and `GeekCli/Skills/Dotnet/`. Bundled source definitions are distinct from skills installed in the active user's or project's scope.
