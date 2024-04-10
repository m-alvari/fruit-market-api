using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fruit_market_api.Models;
using fruit_market_api.Repositories;

namespace fruit_market_api.Services;


public interface ICurrentUserService
{
    JwtToken User { get; }
     bool IsLogin();
}

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJWTManagerRepository _jWTManagerRepository;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IJWTManagerRepository iJWTManagerRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new NullReferenceException();
        _jWTManagerRepository = iJWTManagerRepository;

    }

    private JwtToken _user;
    public JwtToken User
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
       var token = GetToken();
       return token is not null;
    }

    private JwtToken? GetToken()
    {

        JwtToken? jwtToken = null;
        // Read access token from HttpContext
        var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(accessToken))
        {
            var tokenData = accessToken.ToString().Split(' ')[1];
            var result = _jWTManagerRepository.Load(tokenData);
            if (!result.HasError)
            {
                jwtToken = result.Jet;
            }
        }

        return jwtToken;

    }

    public JwtToken GetUser()
    {
        var token = GetToken();
        if (token is null)
            throw new Exception("Invalid token");

        return token;
    }

}