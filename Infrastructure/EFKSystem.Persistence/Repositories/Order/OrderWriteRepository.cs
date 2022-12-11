using EFKSystem.Application.Repositories.Order;
using EFKSystem.Domain.Entities;
using EFKSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Persistence.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<Orders>, IOrderWriteRepository
    {
        public OrderWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
