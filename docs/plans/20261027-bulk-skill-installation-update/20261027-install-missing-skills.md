# Install all missing skills

Related backlog: [Bulk skill installation and update](../../backlog/20261027-bulk-skill-installation-update.md)

Story: 1
Status: Completed

## Story

As a Geek CLI user, I want to install all catalog skills that are not yet installed in the selected scope, so that I can complete my installation in a single operation.

## Acceptance criteria

- [x] Given a scope containing installed and missing skills, when I run bulk installation, then only missing bundled catalog skills are installed and the contents of existing and external skills are preserved.
- [x] Given a valid scope, when I use the CLI or wizard with equivalent inputs, then I obtain the same result; the wizard presents a summary and allows confirmation or cancellation before files are changed.
- [x] Given a bulk operation, when it finishes, then it reports installations, skips, and errors, continues after an individual skill fails, and returns an error if any skill failed. If no skills are missing, it reports that there is nothing to install and succeeds.

## Implementation tasks

- [x] Add `ai skill install --all --scope <project|user>`, retaining `--project-path` for projects and preserving existing individual installation behavior.
- [x] Extend the shared command model and service to select missing skills, process them, and report a final summary.
- [x] Add the operation to the AI wizard reachable from the general wizard, with summary and confirmation.
- [x] Validate scope, project path, and selection of exactly one mode before writing; reject bulk mode combined with an individual name, `--update-all`, or `--overwrite`.
- [x] Update documentation and, after the user installs the version and explicitly authorizes testing, verify CLI and wizard behavior using the agreed scenarios.

## Interfaces and agreed decisions

- CLI: `geek-cli ai skill install --all --scope <project|user> [--project-path <path>]`.
- Scope remains required. Project scope requires an existing project directory; user scope does not accept a project path.
- Preserve destinations: `<projectPath>/.agents/skills/<name>` for projects and the user's home `.agents/skills/<name>` for user scope.
- A catalog skill is installed when its destination directory exists in the selected scope. Existing directories are skipped without inspecting or replacing their contents.
- Copy the complete bundled folder of each missing skill. Use only the catalog included in the running Geek CLI version; do not query remote sources.
- CLI and wizard use the same model and service. Common selection, validation, execution, and reporting remain in the service library; prompts remain in the wizard.
- CLI runs without prompts. The wizard provides a summary, confirmation, and existing Back navigation behavior; cancellation changes no files.
- Invalid requests fail before changes. Per-skill execution failures do not prevent remaining candidates from being processed; retain successful operations and report a nonzero result if any candidate failed.
- No candidates is a successful no-op. Empty-catalog behavior follows the existing installation behavior and must not create destination directories.
- By explicit user decision, do not expose this operation through MCP, overriding the repository's general three-path requirement.

## Validation and responsibilities

The user generates and installs the version to be tested. Do not execute tests until the user explicitly indicates they may begin. Use that installed version, a disposable project, and an isolated user destination. Build and package generation are handled by the user as part of preparing the version.

- [x] Compare CLI and wizard results for project scope using missing, existing, and external skills; the user confirmed successful installation in user scope. Confirmed complete resource copying and preservation of existing contents.
- [x] Check invalid or missing scope, invalid project paths, a project path supplied for user scope, and conflicting mode/name/overwrite inputs without destination changes.
- [x] Check wizard cancellation, no missing skills, an empty catalog, repeated execution, and partial failures followed by successful processing of remaining candidates.
- [x] Confirm the existing individual installation remains usable and MCP does not expose the new operation.
- Keep acceptance criteria and validation work unchecked until verified. Report any scenario that cannot be exercised and why; do not mark the story Completed while required checks remain pending.

## Out of scope

- Updating existing skills, installing external skills, modifying subagents, or exposing skill installation through MCP.
