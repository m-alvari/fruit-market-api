using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models;

    public class UserRefreshToken
{
    public int UserId { get; set; }
    public string RefreshToken { get; set; }

    public Boolean IsActive { get; set; }
}
