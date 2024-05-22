using FruitMarket.Domain.Contracts;
using FruitMarket.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FruitMarket.Infrastructure.Modules
{
    public static class InfraServiceCollectionExtensions
    {

        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FruitMarketDbContext>(
                options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtManagerRepository, JwtManagerRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }


    }
}
