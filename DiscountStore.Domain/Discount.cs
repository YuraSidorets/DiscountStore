using System;
using System.Collections.Generic;

namespace DiscountStore.Domain
{
    public class Discount
    {
        public Guid DiscountGuid { get; set; }
        public int ItemsAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public List<string> SKUs { get; set; }
    }
}
