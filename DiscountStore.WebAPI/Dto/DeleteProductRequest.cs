using DiscountStore.Domain;
using System;

namespace DiscountStore.WebAPI.Dto
{
    public class DeleteProductRequest
    {
        public Guid CartGuid { get; set; }
        public Product Product { get; set; }
    }
}
