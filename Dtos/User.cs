using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Dtos
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }
        public bool Is2Faenabled { get; set; }
        public bool IsAdminApproved { get; set; }
        public bool IsVerified { get; set; }
        public string TwoFactorSecretKey { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
