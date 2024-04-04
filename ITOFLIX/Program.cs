using ITOFLIX.Data;
using ITOFLIX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ITOFLIX.DTO.Converters;

namespace ITOFLIX;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ITOFLIXContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabase")));

        builder.Services.AddIdentity<ITOFLIXUser,ITOFLIXRole>().AddEntityFrameworkStores<ITOFLIXContext>().AddDefaultTokenProviders();



        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //If you are usin dependency injection on controller you have to add this.
        //builder.Services.AddScoped<MediaConverter>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();
        {
            ITOFLIXContext? context = app.Services.CreateScope().ServiceProvider.GetService<ITOFLIXContext>();
            SignInManager<ITOFLIXUser>? signInManager = app.Services.CreateScope().ServiceProvider.GetService < SignInManager<ITOFLIXUser>>();
            RoleManager<ITOFLIXRole>? roleManager = app.Services.CreateScope().ServiceProvider.GetService<RoleManager<ITOFLIXRole>>();

            DataInitialization dataInitialization = new DataInitialization(context!, signInManager!, roleManager!);
        }



        app.Run();
    }
}

