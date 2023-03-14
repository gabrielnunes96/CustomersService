global using CustomerService.Models;
using CustomerService.Data.Context;
using CustomerService.Security;
using CustomerService.Services.CardServices;
using CustomerService.Services.ClientServices;
using CustomerService.Services.LoginServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddDbContext<DataContext>();

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var tokenConfiguration = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    configuration.GetSection("TokenConfiguration"))
    .Configure(tokenConfiguration);

builder.Services.AddSingleton(tokenConfiguration);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfiguration.Audience;
    paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
    paramsValidation.ValidateIssuer = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o Token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


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

app.Run();