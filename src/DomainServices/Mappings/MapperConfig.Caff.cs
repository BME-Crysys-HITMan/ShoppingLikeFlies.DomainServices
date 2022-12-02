﻿using AutoMapper;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;
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
                .ForMember(dest => dest.Tags, m => m.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Year, m => m.MapFrom(src => src.CreationDateTime.Year))
                .ForMember(dest => dest.Month, m => m.MapFrom(src => src.CreationDateTime.Month))
                .ForMember(dest => dest.Day, m => m.MapFrom(src => src.CreationDateTime.Day))
                .ForMember(dest => dest.Hour, m => m.MapFrom(src => src.CreationDateTime.Hour))
                .ForMember(dest => dest.Minute, m => m.MapFrom(src => src.CreationDateTime.Minute));

            mapperConfigurationExpression.CreateMap<CreateCaffContractDTO, Caff>()
                .ForMember(dest => dest.CreationDateTime, m => m.MapFrom(src => new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0)))
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ForMember(dest => dest.Comments, m => m.Ignore());

            return mapperConfigurationExpression;
        }
    }
}
