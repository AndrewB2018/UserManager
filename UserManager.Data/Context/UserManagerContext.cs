using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UserManager.DataEntities.Models;

namespace CPUserManager.DAL
{
    public class UserManagerContext : DbContext
    {

        public UserManagerContext() : base("UserManagerContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}