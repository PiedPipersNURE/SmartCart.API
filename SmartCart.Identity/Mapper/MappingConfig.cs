using AutoMapper;
using SmartCart.Identity.Models;

namespace SmartCart.Identity.Mapper
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
