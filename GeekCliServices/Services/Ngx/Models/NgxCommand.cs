namespace GeekCliServices.Services.Ngx.Models
{
    public sealed record NgxCommand
    {
        public string Name { get; init; }
        public bool Nx { get; init; }
        public string? Project { get; init; }
        public string? Folder { get; init; }
        public string? Module { get; init; }

        public NgxCommand(string name, bool nx = false, string? project = null, string? folder = null, string? module = null)
        {
            Name = name;
            Nx = nx;
            Project = project;
            Folder = folder;
            Module = module;
        }
    }
}
