namespace GeekCliServices.Services.EditorConfig.Models
{
    public sealed record EditorConfigCommand(string ProjectPath, string ProjectType)
    {
        public const string DefaultProjectPath = ".";
        public const string CSharp = "csharp";
        public const string Angular = "angular";
        public const string React = "react";
    }
}
