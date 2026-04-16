using GeekCliServices.Services.Rx.Models;

namespace GeekCliServices.Services.Rx.Native.Component
{
    public sealed class RxNativeComponentService : RxServiceBase<RxCommand>, IRxNativeComponentService
    {
        protected override void Execute(string targetPath, string name, RxCommand command)
        {
            CreateFile(targetPath, $"{name}.component.tsx", $"");
            CreateFile(targetPath, $"{name}.style.tsx", $"");
        }
    }
}
