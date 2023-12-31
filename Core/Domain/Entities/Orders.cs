﻿using EFKSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Domain.Entities
{
    public class Orders : BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public Customers Customer { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
