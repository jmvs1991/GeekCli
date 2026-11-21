---
name: geek-dotnet-sp-reviewer
description: Review stored procedure model classes in a .NET solution, a *Services project, or a specific C# file for scope, property setters, SQL parameters, and readable MapToSqlParameters declarations.
---

# Review .NET stored procedure models

Review existing files without changing them. Accept a `.sln`/`.slnx` file or solution directory, a `*Services` `.csproj` or project directory, or a C# file containing a stored procedure model class. This skill reviews SP model contents, not database procedures, CRUD completeness, service methods, or generated artifacts.

## Select classes

- For a solution, use its project membership to find every project whose name or containing directory ends in `Services` (case-insensitive). In each selected project, search all directories named `Sp` for C# files.
- For a `*Services` project, search its `Sp` directories in the same way. For a C# file, review only SP model classes declared in that file; do not apply project-wide discovery checks.
- Exclude `bin`, `obj`, and generated build artifacts. Identify classes from declarations and their SP contracts or `MapToSqlParameters` implementations, not filenames alone. Include operation-specific SP classes as well as `SpInsert`, `SpUpdate`, and `SpDelete`. Report the searched scope when no SP classes are found; do not claim an empty review passed.

## Checks

- Determine the scope from the class's declared base class, interface, and visible scope parameters. Check that these agree on `basic`, `corp`, or `corp-co`. Resolve inherited contracts from available source, referenced packages, or installed templates when needed. `corp-co-code` is not an SP scope; a code field may coexist with a `corp-co` SP. Do not require an associated Entity or infer its scope. If the class's scope cannot be established, mark the check unverified rather than guessing.
- Check each instance data property declared by the SP class for a `get; init;` or `get; private set;` accessor pair. Do not require a setter on computed properties or inspect inherited properties as though declared locally.
- Accept protected `SqlParameter` helper methods written either with a block body returning `new SqlParameter(...)` or with an expression body using `=>`. Resolve their parameter names and values when comparing the SQL call with the parameter array; neither syntax is a finding by itself.
- Review every implementation of `MapToSqlParameters`, including overloads or overrides that receive method arguments. Apply the same checks to methods with and without arguments. Do not impose formatting rules on the method signature or require every method argument to appear in the SQL call.
- In a `const string sp` declaration, put the procedure name (optionally schema-qualified) on the first line, then one SQL argument on each subsequent line, in call order. Do not combine two SQL arguments on one source line. Keep one `SqlParameter` array element per line, accepting either collection expressions (`[...]`) or array initializers (`{...}`).
- Compare the SQL argument names and order with the `SqlParameter[] parameters` elements returned by the method. Follow local and inherited helpers or direct `SqlParameter` constructions to establish the actual names. Report missing, additional, duplicate, or reordered parameters only when the correspondence is provable. Check that scope parameters required by the established SP scope are represented consistently in the call and array. If an expression or helper cannot be resolved, mark that comparison unverified and explain the missing evidence.

Use declarations and expression flow rather than text matches alone. Avoid duplicate findings for the same defect, and do not invent a missing parameter from an unavailable database definition.

## Finding codes

| Code | Finding |
| --- | --- |
| `GSPR001` | No SP model class was found in the selected scope. |
| `GSPR002` | A class's base, interface, or scope parameters conflict with its established SP scope. |
| `GSPR003` | A declared instance data property has a setter other than `init` or `private set`, or lacks the required accessor pair. |
| `GSPR004` | A procedure declaration does not put the name first and each SQL argument on its own following line. |
| `GSPR005` | A `SqlParameter[]` declaration has more than one element on a line. |
| `GSPR006` | The SQL call and parameter array have provably different names, counts, or order, including required scope parameters. |

## Review report

Write in the user's language. Start with a Markdown table of applicable checks, status, and finding count. Use `Passed`, `Findings`, `Unverified`, or `Skipped` (translated when appropriate), and `—` when a count cannot be determined. List each finding as `GSPRnnn path:line:column — evidence and suggested correction`, omitting unavailable coordinates. End with the reviewed scope, counts of `*Services` projects, `Sp` directories, and SP classes actually reviewed, findings by code, and the overall result. Explain unverified checks and never report an overall pass while an applicable check has findings or remains unverified.
