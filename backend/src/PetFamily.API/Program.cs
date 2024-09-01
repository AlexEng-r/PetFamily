using PetFamily.API.Validation;
using PetFamily.Application.DependencyInjection;
using PetFamily.Infrastructure.DatabaseContext;
using PetFamily.Infrastructure.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationInject();
builder.Services.AddInfrastructureInject();
builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();