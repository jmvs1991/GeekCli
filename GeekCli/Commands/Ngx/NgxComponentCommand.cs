namespace GeekCli.Commands.Ngx
{
    class NgxComponentCommand : NgxCommandBase<NgxComponentSettings>
    {
        protected override string BuildArgs(NgxComponentSettings settings)
        {
            return $"g c {settings.Name} --prefix ngx";
        }
    }
}
