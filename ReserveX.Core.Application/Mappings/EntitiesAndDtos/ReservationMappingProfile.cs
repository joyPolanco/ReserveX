using AutoMapper;
using ReserveX.Core.Application.Dtos.Reservation;
using ReserveX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Mappings.EntitiesAndDtos
{
    public class ReservationMappingProfile: Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<Reservation, ReservationDto>()
                .ForMember(r => r.Status, opt => opt.MapFrom(x => x.Status.ToString().ToLower()))
                .ForMember(r => r.StartTime, opt => opt.MapFrom(src => src.Slot!.StartTime.ToString().ToLower()??""))
                 .ForMember(r => r.EndTime, opt => opt.MapFrom(src => src.Slot!.EndTime.ToString().ToLower()))
                .ForMember(r => r.Date, opt => opt.MapFrom(src => src.Slot!.Date.ToString().ToLower()))
                .ForMember(r => r.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString().ToLower()))
                .ForMember(r => r.StationName, opt => opt.MapFrom(src => src.Slot!.Station!.Name.ToString().ToLower()));

        }
    }
}
