using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {   
        // since we're confining Identity to our API, there's no reason we can't use Data Annotations here for validations
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be complex")]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}