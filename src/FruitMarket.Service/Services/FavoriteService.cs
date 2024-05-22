using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Favorites;
using FruitMarket.Service.Contracts;

namespace FruitMarket.Service.Services;

public class FavoriteService(IFavoriteRepository repo, ICurrentUserService currentUserService) : IFavoriteService
{

    public async Task<FavoriteDetailDto[]> GetFavoriteAsync()
    {
        var favorite = await repo.GetAsync(currentUserService.User.UserId);
        return favorite;
    }

    public async Task<FavoriteDto?> AddOrUpdateFavoriteAsync(UpsertFavoriteRequest dto)
    {
        var favorite = await repo.FindAsync(currentUserService.User.UserId, dto.ProductId);
        if (favorite == null)
        {
            favorite = new Favorite
            {
                ProductId = dto.ProductId,
                UserId = currentUserService.User.UserId,

            };
            await repo.AddAsync(favorite);
        }
        else
        {
            favorite.IsDelete = !favorite.IsDelete;
            await repo.UpdateAsync(favorite);
        }
        return new FavoriteDto
        {
            IsDelete = favorite.IsDelete,
            ProductId = favorite.ProductId,
            UserId = favorite.UserId
        };
    }

    public async Task DeleteFavoriteAsync(int productId)
    {
        var entity = await repo.FindAsync(currentUserService.User.UserId, productId);
        if (entity != null)
        {
            await repo.DeleteAsync(entity);
        }
    }


}
