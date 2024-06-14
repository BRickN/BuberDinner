using AutoMapper;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<RegisterCommand, User>();
    }
}