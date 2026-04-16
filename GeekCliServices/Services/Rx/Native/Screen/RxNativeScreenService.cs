using GeekCliServices.Services.Rx.Native.Screen.Models;

namespace GeekCliServices.Services.Rx.Native.Screen
{
    public sealed class RxNativeScreenService : RxServiceBase<RxScreenCommand>, IRxNativeScreenService
    {
        protected override void Execute(string targetPath, string name, RxScreenCommand command)
        {
            CreateFile(targetPath, $"{name}.screen.tsx", $"");
            CreateFile(targetPath, $"{name}.style.tsx", $"");

            if (command.Schema)
            {
                CreateFile(targetPath, $"{name}.schema.tsx", $"// {name} schema file");
            }

            if (command.Wrapper)
            {
                CreateFile(targetPath, $"{name}.wrapper.tsx", $"// {name} wrapper component");
            }
        }
    }
}
