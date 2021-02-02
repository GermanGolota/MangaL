using FluentValidation;
using MangaLWebAPI.PipelineBehaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MangaLWebAPI.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBahavior<,>));
            return services;
        }
    }
}
