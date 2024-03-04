using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fruit_market_api.Models
{
    public record UpsertUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ImageProfile { get; set; }
    }
}