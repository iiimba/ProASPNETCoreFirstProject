using System.Collections.Generic;

namespace ProASPNETCoreFirstProject.Models
{
    public interface IDataSource
    {
        IEnumerable<Product> Products { get; }
    }
}
