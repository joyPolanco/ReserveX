using MediatR;
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.User.Commands.SetStatus
{
    public class SetUserStatusCommand: IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public bool ToActive { get; set; }
    }

    public class SetUserStatusCommandHandler : IRequestHandler<SetUserStatusCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public SetUserStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }
        public async Task<Unit> Handle(SetUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user= await _userRepository.GetAllQuery().Where(r=>r.Id==request.UserId).FirstOrDefaultAsync();
            if (user == null) throw new KeyNotFoundException("There isn't a user associated to this id");

         
            user.Status = request.ToActive ? Domain.Common.enums.Status.ACTIVE : Domain.Common.enums.Status.INACTIVE;
            await _userRepository.UpdateUserByGuidAsync(user.Id, user);
            return Unit.Value;
                
                }
    }
}
