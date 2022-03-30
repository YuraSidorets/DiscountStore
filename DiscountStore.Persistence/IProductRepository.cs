using DiscountStore.Domain;
using System;

namespace DiscountStore.Persistence
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Remove(Guid productGuid);
        Product Get(Guid productGuid);
    }
}
