using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Application.Dtos.Reservation;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReserveX.Core.Application.Features.User.Queries
{
    public class GetAllUserReservationsQuery : IRequest<IReadOnlyList<ReservationDto>>
    {
        public Guid UserId { get; set; }
        public DateOnly? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }


    }
    public class GetAllUserReservationsQueryHandler : IRequestHandler<GetAllUserReservationsQuery, IReadOnlyList<ReservationDto>>
{
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetAllUserReservationsQueryHandler(IUserRepository userRepository ,IReservationRepository reservationRepository, IMapper mapper)
        {
            _userRepository=userRepository;
            _reservationRepository= reservationRepository;
            _mapper=mapper;
        }
        public async Task<IReadOnlyList<ReservationDto>> Handle(GetAllUserReservationsQuery request, CancellationToken cancellationToken)
        {
            var userExists=await  _userRepository.GetAllQuery().Where(r=>r.Id==request.UserId).AnyAsync();
            if (!userExists) throw new KeyNotFoundException("User not found with this Id");

            var query = _reservationRepository
                .GetAllQuery()
                    .Include(r => r.Slot)
                    .ThenInclude(s => s.Station)
                .Where(r => r.UserId == request.UserId);


            if (request.Date != null)
            {
                query = query.Where(r => r.Slot!.Date == request.Date);
            }

            if (request.StartTime != null)
            {
                query = query.Where(r => r.Slot!.StartTime >= request.StartTime);
            }

            if (request.EndTime != null)
            {
                query = query.Where(r => r.Slot!.EndTime <= request.EndTime);
            }



            return await query.ProjectTo<ReservationDto>(_mapper.ConfigurationProvider).ToListAsync();




        }
    }
}
