using FruitMarket.Domain.Models.Favorites;
using FruitMarket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FruitMarket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController(IFavoriteService services) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<FavoriteDto[]>> Get()
    {
        return Ok(await services.GetFavoriteAsync());
    }

    [HttpPost]
    public async Task<ActionResult<FavoriteDto>> Post([FromBody] UpsertFavoriteRequest favorite)
    {
        return Ok(await services.AddOrUpdateFavoriteAsync(favorite));
    }

    [HttpDelete("{productId:int}")]
    public async Task<ActionResult<FavoriteDto>> Delete([FromRoute] int productId)
    {
        await services.DeleteFavoriteAsync(productId);
        return NoContent();
    }
}
