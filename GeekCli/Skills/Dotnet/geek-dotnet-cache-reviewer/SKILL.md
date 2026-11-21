---
name: geek-dotnet-cache-reviewer
description: Review .NET Cache projects or cache classes against Connection schemas, entities, scope-specific inheritance, and cacheable list searches. Accept a solution, a *Cache project, or a *Cache class.
---

# Review .NET caches

Review existing files without changing them. Accept a `.sln`/`.slnx` file or solution directory, a `*Cache` `.csproj` or project directory, or a C# file declaring a `*Cache` class. Exclude `bin`, `obj`, and generated artifacts. Identify projects from solution membership and classes from declarations, not filenames alone. If a supplied file declares several cache classes, review each and identify them separately.

## Discover the comparison scope

- For a solution, locate its Connection project by project or containing directory name (case-insensitive). If none or several match, report the discovery issue and stop or ask which one to use. Treat each first-level Connection schema directory as a schema, excluding infrastructure such as `bin`, `obj`, and `Migrations` according to the project's conventions. Find `*Cache` projects in the solution. Require exactly one `<Schema>Cache` project for each Connection schema. Report missing, duplicate, or unmatched Cache projects; do not silently treat a similarly named project as a match.
- For a `*Cache` project, identify its schema from its name and locate the corresponding Connection schema through its project references or the containing solution. If the mapping cannot be established, report it as unverified. Review only that Cache project and its mapped schema; do not claim that other schemas have been checked.
- For a `*Cache` class, review that class and its matching interface, entity, and read repository contract when available. Use nearby project and Connection files to establish those relationships. Do not apply solution-wide project or folder checks to a class-only request.

## Project and entity checks

For each selected Cache project, inspect its effective package references, including imported props and central package management. Require a direct `devVzla.NetCore.MemoryCache` package reference; a transitive dependency does not satisfy this check. Do not require a particular version.

For each Entity declared under the mapped Connection schema's `Entities` directory, require one corresponding entity folder within the Cache project, following the entity name without the `Entity` suffix. Determine the folder location from applicable `AGENTS.md` instructions and the project's established layout before judging it: for example, `Sales/Entities/OrderEntity.cs` may map to `SalesCache/Sales/Order` when that project groups caches under `Sales`. If no layout convention is documented, accept any unambiguous folder within the Cache project that contains the matching Cache class and interface; do not impose a first-level layout. Report `GCRV006` only when no corresponding folder exists, not merely because the folder is nested. Report orphan entity folders and duplicate mappings. Do not count Views as Entities. If the Connection schema or its Entities directory cannot be inspected, mark coverage unverified instead of treating an empty discovery result as a pass.

## Cache contract and search coverage

Determine each Entity's scope from its declaration and mapping: unscoped, corporation, or corporation and company. Check that the cache interface and class use the matching scope-specific inherited interface and base class, with the correct Entity or DTO type arguments. Resolve the actual inherited contracts from source, installed templates, or referenced packages when available; do not assume that similarly named types are equivalent. The generator skill `geek-dotnet-cache` supports `basic`, `corp`, and `corp-co`; do not invent a `corp-co-code` cache variant.

Compare the Entity's read repository interface and implementation with the cache interface and class, including methods inherited by both. Every search that returns a collection of Entities, directly or inside an established pagination/result wrapper, must be available through the cache contract and have cache behavior. Require a cache-specific method only when the matching inherited cache contract does not already cover that search. Verify that its implementation delegates to the correct search, preserves filters, pagination and result shape, and uses the project's cache mechanism for the returned collection. A search returning one Entity does not require caching or a cache-specific method. Do not flag a single-result method merely because it is exposed or cached. If a return type, inherited method, or cache behavior cannot be resolved, mark the check unverified and explain what evidence is missing. Do not infer coverage or a violation from a method name or text match alone.

## Cache method readability

Review the flow of every cache method, including inherited-method overrides and local helpers. Identify the stages that actually occur: input preparation, cache-key construction, cache lookup, hit/miss handling, source query, result mapping, cache write, and return. Separate adjacent stages with blank lines so the flow is easy to scan. This applies whether a method reads, writes, or refreshes a cache and regardless of the API names, model types, or return syntax. Keep tightly related operations within one stage together; do not require blank lines after every statement.

Make a constructed cache key a named local before passing it to a cache read or write operation, including a local helper that delegates to one. Follow the call when needed: `Set(BuildKey(...), totalRecords, dtos)` has the same readability issue as passing `BuildKey(...)` directly to `SetSubCacheKey(...)`. Suggest `string key = BuildKey(...);`, a blank line, then `Set(key, totalRecords, dtos)`. Apply the same rule to lookup helpers and other key builders or nontrivial key expressions. Reuse a key already available in the method. A method that receives a completed key as a parameter does not need another local.

Report `GCRV012` once per method when distinct stages run together or key construction is buried in a cache operation. Cite the exact statements and show a suggested rewrite with the relevant blank lines and local key. Do not require stages absent from the method, impose a particular helper or result shape, or flag a short pass-through that merely forwards an existing key.

## Finding codes

Use these codes for confirmed findings. Give each finding the most specific path and line available; avoid duplicates caused by a missing parent project, schema, or folder.

| Code | Finding |
| --- | --- |
| `GCRV001` | No Connection project was found in the supplied solution. |
| `GCRV002` | Several Connection projects match the supplied solution. |
| `GCRV003` | A Connection schema lacks its corresponding `*Cache` project. |
| `GCRV004` | Duplicate or unmatched `*Cache` projects prevent a one-to-one schema mapping. |
| `GCRV005` | A selected Cache project lacks a direct `devVzla.NetCore.MemoryCache` reference. |
| `GCRV006` | An Entity lacks its corresponding folder in the selected Cache project. |
| `GCRV007` | An entity folder lacks its matching Cache class or interface, or has a duplicate mapping. |
| `GCRV008` | An entity folder has no matching Connection Entity. |
| `GCRV009` | A cache interface or class inherits the wrong scope-specific contract, base class, or model type. |
| `GCRV010` | A collection-returning search is not covered by inheritance or the cache interface and class. |
| `GCRV011` | A covered collection search has incorrect delegation, filters, pagination, result shape, or cache behavior. |
| `GCRV012` | A cache method's distinct processing stages lack visual separation, or key construction is buried inside a cache operation. |

## Review report

Write in the user's language. Start with a Markdown table of applicable checks, status, and finding count. Use `Passed`, `Findings`, `Unverified`, or `Skipped` (translated when appropriate), and `—` when the count cannot be determined. List findings as `GCRVnnn path:line:column — evidence and suggested correction`, omitting unavailable coordinates. End with a summary naming the reviewed scope, counts of schemas, Cache projects, Entities, entity folders, and cache classes actually reviewed, findings by code, and the overall result. State which checks could not be verified. Never report an overall pass while an applicable check has findings or is unverified.
