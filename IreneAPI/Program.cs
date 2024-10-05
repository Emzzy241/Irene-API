using IreneAPI.Models;
using IreneAPI.Data;
using IreneAPI.Services;
using IreneAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register TokenService
builder.Services.AddScoped<TokenService>();

// JWT Bearer Token Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{ // TokenValidationParameters defines how the JWT should be validated
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //Ensures the token is from the correct source (your API).
        ValidateAudience = true, //Ensures the token is intended for your audience.
        ValidateLifetime = true, //Ensures the token hasn’t expired
        ValidateIssuerSigningKey = true, //Uses a secret key to verify that the token was signed by the server.
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))  // Pulling SecretKey from appsettings.json
    };
});

builder.Services.AddAuthorization();

// Add services to the DI container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IreneAPI", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your JWT token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Adding DbContext and Scoped Repositories
builder.Services.AddDbContext<IreneAPIContext>(options =>
    options.UseMySql(
        builder.Configuration["ConnectionStrings:DefaultConnection"],
        ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"])));

// Adding ASP.NET Identity configuration
// builder.Services.AddIdentity<ApplicationUser, IdentityRole>() // This adds the ASP.NET Identity system, specifying that ApiUser is your user class and IdentityRole is your role class.

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
{
    options.User.RequireUniqueEmail = true; // Enforce unique email
})
    .AddRoles<IdentityRole>() // This allows you to manage roles in the system.
    .AddEntityFrameworkStores<IreneAPIContext>() // This tells Identity to use your database context (IreneAPIContext) for storing user and role information.
    .AddDefaultTokenProviders(); // provides the ability to generate tokens for a password reset
// To confirm password strength and validation settings(this is optional and it can be edited to fit one's application needs)
// builder.Services.Configure<IdentityOptions>(options =>
// {
//     options.Password.RequireDigit = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = true;
//     options.Password.RequireUppercase = true;
//     options.Password.RequiredLength = 6;
//     options.Password.RequiredUniqueChars = 1;
// });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Enable CORS globally
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Middleware to use Authentication and Authorization
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IreneAPI v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();  // Ensure UseAuthentication comes before UseAuthorization
app.UseAuthorization();
app.MapControllers();


// Role seeding logic for API
var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

string[] roleNames = { "Admin", "Merchant", "Developer", "User" };

async Task RoleDecider()
{
    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
    
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        //Seeding Roles: The RoleDecider method creates roles if they don’t already exist. In this case, the roles Admin, Merchant, Developer, and User are created when the app starts.
        // RoleExistsAsync method checks if a role already exists, and if not, CreateAsync adds the role to the database.
    
    }
}

await RoleDecider();

app.Run();





/*
using Microsoft.EntityFrameworkCore;
using IreneAPI.Models;
using IreneAPI.Data;
using IreneAPI.Controllers;
using IreneAPI.Services;
using IreneAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models; // Required for OpenApiInfo, OpenApiSecurityScheme, etc.


var builder = WebApplication.CreateBuilder(args);

// Register TokenService here
builder.Services.AddScoped<TokenService>();

//Jwt configuration starts here
// CODE FROM Medium.com var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
/* var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

//Jwt configuration ends here
// AI jwt config
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
    
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


// Add services to the DI container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding the middleware for our DbContext
// DbContext Registration: This registers the IreneAPIDbContext to manage your database connections.
builder.Services.AddDbContext<IreneAPIContext>(
                    dbContextOptions => dbContextOptions
                    .UseMySql(
                        builder.Configuration["ConnectionStrings:DefaultConnection"],
                        ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                        )
                    )
);

// Scoped Service Registration: Both PaymentService and PaymentRepository are registered using AddScoped. This means they will be created once per request and disposed after the request ends.
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Enable CORS globally
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Enable Swagger UI and JWT Bearer Token support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IreneAPI", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your JWT token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IreneAPI v1");
    });
}

app.UseHttpsRedirection();


app.MapControllers();
// Middleware to use Authentication and Authorization; UseAuthentication() should come before UseAuthorization().
app.UseAuthentication();
app.UseAuthorization();

app.Run();


*/