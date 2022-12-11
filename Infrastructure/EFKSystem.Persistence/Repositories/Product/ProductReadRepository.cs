using EFKSystem.Application.Repositories.Product;
using EFKSystem.Domain.Entities;
using EFKSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Persistence.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<Products>, IProductReadRepository
    {
        public ProductReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
