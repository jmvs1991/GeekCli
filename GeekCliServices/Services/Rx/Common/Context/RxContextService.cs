using GeekCliServices.Services.Rx.Models;

namespace GeekCliServices.Services.Rx.Common.Context
{
    public sealed class RxContextService : RxServiceBase<RxCommand>, IRxContextService
    {
        protected override void Execute(string targetPath, string name, RxCommand command)
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
