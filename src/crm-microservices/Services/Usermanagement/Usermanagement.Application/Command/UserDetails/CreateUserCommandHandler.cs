using BuildingBlocks.CQRS;
using BuildingBlocks.ExceptionHandling;
using FluentValidation;
using Usermanagement.Application.Dtos;
using Usermanagement.Domain.Entities.UserDetails;
using Usermanagement.Domain.Interfaces;

namespace Usermanagement.Application.Command.UserDetails
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserDto> _validator;

        public CreateUserCommandHandler(IUserRepository userRepository, IValidator<CreateUserDto> validator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (command.CreateUser?.Username != null)
            {
                var errorMessage = await _validator.ValidateAsync(command.CreateUser, cancellationToken);
                var errorsList = errorMessage.Errors.Select(x => x.ErrorMessage).ToList();
                if (errorsList.Any())
                {
                    throw new ValidationException(errorsList.FirstOrDefault());
                }
                var userdetails = await _userRepository.GetUserByUsername(command.CreateUser.Username);
                if (userdetails != null)
                {
                    throw new DataExistingException($"Username existing");
                }
                User user = new()
                {
                    FirstName = command.CreateUser.FirstName ?? string.Empty,
                    LastName = command.CreateUser.LastName ?? string.Empty,
                    EmailAddress = command.CreateUser.EmailAddress ?? string.Empty,
                    Username = command.CreateUser.Username ?? string.Empty
                };
                var response = await _userRepository.CreateUser(user);
                return user.Id;
            }
            return Guid.Empty;
        }
    }
}
