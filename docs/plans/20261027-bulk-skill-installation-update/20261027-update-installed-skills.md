# Update all installed catalog skills

Related backlog: [Bulk skill installation and update](../../backlog/20261027-bulk-skill-installation-update.md)

Story: 2
Status: Completed

## Story

As a Geek CLI user, I want to update all catalog skills already installed in the selected scope, so that I can obtain their versions bundled with Geek CLI in a single operation.

## Acceptance criteria

- [x] Given a scope containing installed catalog skills, when I run bulk update, then their complete contents are replaced with the bundled version, including local changes; external skills and missing catalog skills remain untouched.
- [x] Given a valid scope, when I use the CLI or wizard with equivalent inputs, then I obtain the same result; the wizard warns that local changes will be replaced and requests one global confirmation, while the CLI runs without prompts.
- [x] Given a bulk update, when it finishes, then it reports updates, skips, and errors, continues after an individual skill fails, and returns an error if any skill failed. If no catalog skills are installed, it reports that there is nothing to update and succeeds.

## Implementation tasks

- [x] Add `ai skill install --update-all --scope <project|user>`, retaining the current scope and project-path rules.
- [x] Extend the shared command model and service to select only installed catalog skills and replace their contents using the existing replacement behavior.
- [x] Add the operation to the AI wizard reachable from the general wizard, with summary, a local-change warning, and one global confirmation.
- [x] Reject combinations with `--all`, an individual name, or `--overwrite` before writing; the update mode itself implies replacement.
- [x] Update documentation and, after the user installs the version and explicitly authorizes testing, verify CLI and wizard behavior using the agreed scenarios.

## Interfaces and agreed decisions

- CLI: `geek-cli ai skill install --update-all --scope <project|user> [--project-path <path>]`.
- Scope remains required. Project scope requires an existing project directory; user scope does not accept a project path.
- Preserve destinations: `<projectPath>/.agents/skills/<name>` for projects and the user's home `.agents/skills/<name>` for user scope.
- A catalog skill is installed when its destination directory exists in the selected scope. Update only names present in both the bundled catalog and that destination.
- Replace the entire selected skill folder with its bundled contents, including removal of obsolete files and replacement of local edits. No local-change detection or merging is required.
- Use the catalog included in the running Geek CLI version; do not query remote sources or compare remote versions.
- CLI and wizard use the same model and service. Common selection, validation, execution, and reporting remain in the service library; prompts remain in the wizard.
- CLI runs without prompts. The wizard presents a summary and replacement warning, requests one global confirmation, and preserves existing Back navigation behavior; cancellation changes no files.
- Invalid requests fail before changes. Per-skill execution failures do not prevent remaining candidates from being processed; retain successful operations and report a nonzero result if any candidate failed.
- No candidates is a successful no-op. Empty-catalog behavior follows the existing installation behavior and must not create destination directories.
- By explicit user decision, do not expose this operation through MCP, overriding the repository's general three-path requirement.

## Validation and responsibilities

The user generates and installs the version to be tested. Do not execute tests until the user explicitly indicates they may begin. Use that installed version, a disposable project, and an isolated user destination. Build and package generation are handled by the user as part of preparing the version.

- Compare CLI and wizard results for both scopes with installed catalog skills containing local changes and obsolete files; confirm full replacement and complete resource copying.
- Confirm missing catalog skills are not installed and external skills are preserved.
- Check invalid or missing scope, invalid project paths, a project path supplied for user scope, and conflicting mode/name/overwrite inputs without destination changes.
- Check wizard cancellation and its global replacement warning, no installed catalog skills, an empty catalog, repeated execution, and partial failures followed by successful processing of remaining candidates.
- Confirm the existing individual installation remains usable and MCP does not expose the new operation.
- Keep acceptance criteria and validation work unchecked until verified. Report any scenario that cannot be exercised and why; do not mark the story Completed while required checks remain pending.

## Out of scope

- Installing missing skills, preserving or merging local changes, updating external sources, modifying subagents, rolling back the whole batch after a failure, or exposing skill update through MCP.
