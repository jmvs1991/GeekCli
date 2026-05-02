namespace GeekCliServices.Services.Dotnet.Models
{
    public sealed record DotnetTemplateOption(string Option, string Value)
    {
        public static DotnetTemplateOption ProjectName(string value) => new("--projectName", value);

        public static DotnetTemplateOption DbSchema(string value) => new("--dbSchema", value);

        public static DotnetTemplateOption ContextName(string value) => new("--contextName", value);

        public static DotnetTemplateOption CodeField(string value) => new("--codeField", value);

        public static DotnetTemplateOption ServiceInterface(string value) => new("--serviceInterface", value);

        public static DotnetTemplateOption DtoName(string value) => new("--dtoName", value);

        public static DotnetTemplateOption ResponseName(string value) => new("--responseName", value);

        public static DotnetTemplateOption ContextTestBase(string value) => new("--contextTestBase", value);

        public static DotnetTemplateOption Endpoint(string value) => new("--endpoint", value);
    }
}
