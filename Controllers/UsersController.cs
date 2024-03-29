using fruit_market_api.Db;
using fruit_market_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Controllers
{
    [Authorize]
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.Birthday = req.Birthday;
            user.Email = req.Email;
            user.Gender = req.Gender;

            user.ImageProfile = req.ImageProfile;
            await _context.SaveChangesAsync();
            return Ok(user);
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id){
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user == null){
                return NotFound();
            }
            user.Password = "";
            return Ok(user);
        }
    }
}