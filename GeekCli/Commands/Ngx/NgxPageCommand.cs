namespace GeekCli.Commands.Ngx
{
    class NgxPageCommand : NgxCommandBase<NgxPageSettings>
    {
        protected override string BuildArgs(NgxPageSettings settings)
        {
            return $"g c {settings.Name} --type page";
        }
    }
}
