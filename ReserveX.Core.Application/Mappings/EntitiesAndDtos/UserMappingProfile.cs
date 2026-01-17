using AutoMapper;
using ReserveX.Core.Application.Dtos.User;
using ReserveX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Mappings.EntitiesAndDtos
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role.ToString().ToLower()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status.ToString().ToLower()));

        }
    }
}
