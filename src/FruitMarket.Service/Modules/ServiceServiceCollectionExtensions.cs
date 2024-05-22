
using FruitMarket.Service.Contracts;
using FruitMarket.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FruitMarket.Infrastructure.Modules
{
    public static class ServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
