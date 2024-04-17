using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models;

public record Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public float Price { get; set; }

    public DateTime DataCreation { get; set; }

}

