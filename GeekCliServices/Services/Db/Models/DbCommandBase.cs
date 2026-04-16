namespace GeekCliServices.Services.Db.Models
{
    public abstract record DbCommandBase
    {
        public string ProjectName { get; init; }

        public bool Init { get; init; }
        
        protected DbCommandBase(string projectName, bool init)
        {
            ProjectName = projectName;
            Init = init;
        }
    }
}
