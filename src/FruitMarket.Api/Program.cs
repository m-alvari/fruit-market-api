using FruitMarket.Api.Modules;
using FruitMarket.Infrastructure.Modules;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.AddSecurityDefinition(
            "token",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization
            }
        );
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        },
                    },
                    Array.Empty<string>()
                }
            }
        );
    }
);
builder.Services.AddControllers(opts =>
    opts.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer())));
builder.Services.AddServices();
builder.Services.AddAuth(builder.Configuration);


// Registering
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddRepositories();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

// app.UseHttpsRedirection();


app.Run();

