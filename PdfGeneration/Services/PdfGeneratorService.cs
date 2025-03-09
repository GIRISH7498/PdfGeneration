using PdfGeneration.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using RazorLight;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace PdfGeneration.Services
{
    public class PdfGeneratorService
    {
        private readonly RazorLightEngine _razorEngine;

        public PdfGeneratorService()
        {
            _razorEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Directory.GetCurrentDirectory() + "/Templates")
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<byte[]> GeneratePdfAsync(Student student)
        {
            string htmlContent = await _razorEngine.CompileRenderAsync("EnrollmentReceiptTemplate", student);

            using var document = new PdfDocument();

            PdfGenerator.AddPdfPages(document, htmlContent, PdfSharpCore.PageSize.A4);
            AddFooter(document);
            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }

        private static void AddFooter(PdfDocument pdf)
        {
            foreach (PdfPage page in pdf.Pages)
            {
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 10, XFontStyle.Italic);

                string footerText = "This is a computer-generated document and does not require any signature.";

                gfx.DrawString(footerText,
                    font,
                    XBrushes.Gray,
                    new XRect(0, page.Height - 30, page.Width, 10),
                    XStringFormats.Center);
            }
        }
    }
}
