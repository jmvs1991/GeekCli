# Migration responsibility review

Related backlog: [Migration agent versioning and skills](../../backlog/20261101-migration-agent-versioning-and-skills.md)

Story: 3

Status: Pending

## Story

As a database maintainer, I want to break down the migration agent into functional blocks and assess which can become reusable skills, so that I can plan the necessary skill stories and a final agent redefinition based on the review.

## Acceptance criteria

- [ ] Given the current migration agent, when I review its breakdown, then all its instructions are accounted for in functional blocks with a clear purpose and responsibility.
- [ ] Given those blocks and the existing operation skills, when I review the assessment, then each block has a justified recommendation to remain in the agent, reuse an existing skill, or become a skill, with its inputs, deliverables, and dependencies identified where extraction is proposed.
- [ ] Given the review findings, when they are recorded in the backlog, then existing skill stories are adjusted and additional skill stories are added only where justified, followed by a final agent redefinition story dependent on the required skill work; the proposed boundaries preserve the current workflow, confirmation requirements, plan coordination, and local development database execution restriction.

## Plan

- [ ] Break down all current agent instructions into functional blocks.
- [ ] Assess each block's cohesion, reuse potential, and overlap with existing operation skills.
- [ ] Recommend what to retain, reuse, or extract, describing the boundaries and dependencies of extraction candidates.
- [ ] Update existing skill stories and add any justified skill creation or adaptation stories, avoiding duplicate scope.
- [ ] Define the final agent redefinition story and its dependencies, verify workflow preservation, and present the resulting review and backlog for approval before implementation.

## Validation

TDD is not required because the repository has no test project. Do not introduce a test project or a TDD workflow as a prerequisite for this story.

Validate by directly comparing the proposal with the agent instructions and the four operation skill definitions, checking references and version consistency, and reviewing the documentation diff. No builds or generator execution are required for this documentation-only review.

## Out of scope

- Implementing the skill creation or adaptation stories resulting from this review.
- Implementing the final agent redefinition.
- Changing the agent behavior, version, installation, commands, or MCP tools.
- Generating migrations or scripts, or executing database operations.

## Approval

Refinement approved by the user. The responsibility review and resulting backlog updates remain pending execution.
