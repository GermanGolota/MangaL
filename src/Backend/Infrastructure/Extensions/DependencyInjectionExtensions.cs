using Application.Commands;
using Application.Contracts;
using AutoMapper;
using Infrastructure.Configuration;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Cryptography;

namespace Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            Assembly infrastructureAssembly = typeof(ChapterUploadCommand).Assembly;

            services.AddMediatR(infrastructureAssembly);

            services.AddAutoMapper(infrastructureAssembly);

            services.AddScoped<IFileHandler, FileHandler>();

            return services;
        }

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ISQLClientFactory, SQLClientFactory>();

            services.AddScoped<ISQLClient>(x => x.GetRequiredService<ISQLClientFactory>().CreateClient());

            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IMangaReadRepo, MangaReadRepo>();

            services.AddScoped<IMangaWriteRepo, MangaWriteRepo>();

            services.AddScoped<IChapterRepo, ChapterRepo>();

            services.AddScoped<IImageRepo, ImageRepo>();

            return services;
        }
        internal static IServiceCollection AddHashing(this IServiceCollection services)
        {

            services.AddSingleton<HashAlgorithm>(MD5.Create());
            services.AddSingleton<IHasher, Hasher>();

            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHashing();
            services.AddRepositories();
            services.AddServices();
            services.AddSingleton<AppConfiguration>();
            return services;
        }

    }
}
