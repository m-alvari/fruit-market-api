namespace ConsoleApp.PostgreSQL.Models
{
    public record User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Email {get; set;}
        public bool Gender {get ; set;}
        public string Telephon {get; set;}
        public string Password {get; set;}
        public string ImageProfile {get;set;}
    }
}