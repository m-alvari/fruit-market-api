using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.PostgreSQL.Models;
using fruit_market_api.Db;
using fruit_market_api.Models;
using fruit_market_api.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ShopContext _context;

        public AccountsController(ShopContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LoginAccount(UpsertAccountRequest req)
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

            return Ok(user.PhoneNumber);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> RegisterUser(UpsertUserRequest req)
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
                ImageProfile = req.ImageProfile
            };

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}