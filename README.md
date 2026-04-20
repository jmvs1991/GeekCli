# 🛠️ geek-cli

A developer CLI built with [Spectre.Console](https://spectreconsole.net/cli) for common React, React Native, Angular, and .NET workflows.

---

## ✨ Features

- Generate React contexts
- Scaffold React Native modules, screens, and components
- Generate Angular pages and components
- Create, remove, and rollback EF Core migrations
- Scaffold EF Core entities from an existing database table
- Use branch-specific wizards or a general root wizard

---

## 📂 Command Structure

```text
geek-cli
├── db
│   ├── scaffold
│   └── migration
│       ├── add
│       ├── remove
│       └── rollback
├── ngx
│   ├── page
│   └── component
└── rx
    ├── context
    └── native
        ├── module
        ├── screen
        └── component
```

Running `geek-cli` with no arguments opens the general wizard.

Running `geek-cli db`, `geek-cli ngx`, or `geek-cli rx` opens the wizard for that branch.

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

### Roll back to a migration directly

```bash
geek-cli db migration rollback InitSchema --project Booking
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

---

## 🤝 Contributing

Pull requests are welcome. If you want to suggest a feature or report a bug, open an issue.

---

## 📜 License

MIT License © 2025
