---
name: geek-dotnet-entity-view-reviewer
description: Review the contents of Connection Entity and View C# files. Accept a solution, Connection project, or Entities/Views folder as the search scope.
---

# Review Entity and View files

Review existing C# files without changing them. The solution, project, or folder input determines which files to search; validation applies only to the contents of the discovered Entity and View files. Do not review project configuration, directory layout, filenames, schema contexts, repositories, database metadata, generated output, or other files.

## Select files

Accept a `.sln`/`.slnx` file or solution directory, a Connection `.csproj` or directory, or an `Entities` or `Views` directory. For a solution, locate its Connection project; if none or several match, report the discovery problem and stop or ask which one to use. Search a Connection project recursively for C# files declaring types whose names end in `Entity` or `View`. For a supplied `Entities` or `Views` directory, search only that directory for the corresponding type. Exclude `bin`, `obj`, and generated build artifacts. Identify types from C# declarations rather than filename matches alone. Report the searched scope if no model files are found.

## Entity checks

Apply only rules provable from the selected Entity file itself. Check the class's declared base/interface pair for the scope indicated by its own mapping and key declarations. Check that it does not redeclare properties inherited from that pair. Check `[PrimaryKey]`, `[Key]`, and other present mapping annotations for contradictions or references to absent properties; inherited properties count. Check that the static `Builder` action has the correct Entity type and that its property references resolve to declared or inherited members. Check for contradictions between annotations and builder mappings visible in the file. Do not assert that an annotation or database column is missing without a source present in the reviewed files.

The supported base/interface pairs are `Entity`/`IEntity`, `EntityWithCorporation`/`IEntityWithCorporation`, and `EntityWithCorporationAndCompany`/`IEntityWithCorporationAndCompany`. Do not require an external database comparison or context registration.

## View checks

Check a View file's declared EF mapping, key or keyless configuration, and property references for internal consistency. Flag explicit write-oriented behavior in a View model when it conflicts with its read-only role. Do not impose Entity inheritance, a `Builder`, or context registration on Views.

## Finding codes

Use these stable codes for confirmed file-content findings. Add new codes without changing existing meanings.

| Code | Finding |
| --- | --- |
| `GEVR001` | An Entity's declared base class and interface conflict with each other or with the scope established in that file. |
| `GEVR002` | An Entity redeclares a property inherited from its declared base class. |
| `GEVR003` | An Entity's key or mapping annotations contradict each other or refer to an unavailable property. |
| `GEVR004` | An Entity has no static `Builder` action, its builder has the wrong Entity type, or it references an unavailable property. |
| `GEVR005` | An Entity's visible builder mapping contradicts its annotations. |
| `GEVR006` | A View's key or mapping declarations contradict each other or reference an unavailable property. |
| `GEVR007` | A View file explicitly defines write behavior inconsistent with a read-only View. |

Resolve C# declarations, inheritance, and mapping expressions before reporting a finding; do not infer violations from text matches alone. If a rule depends on unavailable base-class definitions or ambiguous conventions, mark that check unverified and explain why. Avoid duplicate findings for one underlying defect.

## Review report

Write the report in the user's language. Start with a Markdown table containing one row for model discovery and one for each applicable `GEVR` rule or rule group. Include check, status, and finding count. Use `Passed`, `Findings`, `Unverified`, or `Skipped` (translated when appropriate); use `—` when a count cannot be determined. Never mark an unverified or skipped check as passed.

List findings as `GEVRnnn path:line:column — evidence and suggested correction`, omitting unavailable line or column numbers. Group them by Entity and View. End with a **Final summary** giving the searched scope, counts of reviewed Entity and View files, findings by code, and the overall result. Do not claim an overall pass when any applicable check has findings or remains unverified.
