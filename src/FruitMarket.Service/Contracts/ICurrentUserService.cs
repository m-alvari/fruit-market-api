using FruitMarket.Domain.Models.Auth;

namespace FruitMarket.Service.Contracts;

public interface ICurrentUserService
{
    JwtTokenDto User { get; }
     bool IsLogin();
}
