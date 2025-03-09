using MediatR;
using Microsoft.AspNetCore.Mvc;
using PdfGeneration.Commands;

namespace PdfGeneration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var studentId = await _mediator.Send(command);
            return Ok(new { Message = "Student enrolled successfully!", StudentId = studentId });
        }

        [HttpPost("generate-receipt/{studentId}")]
        public async Task<IActionResult> GenerateReceipt(Guid studentId)
        {
            var receiptId = await _mediator.Send(new GenerateEnrollmentReceiptCommand { StudentId = studentId });
            return Ok(new { Message = "Enrollment receipt generated successfully!", ReceiptId = receiptId });
        }

        [HttpGet("download-receipt/{receiptId}")]
        public async Task<IActionResult> DownloadReceipt(Guid receiptId)
        {
            var pdfBytes = await _mediator.Send(new DownloadEnrollmentReceiptQuery { ReceiptId = receiptId });

            if (pdfBytes == null)
                return NotFound("Receipt not found");

            return File(pdfBytes, "application/pdf", "EnrollmentReceipt.pdf");
        }
    }
}
