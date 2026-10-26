---
name: geek-rx-native-module
description: Use the Geek CLI rx_native_module MCP tool to scaffold a React Native module folder structure.
---

# Scaffold a React Native module folder structure

Use this skill when the user asks to create the folder scaffold for a React Native module.

## Prerequisites and inputs

- The `rx_native_module` MCP tool must be available. This scaffold does not invoke React Native, Node.js, .NET templates, or another external generator.
- The tool requires `name` and accepts `flat`, which defaults to `false`. Infer the module name and intended project from the request and workspace when clear; ask if the name or destination is missing or ambiguous.
- The tool has no project, folder, or working-directory parameter. Its service uses the MCP process working directory captured when the service is created and combines it with the supplied `name`. Confirm that this is the intended writable project location.
- With `flat=false`, the service creates the destination directory. With `flat=true`, it skips that explicit step, but still uses the same child destination. The module service creates its subfolders recursively, so they can create the destination and parent directories.

## Invoke the tool

After confirming the name, destination, and `flat` choice, invoke `rx_native_module` through MCP. MCP is the only generation path; do not run shell commands or create scaffold files manually as a fallback.

The scaffold creates `Screens`, `Core`, and `Modals` directories under the destination, each with an empty `.keep` file. This is a folder scaffold only; it does not implement a React Native module or platform bridge. When run again, existing directories are reused and their `.keep` files are written again, so do not claim existing `.keep` contents are preserved.

## Report the result

- The tool result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero, and identify created directories or `.keep` files only when confirmed by the output or workspace inspection.
- A failure can leave some directories or files behind. Report confirmed partial results and available error details; do not imply rollback or full success.
- If `rx_native_module` is unavailable, explain that scaffolding cannot proceed. Do not fall back to CLI commands or manual file generation.
