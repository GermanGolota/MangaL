using DataAccess;
using DataAccess.Repositories;
using FluentValidation;
using Infrastructure.Hashing;
using Infrastructure.ModelConverter;
using Infrastructure.Services;
using MangaLWebAPI.Configuration;
using MangaLWebAPI.PipelineBehaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MangaLWebAPI.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IModelConverter, ModelConverter>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ISQLClientFactory, SQLClientFactory>();

            services.AddScoped<ISQLClient>(x => x.GetRequiredService<ISQLClientFactory>().CreateClient());

            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IMangaReadRepo, MangaReadRepo>();

            services.AddScoped<IMangaWriteRepo, MangaWriteRepo>();

            return services;
        }
        public static IServiceCollection AddHashing(this IServiceCollection services)
        {

            services.AddSingleton<HashAlgorithm>(MD5.Create());
            services.AddSingleton<IHasher, Hasher>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBahavior<,>));
            return services;
        }
    }
}
