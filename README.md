# 🛠️ geek-cli

A developer CLI built with [Spectre.Console](https://spectreconsole.net/cli) for common React, React Native, Angular, and .NET workflows.

---

## ✨ Features

- Generate React contexts
- Scaffold React Native modules, screens, and components
- Generate Angular pages, components, and services, including Nx workspaces
- Run Geek .NET template pack commands
- Create, remove, and rollback EF Core migrations
- Generate Up/Down SQL migration scripts with schema-specific folders and dbo synonyms
- Scaffold EF Core entities from an existing database table
- Install bundled AI skills into a project or user profile
- Install bundled AI subagents into a project or user profile
- Use branch-specific wizards or a general root wizard

---

## 📂 Command Structure

```text
geek-cli
├── db
│   ├── scaffold
│   ├── script
│   └── migration
│       ├── add
│       ├── remove
│       └── rollback
├── dotnet
│   ├── list
│   ├── dto
│   ├── resource
│   ├── cache
│   ├── sp
│   ├── read
│   ├── write
│   ├── controller
│   ├── service
│   ├── unittest-service
│   └── unittest-api
├── ngx
│   ├── page
│   ├── component
│   └── service
├── ai
│   └── skill
│       └── install
│   └── agent
│       └── install
├── mcp
└── rx
    ├── context
    └── native
        ├── module
        ├── screen
        └── component
```

Running `geek-cli` with no arguments opens the general wizard.

Running `geek-cli db`, `geek-cli ngx`, `geek-cli rx`, or `geek-cli ai` opens the wizard for that branch. The AI wizard can also be opened from the general wizard and offers individual, bulk, and update operations for skills, plus subagent installation. Bulk skill operations summarize the scope and destination before confirmation. Updating warns that complete skill folders will be replaced, including local changes. The Angular wizard can generate pages, components, and services. For services, it can select an Nx application or library tagged `tech:angular`, asks for a project-relative folder, and shows a summary for confirmation before generation.

Running `geek-cli dotnet` lists the installed Geek .NET templates.

### Install an AI skill

Install a bundled skill with `geek-cli ai skill install <name> --scope <project|user>`. Project scope requires an existing `--project-path`; user scope installs under `~/.agents/skills`. Available skills are `geek-dotnet-list` for listing installed Geek .NET templates, `geek-dotnet-sp` for generating .NET stored procedure class templates through MCP, `geek-dotnet-cache` for generating .NET cache templates through MCP, `geek-dotnet-resource` for generating .NET message resource templates through MCP, `geek-dotnet-service` for generating .NET entity or view services through MCP, `geek-dotnet-controller` for generating .NET entity or view controllers through MCP, `geek-dotnet-unit-test-service` for generating .NET service unit test templates through MCP, `geek-dotnet-api-unit-test` for generating .NET API unit test templates through MCP, `geek-db-migration-add` for adding a migration, `geek-db-migration-remove` for removing the latest migration, `geek-db-migration-rollback` for updating a database to a target migration, `geek-db-script` for generating SQL script templates through MCP, and `geek-ngx-service` for generating Angular services through MCP. Install any of them for your user profile with `geek-cli ai skill install <name> --scope user`, or for a project with `geek-cli ai skill install <name> --scope project --project-path <PATH>`. Skill folders added under `GeekCli/Skills` are included with the tool package.

Install all bundled skills missing from a scope with `geek-cli ai skill install --all --scope project --project-path <PATH>` or `geek-cli ai skill install --all --scope user`. Existing skill directories are skipped and preserved. Bulk mode cannot be combined with a skill name, `--overwrite`, or `--update-all`; the CLI reports a summary and returns an error if any installation fails. The AI wizard offers the same operation with a confirmation summary.

Update installed bundled skills in a scope with `geek-cli ai skill install --update-all --scope project --project-path <PATH>` or `geek-cli ai skill install --update-all --scope user`. This replaces complete folders for installed skills that exist in the bundled catalog, including local changes and obsolete files. External skills and bundled skills that are not installed are left untouched. Update mode cannot be combined with `--all`, a skill name, or `--overwrite`; it reports updates, skips, and errors, continues after an individual failure, and returns an error if any update fails. If no bundled catalog skills are installed, it succeeds without creating the skills directory. The AI wizard shows the number of catalog skills to update, warns about replacing local changes, and requests one confirmation for the operation.

### Install an AI subagent

Install a bundled subagent with `geek-cli ai agent install <name> --scope <project|user>`. Project scope requires an existing `--project-path`; user scope installs under `~/.codex/agents`. Subagents are standalone TOML files named after the catalog entry. The bundled catalog is initially empty; TOML files added under `GeekCli/Agents` are included with the tool package. Subagent installation is available through the direct CLI and AI wizard, not MCP.

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- A terminal on Windows, macOS, or Linux

---

## 🔨 Build

```bash
dotnet build .\GeekCli\GeekCli.csproj -c Release
```

`Taskfile.yml` also includes ready-to-run tasks for:

- `task geek-cli:build`
- `task geek-cli:pack`
- `task geek-cli:install-tool`
- `task geek-cli:update-tool`
- `task geek-cli:uninstall-tool` (on Windows, stops active `geek-cli.exe mcp` servers before uninstalling)

---

## 📦 Pack As A Tool

Create the tool package:

```bash
dotnet pack .\GeekCli\GeekCli.csproj -c Release
```

This generates a package under:

```text
.\GeekCli\bin\Release\
```

The executable command name is:

```text
geek-cli
```

The package id used by `dotnet tool install` is currently:

```text
GeekCli
```

---

## 📥 Install Globally

```bash
dotnet tool install --global GeekCli --verbosity detailed
```

After installation, run:

```bash
geek-cli
```

---

## 🔌 Install As An MCP Server

`geek-cli` exposes its generators through MCP over stdio.

1. Install the tool globally:

```bash
dotnet tool install --global GeekCli --verbosity detailed
```

2. Register it in your MCP client using `geek-cli mcp` as the server command.

Example MCP client configuration:

```json
{
  "mcpServers": {
    "geek-cli": {
      "command": "geek-cli",
      "args": ["mcp"]
    }
  }
}
```

If you do not want to install the tool globally, you can also point your MCP client at a local build:

```json
{
  "mcpServers": {
    "geek-cli": {
      "command": "dotnet",
      "args": ["run", "--project", ".\\GeekCli\\GeekCli.csproj", "--", "mcp"]
    }
  }
}
```

After registration, start or reload your MCP client and it will discover the `geek-cli` tools from the stdio server.

---

## 🐞 Debug The MCP Server

When testing the MCP server locally, run the [Model Context Protocol Inspector](https://github.com/modelcontextprotocol/inspector) against the `mcp` entrypoint.

From the `.\GeekCli\` folder:

```bash
npx @modelcontextprotocol/inspector dotnet run -- mcp
```

This starts the inspector and launches the local server through `dotnet run`, which matches the `mcp` mode handled in `Program.cs`.

If you prefer Visual Studio or Rider, the project also includes a `Geek:mcp` launch profile in `GeekCli/Properties/launchSettings.json` with:

```text
commandLineArgs: mcp
```

That profile is useful when you want to attach a debugger to the `GeekCli` process while the MCP server is running.

---

## 🔁 Update The Tool

After packing a new version:

```bash
dotnet tool update --global GeekCli
```

---

## 🗑️ Uninstall

The task below stops active `geek-cli.exe mcp` server processes on Windows before uninstalling the global tool. Other Geek CLI invocations are left running. On macOS and Linux, it runs the uninstall command directly.

```bash
task geek-cli:uninstall-tool
```

To uninstall directly without using Task, run:

```bash
dotnet tool uninstall --global GeekCli
```

---

## 🧪 Usage Examples

### Open the general wizard

```bash
geek-cli
```

### Open the database wizard

```bash
geek-cli db
```

### Add a migration directly

```bash
geek-cli db migration add InitSchema --project Booking --issue ABC-123 --init
```

### Scaffold a table directly

```bash
geek-cli db scaffold --table TR_TAG_INVOICE --output-dir Parking/Entities --connection-string "Data Source=localhost;Initial Catalog=ParkingDevelop;User ID=SA;Password=YourStrong!Passw0rd;TrustServerCertificate=True" --provider SqlServer
```

### Generate SQL scripts directly

```bash
geek-cli db script --project Booking --schema Sales --type "Create SP" --issue ABC-123 --object-name usp_GetCustomer
```

### Roll back to a migration directly

```bash
geek-cli db migration rollback InitSchema --project Booking
```

### List the installed Geek .NET templates

```bash
geek-cli dotnet list
```

### Generate a DTO directly

```bash
geek-cli dotnet dto Customer --projectName Billing --scope corp-co
```

### Generate a View DTO directly

```bash
geek-cli dotnet dto Customer --projectName Billing --view --scope corp-co
```

### Generate a resource directly

```bash
geek-cli dotnet resource Customer --projectName Billing --scope corp-co-code
```

### Generate a read repository directly

```bash
geek-cli dotnet read Customer --dbSchema Sales --contextName BillingContext --view --scope corp-co
```

### Generate a controller directly

```bash
geek-cli dotnet controller Customer --projectName Billing --codeField CustomerCode --view
```

### Generate a service directly

```bash
geek-cli dotnet service Customer --projectName Billing --view --scope corp-co-code
```

### Generate a service unit test directly

```bash
geek-cli dotnet unittest-service Customer --projectName Billing --scope corp-co
```

### Generate an API unit test directly

```bash
geek-cli dotnet unittest-api Customer --projectName Billing --codeField CustomerCode --serviceInterface ICustomerService --dtoName CustomerDTO --responseName CustomerResponse --contextTestBase CondominiumContextTest --endpoint Customer --scope corp-co
```

### Open the Angular wizard

```bash
geek-cli ngx
```

When creating a component, the wizard asks whether the current directory is an Nx workspace. If it is, it lists projects tagged `tech:angular` (applications and libraries), then asks for a workspace-relative destination folder and declaring NgModule path. For pages, the Nx project list includes only projects tagged `tech:angular` with `projectType` set to `application`. Run the wizard from the Nx workspace root.

### Generate an Angular component directly

```bash
geek-cli ngx component user-profile
```

For an Nx workspace, run the command from the workspace root and provide the project, folder, component name, and declaring module:

```bash
geek-cli ngx component test --nx --project ing-sel-crm --folder apps/ing-sel-crm/src/app/Modules/Catalog/Pages/FwProducts/Components --module apps/ing-sel-crm/src/app/Modules/Catalog/catalog.module.ts
```

Use a workspace-relative destination folder beneath the application's `apps/<project>/src/app` or the library's `sourceRoot`. The folder path is used as provided, so include the desired casing and do not append the component name; the generator adds the kebab-case component name as the final path segment. Provide the declaring NgModule as a workspace-relative path including `.ts`. Nx generation uses the `ngx` prefix and selector `ngx-<component-name>-component`.

For an Angular library, the command can target its `sourceRoot`:

```bash
geek-cli ngx component test-component --nx --project core --folder packages/ngx/core/src/lib --module packages/ngx/core/src/lib/ngx-core.module.ts
```

### Generate an Angular page directly

```bash
geek-cli ngx page user-profile
```

For an Nx workspace, run the command from the workspace root and provide the Angular application, destination folder, page name, and declaring module:

```bash
geek-cli ngx page user-profile --nx --project ing-sel-crm --folder apps/ing-sel-crm/src/app/Modules/Catalog/Pages --module apps/ing-sel-crm/src/app/Modules/Catalog/catalog.module.ts
```

Nx page generation converts folder segments to PascalCase and the page name to kebab-case. It uses the `ngx` prefix and selector `ngx-<page-name>-page`.

### Generate an Angular service directly

```bash
geek-cli ngx service user-profile
```

For an Nx workspace, run the command from the workspace root and provide the project, project-relative folder, and service name:

```bash
geek-cli ngx service test --nx --project core --folder Services
```

The Nx generator receives the provided folder and name and creates the service with the `.service` suffix. The project can be an application or library tagged `tech:angular`.

### Open the React wizard

```bash
geek-cli rx
```

### Generate a React context directly

```bash
geek-cli rx context Auth --flat
```

### Generate a React Native screen directly

```bash
geek-cli rx native screen Login --schema --wrapper
```

### Start the MCP server

```bash
geek-cli mcp
```

This starts `geek-cli` as a stdio MCP server and exposes the existing CLI generators as MCP tools. Use this command in your MCP client configuration.

The `NgxService` MCP tool accepts a `name` parameter and generates the service using Angular CLI defaults. Set `nx` to `true` to generate in an Nx project; Nx calls must run from the workspace root.

| Parameter | Required | Description |
| --- | --- | --- |
| `name` | Always | Angular service name. |
| `nx` | No | Set to `true` for Nx generation. Defaults to `false`. |
| `project` | In Nx mode | Nx Angular application or library tagged `tech:angular`. |
| `folder` | In Nx mode | Destination folder relative to the selected project. |

The `NgxComponent` MCP tool accepts these parameters:

| Parameter | Required | Description |
| --- | --- | --- |
| `name` | Always | Component name. In Nx mode, it is converted to kebab-case and used as the final path segment. |
| `nx` | No | Set to `true` for Nx generation. Defaults to `false`, which preserves standalone Angular CLI generation. |
| `project` | In Nx mode | Nx project tagged `tech:angular`, such as an application `ing-sel-crm` or a library `core`. |
| `folder` | In Nx mode | Workspace-relative destination under `apps/<project>/src/app` for applications or under the library's `sourceRoot`. The path is used as provided; the component name is appended as the final segment. |
| `module` | In Nx mode | Workspace-relative path to the declaring NgModule file, including `.ts`. |

The `NgxPage` MCP tool accepts the same parameters and defaults, with page-specific naming:

| Parameter | Required | Description |
| --- | --- | --- |
| `name` | Always | Page name. In Nx mode, it is converted to kebab-case and used as the final path segment. |
| `nx` | No | Set to `true` for Nx generation. Defaults to `false`, preserving standalone Angular CLI generation. |
| `project` | In Nx mode | Nx Angular application name, such as `ing-sel-crm`. |
| `folder` | In Nx mode | Folder beneath `src/app`, either relative or a full workspace path beginning `apps/<project>/src/app/`; segments are converted to PascalCase. |
| `module` | In Nx mode | Workspace-relative path to the NgModule file that declares the page. |

For Nx MCP calls, configure the MCP server to run with the Nx workspace root as its working directory. Nx resolves the project and module paths from that directory.

Example Nx tool call:

```json
{
  "name": "Test",
  "nx": true,
  "project": "ing-sel-crm",
  "folder": "apps/ing-sel-crm/src/app/Modules/Catalog/Pages/FwProducts/Components/Test",
  "module": "apps/ing-sel-crm/src/app/Modules/Catalog/catalog.module.ts"
}
```

---

## 🗃️ SQL Script Generator

`geek-cli db script` creates SQL migration scaffolding for schema projects. The command generates `Up` and `Down` `.sql` files using the same rules from the wizard, direct CLI command, and MCP tool.

### Supported execution methods

- Wizard: `geek-cli` then `Database` then `Generate SQL migration scripts`
- Direct CLI: `geek-cli db script ...`
- MCP: `DbScript`

### Required parameters

- `--project`: base migration project name
- `--schema`: affected database schema
- `--type`: migration type
- `--issue`: issue or ticket name used for folder grouping
- `--object-name`: required for `Modify SP`, `Create SP`, `Modify Table`, `Create Table`, `Create View`, and `Modify View`
- `--init`: optional flag for initialization projects

### Supported migration types

- `Query`
- `Modify SP`
- `Create SP`
- `Modify Table`
- `Create Table`
- `Create View`
- `Modify View`

### Project naming rule

- `--init` present: `{{PROJECT_NAME}}.SchemaInitialization`
- `--init` omitted: `{{PROJECT_NAME}}.SchemaUpdates`

### Generated structure

```text
{{FINAL_PROJECT_NAME}}
└── Scripts
    ├── {{SCHEMA}}
    │   ├── Down
    │   │   ├── Queries
    │   │   │   └── {{ISSUE_NAME}}
    │   │   ├── Sp
    │   │   │   └── {{ISSUE_NAME}}
    │   │   ├── Tables
    │   │   │   └── {{ISSUE_NAME}}
    │   │   └── Views
    │   │       └── {{ISSUE_NAME}}
    │   └── Up
    │       ├── Queries
    │       │   └── {{ISSUE_NAME}}
    │       ├── Sp
    │       │   └── {{ISSUE_NAME}}
    │       ├── Tables
    │       │   └── {{ISSUE_NAME}}
    │       └── Views
    │           └── {{ISSUE_NAME}}
    └── dbo
        ├── Down
        │   └── Synonyms
        │       └── {{ISSUE_NAME}}
        └── Up
            └── Synonyms
                └── {{ISSUE_NAME}}
```

### Naming rules

- Up file: `{{ALTER_OR_CREATE}}_{{OBJECT_NAME}}.sql`
- Down file: `ROLLBACK_{{ALTER_OR_CREATE}}_{{OBJECT_NAME}}.sql`
- Query migrations use `QUERY_{{ISSUE_NAME}}.sql` and `ROLLBACK_QUERY_{{ISSUE_NAME}}.sql` when no object name is provided

### Up and Down behavior

- Every execution creates one `Up` script and one `Down` script
- `Create SP`, `Create Table`, and `Create View` generate `DROP` statements in the `Down` file
- `Query`, `Modify SP`, `Modify Table`, and `Modify View` generate an empty `Down` file
- The `Up` file always includes starter SQL content for the selected migration type

### Synonym behavior

- `Create SP`, `Create Table`, and `Create View` also generate synonym scripts under `Scripts/dbo`
- Synonym `Up` creates a synonym from `dbo` to `{{SCHEMA}}`
- Synonym `Down` drops the synonym from `dbo`

### Examples

Wizard:

```bash
geek-cli
```

Direct CLI:

```bash
geek-cli db script --project Booking --schema Sales --type "Create Table" --issue ABC-123 --object-name TR_CUSTOMER
```

Direct CLI for a query migration:

```bash
geek-cli db script --project Booking --schema Sales --type Query --issue ABC-124
```

MCP:

```text
Tool: DbScript
Arguments:
  projectName: Booking
  schema: Sales
  type: Create View
  issue: ABC-125
  init: false
  objectName: VW_CUSTOMER
```

---

## 🤝 Contributing

Pull requests are welcome. If you want to suggest a feature or report a bug, open an issue.

---

## 📜 License

MIT License © 2025
