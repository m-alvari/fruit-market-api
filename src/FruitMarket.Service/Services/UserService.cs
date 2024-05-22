using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Users;
using FruitMarket.Service.Contracts;
using FruitMarket.Common.Extensions;
using FruitMarket.Common.Entity;
namespace FruitMarket.Service.Services;

public class UserService(IUserRepository repo,
 IAccountRepository accountRepository,
 ICurrentUserService currentUserService
 ) : IUserService
{


    public async Task<User[]> GetUserAsync(
          string? q = null,
             int take = 8,
             int skip = 0,
             OrderBy orderBy = OrderBy.Asc
    )
    {
        int? userId = currentUserService.IsLogin() ? currentUserService.User.UserId : null;
        var list = await repo.GetUsers(userId, q, take, skip, orderBy);
        return list;
    }
       public async Task<User?> GetUserWithId(int userId)
    {
        var user = await repo.FindAsync(userId);
        if (user != null)
        {
            return user;
        }
        return null;

    }

    public async Task<UserDto> AddUserAsync(UpsertUserDto user)
    {
        var entity = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Birthday = user.Birthday,
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
            ImageProfile = user.ImageProfile,
            Address = user.Address,
            Email = user.Email
        };
        var newUser = await repo.AddAsync(entity);

        await accountRepository.AddAccount(entity.Id, user.Password.GetHash());


        return new UserDto
        {
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Birthday = newUser.Birthday,
            Gender = newUser.Gender,
            PhoneNumber = newUser.PhoneNumber,
            ImageProfile = newUser.ImageProfile,
            Address = newUser.Address,
            Email = newUser.Email
        };
    }


    public async Task DeleteUserAsync(int id)
    {
        var entity = await repo.FindAsync(id);
        if (entity != null)
        {
            await repo.DeleteAsync(entity);
        }
    }

    public async Task<UserDto?> UpdateUserAsync(UpsertUserDto user, int id)
    {
        var entity = await repo.FindAsync(id);
        if (entity != null)
        {
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Birthday = user.Birthday;
            entity.Gender = user.Gender;
            entity.PhoneNumber = user.PhoneNumber;
            entity.ImageProfile = user.ImageProfile;
            entity.Address = user.Address;
            entity.Email = user.Email;


            await repo.UpdateAsync(entity);

            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ImageProfile = user.ImageProfile,
                Address = user.Address,
                Email = user.Email,

            };
        }
        else
        {
            return null;
        }
    }
}
