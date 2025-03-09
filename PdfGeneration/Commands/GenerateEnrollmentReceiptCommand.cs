using MediatR;
using PdfGeneration.Data;
using PdfGeneration.Models;
using PdfGeneration.Services;

namespace PdfGeneration.Commands
{
    public class GenerateEnrollmentReceiptCommand : IRequest<Guid>
    {
        public Guid StudentId { get; set; }
    }

    public class GenerateEnrollmentReceiptHandler : IRequestHandler<GenerateEnrollmentReceiptCommand, Guid>
    {
        private readonly EnrollmentDbContext _context;
        private readonly PdfGeneratorService _pdfService;

        public GenerateEnrollmentReceiptHandler(EnrollmentDbContext context, PdfGeneratorService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        public async Task<Guid> Handle(GenerateEnrollmentReceiptCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.StudentId);
            if (student == null)
                throw new Exception("Student not found!");

            byte[] pdfBytes = await _pdfService.GeneratePdfAsync(student);

            var receipt = new EnrollmentReceipt
            {
                ReceiptId = Guid.NewGuid(),
                StudentId = student.StudentId,
                FileName = $"EnrollmentReceipt_{student.RollNumber}.pdf",
                FileContent = pdfBytes
            };

            _context.EnrollmentReceipts.Add(receipt);
            await _context.SaveChangesAsync();

            return receipt.ReceiptId;
        }
    }
}
