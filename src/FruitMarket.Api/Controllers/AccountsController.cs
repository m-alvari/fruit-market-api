using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitMarket.Domain.Models.Auth;
using FruitMarket.Domain.Models.Users;
using FruitMarket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FruitMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(IUserService services, IAccountService accountService) : ControllerBase
    {

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<LoginTokenResponseDto>> Register([FromBody] UpsertUserDto user)
        {
            await services.AddUserAsync(user);
            var token = await accountService.GetAccountAsync(new LoginAccountRequestDto { UserName = user.PhoneNumber, Password = user.Password });
            return Ok(token);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginTokenResponseDto>> Login([FromBody] LoginAccountRequestDto user)
        {
            var token = await accountService.GetAccountAsync(user);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }
    }
}
