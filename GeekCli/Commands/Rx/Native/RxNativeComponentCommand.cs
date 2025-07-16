namespace GeekCli.Commands.Rx.Native
{
    class RxNativeComponentCommand : RxCommandBase<RxNativeComponentSettings>
    {
        protected override void Execute(string targetPath, string name, RxNativeComponentSettings settings)
        {
            CreateFile(targetPath, $"{name}.component.tsx", $"");
            CreateFile(targetPath, $"{name}.style.tsx", $"");
        }
    }
}
