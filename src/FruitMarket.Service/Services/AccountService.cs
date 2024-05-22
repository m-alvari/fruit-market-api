using FruitMarket.Common.Extensions;
using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Auth;
using FruitMarket.Domain.Models.Users;
using FruitMarket.Service.Contracts;

namespace FruitMarket.Service.Services;

public class AccountService(IAccountRepository repo,
IJwtManagerRepository jwtManagerRepository) : IAccountService

{


    public async Task<LoginTokenResponseDto?> GetAccountAsync(LoginAccountRequestDto req)
    {
        var query = await repo.GetUser(req.UserName);
        if (query.User is null || query.Account is null || query.Account.Password != AuthExtensions.GetHash(req.Password))
        {
            return null;
        }

        var newJwtToken = jwtManagerRepository.GenerateToken(
        query.User.Id, query.User.PhoneNumber, query.User.Email, query.User.FirstName, query.User.LastName);

        return newJwtToken;

    }
}
