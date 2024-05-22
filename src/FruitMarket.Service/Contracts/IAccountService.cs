using FruitMarket.Domain.Models.Auth;

namespace FruitMarket.Service.Contracts;

public interface IAccountService
{
    Task<LoginTokenResponseDto?> GetAccountAsync(LoginAccountRequestDto req);
}
