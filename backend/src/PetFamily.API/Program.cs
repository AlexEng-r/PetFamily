using PetFamily.API.Middlewares;
using PetFamily.Application.DependencyInjection;
using PetFamily.Infrastructure.DatabaseContext;
using PetFamily.Infrastructure.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInject();
builder.Services.AddInfrastructureInject(builder.Configuration);
builder.Services.AddScoped<ApplicationDbContext>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();