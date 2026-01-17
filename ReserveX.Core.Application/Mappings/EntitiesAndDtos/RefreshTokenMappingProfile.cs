using AutoMapper;
using ReserveX.Core.Application.Dtos.RefreshToken;
using ReserveX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Mappings.EntitiesAndDtos
{
    public class RefreshTokenMappingProfile : Profile
    {
        public RefreshTokenMappingProfile()
        {
            CreateMap<RefreshToken,RefreshTokenDto>();
        }
    }
}
