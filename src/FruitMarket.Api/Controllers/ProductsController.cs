using FruitMarket.Common.Entity;
using FruitMarket.Domain.Models.Products;
using FruitMarket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FruitMarket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService services) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Post([FromBody] UpsertProductDto product)
    {
        return Ok(await services.AddProductAsync(product));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await services.DeleteProductAsync(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDto?>> Update([FromBody] UpsertProductDto product, [FromRoute] int id)
    {
        return Ok(await services.UpdateProductAsync(product, id));
    }

    [HttpGet]
    public async Task<ActionResult<ProductDetailDto[]>> Get(
            [FromQuery] string? q = null,
            [FromQuery] int take = 8,
            [FromQuery] int skip = 0,
            [FromQuery] OrderBy orderBy = OrderBy.Asc)
    {
        return Ok(await services.GetProductAsync(q, take, skip, orderBy));
    }

    [HttpPost("image-upload")]
    public async Task<ActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var indexOf = file.FileName.LastIndexOf(".");
            var extension = file.FileName.Substring(indexOf);

            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads/products", fileName);
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
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads/products", fileName);

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetWithId(int id)
    {
        return Ok(await services.GetProductWithId(id));
    }

        private string GetContentType(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return ext switch
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
