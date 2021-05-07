using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;
        private int pageSize = 4;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<Product[]> GetProductsAsync(int page = 1)
        {
            return await context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(p => p.ProductID)
                .ToArrayAsync();
        }
    }
}
