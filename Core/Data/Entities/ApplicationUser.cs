using Microsoft.AspNetCore.Identity;


namespace Core.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public string? resetToken { get; set; }
    }
}
