---
name: geek-db-table-alter
description: Prepare an existing table alteration in an already generated migration.
---

# Alter an existing database table

Use this skill to prepare an alteration in an existing migration. Do not generate migrations, SQL scripts, or execute database changes.

## Add a column

- Obtain the existing migration, the requested column definition, and the already generated Up SQL script. Confirm that the script belongs to the migration and targets the requested table and schema.
- If the migration, script, table, schema, or column details are missing or ambiguous, ask for clarification and leave the affected work pending. Do not invent column details or forward SQL.
- Integrate the existing Up SQL script into the migration using the target project's established migration convention. Leave its forward SQL for the user to complete; do not write or silently complete that SQL.
- In `Down`, reverse the addition with `MigrationBuilder.DropColumn`, using the confirmed column and table names. Preserve migration attributes and unrelated operations.
- Keep the alteration pending until the user completes the forward SQL. Report the files and remaining work accurately.

## Other MigrationBuilder alterations

- For requested alterations supported by `MigrationBuilder`, prepare both `Up` and `Down` with the matching builder operations; do not require SQL scripts. For example, add a CHECK constraint with `AddCheckConstraint` and reverse it with `DropCheckConstraint`.
- Obtain the exact table, schema, constraint name, and predicate from the request or target-project precedents. Preserve existing migration content.
- If the operation, required details, or a safe reversal is ambiguous or unsupported, ask for clarification and leave the affected work pending. Do not substitute raw SQL or invent a reversal.
- Review both directions and report the prepared migration and any remaining work.
