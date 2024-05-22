using FruitMarket.Common.Entity;
using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Users;
using FruitMarket.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Infrastructure.Repositories;

public class UserRepository(FruitMarketDbContext db) : IUserRepository
{

    public async Task<User> AddAsync(User user)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async ValueTask<User?> FindAsync(int id)
    {
        return await db.Users.FindAsync(id);
    }

    public Task<int> DeleteAsync(User user)
    {
        db.Users.Remove(user);
        return db.SaveChangesAsync();

    }

    public async Task<User> UpdateAsync(User user)
    {
        db.Users.Update(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<User[]> GetUsers(
             int? userId,
             string? q = null,
             int take = 8,
             int skip = 0,
             OrderBy orderBy = OrderBy.Asc
    )
    {
        var query = db.Users.AsQueryable();

        if (q != null && q != string.Empty)
        {
            query = query.Where(x => x.FirstName.Contains(q));
        }
        var pQuery = query.Skip(skip).Take(take);
        if (orderBy == OrderBy.Desc)
        {
            query = query.OrderByDescending(x => x.FirstName);
        }
        else
        {
            query = query.OrderBy(x => x.FirstName);
        }

        return await query.ToArrayAsync();
    }
}
