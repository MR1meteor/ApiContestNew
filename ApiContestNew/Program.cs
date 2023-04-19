using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Application.Services;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApiContestNew.Helpers;
using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using ApiContestNew.Core.Handlers;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Prevent JSON cycles
builder.Services.AddControllers().AddJsonOptions(x => 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// HttpContext
builder.Services.AddHttpContextAccessor();

// Services
builder.Services.AddScoped<ILocationPointService, LocationPointService>();
builder.Services.AddScoped<IAnimalTypeService, AnimalTypeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped
    <ApiContestNew.Core.Interfaces.Services.IAuthenticationService,
    ApiContestNew.Application.Services.AuthenticationService>();
builder.Services.AddScoped<IAnimalVisitedLocationService, AnimalVisitedLocationService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IAreaAnalyticsService, AreaAnalyticsService>();

// Repositories
builder.Services.AddScoped<ILocationPointRepository, LocationPointRepository>();
builder.Services.AddScoped<IAnimalTypeRepository, AnimalTypeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IAnimalVisitedLocationRepository, AnimalVisitedLocationRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();

// DBContext
builder.Services.AddDbContext<DataContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication
builder.Services.AddAuthentication("BasicAuthentication").
            AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
            ("BasicAuthentication", null);

// Configure default Authorization
builder.Services.AddAuthorization(cfg =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder("BasicAuthentication");
    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    cfg.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
    //if (context.Database.GetPendingMigrations().Any())
    //{
    //    context.Database.EnsureDeleted();
    //    context.Database.Migrate();
    //}
}

app.Run();
