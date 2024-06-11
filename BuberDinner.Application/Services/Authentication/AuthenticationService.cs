namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            email,
            firstName,
            lastName,
            "token"
        );
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "John",
            "Doe",
            email,
            "token"
        );
    }
}