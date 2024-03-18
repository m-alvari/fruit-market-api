using fruit_market_api.Db;
using fruit_market_api.Models;
using fruit_market_api.Modules;
using fruit_market_api.Repositories;
using fruit_market_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly IUserService _userService;
        private readonly IJWTManagerRepository _jWTManagerRepository;

        public AccountsController(ShopContext context, IUserService userService, IJWTManagerRepository jWTManagerRepository)
        {
            _jWTManagerRepository = jWTManagerRepository;
            _userService = userService;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginTokenResponse>> LoginAccount(LoginAccountRequest req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == req.UserName);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Password != UserExtensions.GetHash(req.Password))
            {
                return ValidationProblem("password is not correct");
            }
             var newJwtToken = _jWTManagerRepository.GenerateToken(
                           user.Id, user.PhoneNumber, user.Email, user.FirstName, user.LastName);

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserId = user.Id
            };

            _userService.DeleteUserRefreshTokens(user.Id, newJwtToken.RefreshToken);
            _userService.AddUserRefreshTokens(obj);


            return newJwtToken;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<LoginTokenResponse>> RegisterUser(UpsertUserRequest req)
        {
            var exitUser = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == req.PhoneNumber);
            if (exitUser != null)
            {
                return ValidationProblem("phone is already exit");
            }
            var user = new User()
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Birthday = req.Birthday,
                Email = req.Email,
                Gender = req.Gender,
                PhoneNumber = req.PhoneNumber,
                Password = UserExtensions.GetHash(req.Password),
                ImageProfile = req.ImageProfile,

            };

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();
           

            var newJwtToken = _jWTManagerRepository.GenerateToken(
                           user.Id, user.PhoneNumber, user.Email, user.FirstName, user.LastName);

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserId = user.Id
            };

            _userService.DeleteUserRefreshTokens(user.Id, newJwtToken.RefreshToken);
            _userService.AddUserRefreshTokens(obj);


            return newJwtToken;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> Refresh(LoginTokenResponse token)
        {
            var principal = _jWTManagerRepository.GetPrincipalFromExpiredToken(token.AccessToken);
            var userId = int.Parse(principal.Identity?.Name);


            var savedRefreshToken = _userService.GetSavedRefreshTokens(userId, token.RefreshToken);

            if (savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                return Unauthorized("Invalid attempt!");
            }
            var user = await _context.Users.FirstAsync(x => x.Id == userId);

            var newJwtToken = _jWTManagerRepository.GenerateToken(
                user.Id, user.PhoneNumber, user.Email, user.FirstName, user.LastName);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserId = userId
            };

            _userService.DeleteUserRefreshTokens(userId, token.RefreshToken);
            _userService.AddUserRefreshTokens(obj);

            return Ok(newJwtToken);
        }
    }
}
