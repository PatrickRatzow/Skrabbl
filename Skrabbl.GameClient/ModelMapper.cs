using AutoMapper;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.GameClient
{
    public class Tokens
    {
        public RefreshToken RefreshToken { get; set; }
        public Jwt Jwt { get; set; }
    }

    public static class ModelMapper
    {
        private static MapperConfiguration config = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<LoginResponseDto, Tokens>();
        });

        public static IMapper Mapper = new Mapper(config);
    }
}
