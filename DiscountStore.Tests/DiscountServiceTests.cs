using DiscountStore.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DiscountStore.Tests
{
    public class DiscountServiceTests : IDisposable
    {

        private readonly TestWebApplicationFactory _appFactory;
        private readonly List<Product> _products;
        private readonly List<Discount> _discounts;

        public DiscountServiceTests()
        {
            _appFactory = new TestWebApplicationFactory();
            _products = new List<Product>
            {
                new Product{ ItemGuid = Guid.NewGuid(), Name = "Vase", Currency = "EUR", Price = 1.2m, SKU = "V"  },
                new Product{ ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM"  },
                new Product{ ItemGuid = Guid.NewGuid(), Name = "Napkins pack", Currency = "EUR", Price = 0.45m, SKU = "NP"  },
            };
            _discounts = new List<Discount>
            {
                new Discount{ DiscountGuid = Guid.NewGuid(), SKUs = new List<string> { "BM" }, ItemsAmount = 2, DiscountAmount = 0.5m  },
                new Discount{ DiscountGuid = Guid.NewGuid(), SKUs = new List<string> { "NP" }, ItemsAmount = 3, DiscountAmount = 0.45m  },
            };
        }

        [Fact]
        public void DiscountServiceTests_TotalWithOneDiscount_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM" });
            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(new Cart { CartGuid = cartId, Products = _products });
            _appFactory.DiscountRepository.Setup(s => s.GetAll()).Returns(_discounts.ToArray());
            var expectedTotal = 3.15m;

            // act
            var total = _appFactory.CartService.GetTotal(cartId);

            // assert
            total.Should().Be(expectedTotal);
        }

        [Fact]
        public void DiscountServiceTests_TotalWithTwoDiscountPairs_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM" });
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM" });
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM" });
            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(new Cart { CartGuid = cartId, Products = _products });
            _appFactory.DiscountRepository.Setup(s => s.GetAll()).Returns(_discounts.ToArray());
            var expectedTotal = 4.65m;

            // act
            var total = _appFactory.CartService.GetTotal(cartId);

            // assert
            total.Should().Be(expectedTotal);
        }

        [Fact]
        public void DiscountServiceTests_TotalWithTwoDiscountProducts_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM" });
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Napkins pack", Currency = "EUR", Price = 0.45m, SKU = "NP" });           
            _products.Add(new Product { ItemGuid = Guid.NewGuid(), Name = "Napkins pack", Currency = "EUR", Price = 0.45m, SKU = "NP" });           
            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(new Cart { CartGuid = cartId, Products = _products });
            _appFactory.DiscountRepository.Setup(s => s.GetAll()).Returns(_discounts.ToArray());
            var expectedTotal = 3.6m;

            // act
            var total = _appFactory.CartService.GetTotal(cartId);

            // assert
            total.Should().Be(expectedTotal);
        }

        public void Dispose()
        {
            _appFactory.Dispose();
        }
    }
}
