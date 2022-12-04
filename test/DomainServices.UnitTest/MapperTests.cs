using AutoMapper;
using ShoppingLikeFiles.DomainServices.Mappings;

namespace DomainServices.UnitTest
{
    public class MapperTests
    {
        [Fact]
        public void Test1()
        {
            var config = new MapperConfiguration(x => x.AddProfile<DomainServiesProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
