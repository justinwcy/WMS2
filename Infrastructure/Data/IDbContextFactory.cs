using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public interface IWmsDbContextFactory<T> where T : DbContext
    {
        T CreateDbContext();
    }
}
