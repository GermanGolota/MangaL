using Infrastructure.Extensions;
using Infrastructure.Configuration;
using MangaLWebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MangaLWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string WebUIPolicyName = "WebUIClientPolicy";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //needs to be redone in production
            services.AddCors(options =>
            {
                options.AddPolicy(WebUIPolicyName, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MangaLAPI", Version = "v1" });
            });

            services.AddInfrastructure();

            services.AddValidators();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MangaL API v1"));

            if (env.IsProduction())
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(handler => handler.UseCustomErrors(env));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(WebUIPolicyName);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
