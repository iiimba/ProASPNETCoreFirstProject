using System.Threading.Tasks;

namespace CreditCardApplications
{
    public interface IDemoInterfaceAsync
    {
        Task StartAsync();

        Task<int> StopAsync();
    }
}
