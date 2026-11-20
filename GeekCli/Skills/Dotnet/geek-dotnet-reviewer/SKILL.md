---
name: geek-dotnet-reviewer
description: Review a .NET solution, project, or C# file for .editorconfig compliance and additional C# conventions for namespaces, ternaries, using aliases, and constructor formatting. Use when the user requests a .NET code style review.
---

# Review .NET code style

Review an existing `.sln`, `.slnx`, `.csproj`, or `.cs` target without changing files. Resolve the solution and owning project for a project or file target. If more than one solution or project could own the target, explain the ambiguity and ask which one to use.

## Solution prerequisites

Check that the solution root contains `.editorconfig` and `Directory.Build.props`. Read the props file and verify these effective values:

```xml
<Project>
    <PropertyGroup>
        <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
        <AnalysisLevel>latest</AnalysisLevel>
    </PropertyGroup>
</Project>
```

Report missing files or values as configuration findings. Do not add or modify them during the review.

## .NET formatting

When the prerequisites pass, run from the solution root:

```powershell
dotnet format "<solution-or-project-path>" --verify-no-changes --severity warn --verbosity detailed
```

For a single C# file, pass its owning project as the target and append `--include "<path-relative-to-project>"`. Record the exit code and relevant diagnostic IDs and locations. If prerequisites fail, state that formatting was skipped.

## Additional rules

Inspect the requested C# scope and report the following rules separately from `dotnet format` diagnostics. Use these stable codes for future reference; add new codes as new rules are introduced.

- `GDR001` — The namespace containing a type must begin with the project's effective `RootNamespace` followed, in order, by the file's directory segments relative to the project. Compare complete namespace segments, not a text prefix. Additional segments after the physical folder path are valid logical subdivisions: a file in `Models/Request` may use `<RootNamespace>.Models.Request.Post`. Account for nested namespace declarations. Respect explicit project conventions, including a different root namespace or organizational folders excluded from namespaces. If the convention is unclear, identify the file and ask before labeling it a violation.
- `GDR002` — A conditional expression (`?:`) contains another conditional expression in its condition or either result branch. Suggest an `if`/`else` or extracting the inner decision. Do not flag a single simple ternary.
- `GDR003` — When imported namespaces expose types with the same name and a file uses one of those types, resolve the ambiguity with a type alias such as `using CustomerRequest = Company.Customers.Request;` and use that alias at the references. Report the conflicting types and the affected references. Do not flag imports merely because they contain same-named types that the file never uses.
- `GDR004` — For a constructor with many parameters, put the first parameter on the same line as the constructor name. Put each remaining parameter on its own line, aligned with the first parameter immediately after `(`; do not use a fixed indent. Put `)` immediately after the last parameter and `{` on the following line. Apply this when the constructor has four or more parameters or its signature is already wrapped; leave a short single-line signature alone. For example:

  ```csharp
  public Test(string name,
              IService service,
              ILogger logger,
              TimeSpan timeout)
  {
  }
  ```

Read any project convention files already present, including `.geek-dotnet-reviewer.json` if the solution uses it. Do not require or create an extra configuration file.

## Review report

Start with a Markdown summary table in the user's language. Include one row for solution configuration, one for `dotnet format`, and one for each `GDR` rule. Use columns for check, status, and finding count. Use `Passed`, `Findings`, `Skipped`, or `Error` (translated when appropriate); use `—` for a count that cannot be determined. Never mark a skipped or failed check as passed.

After the table, report each finding as `code path:line:column — explanation and suggested correction`. Group configuration findings, relevant `dotnet format` diagnostics and exit code, and `GDR` findings. Briefly explain skipped or failed checks. Keep the detailed `dotnet format` log out of the report unless it is needed to explain a failure.
