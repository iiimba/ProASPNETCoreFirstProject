using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        Task<Product[]> GetProductsAsync();
    }
}
