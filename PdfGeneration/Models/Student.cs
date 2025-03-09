using System.ComponentModel.DataAnnotations;

namespace PdfGeneration.Models
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        public string FullName { get; set; }
        public string RollNumber { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool PaymentCompleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
