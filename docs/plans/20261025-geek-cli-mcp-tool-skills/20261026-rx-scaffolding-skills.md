# React and React Native scaffolding skills

Related backlog: [Geek CLI MCP tool skills](../../backlog/20261025-geek-cli-mcp-tool-skills.md)

Story: 14
Status: Completed
Approval: Approved on 2026-10-26.

## Story

As a Geek CLI user, I want one skill per React context, React Native module, component, and screen tool, so that I can generate each resource according to my needs.

## Acceptance criteria

- [x] Given the corresponding skill is installed and its MCP tool is available, when I request a React context or React Native module, component, or screen, then the assistant explains prerequisites and the scaffold's scope, clarifies the destination, asks for missing or ambiguous inputs, and applies supported defaults.
- [x] Given valid inputs, when I request scaffolding, then I receive an outcome supported by the tool response, identifying the requested resource and confirmed created or skipped artifacts, including requested optional screen artifacts, without presenting placeholder files as working application behavior.
- [x] Given an unavailable tool, unsuitable destination, or failed execution, when I request scaffolding, then the assistant explains the impediment and any confirmed partial results without claiming full success or executing alternative generation commands directly.

## Plan

- [x] Add four independent English-language skills to the existing installable catalog, one for each resource tool.
- [x] Describe usage conditions, prerequisites, required inputs, defaults, destination behavior, and each tool's actual scaffold output.
- [x] Make MCP the exclusive generation path and explain confirmed outcomes, skipped files, partial failures, and unavailable tools.
- [x] Review all four skills against the current tools and shared services, document their availability, and update tracking statuses.

## Current implementation context

- Proposed skill names: `geek-rx-context`, `geek-rx-native-module`, `geek-rx-native-component`, and `geek-rx-native-screen`.
- MCP tools: `rx_context`, `rx_native_module`, `rx_native_component`, and `rx_native_screen`. Each requires `name` and accepts `flat=false`. Screens additionally accept `schema=false` and `wrapper=false`.
- The assistant must invoke the corresponding MCP tool exclusively for scaffolding. If unavailable, report the impediment without replacing it with shell commands or manual file generation.
- These tools write files and directories directly through shared services; they do not invoke React, React Native, Node.js, .NET template generation, or another external generator. Do not invent template or external CLI prerequisites for these operations. A writable destination and appropriate existing project context are needed to use the scaffold.
- The scaffolding service captures the process working directory as its base path when instantiated. Every operation computes the destination as `Path.Combine(basePath, name)`. No tool exposes a project, folder, or working-directory parameter.
- `flat=true` only skips explicit creation of the destination directory; it does not change the computed destination to the base path. Context, component, and screen file creation can fail if that destination does not exist. Module generation creates its subfolders and their parents even with `flat=true`. Explain this actual behavior rather than promising conventional flat output; fixes are outside this story.
- Filenames use invariant-culture title casing of the lowercased supplied name. Do not promise PascalCase or kebab-case normalization.
- Context generation creates empty actions, context, hook, provider, reducer, and state files with the corresponding `.ts` or `.tsx` suffixes.
- Native component generation creates empty `.component.tsx` and `.style.tsx` files. Native screen generation creates empty `.screen.tsx` and `.style.tsx` files; optional schema and wrapper files contain placeholder comments.
- Native module generation creates `Screens`, `Core`, and `Modals` directories with `.keep` files. This is an application folder scaffold, not a platform bridge implementation.
- Existing context, component, and screen files are skipped. Module `.keep` files are written as empty files even when already present. Do not claim that every existing file is left untouched.
- Responses contain `ExitCode` and captured `Output`. Handle invocation exceptions, distinguish generated and skipped artifacts, and confirm paths through output or file inspection. Failures can leave partial artifacts; do not assume rollback or full success from a generic success message.

## Validation

Review each skill against its current MCP contract and shared implementation for prerequisites, parameters, defaults, destination and flat behavior, invocation, actual artifacts, skipped files, results, and error handling. As agreed in the backlog, no TDD or installation testing is required. No CLAUDE.md was found in the repository file listing; repository AGENTS.md instructions and existing skill refinements provide the local context.

## Out of scope

- Implementing React state behavior, UI, navigation, schemas, wrappers, or platform bridge code beyond existing placeholders.
- Changes to MCP tools, shared services, CLI commands, wizards, or installers, including corrections to flat behavior or naming.
- Skills for other tools.
