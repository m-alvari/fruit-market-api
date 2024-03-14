using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models;

    public record LoginAccountRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public record LoginTokenResponse {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
