using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
    builder.Services.AddAutoMapper(typeof(ApiMappingProfile));
}

//Swagger
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

//Swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    app.UseExceptionHandler("/errors");
    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();