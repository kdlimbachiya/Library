using System.ComponentModel.DataAnnotations;

namespace LibraryCoreApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The Name field must be between 2 and 30 character")]
        [RegularExpression("(-?([A-Z].\\s)?([A-Z][a-z]+)\\s?)+([A-Z]'([A-Z][a-z]+))?", ErrorMessage = "The Name field is invalid")]
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The Password field must be atleast 3 character long")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
