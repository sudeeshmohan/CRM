using BuildingBlocks.CQRS;
using FluentValidation;
using Usermanagement.Application.Dtos;

namespace Usermanagement.Application.Command.UserDetails
{
    public class CreateUserCommand : ICommand<Guid>
    {
        public CreateUserCommand(CreateUserDto createUser)
        {
            CreateUser = createUser;
        }

        public CreateUserDto CreateUser { get; }
    }
    public class CreateUserCommandValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");
        }
    }
}
