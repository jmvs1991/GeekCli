namespace GeekCli.Commands.Dotnet.Wizard
{
    internal interface IDotnetWizard
    {
        int RunWizard(bool showBackOption = false);
    }
}
