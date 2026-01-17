using System.ComponentModel.DataAnnotations;

namespace EdukateMVC.ViewModels.CourseViewModels
{
    public class CourseCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title can not be empty! Please fill the title gap!")]
        [MaxLength(100, ErrorMessage = "Maximum character limit is 100!")]
        [MinLength(15, ErrorMessage = "Minimum character limit is 15!")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Rating can not be empty! Please fill the rating gap!")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5!")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Review can not be empty! Please fill the review gap!")]
        [Range(0, int.MaxValue, ErrorMessage = "Review can not be negative!")]
        public int Review { get; set; }
        [Required(ErrorMessage = "Image can not be empty! Please put your Image!")]
        public IFormFile Image { get; set; } = null!;
        [Required(ErrorMessage = "Teacher can not be empty! Please choose a teacher!")]
        public int TeacherId { get; set; }
    }
}
