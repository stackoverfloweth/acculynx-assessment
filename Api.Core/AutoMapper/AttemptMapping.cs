using Api.Contract;
using AutoMapper;
using Data.Entities;
using System;

namespace Api.Core.AutoMapper {
    public class AttemptMapping : Profile {
        public AttemptMapping() {
            CreateMap<Attempt, AttemptDto>();
            CreateMap<AttemptDto, Attempt>()
                .ForMember(p => p.AttemptDate, opt=> opt.MapFrom(src => DateTime.Now));
        }
    }
}
