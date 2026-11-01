---
name: geek-rx-native-component
description: Use the Geek CLI rx_native_component MCP tool to scaffold a React Native component file set.
---

# Scaffold a React Native component file set

Use this skill when the user asks to scaffold files for a React Native component.

## Prerequisites and inputs

- The `rx_native_component` MCP tool must be available. This scaffold does not invoke React Native, Node.js, .NET templates, or another external generator.
- The tool requires `name` and accepts `flat`, which defaults to `false`. Infer the component name and intended project from the request and workspace when clear; ask if the name or destination is missing or ambiguous.
- The tool has no project, folder, or working-directory parameter. Its service uses the MCP process working directory captured when the service is created and combines it with the supplied `name`. Confirm that this is the intended writable project location.
- With `flat=false`, the service creates the destination directory. With `flat=true`, it skips that explicit directory creation but still targets the same child path; file creation can fail if the destination does not already exist.

## Invoke the tool

After confirming the name, destination, and `flat` choice, invoke `rx_native_component` through MCP. MCP is the only generation path; do not run shell commands or create scaffold files manually as a fallback.

The service lowercases the supplied name and applies invariant-culture title casing for filenames. Do not promise PascalCase or kebab-case normalization. The scaffold creates empty `<TitleCasedName>.component.tsx` and `<TitleCasedName>.style.tsx` files under the destination. Existing files are skipped. These empty files are placeholders, not a working component or styles.

## Report the result

- The tool result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero, and identify generated or skipped files only when confirmed by the output or workspace inspection.
- A failure can leave some files behind. Report confirmed partial results and available error details; do not imply rollback or full success.
- If `rx_native_component` is unavailable, explain that scaffolding cannot proceed. Do not fall back to CLI commands or manual file generation.
