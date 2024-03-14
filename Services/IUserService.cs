using fruit_market_api.Models;

namespace fruit_market_api.Services;

public interface IUserService
{
  Task<bool> IsValidUserAsync(LoginAccountRequest users);

  UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);

  UserRefreshToken GetSavedRefreshTokens(int userId, string refreshtoken);

  void DeleteUserRefreshTokens(int userId, string refreshToken);
 }