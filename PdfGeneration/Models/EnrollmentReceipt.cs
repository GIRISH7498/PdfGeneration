using System.ComponentModel.DataAnnotations;

namespace PdfGeneration.Models
{
    public class EnrollmentReceipt
    {
        [Key]
        public Guid ReceiptId { get; set; }
        public Guid StudentId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
