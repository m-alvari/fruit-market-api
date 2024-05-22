using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Auth;
using FruitMarket.Service.Contracts;

namespace FruitMarket.Service.Services;

public class CurrentUserService(IJwtManagerRepository iJWTManagerRepository) : ICurrentUserService
{

    private JwtTokenDto? _user;
    public JwtTokenDto User
    {
        get
        {
            if (_user is null)
                _user = GetUser();
            return _user;
        }
    }

    public bool IsLogin()
    {
        var token = iJWTManagerRepository.GetToken();
        return token is not null;
    }



    public JwtTokenDto GetUser()
    {
        var token = iJWTManagerRepository.GetToken();
        if (token is null)
            throw new Exception("Invalid token");

        return token;
    }

}
