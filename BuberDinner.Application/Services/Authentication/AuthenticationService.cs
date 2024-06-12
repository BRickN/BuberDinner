using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    : IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user already exists
        if (userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User already exists");
        }

        // Create user (generate unique id)
        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };
        userRepository.Add(user);

        // Create JWT token
        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }

    public AuthenticationResult Login(string email, string password)
    {
        // Validate user exists
        if (userRepository.GetUserByEmail(email) is not { } user)
        {
            throw new Exception("User does not exist");
        }

        // Validate password
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        // Create JWT token
        var token = jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }
}