﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Mappings
{
    public static partial class MapperConfig
    {
        public static IMapperConfigurationExpression ConfigureComment(this IMapperConfigurationExpression mapperConfigurationExpression)
        {

            mapperConfigurationExpression.CreateMap<Comment, CommentDTO>();


            //mapperConfigurationExpression.CreateMap<User, UserDto>()
            //    .ForMember(dest => dest.Id, m => m.MapFrom(src => src.CaffTagId))
            //    .ForMember(dest => dest.Tag, m => m.MapFrom(src => src.CaffTag.Id));

            return mapperConfigurationExpression;
        }
    }
}