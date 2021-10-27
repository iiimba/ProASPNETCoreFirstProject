using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CreditCardApplications.Tests
{
    public class DemoInterfaceAsyncShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            var mock = new Mock<IDemoInterfaceAsync>();

            mock.Setup(x => x.StartAsync()).Returns(Task.CompletedTask);
            //mock.Setup(x => x.StopAsync()).Returns(Task.FromResult(42));
            mock.Setup(x => x.StopAsync()).ReturnsAsync(42);
        }
    }
}
