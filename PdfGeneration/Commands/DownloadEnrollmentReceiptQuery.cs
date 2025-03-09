using MediatR;
using PdfGeneration.Data;

namespace PdfGeneration.Commands
{
    public class DownloadEnrollmentReceiptQuery : IRequest<byte[]>
    {
        public Guid ReceiptId { get; set; }
    }

    public class DownloadEnrollmentReceiptHandler : IRequestHandler<DownloadEnrollmentReceiptQuery, byte[]?>
    {
        private readonly EnrollmentDbContext _context;

        public DownloadEnrollmentReceiptHandler(EnrollmentDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]?> Handle(DownloadEnrollmentReceiptQuery request, CancellationToken cancellationToken)
        {
            var receipt = await _context.EnrollmentReceipts.FindAsync(request.ReceiptId);
            return receipt?.FileContent;
        }
    }
}
