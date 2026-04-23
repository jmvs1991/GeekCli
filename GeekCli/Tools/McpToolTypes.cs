namespace GeekCli.Tools
{
    internal static class McpToolTypes
    {
        public static readonly Type[] All =
        [
            typeof(DotnetListTool),
            typeof(DotnetDtoTool),
            typeof(DotnetResourceTool),
            typeof(DotnetCacheTool),
            typeof(DotnetSpTool),
            typeof(DotnetReadTool),
            typeof(DotnetWriteTool),
            typeof(DotnetControllerTool),
            typeof(DotnetServiceTool),
            typeof(DbMigrationAddTool),
            typeof(DbMigrationRemoveTool),
            typeof(DbMigrationRollbackTool),
            typeof(DbScaffoldTool),
            typeof(DbScriptTool),
            typeof(NgxComponentTool),
            typeof(NgxPageTool),
            typeof(RxContextTool),
            typeof(RxNativeComponentTool),
            typeof(RxNativeModuleTool),
            typeof(RxNativeScreenTool)
        ];
    }
}
