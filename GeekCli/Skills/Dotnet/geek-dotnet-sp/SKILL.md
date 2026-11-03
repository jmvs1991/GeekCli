---
name: geek-dotnet-sp
description: Use the Geek CLI dotnet_sp MCP tool to generate a .NET stored procedure class template.
---

# Generate a .NET stored procedure class

Use this skill when the user asks to generate a Geek .NET stored procedure class from an installed template.

## Prerequisites and inputs

- The `dotnet_sp` MCP tool and .NET SDK must be available. The Geek stored procedure template for the selected scope must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated type or template name) and `projectName` (the target project name). Infer them from the request and workspace when clear; ask if either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, and `corp-co`. Do not offer `corp-co-code`; stored procedure generation does not support it.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## API Services conventions

- Place stored procedure classes under `<Schema>Services/<Entity>/Models/Sp`, using the entity name without the `Entity` suffix for its folder.
- Each entity has at least `SpInsert`, `SpUpdate`, and `SpDelete`; additional stored procedure classes may be needed for its operations. Views do not have stored procedure classes.
- For a `corp-co-code` entity, select `corp-co`, the strongest supported SP scope. Do not pass `corp-co-code`; it is unsupported by this generator.
- Follow the target project's examples for `init` properties, protected parameter helper methods, and readable `MapToSqlParameters()` declarations with one parameter per line. Do not add shared inheritance solely to remove repeated properties across SP classes.
- Start from the entity's properties and infer operation-specific parameter inclusion from the existing solution and supplied Insert, Update, and Delete reference examples. Ask when the target conventions or inclusion rules remain ambiguous.
- Treat reference examples as guides for adapting and validating generated output. The MCP still invokes the installed `dotnet new` template.

## Invoke the tool

After confirming the required inputs, scope, and target workspace, call `dotnet_sp` with `name`, `projectName`, and `scope`. Use MCP as the only generation path; do not run `dotnet new`, the CLI, or another command as a fallback.

The tool selects `geek-sp`, `geek-sp-corp`, or `geek-sp-corp-co` for `basic`, `corp`, or `corp-co`, respectively, and passes the requested name and project name to the template. It generates template files; it does not create or execute a stored procedure in a database. Do not promise specific generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report the requested scope and report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or workspace inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful available details without claiming success.
- If `dotnet_sp` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
