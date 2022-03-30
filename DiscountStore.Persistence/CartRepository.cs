using DiscountStore.Domain;
using System;
using System.Collections.Generic;

namespace DiscountStore.Persistence
{
    public class CartRepository : ICartRepository
    {
        Dictionary<Guid, Cart> _cartsDictionary = new Dictionary<Guid, Cart>();

        public void Add(Cart cart)
        {
            if (!_cartsDictionary.ContainsKey(cart.CartGuid))
            {
                _cartsDictionary.Add(cart.CartGuid, cart);
            }
        }

        public Cart Get(Guid cartGuid)
        {
            var cartExists = _cartsDictionary.TryGetValue(cartGuid, out var cart);
            if (!cartExists) return null;
            return cart;
        }

        public void Remove(Guid cartGuid)
        {
            _cartsDictionary.Remove(cartGuid);
        }

        public Cart Update(Cart cart)
        {
            if (_cartsDictionary.ContainsKey(cart.CartGuid))
            {
                _cartsDictionary[cart.CartGuid] = cart;
            }
            return _cartsDictionary[cart.CartGuid];
        }
    }
}
