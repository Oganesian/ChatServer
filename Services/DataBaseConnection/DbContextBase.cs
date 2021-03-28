using AccountAndConnection;
using Microsoft.EntityFrameworkCore;
//using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Services.DataBaseConnection
{
    public class DbContextBase : DbContext
    {
        public DbSet<BaseAccount> Accounts { get; set; }

        public DbContextBase(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<BaseAccount>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Email).IsRequired();
            //});
        }
    }
}
