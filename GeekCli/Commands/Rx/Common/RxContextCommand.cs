namespace GeekCli.Commands.Rx.Common
{
    class RxContextCommand : RxCommandBase<RxContextSettings>
    {
        protected override void Execute(string targetPath, string name, RxContextSettings settings)
        {
            CreateFile(targetPath, $"{name}.actions.ts", "");
            CreateFile(targetPath, $"{name}.context.tsx", "");
            CreateFile(targetPath, $"{name}.hook.ts", "");
            CreateFile(targetPath, $"{name}.provider.tsx", "");
            CreateFile(targetPath, $"{name}.reducer.ts", "");
            CreateFile(targetPath, $"{name}.state.ts", "");
        }
    }
}
