namespace GeekCliServices.Services.Dotnet.Models
{
    public sealed record DotnetTemplateOption(string Option, string Value)
    {
        public static DotnetTemplateOption ProjectName(string value) => new("--projectName", value);

        public static DotnetTemplateOption DbSchema(string value) => new("--dbSchema", value);

        public static DotnetTemplateOption ContextName(string value) => new("--contextName", value);

        public static DotnetTemplateOption CodeField(string value) => new("--codeField", value);
    }
}
