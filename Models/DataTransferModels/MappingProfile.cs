using AutoMapper;

namespace ItemManagment.Models.DataTransferModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Measure, MeasureDto>().ReverseMap();
            CreateMap<ItemGroup, ItemGroupDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<UnionBarcode, UnionBarcodeDto>().ReverseMap();

            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.Measure, opt => opt.MapFrom(src => src.Measure))
                .ForMember(dest => dest.ItemGroupId, opt => opt.MapFrom(src => src.ItemGroupId))
                .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup))
                .ForMember(dest => dest.UnionBarcodeId, opt => opt.MapFrom(src => src.UnionBarcodeId))
                .ForMember(dest => dest.UnionBarcode, opt => opt.MapFrom(src => src.UnionBarcode))
                .ReverseMap()
                .ForMember(dest => dest.Measure, opt => opt.MapFrom(src => src.Measure))
                .ForMember(dest => dest.ItemGroupId, opt => opt.MapFrom(src => src.ItemGroupId))
                .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup))
                .ForMember(dest => dest.UnionBarcodeId, opt => opt.MapFrom(src => src.UnionBarcodeId))
                .ForMember(dest => dest.UnionBarcode, opt => opt.MapFrom(src => src.UnionBarcode));

            CreateMap<IEnumerable<Item>, List<ItemDto>>().ReverseMap();
            CreateMap<IEnumerable<ItemGroup>, List<ItemGroupDto>>().ReverseMap();
            CreateMap<IEnumerable<Measure>, List<MeasureDto>>().ReverseMap();
            CreateMap<IEnumerable<Supplier>, List<SupplierDto>>().ReverseMap();

        }
    }
}
