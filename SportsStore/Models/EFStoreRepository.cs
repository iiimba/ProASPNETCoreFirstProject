using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Product[]> GetProductsAsync()
        {
            return await context.Products.ToArrayAsync();
        }
    }
}
