using IISTestApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _context;

        public TransactionController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CancellationToken token)
        {
            await _context.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, token);

            try
            {
                var order1 = new Order { Id = 1, Name = "Order3" };
                _context.Entry(order1).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var order2 = new Order { Id = 3, Name = "Order4" };
                _context.Entry(order2).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync(token);
            }
            finally
            {
                await _context.Database.RollbackTransactionAsync(token);
            }

            return Ok();
        }
    }
}
