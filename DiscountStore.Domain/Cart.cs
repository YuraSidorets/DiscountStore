using System;
using System.Collections.Generic;

namespace DiscountStore.Domain
{
    public class Cart
    {
        public Guid CartGuid { get; set; }
        public List<Product> Products { get; set; }
    }
}
