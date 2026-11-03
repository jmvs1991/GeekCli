---
name: geek-dotnet-service
description: Use the Geek CLI dotnet_service MCP tool to generate a .NET service for an entity or view.
---

# Generate a .NET service

Use this skill when the user asks to generate a Geek .NET service from an installed template.

## Prerequisites and inputs

- The `dotnet_service` MCP tool and .NET SDK must be available. The selected Geek service template must already be installed and usable in the MCP process environment.
- The tool requires `name` (the generated service or template name) and `projectName` (the target project name). Infer values from the request and workspace when clear; ask when either is missing or ambiguous.
- Optional `scope` defaults to `basic`. Supported values are `basic`, `corp`, `corp-co`, and `corp-co-code`.
- Optional `view` defaults to `false`. Use `false` for an entity service and `true` for a view service. Clarify the requested variant when it is unclear.
- The tool has no working-directory or output-directory parameter. Confirm that the MCP process working directory and template environment target the intended workspace; ask if that is unclear.

## API Services conventions

- Use the entity or view name without an `Entity` suffix and place service files under `<Schema>Services/<Entity>/Services`.
- Entity generation produces `<Entity>DBService.cs`, `<Entity>MCService.cs`, and `I<Entity>Service.cs`. Select scope-specific base classes and service contracts through the MCP template. See `templates/entity-corp-co-code/` for all three files in the agreed corporation/company/code example.
- A DB service implements the feature interface, exposes the DTO transform, and receives its read repository, write repository when applicable, and `IHttpContextAccessor`. Construct its resource with the culture from the request headers, following the reference example.
- The entity interface combines the scope-specific read service contract with `IServiceCRUD<DTO, Entity, SpInsert, SpUpdate, SpDelete>` when the entity supports the agreed CRUD operations. Use the target scope's matching contract; do not assume `corp-co-code` for other scopes.
- An entity MC service implements the same interface and wraps the DB service with the matching cache interface. Generate this class during Services even if the cache interface has not yet been created; record that dependency for resolution during the Cache stage.
- Views generate a DB service and interface only. They use a read-only view service base, a read repository, DTO transform, and localized resource. Do not generate an MC service, write repository, CRUD contract, or SPs for a view. See `templates/view-corp-co/` for the agreed DB service and interface examples.
- Keep view-specific query methods and no-result behavior grounded in the target repository and request. The supplied view example's paginated corporation/company/year/month query and localized not-found exception are reference behavior for that view, not universal requirements.
- Inspect existing DB services for feature-specific operations beyond inherited CRUD and reads. Add each required operation to the service interface and DB service, and preserve corresponding behavior in the MC service when one exists. Examples include overloaded `FindAsync` methods, calculation or factor operations, and extra stored procedure models; do not assume every feature has only the inherited CRUD surface.
- Register generated services in the corresponding API `IServiceCollectionExtension`, in `AddServices`. Register entities with `services.AddScoped<I<Entity>Service, <Entity>DBService>().Decorate<I<Entity>Service, <Entity>MCService>()`; register views with their DB service only. Resolve entity cache dependencies during the Cache stage before declaring the feature fully validated.
- Treat reference files under `templates/` as adaptation and validation guides. The installed `dotnet new` templates remain the MCP generation source.

## Invoke the tool

After confirming the required inputs, scope, variant, and target workspace, call `dotnet_service` with those values. Use MCP as the only generation path; do not run `dotnet new` or another command as a fallback.

The tool selects `geek-entity-service-paginationful-filterless` when `view=false` or `geek-view-service-paginationful-filterless` when `view=true`, then appends the selected scope suffix (`-corp`, `-corp-co`, or `-corp-co-code`; `basic` adds no suffix). It passes `name` and `projectName` to the template. Do not promise exact generated files unless confirmed by the tool output or workspace inspection.

## Report the result

- The result contains `ExitCode` and captured `Output`. Report success only when `ExitCode` is zero; describe generated artifacts only when confirmed by output or workspace inspection.
- For a nonzero exit code or invocation error, explain that generation failed and share useful details from the result without claiming success.
- If `dotnet_service` is unavailable, explain that generation cannot proceed because the MCP tool is unavailable. Do not fall back to direct CLI execution.
