using DiscountStore.Persistence;
using DiscountStore.WebAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DiscountStore.Tests
{
    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddControllersAsServices();

            services.AddSingleton(new Mock<ICartRepository>())
                    .AddSingleton(sp => sp.GetRequiredService<Mock<ICartRepository>>().Object)
                    .AddSingleton(new Mock<IDiscountRepository>())
                    .AddSingleton(sp => sp.GetRequiredService<Mock<IDiscountRepository>>().Object)
                    .AddSingleton(new Mock<IProductRepository>())
                    .AddSingleton(sp => sp.GetRequiredService<Mock<IProductRepository>>().Object);
        }
    }
}
