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
        public static IMapperConfigurationExpression ConfigureCaff(this IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.CreateMap<Caff, CaffDTO>()
                .ForMember(dest=> dest.Tags, m => m.MapFrom(src=> src.Tags))
                .ForMember(dest => dest.Year, m=> m.MapFrom(src=> src.CreationDateTime.Year))
                .ForMember(dest => dest.Month, m=> m.MapFrom(src=> src.CreationDateTime.Month))
                .ForMember(dest => dest.Day, m=> m.MapFrom(src=> src.CreationDateTime.Day))
                .ForMember(dest => dest.Hour, m=> m.MapFrom(src=> src.CreationDateTime.Hour))
                .ForMember(dest => dest.Minute, m=> m.MapFrom(src=> src.CreationDateTime.Minute));

            return mapperConfigurationExpression;
        }
    }
}
