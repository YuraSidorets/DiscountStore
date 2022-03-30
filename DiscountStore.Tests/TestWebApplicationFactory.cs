using DiscountStore.Lib;
using DiscountStore.Persistence;
using DiscountStore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace DiscountStore.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<TestStartUp>
    {
        public Mock<IDiscountRepository> DiscountRepository => this.Services.GetService<Mock<IDiscountRepository>>();
        public Mock<IProductRepository> ProductRepository => this.Services.GetService<Mock<IProductRepository>>();
        public Mock<ICartRepository> CartRepository => this.Services.GetService<Mock<ICartRepository>>();

        public IDiscountService DiscountService => this.Services.GetRequiredService<IDiscountService>();
        public ICartService CartService => this.Services.GetRequiredService<ICartService>();

        public TestWebApplicationFactory()
        {
        }

        protected override IHostBuilder CreateHostBuilder() =>
            new HostBuilder()
            .ConfigureWebHost(w => w.UseStartup<TestStartUp>())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });

        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }
}
