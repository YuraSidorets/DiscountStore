using DiscountStore.Domain;
using System;

namespace DiscountStore.Persistence
{
    public interface IDiscountRepository
    {
        void Add(Discount discount);
        Discount[] GetAll();
        void Remove(Guid discountGuid);
    }
}
