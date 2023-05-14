using Bora.API.Security.Authorization.Handlers.Implementations;
using Bora.API.Security.Authorization.Handlers.Interfaces;
using Bora.API.Security.Authorization.Middleware;
using Bora.API.Security.Authorization.Settings;
using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Repository;
using Bora.API.Security.Domain.Service;
using Bora.API.Security.Mapping;
using Bora.API.Security.Repositories;
using Bora.API.Security.Services;
using Bora.API.Shared.Domain.Repository;
using Bora.API.Shared.Persistence;
using Bora.API.Shared.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings")); // The "why" are IOptions
// Open API Configuration
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Bora.API",
            Description = "Bora.API v1. A ControlWare S.A.C first client."
        });
        options.EnableAnnotations();
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new List<string>()
            }
        });
    });

// Connection String 
var connectionString = builder.Configuration.GetConnectionString("MySqlConnectionForBora");

// Adding DbContext with Connection String
builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder =>
    {
        optionsBuilder.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    });

// Lowercase url configuration
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// CORS Service Addition
builder.Services.AddCors();

// UserApp services 
// --User-- |Services and Repositories|
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// --Role-- |Services and Repositories|
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// --Inheritance
// --User--
builder.Services.AddScoped<IBaseRepository<User, string>, BaseRepository<User, string>>();
builder.Services.AddScoped<IBaseRepository<Role, string>, BaseRepository<Role, string>>();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Jwt Handler
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

//Automapper Service
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

// Validation for ensuring database tables are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context?.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v0");
        options.RoutePrefix = "swagger";
    });
}

app.UseAuthentication();

// Setting Middlewares
// Configure JwtMiddleware to intercepts every Http Request
app.UseMiddleware<JwtMiddleware>();

// Configure ErrorHandlingMiddleware for JwtMiddleware for every Http Request
app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseCors(policyBuilder => 
    policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();