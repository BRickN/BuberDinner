using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    /// <summary>
    /// Errors controller for general unexpected exceptions
    /// that don't get handled by ErrorOr error handling 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpPost]
    [HttpPut]
    [HttpDelete]
    [Route("/errors")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception?.Message);   
    }
}