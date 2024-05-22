using FruitMarket.Common.Entity;
using FruitMarket.Domain.Models.Users;

namespace FruitMarket.Service.Contracts;

public interface IUserService
{
    Task<User?> GetUserWithId(int userId);
    Task<UserDto> AddUserAsync(UpsertUserDto user);
    Task DeleteUserAsync(int id);
    Task<UserDto?> UpdateUserAsync(UpsertUserDto user, int id);
     Task<User[]> GetUserAsync(
          string? q = null,
             int take = 8,
             int skip = 0,
             OrderBy orderBy = OrderBy.Asc
    );

}
