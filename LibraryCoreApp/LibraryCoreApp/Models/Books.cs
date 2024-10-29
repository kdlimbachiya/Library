using System.ComponentModel.DataAnnotations;

namespace LibraryCoreApp.Models
{
    public class Books
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The Title field must be between 4 and 30 character")]
        [RegularExpression("^([A-Z][a-z]+([ ]?[a-z]?['-]?[A-Z][a-z]+)*)$", ErrorMessage = "The Title field is invalid")]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The Author field must be between 4 and 30 character")]
        [RegularExpression("^([A-Z][a-z]+([ ]?[a-z]?['-]?[A-Z][a-z]+)*)$", ErrorMessage = "The Author field is invalid")]
        public string Author { get; set; }
        [Required]
        [Range(1, 200, ErrorMessage = "Enter a value between 1 and 200")]
        public int Copies { get; set; }
        public string Publication { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
    }
}
