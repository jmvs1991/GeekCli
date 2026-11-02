---
name: geek-db-stored-procedure-create
description: Complete an existing Geek SQL script with one stored procedure following the target project's conventions.
---

# Create one Geek stored procedure

Use this skill when the user asks to add an individual stored procedure to an existing SQL file.

## Required context

- Obtain the path to the existing SQL file previously generated through `geek-db-script`. Work in that file only. Do not invoke `geek-db-script`, generate scripts or synonyms, or execute SQL against a database.
- Obtain the existing migration that will integrate the procedure and synonym scripts. If it has not been supplied, request it and keep migration integration pending; do not create a migration or treat the integration as complete.
- Identify the schema, exact table name, operation (`INSERT`, `UPDATE`, `DELETE`, or a user-defined operation), table definition, complete primary key, table scope, and target-project precedents.
- Inspect the table and project precedents when available. Ask for missing information that affects the procedure. Keep the affected work pending when required information or precedents are unavailable; do not invent it or report it as complete.
- Name the procedure `SP_<EXACT_TABLE_NAME>_<OPERATION>` in the table's schema. Preserve the table's exact spelling, including its singular or plural form.
- Before editing, present the intended procedure shape and each proposed business validation with its reason. Ask the user to confirm which proposed validations to include, which business fields may be updated, and whether they want additional validations. Include only confirmed validations.

## Procedure rules

- All operations except `INSERT` receive the complete primary key for the table's scope. All operations except `DELETE` receive `@user`.
- `INSERT` does not receive the table's code field. After approved validations and immediately before insertion, generate it with `SP_PR_SEQUENCE_INSERT`, using the actual table and code-column names. Follow the target project's signature; ask for its precedent if unavailable.
- For `UPDATE`, ask which business fields may change. Update only the confirmed fields, and always set `[UPDATE_DATE] = @date`, `[UPDATE_DATEL] = @datel`, and `[UPDATE_USER] = @user`. Set `@date` from `SYSUTCDATETIME()` and calculate `@datel` as numeric `yyyyMMdd`, following the target project's precedent.
- A confirmed existence check in an `UPDATE` is optional, never a default. Do not copy business validations from examples without confirmation.
- `DELETE` deletes directly using the complete primary key, without a prior existence check, and has no returned `SELECT`.
- Every non-`DELETE` operation returns the affected record with a `SELECT` filtered by the complete primary key. Enumerate every table column explicitly; never use `SELECT *`.
- For readability, put each column on its own line in `INSERT` column lists, matching `VALUES` lists, and returned `SELECT` lists. Put each `UPDATE` assignment and each primary-key predicate condition on its own line. Use leading commas for lists and leading `AND` for predicate continuations, matching the supplied examples. Keep INSERT columns and values in matching order.
- For a user-defined operation, ask the user to define its behavior, parameters, affected data, and result before writing its SQL. Apply the complete primary key and `@user` rules above. If it updates table data, always set `[UPDATE_DATE] = @date`, `[UPDATE_DATEL] = @datel`, and `[UPDATE_USER] = @user` as described above. Apply the returned-record rule where applicable and clarify any uncertain behavior instead of guessing.

## SQL templates

Treat the files in `templates/` as source patterns, not ready-to-run SQL. Replace every `<...>` placeholder from the confirmed table definition and target-project precedents before adding the procedure to the supplied SQL file. Omit empty optional column groups and their adjacent separators so the resulting lists remain valid.

### INSERT template

Use `templates/insert-procedure.sql.txt`. Keep only confirmed business validations; do not copy the quotation-specific validations from the supplied example.

### UPDATE template

Use `templates/update-procedure.sql.txt`. Include only the business assignments confirmed by the user; the template omits an existence check by default.

### DELETE template

Use `templates/delete-procedure.sql.txt`. Supply the complete primary key and scope parameters; do not add `@user`, a pre-delete existence check, or a returned `SELECT`.

### Migration integration template

Use `templates/migration-integration.cs.txt` as an adaptation pattern for the supplied existing migration, not as a replacement migration class. Resolve the procedure schema, scripts folder, and filenames from the supplied artifacts and target-project precedents. In `Up`, integrate procedure creation with `CreateOrAlterSpFromSqlFile` and synonym creation with `CreateOrAlterSynomFromSqlFile` using the `dbo` schema. In `Down`, integrate their respective removal scripts with `RollbackSpFromSqlFile` and `RollbackSynomFromSqlFile`, in procedure-then-synonym order. Use `CREATE_<SP>.sql` and `ROLLBACK_CREATE_<SP>.sql`; the procedure and synonym scripts share each filename in their respective directories.

Preserve the migration's attributes, metadata, and unrelated operations. Reuse existing schema and folder constants where applicable, and do not add duplicate declarations or matching migration calls. If a required script or migration is missing, request it and leave integration pending; do not generate it or report integration complete.

## Error handling and completion

- Use the agreed `BEGIN TRY`/`BEGIN CATCH` body for every operation. In `CATCH`, declare `@RC` and the error fields; populate message, line, number, severity, state, and procedure with the corresponding `ERROR_*` functions and the agreed `ISNULL` defaults. Call `SP_PR_ERRORS_INSERT` only when `@severity <> 12`, preserving its argument order and target-project scope, then propagate with `RAISERROR(@error_message, @severity, @statusCatch)`.
- Inspect the target project's error-procedure signature and representative procedure. If the signature, argument order, scope, or required defaults cannot be established, ask for the missing precedent and leave that procedure pending.
- Use valid T-SQL comments (`--` or `/* ... */`), not `//` comments.
- Add the procedure to the supplied existing file only after required behavior and validations are confirmed. Summarize the file and procedure prepared, the confirmed validations, and any pending questions. Do not claim pending work is complete.
