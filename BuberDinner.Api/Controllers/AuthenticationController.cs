using AutoMapper;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController(IMediator mediator, IMapper mapper) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = mapper.Map<RegisterCommand>(request);
        
        var authResult = await mediator.Send(command);

        return authResult.Match(
            authenticationResult => Ok(MapAuthResult(authenticationResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);
        
        var authResult = await mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: Errors.Authentication.InvalidCredentials.Description);
        }

        return authResult.Match(
            authenticationResult => Ok(MapAuthResult(authenticationResult)),
            errors => Problem(errors)
        );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}