using EFKSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Domain.Entities
{
    public class Customers : BaseEntity
    {
        public ICollection<Orders> Orders { get; set; }
    }
}
