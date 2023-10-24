using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Domain.Entities
{
    public class ProductImageFile : File
    {
        public ICollection<Products> Products { get; set; }
    }
}
