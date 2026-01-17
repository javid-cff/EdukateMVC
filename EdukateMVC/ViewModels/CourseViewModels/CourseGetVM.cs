namespace EdukateMVC.ViewModels.CourseViewModels
{
    public class CourseGetVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int Review { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
    }
}
