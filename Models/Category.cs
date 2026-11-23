using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MiniELearning.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Numele trebuie să aibă între 2 și 100 de caractere.")]
        [Display(Name = "Nume")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}