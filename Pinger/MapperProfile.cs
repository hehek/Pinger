﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<PingerSettings, TcpPingSettings>();
            CreateMap<PingerSettings, HttpPingSettings>();
            CreateMap<PingerSettings, IcmpPingSettings>();

        }
    }

    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config;
        }
    }
}
