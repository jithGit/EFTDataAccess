using System.Data.Entity;

namespace EFDataAccess
{
    public interface IDbContextFactory
    {
        DbContext GetContext();
    }
}