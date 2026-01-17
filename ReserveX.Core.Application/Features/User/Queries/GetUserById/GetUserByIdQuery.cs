using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Application.Dtos.User;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQuery: IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository=userRepository;
            _mapper =mapper;
        }
        public async Task <Result<UserDto>>Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAllQuery().Where(r=>r.Id == request.Id).FirstOrDefaultAsync();
            if (user == null)return Result<UserDto>.Failure("There isn't any user associated to this Id", Domain.Common.enums.ErrorType.NotFound);
            return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
        }
    }
}
