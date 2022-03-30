using DiscountStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscountStore.Persistence
{
    public class DiscountRepository : IDiscountRepository
    {
        Dictionary<Guid, Discount> _discountsDictionary = new Dictionary<Guid, Discount>();

        public void Add(Discount discount)
        {
            if (!_discountsDictionary.ContainsKey(discount.DiscountGuid))
            {
                _discountsDictionary.Add(discount.DiscountGuid, discount);
            }
        }

        public Discount[] GetAll()
        {
            return _discountsDictionary.Select(d => d.Value).ToArray();
        }

        public void Remove(Guid discountGuid)
        {
            _discountsDictionary.Remove(discountGuid);
        }
    }
}
