using EFKSystem.Application.Abstractions;
using EFKSystem.Application.Repositories.Customer;
using EFKSystem.Application.Repositories.Order;
using EFKSystem.Application.Repositories.Product;
using EFKSystem.Persistence.Concretes;
using EFKSystem.Persistence.Configuration;
using EFKSystem.Persistence.Contexts;
using EFKSystem.Persistence.Repositories.Customer;
using EFKSystem.Persistence.Repositories.Order;
using EFKSystem.Persistence.Repositories.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configurations.ConnectionString));

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>(); 
        }
    }
}
