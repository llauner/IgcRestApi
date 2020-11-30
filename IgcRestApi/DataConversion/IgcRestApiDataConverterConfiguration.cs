using AutoMapper;
using IgcRestApi.Dto;

namespace IgcRestApi.DataConversion
{
    public static class IgcRestApiDataConverterConfiguration
    {
        /// <summary>
        /// Mapping
        /// </summary>
        /// <param name="cfg"></param>
        public static void ConfigureMapping(IMapperConfigurationExpression cfg)
        {
            // Storage
            cfg.CreateMap<Google.Apis.Storage.v1.Data.Object, IgcFlightDto>()
                .ForMember(to => to.Id, opt => opt.Ignore())
                .ForMember(to => to.ZipFileName, opt => opt.Ignore())
                .ForMember(to => to.Status, opt => opt.Ignore())
                .ForMember(to => to.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }


    }
}
