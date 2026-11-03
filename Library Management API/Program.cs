using Business_.BusinessLayer.BLL;
using Business_.Interfaces;
using DataAccess.Library_DbContext;
using Library_Management_API.GlobalExceptionMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Sinks.File;
using System.Net.Http.Headers;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the containe
builder.Services.AddDbContext<DBContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Host.UseSerilog();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        foreach (var state in context.ModelState)
        {
            foreach (var error in state.Value.Errors)
            {
                logger.LogWarning(
                    "Validation failed. Field: {Field}, Error: {Error}",
                    state.Key,
                    error.ErrorMessage);
            }
        }

        return new BadRequestObjectResult(context.ModelState);
    };
});

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var logger = context.HttpContext.RequestServices
        .GetRequiredService<ILogger<Program>>();

            logger.LogWarning(
                "Unauthorized access attempt. Path: {Path}",
                context.Request.Path);
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Geçersiz veya eksik token."
            });
        },

        OnForbidden = async context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            var logger = context.HttpContext.RequestServices
        .GetRequiredService<ILogger<Program>>();

            logger.LogWarning(
                "Forbidden access attempt. Path: {Path}",
                context.Request.Path);
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "Bu işlemi yapma yetkiniz yok."
            });
        }
    };
    });


builder.Services.AddScoped<IJWTService, AuthBBL>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
