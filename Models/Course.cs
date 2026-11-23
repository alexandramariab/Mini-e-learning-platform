//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniELearning.Models
{
    public enum Level { AllLevels, Beginner, Intermediate, Advanced, Expert }

    public enum Status { Pending, Approved, Rejected }
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The 'Title' field is Required.")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "The 'Description' field is Required.")]
        public string Description { get; set; } = string.Empty;


        ///de vazut cum functioneaza upload photo si daca e de ajuns un string

        //[Required(ErrorMessage = "The 'Description' field is Required.")]
        //public string ImagePath {  get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter the maximum number of students.")]
        public int MaxEnrollments { get; set; }



        [NotMapped] //nu vrem sa stocam informatia asta in tabel
        public int CurrentEnrollments { get; set; }


        [Required(ErrorMessage = "Please select the level of the course.")]
        public Level Level { get; set; }


        public Status Status { get; set; } = Status.Pending;


        public string? AdminFeedback { get; set; } = string.Empty;


        [NotMapped]
        public decimal AvgRating { get; set; }

        //cheia straina
        [Required(ErrorMessage = "Please select the category of the course.")]
        public int CategoryId {  get; set; }

        //prop de navigare
        public virtual Category Category { get; set; } = null!;

        //pt dropdown din forms
        [NotMapped]
        public IEnumerable<SelectListItem> CategoryList { get; set; } = Enumerable.Empty<SelectListItem>();


    }
}
