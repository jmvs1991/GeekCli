using System.ComponentModel;
using GeekCliServices.Services.Rx.Native.Screen;
using GeekCliServices.Services.Rx.Native.Screen.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class RxNativeScreenTool : McpToolBase
    {
        private readonly IRxNativeScreenService _service;

        public RxNativeScreenTool(IRxNativeScreenService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Scaffolds a React Native screen with optional schema and wrapper files.")]
        public McpToolResult RxNativeScreen(string name, bool flat = false, bool schema = false, bool wrapper = false)
            => Capture(() => _service.RunProcess(string.Empty, new RxScreenCommand(name, flat, schema, wrapper)));
    }
}
