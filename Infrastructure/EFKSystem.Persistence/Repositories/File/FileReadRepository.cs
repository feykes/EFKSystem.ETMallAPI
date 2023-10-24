using EFKSystem.Application.Repositories;
using EFKSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Persistence.Repositories
{
    internal class FileReadRepository : ReadRepository<EFKSystem.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
