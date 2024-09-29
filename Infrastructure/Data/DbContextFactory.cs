using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public class DbContextFactory<T>(IServiceProvider serviceProvider) : IWmsDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
