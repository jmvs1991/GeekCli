---
name: geek-dotnet-read
description: Use the Geek CLI dotnet_read MCP tool to generate a .NET read repository for an entity or view.
---

# Generate a .NET read repository

Use this skill when the user asks to generate a Geek .NET read repository from an installed template.

## Connection conventions

When generating a repository for Connection in an existing API solution, inspect the target schema and follow its established naming and namespace conventions. Place entity repositories under `Repositories/<Entity>/Read` and view repositories under `Repositories/<View>/Read`, with interface and implementation in their respective folders. Views are read-only. Verify generated file paths, filenames, namespaces, and interface-to-implementation mapping against the target project. Register each generated repository in the consuming API project's `ExtensionMethods/IServiceCollectionExtension.cs`, inside `AddRepositories`, following its existing service lifetime. If the registration location, mapping, or lifetime is missing or ambiguous, ask before adding registrations. Report any correction and ask the user to review Connection before advancing to another section.

### Complete `FindAsync` for corporation, company, and code

When the target entity is scoped by corporation and company and its agreed lookup key also includes a code, complete the generated read repository's `FindAsync` to filter by all three values. Use the reference template in `templates/find-by-corporation-company-code.cs.txt`, adapting entity, context, repository, and code-property names to the inspected solution.

Do not add this overload or assume a code property when the target entity's lookup key does not establish it. Inspect the entity and repository interface; ask the user if the lookup fields or naming are ambiguous. Verify the repository applies both scope filters and the code predicate before `FirstOrDefaultAsync`.

## Prerequisites and inputs

- The `dotnet_read` MCP tool and .NET SDK must be available. The selected Geek read repository template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated repository or template name), `dbSchema` (the database schema), and `contextName` (the database context name). Infer values from the request and workspace when clear; ask when any required value is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, `corp-co`, and `corp-co-code`.
- Optional `view` defaults to `false`. Use `false` for an entity repository and `true` for a view repository. Clarify the requested variant when it is unclear.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## Invoke the tool

After confirming the required inputs, scope, variant, and target workspace, call `dotnet_read` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects `geek-read-paginationful-filterless-entity` when `view=false` or `geek-read-paginationful-filterless-view` when `view=true`, then appends the selected scope suffix (`-corp`, `-corp-co`, or `-corp-co-code`; `basic` adds no suffix). It passes `name`, `dbSchema`, and `contextName` to the template. Do not promise exact generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or file inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful details from the result without claiming success.
- If `dotnet_read` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
