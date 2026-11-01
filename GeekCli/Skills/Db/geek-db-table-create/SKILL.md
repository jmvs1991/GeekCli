---
name: geek-db-table-create
description: Prepare a Geek table migration from a complete definition or the scope-based base structure.
---

# Create a Geek database table

Use this skill to prepare a table in a migration that has already been created. Use `MigrationBuilder` and the target project's Schema Builder column extensions; do not generate table SQL scripts with `geek-db-script`.

## Gather the table definition

Ask only for missing or ambiguous information:

- Table name and schema.
- The actual name of the primary business code column. Never use the illustrative `CODTEST` as a default.
- Scope: corporation + company + code, corporation + code, or code only.
- Whether the user wants the base structure or a complete definition. In complete mode, gather the additional columns and their types, nullability, defaults, and constraints from the request or ask; do not invent them.

The base structure is complete for the selected scope and requires no business columns. In complete mode, put requested additional columns strictly after the code column and before the audit columns. Use each actual column name with `CodeColumn("<actual-name>")` where applicable.

## Prepare the migration

Use private constants named `__TABLE_NAME__` and `__SCHEMA__`. In the bundled `templates/table-migration.cs.txt`, replace only the `__TABLE_NAME_VALUE__` and `__SCHEMA_VALUE__` string values with the confirmed table name and schema; keep the constant identifiers unchanged. Preserve existing migration content and adapt the namespace, class name, and surrounding project conventions from nearby migrations.

Keep columns in this order: `ID`; applicable `CORPORATION` and `COMPANY`; code; requested additional columns; `CREATION_DATE`, `CREATION_DATEL`, `UPDATE_DATE`, `UPDATE_DATEL`, `CREATE_USER`, `UPDATE_USER`. The template starts with code-only scope; add the indicated scope columns immediately before the code column when selected. Use the matching `IdColumn`, `CorporationColumn`, `CompanyColumn`, `CodeColumn`, `CreationDateColumn`, `CreationDatelColumn`, `UpdateDateColumn`, `UpdateDatelColumn`, `CreateUserColumn`, and `UpdateUserColumn` extensions. Inspect the target project for exact types and established conventions when a required detail is not specified here.

Create primary key `PK_<table>` over the selected scope columns followed by code, and unique index `IX_<table>_ID` on `ID` without a filter. Add the table synonym with `CreateSynom`. In `Down`, call `DropSynom` before `DropTable`.

## Review and report

Review the prepared `Up` and `Down` against the confirmed mode, scope, column order, key, index, and synonym. Do not claim database changes were applied. If required information or a project convention remains unclear, leave preparation pending and ask rather than guessing. The bundled base structure is `templates/table-migration.cs.txt`.
