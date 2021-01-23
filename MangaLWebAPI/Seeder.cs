using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI
{
    public static class Seeder
    {
        public static async Task SeedUsers(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var userService = provider.GetRequiredService<IUserServices>();

                UserRegistrationModel model =
                    new UserRegistrationModel("Olezhka", "oleg123");

                await userService.RegisterUser(model);
            }
        }
    }
}
