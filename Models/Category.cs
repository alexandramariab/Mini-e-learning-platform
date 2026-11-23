using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MiniELearning.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The name must be between 2 and 100 characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}