using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.DataBaseConnection;

namespace Services.Factories
{
    public class ChatClientDbContextFactory : IDesignTimeDbContextFactory<DbContextBase>
    {
        protected const string SERVER = "localhost";
        protected const string DATABASE = "tcpchat";
        protected const string USER = "root";
        protected const string PASSWORD = "";

        protected static readonly string CONNECTION_STRING = string.Format("Server={0};Database={1};User Id={2};Password={3};",
               SERVER, DATABASE, USER, PASSWORD);

        public DbContextBase CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<DbContextBase>();
            options.UseMySql(CONNECTION_STRING);

            return new DbContextBase(options.Options);
        }
    }
}
