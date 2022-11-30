using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Mappings
{
    public static partial class MapperConfig
    {
        public static IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(config =>
            {
                config.ConfigureCaff();
                config.ConfigureCaffTag();
                config.ConfigureCaffToTag();
                config.ConfigureComment();
            });
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }

    }
}
