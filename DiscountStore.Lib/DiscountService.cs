using DiscountStore.Domain;
using DiscountStore.Persistence;
using System.Linq;

namespace DiscountStore.Lib
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new System.ArgumentNullException(nameof(discountRepository));
        }

        public void AddDiscount(Discount discount)
        {
            _discountRepository.Add(discount);
        }

        public decimal ApplyDiscounts(Cart cart)
        {
            var discounts = _discountRepository.GetAll();

            decimal result = 0;
            foreach (var discount in discounts)
            {
                foreach (var discountSKU in discount.SKUs)
                {
                    var productCount = cart.Products.Count(p => p.SKU == discountSKU);
                    if (productCount == 0 || productCount < discount.ItemsAmount) continue;

                    var discountsToApply = productCount / discount.ItemsAmount;
                    result -= discountsToApply * discount.DiscountAmount;
                }
            }
            return result;
        }
    }
}
