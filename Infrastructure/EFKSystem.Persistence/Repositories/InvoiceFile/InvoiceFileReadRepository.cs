using EFKSystem.Application.Repositories;
using EFKSystem.Domain.Entities;
using EFKSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Persistence.Repositories
{
    public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
