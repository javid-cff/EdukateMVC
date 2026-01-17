using System.ComponentModel.DataAnnotations;

namespace EdukateMVC.ViewModels
{
    public class TeacherVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Teacher name can not be empty! Please write teacher name!")]
        [MaxLength(50)]
        [MinLength(3)]
        public string FullName { get; set; } = string.Empty;
    }
}
