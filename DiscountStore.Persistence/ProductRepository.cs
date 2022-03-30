using DiscountStore.Domain;
using System;
using System.Collections.Generic;

namespace DiscountStore.Persistence
{
    public class ProductRepository : IProductRepository
    {
        Dictionary<Guid, Product> _productsDictionary = new Dictionary<Guid, Product>();

        public void Add(Product product)
        {
            if (!_productsDictionary.ContainsKey(product.ItemGuid))
            {
                _productsDictionary.Add(product.ItemGuid, product);
            }
        }

        public Product Get(Guid productGuid)
        {
            var productExists = _productsDictionary.TryGetValue(productGuid, out var product);
            if (!productExists) return null;

            return product;
        }

        public void Remove(Guid productGuid)
        {
            _productsDictionary.Remove(productGuid);
        }
    }
}
