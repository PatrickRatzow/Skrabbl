using AutoMapper;
using Skrabbl.Model;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient.Helper
{
    public static class ModelMapper
    {
        private static readonly MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<LoginResponseDto, Tokens>();
            cfg.CreateMap<GameSetting, GameSettingDto>();
        });

        public static readonly IMapper Mapper = new Mapper(Config);
    }
}