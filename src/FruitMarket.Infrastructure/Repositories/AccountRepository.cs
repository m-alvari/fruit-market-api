using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Auth;
using FruitMarket.Domain.Models.Users;
using FruitMarket.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Infrastructure.Repositories;

public class AccountRepository(FruitMarketDbContext db) : IAccountRepository
{

    public async Task<(Account? Account, User? User)> GetUser(string userName)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == userName);
        if (user == null)
        {
            return (null, null);
        }
        var account = await db.Accounts.FirstOrDefaultAsync(x => x.UserId == user.Id);
        return (account, user);
    }

    public async Task<Account> AddAccount(int userId, string password)
    {
        var entity = new Account()
        {
            UserId = userId,
            Password = password
        };

        await db.Accounts.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

}
