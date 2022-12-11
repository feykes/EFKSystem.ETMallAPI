using EFKSystem.Application.Abstractions;
using EFKSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Products> GetProducts()
            => new()
            {
                new() { Id = Guid.NewGuid(), Name ="Product-1", CreatedDate = DateTime.Now , Price=100, Stock=200},
                new() { Id = Guid.NewGuid(), Name ="Product-2", CreatedDate = DateTime.Now , Price=200, Stock=300},
                new() { Id = Guid.NewGuid(), Name ="Product-3", CreatedDate = DateTime.Now , Price=300, Stock=500},
                new() { Id = Guid.NewGuid(), Name ="Product-4", CreatedDate = DateTime.Now , Price=400, Stock=700},
                new() { Id = Guid.NewGuid(), Name ="Product-5", CreatedDate = DateTime.Now , Price=500, Stock=800},
            };
    }
}
