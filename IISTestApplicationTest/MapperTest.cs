using IISTestApplication;
using Xunit;

namespace IISTestApplicationTest
{
    public class MapperTest
    {
        [Fact]
        public void Test1()
        {
            var mapperConfiguration =  MapperProfile.GetMapperConfiguration();
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
