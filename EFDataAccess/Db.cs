using System.Data.Entity;
using EFDataAccess.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace EFDataAccess
{
    public class Db : DbContext
    {
        public Db()
        {
            //Database.SetInitializer<Db>(new CreateDatabaseIfNotExists<Db>());
            //Database.SetInitializer<Db>(new DropCreateDatabaseIfModelChanges<Db>());
            //this.Database.Initialize(true);

#if DEBUG
            Database.SetInitializer<Db>(new DropCreateIfChangeInitializer());
            this.Database.Initialize(true);


#else
            Database.SetInitializer<Db> (new CreateInitializer ());
#endif
            //this.Configuration.ProxyCreationEnabled = true;
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbBuilder)
        {
            //dbBuilder.Entity< >().HasMany(r => r. ).WithMany(o => o. ).Map(f =>
            //{
            //    f.MapLeftKey(" ");
            //    f.MapRightKey(" ");
            //});
            base.OnModelCreating(dbBuilder);
            // modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }

        //Custom Seed for initial data
        public void JMSSeed(Db context)
        {

            this.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<Db>
        {
            protected override void Seed(Db context)
            {
                context.JMSSeed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<Db>
        {
            protected override void Seed(Db context)
            {
                context.JMSSeed(context);
                base.Seed(context);
            }
        }


    }


}