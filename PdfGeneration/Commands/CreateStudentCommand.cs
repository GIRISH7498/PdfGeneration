using MediatR;
using PdfGeneration.Data;
using PdfGeneration.Models;

namespace PdfGeneration.Commands
{
    public class CreateStudentCommand : IRequest<Guid>
    {
        public string FullName { get; set; }
        public string RollNumber { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool PaymentCompleted { get; set; }
    }

    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly EnrollmentDbContext _context;

        public CreateStudentHandler(EnrollmentDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                StudentId = Guid.NewGuid(),
                FullName = request.FullName,
                RollNumber = request.RollNumber,
                Course = request.Course,
                Department = request.Department,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                PaymentCompleted = request.PaymentCompleted,
                EnrollmentDate = DateTime.UtcNow
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student.StudentId;
        }
    }
}
