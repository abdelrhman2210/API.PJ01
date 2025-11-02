using API_PJ01_Domain.Contracts;
using API_PJ01_Persistence;
using API_PJ01_Persistence.Data.Contexts;
using API_PJ01_Services;
using API_PJ01_Services.Abstractions;
using API_PJ01_Services.Mapping.Products;
using API_PJ01_Shared.ErrorModels;
using API_PJ01_Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API_PJ01_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAllServices(builder.Configuration);


            var app = builder.Build();

            await app.ConfigureMiddlewareAsync();

            app.Run();
        }
    }
}
