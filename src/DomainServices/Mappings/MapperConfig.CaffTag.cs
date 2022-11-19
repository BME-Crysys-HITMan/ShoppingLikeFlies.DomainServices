using AutoMapper;
using DataAccessLogic.Entities;
using ShoppingLikeFiles.DomainServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Mappings
{
    public static partial class MapperConfig
    {
        public static IMapperConfigurationExpression ConfigureCaffTag(this IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.CreateMap<CaffTag, CaffTagDTO>();

            return mapperConfigurationExpression;
        }
    }
}
