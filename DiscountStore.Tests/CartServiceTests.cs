using DiscountStore.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DiscountStore.Tests
{
    public class CartServiceTests : IDisposable
    {
        private readonly TestWebApplicationFactory _appFactory;
        private readonly List<Product> _products;

        public CartServiceTests()
        {
            _appFactory = new TestWebApplicationFactory();
            _products = new List<Product>
            {
                new Product{ ItemGuid = Guid.NewGuid(), Name = "Vase", Currency = "EUR", Price = 1.2m, SKU = "V"  },
                new Product{ ItemGuid = Guid.NewGuid(), Name = "Big Mug", Currency = "EUR", Price = 1m, SKU = "BM"  },
                new Product{ ItemGuid = Guid.NewGuid(), Name = "Napkins pack", Currency = "EUR", Price = 0.45m, SKU = "NP"  },
            };
        }

        [Fact]
        public void CartServiceTests_EmptyCart_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(new Cart { CartGuid = cartId, Products = new List<Product>() });

            // act
            var total = _appFactory.CartService.GetTotal(cartId);

            // assert
            total.Should().Be(0);
        }

        [Fact]
        public void CartServiceTests_FullCartTotal_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(new Cart { CartGuid = cartId, Products = _products });
            var expectedTotal = _products.Sum(p => p.Price);

            // act
            var total = _appFactory.CartService.GetTotal(cartId);

            // assert
            total.Should().Be(expectedTotal);
        }

        [Fact]
        public void CartServiceTests_AddItem_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            var product = _products.First();
            var expectedCart = new Cart
            {
                CartGuid = cartId,
                Products = new List<Product>()
            };
            
            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(expectedCart);

            // act
            _appFactory.CartService.Add(cartId, product);

            // assert
            var cart = _appFactory.CartRepository.Object.Get(cartId);
            cart.Products.Count.Should().Be(1);
            cart.Products.FirstOrDefault().Should().Be(product);
        }

        [Fact]
        public void CartServiceTests_RemoveProduct_Success()
        {
            // arrange
            var cartId = Guid.NewGuid();
            var product = _products.First();
            var expectedCart = new Cart
            {
                CartGuid = cartId,
                Products = new List<Product> { product }
            };

            _appFactory.CartRepository.Setup(s => s.Get(cartId)).Returns(expectedCart);

            // act
            _appFactory.CartService.RemoveProduct(cartId, product);

            // assert
            var cart = _appFactory.CartRepository.Object.Get(cartId);
            cart.Products.Count.Should().Be(0);
        }

        public void Dispose()
        {
            _appFactory.Dispose();
        }
    }
}
