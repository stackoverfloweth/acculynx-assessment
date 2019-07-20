using Api.Contract;
using AutoMapper;
using Data.Entities;

namespace Api.Core.AutoMapper {
    public class AttemptMapping : Profile {
        public AttemptMapping() {
            CreateMap<Attempt, AttemptDto>()
                .ForMember(p => p.AnsweredCorrectly, opt => opt.MapFrom(src => src.AnswerId == src.AttemptAnswerId));
        }
    }
}
