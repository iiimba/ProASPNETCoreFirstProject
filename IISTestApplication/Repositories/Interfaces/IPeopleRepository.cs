using IISTestApplication.Models;
using System.Threading.Tasks;

namespace IISTestApplication.Repositories.Interfaces
{
    public interface IPeopleRepository
    {
        Task<Person[]> GetAllAsync();
    }
}
