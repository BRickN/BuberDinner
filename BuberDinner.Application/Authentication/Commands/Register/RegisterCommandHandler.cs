#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) 
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user (generate unique id)
        var user = new User
        { 
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Password = command.Password
        };
        userRepository.Add(user);

        // Create JWT token
        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}