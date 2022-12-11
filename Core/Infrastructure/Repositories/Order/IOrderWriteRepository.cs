using EFKSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Application.Repositories.Order
{
    public interface IOrderWriteRepository  : IWriteRepository<Orders>
    {
    }
}
