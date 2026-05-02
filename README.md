# 🛠️ geek-cli

A developer CLI built with [Spectre.Console](https://spectreconsole.net/cli) for common React, React Native, Angular, and .NET workflows.

---

## ✨ Features

- Generate React contexts
- Scaffold React Native modules, screens, and components
- Generate Angular pages and components
- Run Geek .NET template pack commands
- Create, remove, and rollback EF Core migrations
- Generate Up/Down SQL migration scripts with schema-specific folders and dbo synonyms
- Scaffold EF Core entities from an existing database table
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
│   └── component
├── mcp
└── rx
    ├── context
    └── native
        ├── module
        ├── screen
        └── component
```

Running `geek-cli` with no arguments opens the general wizard.

Running `geek-cli db`, `geek-cli ngx`, or `geek-cli rx` opens the wizard for that branch.

Running `geek-cli dotnet` lists the installed Geek .NET templates.

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
dotnet tool install --global --add-source .\GeekCli\bin\Release GeekCli --verbosity detailed
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
dotnet tool install --global --add-source .\GeekCli\bin\Release GeekCli --verbosity detailed
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
dotnet tool update --global --add-source .\GeekCli\bin\Release GeekCli
```

---

## 🗑️ Uninstall

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

### Generate an Angular component directly

```bash
geek-cli ngx component user-profile
```

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
