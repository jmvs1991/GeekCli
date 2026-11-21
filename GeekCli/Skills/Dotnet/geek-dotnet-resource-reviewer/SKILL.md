---
name: geek-dotnet-resource-reviewer
description: Review .NET Resource classes and their English and Spanish .resx messages in a solution, *Services project, Resources folder, or *Resource.cs file.
---

# Review .NET message resources

Review existing resources without changing files. Check each `*Resource` class against its English and Spanish `.resx` files and, when the relationship can be established, its Entity. Do not review consuming services or run the resource generator.

## Select resources

- For a `.sln`/`.slnx` file or solution directory, use solution membership to find projects whose name or containing directory ends in `Services` (case-insensitive). Search those projects for Resource classes.
- For a `*Services` `.csproj` or project directory, search only that project. For a `Resources` directory, search only that directory. For a `*Resource.cs` file, review only Resource classes declared in that file and their associated `.resx` files.
- Exclude `bin`, `obj`, and generated build artifacts. Identify Resource classes from C# declarations, not filenames alone. If none are found, report `GDRR001` with the searched scope; do not claim an empty review passed.
- Pair a class named `<Name>Resource` with `<Name>Resource.resx` (English) and `<Name>Resource.es.resx` (Spanish) in its resource directory. Use established project conventions if they specify a different unambiguous location. Report a missing file only after checking the applicable location.

## Checks

- Determine the declared resource scope from its base class: `ResourceBase` (`basic`), `ResourceWithCorporationBase` (`corp`), `ResourceWithCorporationCompanyBase` (`corp-co`), or `ResourceWithCorporationCompanyAndCodeBase` (`corp-co-code`). When the related Entity and its scope can be identified unambiguously, check that the resource base agrees. If the Entity relationship or scope cannot be established, mark that comparison unverified rather than guessing. Do not infer `corp-co-code` solely from an Entity having a code property.
- Check that the Resource constructor accepts `CultureInfo` and passes it to the base constructor. Check required message overrides against the actual base contract when available. The four C# examples in the sibling `geek-dotnet-resource/templates/` directory show the intended patterns but do not replace a visible project-specific base contract.
- Check that localized message properties use the correct Resource type in `Localization<T>.GetValue(...)`, pass the Resource's culture, and reference the intended key. Resolve the property and inherited contract before reporting a mismatch; equivalent C# formatting is acceptable.
- Check that both `.resx` files exist, their keys match, and every key referenced by the Resource class is present in both. Additional matching keys are acceptable; they may be used outside the class.
- Report empty values, unmistakable template filler or generic sample text, mismatched placeholders, and clearly incompatible meanings between messages with the same key. Compare literal placeholders such as `{{code}}` as well as format placeholders when present. Do not report a merely debatable translation or a stylistic preference as a finding.
- Use declarations, inherited contracts, and parsed `.resx` entries as evidence. If a check depends on unavailable definitions or unreadable files, mark it unverified and explain what evidence is missing. Avoid duplicate findings for one underlying defect.

## Finding codes

| Code | Finding |
| --- | --- |
| `GDRR001` | No Resource class was found in the selected scope. |
| `GDRR002` | An associated English or Spanish `.resx` file is missing. |
| `GDRR003` | A Resource base conflicts with the verifiable scope of its related Entity. |
| `GDRR004` | A constructor or localized property contradicts the verifiable Resource contract. |
| `GDRR005` | The English and Spanish key sets differ, or a class-referenced key is absent from either file. |
| `GDRR006` | A message is empty or retains unmistakable template or sample text. |
| `GDRR007` | Matching messages use different placeholders. |
| `GDRR008` | Matching messages have clearly incompatible meanings. |

## Review report

Write in the user's language. Start with a Markdown table of applicable checks, status, and finding count. Use `Passed`, `Findings`, `Unverified`, or `Skipped` (translated when appropriate), and `—` when the count cannot be determined. Never mark an unverified or skipped check as passed.

List findings as `GDRRnnn path:line:column — evidence and suggested correction`, omitting unavailable coordinates. Group findings by Resource. End with the reviewed scope, counts of `*Services` projects, Resource classes, and English/Spanish `.resx` pairs actually reviewed, findings by code, and the overall result. Explain unverified checks separately. Never report an overall pass while an applicable check has findings or remains unverified.
