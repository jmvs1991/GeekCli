namespace GeekCliServices.Services.Ngx.Models
{
    public sealed record NgxCommand
    {
        public string Name { get; init; }

        public NgxCommand(string name)
        {
            Name = name;
        }
    }
}
