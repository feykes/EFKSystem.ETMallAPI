using EFKSystem.Application.Abstractions.Storage;
using EFKSystem.Infrastructure.Enums;
using EFKSystem.Infrastructure.Services.Storage;
using EFKSystem.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace EFKSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }

    }
}
