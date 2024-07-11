using AutoMapper;
using DAL.Interface;
using DAL.Data;
using DAL.Profiles;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Globalization;
namespace project_.net_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture);
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register IUser service
            builder.Services.AddScoped<IUser, UserData>();

            // Add DbContext
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDataBase")));

            // Add AutoMapper with the mapping profile
            builder.Services.AddAutoMapper(typeof(ProfileProj).Assembly);

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
        }
    }
}

//using DAL.Interface;
//using DAL.Data;
//using Microsoft.AspNetCore.Localization;
//using Microsoft.EntityFrameworkCore;
//using Models.Models;
//using System.Globalization;
//namespace project_.net_core
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);
//            builder.Services.Configure<RequestLocalizationOptions>(options =>

//            {

//                options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture);

//            });
//            // Add services to the container.
//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            // הוסיפי את השורה הבאה כדי לרשום את השירות IUser
//            builder.Services.AddScoped<IUser, UserData>();

//            // Add DbContext
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddDbContext<ProjectContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDataBase")));
//            // builder.Services.AddDbContext<BooksContext>(op => op.UseSqlServer("Data Source=DESKTOP-UE6H0IP;Initial Catalog=Books;Integrated Security=SSPI;Trusted_Connection=True;"));

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();

//            app.MapControllers();

//            app.Run();
//        }
//    }
//}
