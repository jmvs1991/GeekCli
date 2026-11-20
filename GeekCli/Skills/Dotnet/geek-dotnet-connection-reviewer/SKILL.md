---
name: geek-dotnet-connection-reviewer
description: Review a .NET solution or Connection project for required EF Core packages, schema contexts, entity and view layout, and repository structure. Use when the user requests a Connection-layer review.
---

# Review a .NET Connection project

Review the target without changing its files. Accept a solution (`.sln` or `.slnx`), a solution directory, or a Connection `.csproj` or directory. For a solution target, find projects in the solution whose project name or containing directory is `Connection` (case-insensitive). If none exists, report `GCR001` and stop. If several match, report `GCR002`, identify them, and ask which project to review. For a project target, review that project directly.

Inspect the project's effective package references, including imported props and central package management when present. Require `devVzla.NetCore.EFExtension` and `Microsoft.EntityFrameworkCore.Design`; do not require particular versions. For the Design reference, verify `PrivateAssets=all` and `IncludeAssets` contains `runtime`, `build`, `native`, `contentfiles`, `analyzers`, and `buildtransitive`. Report missing packages or metadata separately. Do not mistake a transitive dependency for a project package reference.

Treat each first-level project directory representing a database schema as a schema folder. Exclude build artifacts and infrastructure folders such as `bin`, `obj`, and `Migrations`; use the project's existing conventions to distinguish other non-schema folders. If no schema folder exists, report it. For each schema folder `<Schema>`:

- Require `<Schema>Context.cs` containing a `<Schema>Context` class that derives from `DbContext` (directly or through an established project base context). Its `OnModelCreating(ModelBuilder modelBuilder)` override must call `modelBuilder.HasDefaultSchema("<Schema>")`. Check the actual schema value against the folder name.
- Require a registration for every entity in that schema through `modelBuilder.Entity(<EntityType>.Builder)` in `OnModelCreating`. Allow equivalent formatting, but report absent or mismatched registrations. Do not require views to be registered as entities unless the project establishes that convention. Report any `DbSet` declaration in the context.
- Require `Entities` and `Repositories` directories. `Views` is optional.
- Require each C# entity file in `Entities` to be named `*Entity.cs` and each C# view file in `Views` to be named `*View.cs`. Apply the rule to nested files as well, excluding generated files and build artifacts.
- For each `<Name>Entity` in `Entities`, require `Repositories/<Name>/Read` and `Repositories/<Name>/Write`, each containing a matching `*ReadRepository.cs` or `*WriteRepository.cs` file, respectively.
- For each `<Name>View` in `Views`, require `Repositories/<Name>/Read` containing a matching `*ReadRepository.cs` file. A view does not require a Write directory or write repository; report one if present because views are read-only.
- Check C# files inside repository `Read` and `Write` folders for the `*ReadRepository.cs` or `*WriteRepository.cs` naming pattern, respectively. Report misplaced files or repository folders with no matching entity or view. If entity and view names collide, explain the ambiguity instead of assuming one repository serves both.

Use C# declarations and project configuration to resolve ambiguous file names, imports, and inherited behavior; do not infer compliance from a string search alone. If a check cannot be established from available files, mark it as unverified and explain why. Do not run generators or edit code during the review.

## Finding codes

Use these stable codes for the corresponding findings. Add new codes without changing existing meanings.

| Code | Finding |
| --- | --- |
| `GCR001` | No Connection project exists in the supplied solution. |
| `GCR002` | More than one Connection project matches the supplied solution. |
| `GCR003` | The `devVzla.NetCore.EFExtension` package reference is missing. |
| `GCR004` | The `Microsoft.EntityFrameworkCore.Design` package reference is missing. |
| `GCR005` | The Design package reference lacks `PrivateAssets=all` or one of the required `IncludeAssets` values; name each missing value. |
| `GCR006` | No schema folder was found in Connection. |
| `GCR007` | A schema's `<Schema>Context.cs` file or `<Schema>Context` class is missing. |
| `GCR008` | A schema context does not derive from `DbContext`. |
| `GCR009` | A schema context does not override `OnModelCreating(ModelBuilder)`. |
| `GCR010` | `HasDefaultSchema` is missing or names a schema different from the schema folder. |
| `GCR011` | A schema entity lacks its `modelBuilder.Entity(<EntityType>.Builder)` registration, or the registration refers to the wrong entity or builder. |
| `GCR012` | A schema context declares a `DbSet`. |
| `GCR013` | A schema's `Entities` directory is missing. |
| `GCR014` | A schema's `Repositories` directory is missing. |
| `GCR015` | A C# entity file does not use the `*Entity.cs` suffix. |
| `GCR016` | A C# view file does not use the `*View.cs` suffix. |
| `GCR017` | An entity's `Repositories/<Name>` directory is missing. |
| `GCR018` | An entity repository's `Read` directory is missing. |
| `GCR019` | An entity repository's `Write` directory is missing. |
| `GCR020` | An entity repository's `Read` directory has no matching `*ReadRepository.cs` file. |
| `GCR021` | An entity repository's `Write` directory has no matching `*WriteRepository.cs` file. |
| `GCR022` | A view's `Repositories/<Name>` directory is missing. |
| `GCR023` | A view repository's `Read` directory is missing. |
| `GCR024` | A view repository's `Read` directory has no matching `*ReadRepository.cs` file. |
| `GCR025` | A view repository contains a `Write` directory or write repository. |
| `GCR026` | A C# file in a `Read` or `Write` directory has the wrong repository filename suffix or is misplaced. |
| `GCR027` | A repository folder has no matching entity or view. |
| `GCR028` | An entity and view share the same repository folder name, so the ownership cannot be determined. |

Report each finding at the most specific location available. Do not multiply findings caused only by a missing parent directory or file: for example, report `GCR014` without also reporting every missing repository beneath it. Mark dependent checks as unverified or skipped until the parent exists. Treat `GCR001` and `GCR002` as discovery errors; do not claim that downstream checks passed.

## Review report

Write the report in the user's language. Start with a Markdown table with one row for project discovery and one for each applicable `GCR` code or rule group. Include check, status, and finding count. Use `Passed`, `Findings`, `Unverified`, or `Error` (translated when appropriate), and `—` when a count cannot be determined. Do not mark an incomplete check as passed.

After the table, list each finding as `GCRnnn path:line:column — explanation and suggested correction`, omitting unavailable line or column numbers. Group findings by project discovery, packages, schema contexts, and repositories. Briefly explain checks that could not be completed.

End with a **Final summary** section: name the reviewed project; give counts of reviewed schemas, entities, views, and repository folders; total the findings and list their codes with occurrence counts; state the overall result as passed, findings, unverified, or error. If discovery stopped the review, state which checks were not run. Do not claim an overall pass when any check has findings, errors, or remains unverified.
