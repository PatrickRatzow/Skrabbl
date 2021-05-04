using AutoMapper;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient
{
    public class ModelMapper
    {
        private static MapperConfiguration _config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LoginResponseDto, Tokens>();
        });

        public static IMapper Mapper = new Mapper(_config);
    }
}