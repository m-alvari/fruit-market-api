using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models;

public record Favorite
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public bool IsDelete { get; set; }

}

