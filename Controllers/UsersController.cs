using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.PostgreSQL;
using ConsoleApp.PostgreSQL.Models;
using fruit_market_api.Db;
using fruit_market_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ShopContext _context;

        public UsersController(ShopContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<User> CreateUser(UpsertUserRequest req)
        {

            var user = new User()
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Birthday = req.Birthday,
                Email = req.Email,
                Gender = req.Gender,
                PhoneNumber = req.PhoneNumber,
                Password = req.Password,
                ImageProfile = req.ImageProfile
            };

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();
            return user;
        }

        [HttpGet]
        public async Task<User[]> GetAll()
        {
            var users = await _context.Users.ToArrayAsync();
            return users;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UpsertUserRequest req)
        {
            var ntt = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (ntt == null)
            {
                return NotFound();
            }
            ntt.FirstName = req.FirstName;
            ntt.LastName = req.LastName;
            ntt.Birthday = req.Birthday;
            ntt.Email = req.Email;
            ntt.Gender = req.Gender;
            ntt.PhoneNumber = req.PhoneNumber;
            ntt.Password = req.Password;
            ntt.ImageProfile = req.ImageProfile;
            await _context.SaveChangesAsync();
            return Ok(ntt);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}