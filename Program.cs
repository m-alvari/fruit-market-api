using ConsoleApp.PostgreSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
  builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder =>
            {
                builder.WithOrigins("http://localhost:4200") // Replace with your Angular app's origin
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
builder.Services.AddRouting(options => options.LowercaseUrls = true);


var Configuration = builder.Configuration;
builder.Services.AddDbContext<ShopContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();
