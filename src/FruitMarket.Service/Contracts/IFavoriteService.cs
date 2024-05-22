using FruitMarket.Domain.Models.Favorites;

namespace FruitMarket.Service.Contracts;

public interface IFavoriteService
{
    Task<FavoriteDetailDto[]> GetFavoriteAsync();
    Task<FavoriteDto?> AddOrUpdateFavoriteAsync(UpsertFavoriteRequest dto);
    Task DeleteFavoriteAsync(int productId);
}
