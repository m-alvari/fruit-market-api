using FruitMarket.Common.Entity;
using FruitMarket.Domain.Models.Users;

namespace FruitMarket.Domain.Contracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    ValueTask<User?> FindAsync(int id);
    Task<int> DeleteAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User[]> GetUsers(
             int? userId,
             string? q = null,
             int take = 8,
             int skip = 0,
             OrderBy orderBy = OrderBy.Asc
    );
}
