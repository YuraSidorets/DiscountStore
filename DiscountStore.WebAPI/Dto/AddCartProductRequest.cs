using DiscountStore.Domain;
using System;

namespace DiscountStore.WebAPI.Dto
{
    public class AddCartProductRequest
    {
        public Guid CartGuid { get; set; }
        public Product Product { get; set; }
    }
}
