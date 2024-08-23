using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Dtos
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool AdminPermission { get; set; }
        public string TwoFactorSecretKey { get; set; }
        public DateTime? TwoFactorExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
