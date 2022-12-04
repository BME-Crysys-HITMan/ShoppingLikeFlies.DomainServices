
using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Mappings;

internal class DomainServiesProfile : Profile
{
    public DomainServiesProfile()
    {
        CreateMap<CaffDTO, Caff>()
            .ForMember(d => d.CreationDateTime, o => o.MapFrom(src => new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0, DateTimeKind.Utc)))
            .ForMember(d => d.Tags, o => o.MapFrom(s => s.Tags.ToCsv()))
            .ForMember(d => d.Comments, o => o.Ignore())
            .ForMember(d => d.Created, o => o.Ignore())
            .ForMember(d => d.Updated, o => o.Ignore());

        CreateMap<Caff, CaffDTO>()
            .ForMember(d => d.Year, o => o.MapFrom(s => s.CreationDateTime.Year))
            .ForMember(d => d.Month, o => o.MapFrom(s => s.CreationDateTime.Month))
            .ForMember(d => d.Day, o => o.MapFrom(s => s.CreationDateTime.Day))
            .ForMember(d => d.Hour, o => o.MapFrom(s => s.CreationDateTime.Minute))
            .ForMember(d => d.Minute, o => o.MapFrom(s => s.CreationDateTime.Minute))
            .ForMember(d => d.Tags, o => o.MapFrom(s => s.Tags.Split(';', StringSplitOptions.RemoveEmptyEntries)));

        CreateMap<CreateCaffContractDTO, Caff>()
                .ForMember(dest => dest.CreationDateTime, m => m.MapFrom(src => new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0, DateTimeKind.Utc)))
                .ForMember(dest => dest.Id, m => m.Ignore())
                .ForMember(dest => dest.Comments, m => m.Ignore())
                .ForMember(d => d.Created, o => o.Ignore())
                .ForMember(d => d.Updated, o => o.Ignore());

        CreateMap<Comment, CommentDTO>();
    }
}
