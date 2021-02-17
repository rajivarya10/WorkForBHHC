using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForBHHC.Data.Entities;
using WorkForBHHC.Models;

namespace WorkForBHHC.Data
{
    public class WorkForBHHCMappingProfile : Profile
    {
        public WorkForBHHCMappingProfile()
        {
            CreateMap<Reason, ReasonViewModel>()
                .ForMember(r => r.Id, ex => ex.MapFrom(r => r.Id))
                .ReverseMap();
        }
    }
}
