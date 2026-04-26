using System.ComponentModel;
using GeekCliServices.Services.Rx.Models;
using GeekCliServices.Services.Rx.Native.Module;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class RxNativeModuleTool : McpToolBase
    {
        private readonly IRxNativeModuleService _service;

        public RxNativeModuleTool(IRxNativeModuleService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Scaffolds a React Native module folder structure.")]
        public McpToolResult RxNativeModule(string name, bool flat = false)
            => Capture(() => _service.RunProcess(string.Empty, new RxCommand(name, flat)));
    }
}
