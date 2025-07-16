namespace GeekCli.Commands.Rx.Native
{
    class RxNativeModuleCommand : RxCommandBase<RxNativeModuleSettings>
    {
        protected override void Execute(string targetPath, string name, RxNativeModuleSettings settings)
        {
            CreateSubfolder(targetPath, "Screens");
            CreateSubfolder(targetPath, "Core");
            CreateSubfolder(targetPath, "Modals");
        }
    }
}
