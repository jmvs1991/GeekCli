---
name: geek-dotnet-controller-reviewer
description: Review existing .NET API controllers for endpoint contracts, required filters, pagination attributes, and exception handling. Accept a solution, API project, controller folder, or C# file.
---

# Review .NET controllers

Review existing controllers without changing files. Accept a `.sln` or `.slnx` file, a solution directory, an API `.csproj` or project directory, a controller folder, or a C# file. Search only the selected scope. Exclude `bin`, `obj`, and generated artifacts. Identify controllers from C# declarations, not filenames alone. If no controllers are found, report the searched scope; do not claim a passing review.

Use the sibling [`geek-dotnet-controller`](../geek-dotnet-controller/SKILL.md) skill for the supported Entity and View variants. Resolve actual behavior from the selected project's controllers, service contracts, models, base classes, and available templates. Do not assume that a generated template establishes every project's behavior. Mark a check unverified when the evidence needed to decide is unavailable.

## Controller and endpoint checks

- Check that the controller's Entity or View role, inheritance, routes, and HTTP verb attributes agree with its visible contracts. Report route or verb conflicts only when they can be established from the effective controller and action routes.
- Check that dependencies used by actions are available through the controller's declared injection pattern. Follow each action's inputs and service calls to verify parameter mapping and delegation against the visible service contract.
- Check that an action's response and status behavior agree with its visible return type and service result. For a View controller, flag write actions that contradict its established read-only role.
- Require `[ServiceFilter(typeof(ExceptionHandlerFilter))]` on each controller class.
- For every endpoint that uses pagination, require an `AllowFetchAllDataFilter` attribute on the action with an explicit boolean value: `[AllowFetchAllDataFilter(Allow = true)]` or `[AllowFetchAllDataFilter(Allow = false)]`. Establish pagination from the action parameters, service call, or result contract; do not infer it from the action name alone.
- Combine controller-level and action-level route templates to determine each endpoint's effective route. When it contains a Corporation route parameter without Company, require `[ServiceFilter(typeof(CorporationFilter))]`. When it contains both Corporation and Company route parameters, require `[ServiceFilter(typeof(CorporationCompanyFilter))]`; do not additionally require `CorporationFilter`. For either case, accept the required filter on the controller class or the action.
- Flag any `try`/`catch` block inside an endpoint action, including nested blocks. Point to the block and recommend allowing the exception to reach the controller's `ExceptionHandlerFilter`. Do not treat exception handling inside a called service as an endpoint block.

Resolve attribute names and route parameters from C# syntax and symbols where available. Accept equivalent qualified attribute names and case-insensitive route parameter names. Avoid findings based on text matches alone, and avoid duplicate findings for the same defect.

## Finding codes

| Code | Finding |
| --- | --- |
| `GDCR001` | No controller class was found in the selected scope. |
| `GDCR002` | A controller's established Entity or View role conflicts with its inheritance or contract. |
| `GDCR003` | An endpoint has a provable route or HTTP verb conflict. |
| `GDCR004` | An action uses a dependency unavailable through the controller's declared injection pattern. |
| `GDCR005` | An action maps inputs incorrectly or contradicts its visible service contract. |
| `GDCR006` | An action's response or status behavior contradicts its visible contract. |
| `GDCR007` | A View controller exposes a write action inconsistent with its established read-only role. |
| `GDCR008` | A controller class lacks `ExceptionHandlerFilter`. |
| `GDCR009` | A paginated endpoint lacks `AllowFetchAllDataFilter` with an explicit `Allow = true` or `Allow = false`. |
| `GDCR010` | An endpoint with Corporation but not Company in its effective route lacks `CorporationFilter`. |
| `GDCR011` | An endpoint with Corporation and Company in its effective route lacks `CorporationCompanyFilter`. |
| `GDCR012` | An endpoint action contains a `try`/`catch` block. |

## Review report

Write the report in the user's language. Start with a Markdown table of applicable checks, status, and finding count. Use `Passed`, `Findings`, `Unverified`, or `Skipped`, translated when appropriate; use `—` when a count cannot be determined.

List findings as `GDCRnnn path:line:column — evidence and suggested correction`, omitting unavailable coordinates. Group findings by controller. End with the reviewed scope, the number of controllers and endpoint actions examined, findings by code, and the overall result. Explain unverified checks. Never report an overall pass while an applicable check has findings or remains unverified.
