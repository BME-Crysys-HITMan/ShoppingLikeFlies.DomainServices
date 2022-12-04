
using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Mappings;

internal class DomainServiesProfile : Profile
{
	public DomainServiesProfile()
	{
        CreateMap<Caff, CaffDTO>()
                .ForMember(dest => dest.Tags, m => m.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Year, m => m.MapFrom(src => src.CreationDateTime.Year))
                .ForMember(dest => dest.Month, m => m.MapFrom(src => src.CreationDateTime.Month))
                .ForMember(dest => dest.Day, m => m.MapFrom(src => src.CreationDateTime.Day))
                .ForMember(dest => dest.Hour, m => m.MapFrom(src => src.CreationDateTime.Hour))
                .ForMember(dest => dest.Minute, m => m.MapFrom(src => src.CreationDateTime.Minute)).ReverseMap();

        CreateMap<CreateCaffContractDTO, Caff>()
                .ForMember(dest => dest.CreationDateTime, m => m.MapFrom(src => new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0)))
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ForMember(dest => dest.Comments, m => m.Ignore()).ReverseMap();

        /*CreateMap<CaffTag, CaffTagDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ForMember(dest => dest.CaffToTags, m => m.Ignore()).ReverseMap();

        CreateMap<CaffTagDTO, CaffToTag>()
                .ForMember(dest => dest.CaffTag, m => m.MapFrom(src => src))
                .ForMember(dest => dest.CaffTagId, m => m.Ignore())
                .ForMember(dest => dest.CaffId, m => m.Ignore())
                .ForMember(dest => dest.Caff, m => m.Ignore()).ReverseMap();*/

        /*CreateMap<CaffToTag, CaffDTO>()
               .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Caff.Id))
               .ForMember(dest => dest.FilePath, m => m.MapFrom(src => src.Caff.FilePath))
               .ForMember(dest => dest.Creator, m => m.MapFrom(src => src.Caff.Creator))
               .ForMember(dest => dest.Tags, m => m.MapFrom(src => src.Caff.Tags))
               .ForMember(dest => dest.Year, m => m.MapFrom(src => src.Caff.CreationDateTime.Year))
               .ForMember(dest => dest.Month, m => m.MapFrom(src => src.Caff.CreationDateTime.Month))
               .ForMember(dest => dest.Day, m => m.MapFrom(src => src.Caff.CreationDateTime.Day))
               .ForMember(dest => dest.Hour, m => m.MapFrom(src => src.Caff.CreationDateTime.Hour))
               .ForMember(dest => dest.Minute, m => m.MapFrom(src => src.Caff.CreationDateTime.Minute))
               .ForMember(dest => dest.ThumbnailPath, m => m.MapFrom(src => src.Caff.ThumbnailPath))
               .ForMember(dest => dest.Comments, m => m.MapFrom(src => src.Caff.Comments))
               .ForMember(dest => dest.Captions, m => m.MapFrom(src => src.Caff.Captions)).ReverseMap();

        CreateMap<CaffToTag, CaffTagDTO>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.CaffTagId))
                .ForMember(dest => dest.Tag, m => m.MapFrom(src => src.CaffTag.Id)).ReverseMap();

        CreateMap<Caption, CaptionDTO>()
                .ReverseMap();*/
    }
}
