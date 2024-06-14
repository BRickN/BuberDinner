using AutoMapper;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;

namespace BuberDinner.Api.Common.Mapping;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginRequest, LoginQuery>();
    }
}