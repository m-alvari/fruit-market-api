using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fruit_market_api.Db;
using fruit_market_api.Models;
using Microsoft.AspNetCore.Identity;

namespace fruit_market_api.Services;

public class UserService : IUserService
{
    private readonly ShopContext _db;

    public UserService(ShopContext db)
    {
        this._db = db;
    }

    public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
    {
        _db.RefreshTokens.Add(user);
        _db.SaveChanges();
        return user;
    }

    public void DeleteUserRefreshTokens(int userId, string refreshToken)
    {
        var item = _db.RefreshTokens.FirstOrDefault(x => x.UserId == userId && x.RefreshToken == refreshToken);
        if (item != null)
        {
            _db.RefreshTokens.Remove(item);
        }
    }

    public UserRefreshToken? GetSavedRefreshTokens(int userId, string refreshToken)
    {
        return _db.RefreshTokens.FirstOrDefault(x => x.UserId == userId &&
        x.RefreshToken == refreshToken
        && x.IsActive == true);
    }

    public async Task<bool> IsValidUserAsync(LoginAccountRequest users)
    {
        var u = _db.Users.FirstOrDefault(o => o.PhoneNumber == users.UserName && o.Password == users.Password);

        if (u != null)
            return true;
        else
            return false;
    }
}