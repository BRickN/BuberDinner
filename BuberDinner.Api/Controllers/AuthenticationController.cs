using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        
        var response = new AuthenticationResponse(
            authResult.user.Id, 
            authResult.user.FirstName, 
            authResult.user.LastName,
            authResult.user.Email, 
            authResult.Token);
        
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = authenticationService.Login(request.Email, request.Password);
        
        var response = new AuthenticationResponse(
            authResult.user.Id, 
            authResult.user.FirstName, 
            authResult.user.LastName,
            authResult.user.Email, 
            authResult.Token);
        
        return Ok(response);
    }
}