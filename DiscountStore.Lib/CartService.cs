using DiscountStore.Domain;
using DiscountStore.Lib;
using DiscountStore.Persistence;
using System;
using System.Linq;

namespace DiscountStore.Services
{
    public class CartService : ICartService
    {
        private readonly IDiscountService _discountService;
        private readonly ICartRepository _cartRepository;

        public CartService(IDiscountService discountService, ICartRepository cartRepository)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public void Add(Guid cartId, Product product)
        {
            var cart = _cartRepository.Get(cartId);
            if (cart == null) return;

            cart.Products.Add(product);

            _cartRepository.Update(cart);
        }

        public decimal GetTotal(Guid cartId)
        {
            var cart = _cartRepository.Get(cartId);
            if (cart == null) return 0;

            var discount = _discountService.ApplyDiscounts(cart);
            return cart.Products.Sum(x => x.Price) + discount;
        }

        public void RemoveProduct(Guid cartId, Product item)
        {
            var cart = _cartRepository.Get(cartId);
            if (cart == null) return;

            cart.Products.Remove(cart.Products.First(p => p.ItemGuid == item.ItemGuid));
            _cartRepository.Update(cart);
        }

        public void RemoveCart(Guid cartId)
        {
            _cartRepository.Remove(cartId);
        }
    }
}
