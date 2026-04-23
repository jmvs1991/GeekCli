namespace GeekCliServices.Services.Db.Scripts
{
    public enum DbScriptType
    {
        Query,
        ModifyStoredProcedure,
        CreateStoredProcedure,
        ModifyTable,
        CreateTable,
        CreateView,
        ModifyView
    }
}
