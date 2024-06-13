using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Authentication.InvalidCredentials",
            description: "Invalid credentials");
        
        public static Error UserNotFound => Error.Validation(
            code: "Authentication.UserNotFound",
            description: "User doesn't exist");
    }
}