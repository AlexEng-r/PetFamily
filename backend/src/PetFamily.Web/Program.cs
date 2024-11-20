using PetFamily.SpeciesManagement.Presentation.DependencyInjection;
using PetFamily.VolunteerManagement.Presentation.DependencyInjection;
using PetFamily.Web.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => { loggerConfig.ReadFrom.Configuration(context.Configuration); });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSpeciesManagement(builder.Configuration);
builder.Services.AddVolunteerManagement(builder.Configuration);

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