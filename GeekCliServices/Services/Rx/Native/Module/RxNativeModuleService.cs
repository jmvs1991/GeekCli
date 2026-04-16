using GeekCliServices.Services.Rx.Models;

namespace GeekCliServices.Services.Rx.Native.Module
{
    public sealed class RxNativeModuleService : RxServiceBase<RxCommand>, IRxNativeModuleService
    {
        protected override void Execute(string targetPath, string name, RxCommand command)
        {
            CreateSubfolder(targetPath, "Screens");
            CreateSubfolder(targetPath, "Core");
            CreateSubfolder(targetPath, "Modals");
        }
    }
}
