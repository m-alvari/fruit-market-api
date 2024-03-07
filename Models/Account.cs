using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace fruit_market_api.Models
{
    public record Account
    {
        public string UserName { get; set; }

        public string HashedPassword { get; set; }

      
    }
}