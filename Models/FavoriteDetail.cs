using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models;

   public record FavoriteDetail
{
    public int ProductId { get; set; }

    public string Name { get; set; }
    public float Price { get; set; }
}