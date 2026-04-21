using System.ComponentModel;
using GeekCliServices.Services.Rx.Common.Context;
using GeekCliServices.Services.Rx.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class RxContextTool : McpToolBase
    {
        private readonly IRxContextService _service;

        public RxContextTool(IRxContextService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Scaffolds a React context file set.")]
        public McpToolResult RxContext(string name, bool flat = false)
            => Capture(() => _service.RunProcess(string.Empty, new RxCommand(name, flat)));
    }
}
