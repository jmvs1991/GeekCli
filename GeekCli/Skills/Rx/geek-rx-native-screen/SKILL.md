---
name: geek-rx-native-screen
description: Use the Geek CLI rx_native_screen MCP tool to scaffold a React Native screen file set.
---

# Scaffold a React Native screen file set

Use this skill when the user asks to scaffold files for a React Native screen.

## Prerequisites and inputs

- The `rx_native_screen` MCP tool must be available. This scaffold does not invoke React Native, Node.js, .NET templates, or another external generator.
- The tool requires `name` and accepts `flat`, `schema`, and `wrapper`; all default to `false`. Infer the screen name and intended project from the request and workspace when clear; ask if the name or destination is missing or ambiguous. Ask whether schema or wrapper placeholder files are wanted if the request does not make that clear.
- The tool has no project, folder, or working-directory parameter. Its service uses the MCP process working directory captured when the service is created and combines it with the supplied `name`. Confirm that this is the intended writable project location.
- With `flat=false`, the service creates the destination directory. With `flat=true`, it skips that explicit directory creation but still targets the same child path; file creation can fail if the destination does not already exist.

## Invoke the tool

After confirming the name, destination, and optional-file choices, invoke `rx_native_screen` through MCP with the corresponding `flat`, `schema`, and `wrapper` values. MCP is the only generation path; do not run shell commands or create scaffold files manually as a fallback.

The service lowercases the supplied name and applies invariant-culture title casing for filenames. Do not promise PascalCase or kebab-case normalization. It creates empty `<TitleCasedName>.screen.tsx` and `<TitleCasedName>.style.tsx` files. When `schema=true`, it also creates `<TitleCasedName>.schema.tsx` with a placeholder comment; when `wrapper=true`, it creates `<TitleCasedName>.wrapper.tsx` with a placeholder comment. Existing files are skipped. These files are scaffolding placeholders, not working screen, style, schema, or wrapper behavior.

## Report the result

- The tool result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero, and identify generated or skipped files only when confirmed by the output or workspace inspection. Include optional files only when requested and confirmed.
- A failure can leave some files behind. Report confirmed partial results and available error details; do not imply rollback or full success.
- If `rx_native_screen` is unavailable, explain that scaffolding cannot proceed. Do not fall back to CLI commands or manual file generation.
