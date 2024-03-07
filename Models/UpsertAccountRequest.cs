using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models
{
    public record UpsertAccountRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}