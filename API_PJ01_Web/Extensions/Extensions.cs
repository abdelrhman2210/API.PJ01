using API_PJ01_Domain.Contracts;
using API_PJ01_Persistence;
using API_PJ01_Services;
using API_PJ01_Shared.ErrorModels;
using API_PJ01_Web.Middlewares;
using Microsoft.AspNetCore.Mvc;


namespace API_PJ01_Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebServices();

            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);
            services.ConfigureApibehaviourOptions();

            return services;
        }

        private static IServiceCollection ConfigureApibehaviourOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                        .Select(M => new ValidationError()
                        {
                            Field = M.Key,
                            Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                        }).ToList();

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }




        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            #region Initialize DB

            await app.SeedData();

            #endregion

            app.AddErrorHandling();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }

        private static WebApplication AddErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
