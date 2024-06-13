#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Validate user exists
        if (userRepository.GetUserByEmail(query.Email) is not { } user)
        {
            return Errors.Authentication.UserNotFound;
        }

        // Validate password
        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // Create JWT token
        var token = jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }
}