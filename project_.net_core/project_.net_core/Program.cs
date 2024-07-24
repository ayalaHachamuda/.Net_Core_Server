using AutoMapper;
using BL;
using BL.Interface;
using BL.Middlewares;
using BL.Services;
using DAL.Data;
using DAL.Interface;
using DAL.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Models;
using Serilog;
using System.Security.Claims;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string myCors = "_myCors";
        // Setup Serilog to write to a file
        Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"C:\git_proj_.netcore\project_.net_core\project_.net_core\myLogDoc.txt",
    rollingInterval: RollingInterval.Day)
    .CreateLogger();
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();



        builder.Services.AddCors(op =>
        {
            op.AddPolicy(myCors,
                builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        //jwt
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtKey = builder.Configuration["Jwt:Key"];

        // בדיקת ערכים שהוגדרו
        if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtKey))
        {
            throw new ArgumentNullException("JWT settings are not configured properly.");
        }


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
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                RoleClaimType = ClaimTypes.Role
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });


        builder.Services.AddAuthorization();
        builder.Services.AddSwaggerGen(op =>
        {
            op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            op.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                             Id="Bearer"
                          }
                    },
                    new string[]{}
                }
            });
        });

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IUser, UserData>();
        builder.Services.AddScoped<IAdminUser, AdminUserData>();
        builder.Services.AddScoped<ICompetition, CompetitionData>();
        builder.Services.AddScoped<IRecipe, RecipeData>();
        builder.Services.AddScoped<IVote, VoteData>();
       // builder.Services.AddScoped<IUserService, UserService>();
       // builder.Services.AddScoped<IAdminUserService, AdminUserService>(); // הוספת השירות של AdminUser
        builder.Services.AddDbContext<ProjectContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDataBase")));
        builder.Services.AddAutoMapper(typeof(ProfileProj).Assembly);
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(myCors);
        app.UseMiddleware<JwtMiddleware>();
        app.UseMiddleware<LogMiddleware>();

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
