namespace GeekCliServices.Services
{
    public interface ICommandService<TCommand>
    {
        int RunProcess(string processToRun, TCommand command);
    }
}
