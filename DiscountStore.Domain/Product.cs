using System;

namespace DiscountStore.Domain
{
    public class Product
    {
        public Guid ItemGuid { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
