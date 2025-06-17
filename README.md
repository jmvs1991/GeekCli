# 🛠️ geek-cli

A powerful developer CLI tool built with [Spectre.Console](https://spectreconsole.net/cli), focused on automating common tasks for React, React Native, and .NET development.

---

## ✨ Features

- 🧠 Generate React Contexts
- 📱 Scaffold React Native modules, screens, and components
- 🧩 Create EF Core migrations with project conventions
- 📦 CLI commands with both structured and interactive workflows

---

## 📂 Command Structure

```
geek-cli
├── rx
│   ├── context
│   └── native
│       ├── module
│       ├── screen
│       └── component
└── db
    └── migration
        ├── add
        └── remove
```

---

## 🚀 Getting Started

### 🧱 Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- Windows, macOS or Linux terminal

---

## 🔨 Build the project

```bash
dotnet build -c Release
```

---

## 📦 Package as a global tool

Create the `.nupkg` file:

```bash
dotnet pack -c Release
```

This will generate a file like:

```
./bin/Release/geek-cli.<version>.nupkg
```

---

## 📥 Install the CLI globally

```bash
dotnet tool install -g geek-cli --add-source ./bin/Release
```

> You can now run `geek-cli` from anywhere in your terminal.

---

## 🔁 Update the CLI

If you made changes and re-packed:

```bash
dotnet tool update -g geek-cli --add-source ./bin/Release
```

---

## 🗑️ Uninstall the CLI

```bash
dotnet tool uninstall -g geek-cli
```

---

## 🧪 Example Usages

### Create a React Context

```bash
geek-cli rx context Auth --flat false
```

### Generate a React Native screen

```bash
geek-cli rx native screen Login --flat false --schema true --wrapper true
```

### Create a new EF Core migration

```bash
geek-cli db migration add InitSchema --project Booking --version 1.0 --issue 123 --init true --update true
```

---

## 🤝 Contributing

Pull requests are welcome! If you'd like to suggest features or report bugs, feel free to open an issue.

---

## 🧠 Built With

- [Spectre.Console](https://spectreconsole.net/)
- 💡 Designed for developer speed and consistency

---

## 📜 License

MIT License © 2025