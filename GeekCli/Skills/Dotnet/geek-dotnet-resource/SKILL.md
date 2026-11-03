---
name: geek-dotnet-resource
description: Use the Geek CLI dotnet_resource MCP tool to generate .NET message resource templates.
---

# Generate .NET message resources

Use this skill when the user asks to generate Geek .NET message resources for localized messages.

## Prerequisites and inputs

- The `dotnet_resource` MCP tool and .NET SDK must be available. The Geek resource template for the selected scope must already be installed and usable in the MCP process environment.
- The tool requires `name` and `projectName`. Infer them from the request and workspace when clear; ask if either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, `corp-co`, and `corp-co-code`. Clarify the scope when the request does not establish it and the intended template is ambiguous.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if unclear.

## API Services conventions

- For an entity in `<Schema>Services/<Entity>`, use the entity name without the `Entity` suffix. For example, `BrandEntity` uses `BrandResource` in `<Schema>Services/Brand/Resources`.
- The expected files are `<Entity>Resource.cs`, `<Entity>Resource.resx` for English, and `<Entity>Resource.es.resx` for Spanish. Confirm the generated files after invoking the MCP tool; do not assume installed templates produce all three files.
- Select the scope that matches the entity. The MCP chooses the corresponding template and base class: `basic` uses `ResourceBase`, `corp` uses `ResourceWithCorporationBase`, `corp-co` uses `ResourceWithCorporationCompanyBase`, and `corp-co-code` uses `ResourceWithCorporationCompanyAndCodeBase`.
- Adapt the supplied `BrandResource` reference pattern: accept `CultureInfo` in the constructor and pass it to the base class; implement localized message properties by calling `Localization<BrandResource>.GetValue("MessageKey", _culture)`.
- Keep message keys and meanings aligned between English and Spanish resources. Write clear, descriptive messages. Add placeholders such as `{{code}}` only when they provide useful context, and keep the same placeholders in both languages. The consuming code is responsible for replacing placeholders.
- Treat these examples as skill reference templates for adapting generated output; they do not replace the installed `dotnet new` template used by the MCP tool.

### Reference templates

See `templates/` for a C# resource example for each supported scope. Adapt the namespace, entity name, and resource entries to the target project. The corresponding English and Spanish `.resx` files must define matching keys and equivalent clear messages. Add a placeholder such as `{{code}}` to a message only when that message benefits from including the entity code.

## Invoke the tool

After confirming the required inputs, scope, and target workspace, call `dotnet_resource` with `name`, `projectName`, and `scope`. The tool selects `geek-resource`, `geek-resource-corp`, `geek-resource-corp-co`, or `geek-resource-corp-co-code` for the corresponding scope and asks the .NET SDK to generate the template. Do not run `dotnet new` or another command as a fallback.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero.
- Describe generated files only when they are confirmed by the tool output or workspace inspection; do not promise specific template artifacts in advance.
- For a nonzero exit code, report that generation failed and include useful details from `Output`. If invocation raises an exception, explain the error using only details that are available; do not claim success.
- If the requested scope is unsupported, explain that only `basic`, `corp`, `corp-co`, and `corp-co-code` are accepted, then ask which supported scope to use. Do not invoke the tool with an unsupported value.
- If `dotnet_resource` is unavailable or execution fails, explain why generation could not be confirmed. Do not switch to direct CLI or another generation path.
