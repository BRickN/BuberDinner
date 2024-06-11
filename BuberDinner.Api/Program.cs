using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddControllers();
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
    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();