using Microsoft.EntityFrameworkCore;
using PdfGeneration.Models;

namespace PdfGeneration.Data
{
    public class EnrollmentDbContext : DbContext
    {
        public EnrollmentDbContext(DbContextOptions<EnrollmentDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<EnrollmentReceipt> EnrollmentReceipts { get; set; }
    }
}
