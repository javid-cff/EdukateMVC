namespace EdukateMVC.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int Review { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
    }
}
