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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Services
builder.Services.AddScoped<ILocationPointService, LocationPointService>();
builder.Services.AddScoped<IAnimalTypeService, AnimalTypeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped
    <ApiContestNew.Core.Interfaces.Services.IAuthenticationService,
    ApiContestNew.Application.Services.AuthenticationService>();

// Repositories
builder.Services.AddScoped<ILocationPointRepository, LocationPointRepository>();
builder.Services.AddScoped<IAnimalTypeRepository, AnimalTypeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

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
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
}

app.Run();
