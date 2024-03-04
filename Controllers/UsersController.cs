using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.PostgreSQL;
using ConsoleApp.PostgreSQL.Models;
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
                LastName = req.LastName
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
            ntt.FirstName =req.FirstName ;
            ntt.LastName = req.LastName;
   
            await _context.SaveChangesAsync();
            return Ok(ntt);
        }
    }
}