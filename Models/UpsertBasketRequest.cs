using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models
{
    public class UpsertBasketRequest
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}