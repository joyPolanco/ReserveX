using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Application.Dtos.Pagination;
using ReserveX.Core.Application.Dtos.User;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.User.Queries.GetUserListPaged
{
    public class GetUserListPagedQuery: IRequest<PagedResultDto<UserDto>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        
    }

    public class GetAllUserListQueryHandler : IRequestHandler<GetUserListPagedQuery, PagedResultDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserListQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository=userRepository;
            _mapper=mapper;
        }
        public async Task<PagedResultDto<UserDto>> Handle(GetUserListPagedQuery request, CancellationToken cancellationToken)
        {
            var totalItems = await _userRepository
              .GetAllQuery()
              .CountAsync(cancellationToken);

           
            var users = await _userRepository
                .GetAllQuery()
                .AsNoTracking()
                .OrderBy(x=>x.CreatedAt)
                .Skip((request.Page-1)*request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PagedResultDto<UserDto>
            {
                TotalItems = totalItems,
                Items = users,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize)

            };
        }
    }
}
