using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text;

namespace PNL_and_Cutlist_Generator
{
    public class SimplePDFParser
    {
        public string Parse(string pdfFilePath)
        {
            if (!File.Exists(pdfFilePath)) throw new FileNotFoundException("PDF not found!", pdfFilePath);

            var reader = new PdfReader(pdfFilePath);
            var sb = new StringBuilder();

            for (var i = 1; i < reader.NumberOfPages + 1; i++)
            {
                sb.Append(PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy()));
            }
            reader.Close();
            return sb.ToString();
        }
    }
}
