using EFKSystem.Application.Abstractions.Storage;
using EFKSystem.Application.Abstractions.Token;
using EFKSystem.Infrastructure.Enums;
using EFKSystem.Infrastructure.Services.Storage;
using EFKSystem.Infrastructure.Services.Storage.Local;
using EFKSystem.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace EFKSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }

    }
}
