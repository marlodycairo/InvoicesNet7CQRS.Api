using System.ComponentModel.DataAnnotations;

namespace InvoicesNet7CQRS.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
    }
}
