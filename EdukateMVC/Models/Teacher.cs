namespace EdukateMVC.Models
{
    public class Teacher : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
