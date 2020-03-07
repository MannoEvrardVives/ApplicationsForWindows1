using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VivesRental.Model;
using VivesRental.Repository.Exceptions;

namespace VivesRental.Repository.Core
{
    public class VivesRentalDbContext: DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<RentalItem> RentalItems { get; set; }
        public DbSet<RentalOrder> RentalOrders { get; set; }
        public DbSet<RentalOrderLine> RentalOrderLines { get; set; }
        public DbSet<User> Users { get; set; }

        public VivesRentalDbContext(string connectionString) : base(connectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Makes sure nothing gets initialized. We do this ourselves
            Database.SetInitializer<VivesRentalDbContext>(null);

            //We manage our own deletes, not EF
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Table names are not Plural! (collections are, however)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }

    
}
