using IISTestApplication.Models;
using IISTestApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IISTestApplication.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly DataContext _context;

        public PeopleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Person[]> GetAllAsync()
        {
            var peoples = await _context.People.ToArrayAsync();

            return peoples;
        }
    }
}
