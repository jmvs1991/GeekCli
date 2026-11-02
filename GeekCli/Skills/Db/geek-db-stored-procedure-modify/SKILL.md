---
name: geek-db-stored-procedure-modify
description: Modify an existing stored procedure in its generated migration and SQL files.
---

# Modify one stored procedure

Use this skill to prepare a requested change to an existing stored procedure using an already generated migration and its existing Up and Down SQL files. Do not generate migrations or SQL files, or execute SQL.

## Required inputs and history

- Obtain the generated migration, its Up and Down SQL file paths, and the requested procedure change. Confirm their relationship and identify the procedure, schema, and SQL folder from the files and project conventions.
- Trace applicable migration history and referenced SQL to establish the procedure definition immediately before this change. Inspect the latest applicable definition and identify its source when presenting the proposed reversal.
- If the migration, either SQL file, requested change, or conclusive previous definition is missing or ambiguous, explain what is needed and ask for clarification. Leave affected work pending; do not infer SQL or report it finished.

## Propose and prepare

- Present the proposed procedure change and the exact previous definition that Down will restore, with the migration or SQL file that establishes it. Ask the user to confirm before editing.
- After confirmation, update the existing Up SQL file with the requested procedure definition and the existing Down SQL file with the definition from immediately before the change. Preserve unrelated SQL and migration operations. Down must not drop the procedure or repeat the new definition.
- Adapt `templates/integration.cs.txt` into the supplied generated migration. Replace its placeholders with the actual schema, folder, and existing Up and Down SQL filenames. Keep the private string constant names `__SCHEMA__` and `__FOLDER__`; reuse equivalent existing constants when applicable. Add the calls without replacing unrelated operations or duplicating matching declarations.
- Review both SQL definitions, their historical basis, and the migration references. Report prepared files and any remaining questions accurately; do not claim pending work is complete.

## Integration template

The bundled `templates/integration.cs.txt` is an adaptation pattern, not a replacement migration class. Preserve the generated migration's attributes, metadata, and unrelated operations when integrating it.
