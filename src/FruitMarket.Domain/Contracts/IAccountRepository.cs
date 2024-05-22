using FruitMarket.Domain.Models.Users;

namespace FruitMarket.Domain.Contracts;

public interface IAccountRepository
{
    Task<(Account? Account, User? User)> GetUser(string userName);
    Task<Account> AddAccount(int userId, string password);
}
