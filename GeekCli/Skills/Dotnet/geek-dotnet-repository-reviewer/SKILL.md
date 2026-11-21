---
name: geek-dotnet-repository-reviewer
description: Review the contents of .NET Connection ReadRepository and WriteRepository classes, including inheritance, scope filters, pagination, and readability. Accept a solution, Connection project, folder, or repository file.
---

# Review .NET repositories

Review existing repositories without changing them. Accept a `.sln`/`.slnx` file or solution directory, a Connection `.csproj` or directory, a folder, or a specific C# file. For a solution, find its Connection project by project or containing directory name (case-insensitive). If none or several match, report the discovery error and stop or ask which project to use. Search the selected scope recursively for C# classes named `*ReadRepository` or `*WriteRepository`; for a specific file, review only that file if it declares either class. Exclude `bin`, `obj`, and generated build artifacts. Discover classes from C# declarations rather than filenames alone. Report `GRR003` if no repositories are found.

Review repository content, not directory structure, dependency-injection registration, package references, contexts, or generated output. Read related interfaces, entity or view types, base-class definitions, installed templates, or nearby repositories as needed to establish a check. Resolve C# symbols and expressions before reporting a violation; a text match alone does not prove correctness or an error. If the expected contract or base class cannot be established from available evidence, mark that check unverified and explain why.

## Checks

- Verify that each repository implements the matching `I*ReadRepository` or `I*WriteRepository` interface and that the implementation satisfies its visible method contract. Separately, inspect the matching interface's inherited interface and generic model type: they must match the model's unscoped, corporation, or corporation-and-company scope and the repository's read/write role. For example, a paginationful filterless Entity read interface inherits the corresponding `IReadEntityRepositoryAsync<T>`, `IReadEntityRepositoryWithCorporationAsync<T>`, or `IReadEntityRepositoryWithCorporationAndCompanyAsync<T>`; the specialized corporation-company-code interface applies when its declared lookup contract fits the entity's key. Verify that the repository's inherited class matches its role, Entity or View type, and scope. Resolve actual interface/base definitions and template variants rather than treating similarly named types in other namespaces as interchangeable. Views are read-only.
- For an Entity whose ordered `[PrimaryKey]` begins with `Corporation, Company` and has at least two more fields, verify incremental read searches for every additional key prefix. The inherited repository covers corporation and corporation/company searches; do not require redeclarations. For each prefix after company but before the full key, require a declared paginated lookup in the repository interface and its implementation, with parameters for that prefix and `pageNumber`/`pageSize`, filtering on all prefix fields and returning the filtered total and page. Require a final lookup by every primary-key field returning one nullable Entity (or the established equivalent single-result contract). For `Corporation, Company, Codfield1, Codfield2`, this means a paginated `FindAsync(corporation, company, codfield1, pageNumber, pageSize)` and an exact `FindAsync(corporation, company, codfield1, codfield2)`; for longer keys, add one paginated lookup per intermediate prefix. Follow `[PrimaryKey]` order, not property declaration order. Do not infer missing key fields or demand this pattern when the key mapping is unavailable or does not start with corporation and company.
- For read methods scoped by corporation alone, verify that the query applies `FilterByCorporation(corporation)`. For methods scoped by corporation and company, verify `FilterByCorporationAndCompany(corporation, company)`. Establish the applicable scope from method parameters, contract, and model; do not demand scope filters on unscoped methods. Follow the query to ensure the filter affects the returned result.
- For a paginated read method, construct one filtered `IQueryable`, compute `LongCountAsync()` on it before pagination, then order that same query by a stable key and apply `Pagination(pageNumber, pageSize)` before materializing the page. The returned total and values must represent the same filters. Do not flag an equivalent implementation solely because of variable names or formatting.
- In write methods, check contradictions that are demonstrable from the visible contract, types, and operation. Do not impose an unestablished stored-procedure, transaction, or method pattern.
- Review legibility with concrete evidence. For a multi-line LINQ chain, align continuation calls and predicate lines consistently with the surrounding style. In a paginated method that declares the query, counts it, materializes the page, and returns a result, require a blank line between each of those four stages. Report `GRR009` for each method missing one or more of these separations, even when its query and pagination are functionally correct. Show the suggested code with those blank lines. Do not require one fixed indentation column or flag unrelated stylistic differences.

## Error codes

Use these stable codes; do not change their meanings.

| Code | Error |
| --- | --- |
| `GRR001` | No Connection project was found in the supplied solution. |
| `GRR002` | Multiple Connection projects match the supplied solution. |
| `GRR003` | No ReadRepository or WriteRepository class was found in the selected scope. |
| `GRR004` | A repository does not implement its corresponding interface or contradicts its visible interface contract. |
| `GRR005` | A repository inherits an incorrect base class for its role, model, or scope. |
| `GRR006` | An applicable corporation/company filter is missing or incorrect. |
| `GRR007` | A paginated read method violates the shared filtered-query, count-before-pagination, stable-order, or pagination pattern. |
| `GRR008` | A write method contradicts its visible contract, types, or operation. |
| `GRR009` | Repository code has a specific readability problem, including missing blank lines between the query, count, page materialization, and return stages. |
| `GRR010` | The repository interface inherits an interface for the wrong role, model, scope, or lookup contract. |
| `GRR011` | A required incremental primary-key lookup is missing from the interface or implementation, or does not filter and return results for the required key prefix. |

Do not duplicate findings for one underlying defect. Give the most specific location available. For discovery errors, do not claim downstream checks passed.

## Review report

Write the report in the user's language. Begin with a Markdown table showing each applicable check, its status, and error count. Use `Passed`, `Errors`, `Unverified`, or `Skipped` (translated as appropriate), and `—` when a count cannot be determined. Never count an unverified check as passed.

List each error as `GRRnnn path:line:column — evidence and suggested correction`, omitting unavailable line or column numbers. Group related findings by repository. End with an **Error summary** giving the selected scope, counts of reviewed ReadRepository and WriteRepository classes, total errors, occurrences by code, and overall result. Explain unverified checks separately. Do not claim an overall pass when any applicable check has errors or remains unverified.
