using System.ComponentModel;
using GeekCliServices.Services.Rx.Models;
using GeekCliServices.Services.Rx.Native.Component;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class RxNativeComponentTool : McpToolBase
    {
        private readonly IRxNativeComponentService _service;

        public RxNativeComponentTool(IRxNativeComponentService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Scaffolds a React Native component file set.")]
        public McpToolResult RxNativeComponent(string name, bool flat = false)
            => Capture(() => _service.RunProcess(string.Empty, new RxCommand(name, flat)));
    }
}
