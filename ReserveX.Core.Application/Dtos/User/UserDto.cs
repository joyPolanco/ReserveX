using ReserveX.Core.Domain.Common.enums;


namespace ReserveX.Core.Application.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Status { get; set; }
    }
}
