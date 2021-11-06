using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models;
using TolabPortal.Models;
using TolabPortal.ViewModels;

namespace TolabPortal.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentProfileViewModel>();
            CreateMap<Interest, InterestViewModel>();
        }
    }
}
