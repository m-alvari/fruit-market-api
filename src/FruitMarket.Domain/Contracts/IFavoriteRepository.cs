using FruitMarket.Domain.Models.Favorites;

namespace FruitMarket.Domain.Contracts;

public interface IFavoriteRepository
{
    Task<FavoriteDetailDto[]> GetAsync(int userId);
    Task<Favorite> AddAsync(Favorite favorite);
    Task<Favorite?> FindAsync(int userId, int productId);
    Task<int> DeleteAsync(Favorite favorite);
    Task<Favorite?> UpdateAsync(Favorite favorite);
}
