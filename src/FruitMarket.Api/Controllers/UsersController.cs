using FruitMarket.Common.Entity;
using FruitMarket.Domain.Models.Users;
using FruitMarket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FruitMarket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService services) : ControllerBase
{

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetWithId(int id)
    {
        return Ok(await services.GetUserWithId(id));
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] UpsertUserDto user)
    {
        return Ok(await services.AddUserAsync(user));
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await services.DeleteUserAsync(id);
        return NoContent();
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDto>> Update([FromBody] UpsertUserDto user, [FromRoute] int id)
    {
        return Ok(await services.UpdateUserAsync(user, id));
    }

    [HttpGet]
    public async Task<ActionResult<UserDto[]>> Get(
         [FromQuery] string? q = null,
         [FromQuery] int take = 8,
         [FromQuery] int skip = 0,
         [FromQuery] OrderBy orderBy = OrderBy.Asc)
    {
        return Ok(await services.GetUserAsync(q, take, skip, orderBy));
    }

    [HttpPost("image-upload")]
    public async Task<ActionResult> UploadUser(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var indexOf = file.FileName.LastIndexOf(".");
            var extension = file.FileName.Substring(indexOf);

            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads/users", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { fileName });
        }
        return BadRequest();
    }


    [HttpGet("image-download/{fileName}")]
    public async Task<ActionResult> Download(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads/users", fileName);
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var memory = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
    }

    private string GetContentType(string path)
    {
        var x = Path.GetExtension(path).ToLowerInvariant();
        return x switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            _ => "application/octet-stream",
        };
    }
}


