---
name: geek-ngx-service
description: Use the Geek CLI ngx_service MCP tool to generate an Angular service in an Angular project or Nx workspace.
---

# Generate an Angular service

Use this skill when the user asks to generate an Angular service.

## Prerequisites and inputs

- The `ngx_service` MCP tool must be available. Generation runs through the tool using Windows `cmd`, so Windows command availability, Node.js, and the relevant Angular CLI or Nx workspace tooling must be available in the MCP process environment.
- The tool requires `name`. Optional `nx` defaults to `false`; `project` and `folder` are nullable and used only in Nx mode. Infer values from the request and workspace when clear, and ask when the target workspace, mode, or required inputs are missing or ambiguous.
- Confirm that the MCP process working directory is the intended writable workspace. Non-Nx mode uses that directory as the Angular project directory. Nx mode requires `nx.json` in that directory.
- In Nx mode, `project` and a project-relative `folder` are required. The service rejects blank values and folder paths containing a `..` segment. It does not verify that the project exists, check project tags, or confirm folder ownership.
- The tool has no module, working-directory, output-directory, or provider-scope parameter. Do not ask for or promise these options.

## Invoke the tool

After confirming the workspace, mode, service name, and Nx inputs when applicable, invoke `ngx_service` through MCP. MCP is the only generation path; do not run `ng`, `npx nx`, or Geek CLI generation commands directly, and do not create service files manually as a fallback.

- With `nx=false`, the service runs `ng g s <name>` in the MCP process working directory. `project` and `folder` are unused.
- With `nx=true`, the service runs `npx nx generate @nx/angular:service "<folder>/<name>" --project="<project>" --type=service`. Folder separators are normalized and leading or trailing slashes are trimmed before the name is appended. The service does not explicitly convert names or folder segments to PascalCase or kebab-case.

## Report the result

- The tool result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero. Identify the requested service and target location, and list generated files only when confirmed by the output or workspace inspection. Do not promise exact filenames, provider registration, or tests.
- For a nonzero exit code or invocation exception, explain that generation failed and share actionable details from the result. Do not claim success.
- If `ngx_service` is unavailable or a prerequisite is missing, explain what prevents generation and what the user can do next. Do not fall back to direct CLI execution or manual file generation.
