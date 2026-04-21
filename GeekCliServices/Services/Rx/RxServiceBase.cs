using GeekCliServices.Services.Rx.Models;

namespace GeekCliServices.Services.Rx
{
    public abstract class RxServiceBase<TCommand> : ScaffoldingServiceBase<TCommand> where TCommand : RxCommand
    {
        public override int RunProcess(string processToRun, TCommand command)
        {
            string name = _textInfo.ToTitleCase(command.Name.ToLower());
            string targetPath = Path.Combine(_basePath, command.Name);

            if (!command.Flat)
            {
                CreateDirectory(targetPath);
            }

            Execute(targetPath, name, command);

            CommandOutput.Info($"Files created successfully in: {targetPath}");

            return 0;
        }
    }
}
