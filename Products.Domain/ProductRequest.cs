﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain
{
    public class ProductRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        public bool Available { get; set; }

        public string? Description { get; set; }
    }
}
