# Migration agent skill source references

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 2

Status: Pending

## Story

As a database maintainer, I want the migration agent to reference the correct skill sources, so that I can locate its dependency definitions.

## Acceptance criteria

- [ ] Given the migration agent definition, when I follow each of its four skill source references, then I find the corresponding existing skill definition under `GeekCli/Skills/Db/`.
- [ ] Given the corrected references, when I read the dependency instructions, then they still distinguish bundled sources from separately installed skills.

## Plan

- [ ] Correct the four skill source paths in the agent definition.
- [ ] Check that each path exists and review the diff to confirm the change is limited to source references.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Check the four referenced source paths against the repository and review the diff. This is a documentation-only correction: no builds, installation checks, MCP execution, or generator execution are required.

## Out of scope

- Extracting workflows or changing responsibility boundaries (stories 3–6).
- Changing or testing installation, scope resolution, or dependency discovery.
- Changing commands, MCP tools, migration behavior, or versioning policy.

## Approval

Proposed for user approval. No implementation work has started.
