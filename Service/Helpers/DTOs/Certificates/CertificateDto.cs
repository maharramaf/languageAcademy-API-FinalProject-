namespace Service.Helpers.DTOs.Certificates
{
    public class CertificateDto
    {
        public string Number { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public string CourseSlug { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool Created { get; set; }
    }
}
