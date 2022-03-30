using DiscountStore.Domain;
using System;

namespace DiscountStore.Persistence
{
    public interface ICartRepository
    {
        Cart Get(Guid cartGuid);
        void Remove(Guid cartGuid);
        void Add(Cart cart);
        Cart Update(Cart cart);
    }
}
