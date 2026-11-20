using GeekCli.Commands.EditorConfig;
using GeekCli.Commands.EditorConfig.Wizard;
using Spectre.Console.Cli;

namespace GeekCli.Branches
{
    internal static class EditorConfigBranchExtension
    {
        public static IBranchConfigurator AddEditorConfig(this IConfigurator configurator)
        {
            return configurator.AddBranch("editorconfig", editorConfig =>
            {
                editorConfig.SetDescription("EditorConfig templates for C#, Angular, and React.");
                editorConfig.SetDefaultCommand<EditorConfigWizardCommand>();

                editorConfig.AddCommand<AddEditorConfigCommand>("add")
                            .WithDescription("Adds a .editorconfig file to a project directory.");
            });
        }
    }
}
