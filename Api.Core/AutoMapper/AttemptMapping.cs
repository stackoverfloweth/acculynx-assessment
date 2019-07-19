using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
