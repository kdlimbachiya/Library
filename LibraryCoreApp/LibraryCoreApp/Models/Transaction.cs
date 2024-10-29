using System.ComponentModel.DataAnnotations;

namespace LibraryCoreApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The Name field must be between 2 and 30 character")]
        [RegularExpression("(-?([A-Z].\\s)?([A-Z][a-z]+)\\s?)+([A-Z]'([A-Z][a-z]+))?", ErrorMessage = "The BookTitle field is invalid")]
        public string BookTitle { get; set; }      
        public DateTime TransDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool Borrow { get; set; }
        public bool Return { get; set; }
        public bool IsRequested { get; set; }
        public bool IsRejected { get; set; }
    }
}
