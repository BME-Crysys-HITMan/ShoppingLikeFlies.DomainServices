using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingLikeFiles.DomainServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DomainServices.UnitTest
{
    public class DependencyTests
    {
        [Fact]
        public void Test1()
        {
            IConfiguration cfg = getConfig();
            IServiceCollection services = new ServiceCollection();

            services.AddCaffProcessor(cfg);

            var provider = services.BuildServiceProvider();


            var service = provider.GetRequiredService<ICaffService>();
            var data = provider.GetRequiredService<IDataService>();
            var payment = provider.GetRequiredService<IPaymentService>();
        }

        [Fact]
        public void Test2()
        {
            IConfiguration cfg = getConfig();
            IServiceCollection services = new ServiceCollection();

            services.AddCaffProcessor(
                x => { x.GeneratorDir = "."; x.Validator = "CAFF_Processor.exe"; },
                y => { y.ShouldUploadToAzure = false; y.DirectoryPath = "generator"; },
                cfg);

            var provider = services.BuildServiceProvider();


            var service = provider.GetRequiredService<ICaffService>();
            var data = provider.GetRequiredService<IDataService>();
            var payment = provider.GetRequiredService<IPaymentService>();
        }

        private IConfiguration getConfig()
        {
            Dictionary<string, string> config = new()
            {
                { "ConnectionStrings:DefaultConnection" , "asd" },
            };
            return new ConfigurationBuilder().AddInMemoryCollection(config).Build();
        }
    }
}
