---
name: geek-ngx-page
description: Use the Geek CLI ngx_page MCP tool to generate an Angular page in an Angular CLI project or an Nx workspace.
---

# Generate an Angular page

Use this skill when the user asks to generate an Angular page.

## Prerequisites and inputs

- The `ngx_page` MCP tool must be available. Use MCP as the only generation path; do not run Angular CLI, Nx, Geek CLI commands, or create the generated files manually.
- The tool runs from the MCP server's current working directory and has no working-directory input. Confirm the intended workspace and ensure the server is running from its root. The tool launches Windows `cmd`; the relevant Node.js and workspace tooling must be installed and available there.
- `name` is required. Ask for it if it is missing or ambiguous. The tool converts the name to kebab-case in Nx mode; Angular CLI mode passes it as supplied.
- `nx` defaults to `false`. Clarify whether to use Angular CLI mode or Nx mode if the workspace or request does not make that clear. Angular CLI mode requires `ng` to be available. It invokes `ng g c <name> --type page`; it does not explicitly select standalone component generation. The optional `project`, `folder`, and `module` inputs are not used in this mode.
- Nx mode requires an Nx workspace root containing `nx.json`, with `npx nx` available. It also requires `project`, `folder`, and `module`. The target is an Angular application under `apps/<project>/src/app`; inspect workspace configuration and ask if the application is unclear. `folder` can be relative to `src/app` or begin with `apps/<project>/src/app/`; its remaining path segments are converted to PascalCase. `module` is the workspace-relative path to the declaring NgModule file, including `.ts`, and is passed as supplied. Ask for any missing or ambiguous value.
- Nx mode generates a non-standalone page declared in the supplied NgModule, with selector `ngx-<kebab-case-name>-page`. The shared validation checks required inputs, folder traversal, application-folder targeting, and that execution is at a workspace root; it does not fully verify project tags, application ownership, or that the module exists.

## Invoke and report

- Invoke `ngx_page` through MCP with the confirmed inputs. Do not fall back to direct commands or manual generation if the tool is unavailable.
- The result contains `ExitCode` and captured `Output`. Report success only for exit code zero. Identify the page and target location, and list generated files or module updates only when confirmed by the output or workspace inspection.
- For invocation exceptions, validation errors, or a nonzero exit code, explain the impediment using available output. Do not claim generation succeeded; report any partial artifacts only if confirmed.
