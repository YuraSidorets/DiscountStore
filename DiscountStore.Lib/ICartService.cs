using DiscountStore.Domain;
using System;

namespace DiscountStore.Services
{
    public interface ICartService
    {
        void Add(Guid cartId, Product product);
        void RemoveProduct(Guid cartId, Product product);
        decimal GetTotal(Guid cartId);
        void RemoveCart(Guid cartId);
    }
}
