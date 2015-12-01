using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EFDataAccess.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Test
{
    public class EFDataAcessTest
    {
        private IWindsorContainer iocContainer;
        IDbContextFactory iocCtxFactory;

        [SetUp]
        public void initializer()
        {
            iocContainer = new WindsorContainer();

            iocContainer.Register(Component.For<IDbContextFactory>().ImplementedBy<DbContextFactory>().LifeStyle.Singleton);
            //iocContainer.Register(Component.For<IDbContextFactory>().ImplementedBy<DbContextFactory>().LifeStyle.Transient);
           
        }

        [Test]
        public void TestCustomerCreation()
        {
            //Creating customer
            

            var Customer = new Customer() { CustomerName = "Customer 1", Telephome = "78-676-121212", Sites = new List<Site>() };
            Customer.Sites.Add(new Site() { Address = "Site 1", PostCode = "001", SiteNumber = "ST01" });
            Customer.Sites.Add(new Site() { Address = "Site 2", PostCode = "002", SiteNumber = "ST02" });

            iocCtxFactory = iocContainer.Resolve<IDbContextFactory>();
            var iocDBContext = iocCtxFactory.GetContext();

            //adding customer to database
            var sotredCustomer = iocDBContext.Set<Customer>().Add(Customer);
            iocDBContext.SaveChanges();
            var customerId = sotredCustomer.Id;

            //Test
            var nonIoCContext = new DbContextFactory().GetContext();

            var customerFrom_IOC_Context = iocCtxFactory.GetContext().Set<Customer>().Where(c => c.Id == customerId).SingleOrDefault();

            var customerNon_IOC_Context = nonIoCContext.Set<Customer>().Where(c => c.Id == customerId).SingleOrDefault();

            Assert.IsNull(customerNon_IOC_Context.Sites);

            //Expecting empty but having values if IOC lifestyle is singleton or PerWebRequest :(
            //transient is working as expected
            Assert.IsNull(customerFrom_IOC_Context.Sites);
        }

    }
}
