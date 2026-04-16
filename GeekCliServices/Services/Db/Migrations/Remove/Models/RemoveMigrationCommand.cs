using GeekCliServices.Services.Db.Models;

namespace GeekCliServices.Services.Db.Migrations.Remove.Models
{
    public sealed record RemoveMigrationCommand : DbCommandBase
    {
        public RemoveMigrationCommand(string projectName, bool init) : base(projectName, init)
        {
        }
    }
}
