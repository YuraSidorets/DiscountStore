using DiscountStore.Domain;

namespace DiscountStore.Lib
{
    public interface IDiscountService
    {
        void AddDiscount(Discount discount);
        decimal ApplyDiscounts(Cart cart);
    }
}