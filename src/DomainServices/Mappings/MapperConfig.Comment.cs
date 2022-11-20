using System;
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
            mapperConfigurationExpression.CreateMap<Comment, CaffDTO>()
               .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Caff.Id))
               .ForMember(dest => dest.FilePath, m => m.MapFrom(src => src.Caff.FilePath))
               .ForMember(dest => dest.Creator, m => m.MapFrom(src => src.Caff.Creator))
               .ForMember(dest => dest.Tags, m => m.MapFrom(src => src.Caff.Tags))
               .ForMember(dest => dest.Year, m => m.MapFrom(src => src.Caff.CreationDateTime.Year))
               .ForMember(dest => dest.Month, m => m.MapFrom(src => src.Caff.CreationDateTime.Month))
               .ForMember(dest => dest.Day, m => m.MapFrom(src => src.Caff.CreationDateTime.Day))
               .ForMember(dest => dest.Hour, m => m.MapFrom(src => src.Caff.CreationDateTime.Hour))
               .ForMember(dest => dest.Minute, m => m.MapFrom(src => src.Caff.CreationDateTime.Minute))
               .ForMember(dest => dest.ThumbnailPath, m => m.MapFrom(src => src.Caff.ThumbnailPath));

            mapperConfigurationExpression.CreateMap<Comment, CommentDTO>();


            //mapperConfigurationExpression.CreateMap<User, UserDto>()
            //    .ForMember(dest => dest.Id, m => m.MapFrom(src => src.CaffTagId))
            //    .ForMember(dest => dest.Tag, m => m.MapFrom(src => src.CaffTag.Id));

            return mapperConfigurationExpression;
        }
    }
}
