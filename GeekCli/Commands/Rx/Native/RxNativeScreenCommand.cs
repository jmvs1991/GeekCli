namespace GeekCli.Commands.Rx.Native
{
    class RxNativeScreenCommand : RxCommandBase<RxNativeScreenSettings>
    {
        protected override void Execute(string targetPath, string name, RxNativeScreenSettings settings)
        {
            CreateFile(targetPath, $"{name}.screen.tsx", $"");
            CreateFile(targetPath, $"{name}.style.tsx", $"");

            if (settings.Schema)
            {
                CreateFile(targetPath, $"{name}.schema.tsx", $"// {name} schema file");
            }

            if (settings.Wrapper)
            {
                CreateFile(targetPath, $"{name}.wrapper.tsx", $"// {name} wrapper component");
            }
        }
    }
}
