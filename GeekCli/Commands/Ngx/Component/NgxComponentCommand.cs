namespace GeekCli.Commands.Ngx.Component
{
    class NgxComponentCommand : NgxCommandBase<NgxComponentSettings>
    {
        protected override string BuildArgs(NgxComponentSettings settings)
        {
            return $"g c {settings.Name} --prefix ngx";
        }
    }
}
