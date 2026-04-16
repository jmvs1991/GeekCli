using GeekCli.Commands.Db.Migrations.Add;
using GeekCli.Commands.Db.Migrations.Remove;
using GeekCli.Commands.Db.Migrations.Rollback;
using GeekCli.Commands.Db.Wizard;
using Spectre.Console.Cli;

namespace GeekCli.Branches
{
    internal static class DbBranchExtension
    {
        public static IBranchConfigurator AddDB(this IConfigurator configurator)
        {
            return configurator.AddBranch("db", db =>
            {
                db.SetDescription("Database utilities");
                db.SetDefaultCommand<DbWizardCommand>();

                db.AddBranch("migration", migration =>
                {
                    migration.AddCommand<DbAddMigrationCommand>("add")
                             .WithDescription("Creates a new database migration and related files.");

                    migration.AddCommand<DbRemoveMigrationCommand>("remove")
                             .WithDescription("Removes the last generated database migration.");

                    migration.AddCommand<DbRollbackMigrationCommand>("rollback")
                             .WithDescription("Rolls back the database to the previous migration state.");
                });
            });
        }
    }
}
