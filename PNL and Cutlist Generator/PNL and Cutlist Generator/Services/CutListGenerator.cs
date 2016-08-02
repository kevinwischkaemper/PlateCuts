using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace PNL_and_Cutlist_Generator
{
    public class CutListGenerator
    {
        public void GenerateCutlistsPDFs(List<Cutlist> listofcutlists, List<int> listOfCutNumbers, string outputdirectory, bool print)
        {
            int index = 0;
            foreach (Cutlist cutlist in listofcutlists)
            {
                string filebatch;
                string sequences = "";
                foreach (string sequence in cutlist.SequenceList)
                {
                    sequences += $"{sequence},";
                }
                sequences = sequences.TrimEnd(new[] { ',' });
                if (!IsNullOrWhiteSpace(cutlist.Batchnumber))
                    filebatch = cutlist.Batchnumber;
                else
                {
                    filebatch = sequences;
                }
                string gradefifty;
                if (cutlist.Grade.Contains("-50"))
                    gradefifty = " GR50";
                else
                    gradefifty = "";
                string pdfname = string.Format("{0} {1} {2}{4} BATCH {3}.pdf", listOfCutNumbers[index], cutlist.JobNumber, cutlist.Thickness.Replace("/", "").Trim(), filebatch, gradefifty);
                string outputpath = Path.Combine(outputdirectory, pdfname);

                var doc = new Document(PageSize.LETTER.Rotate());
                var fs = new FileStream(outputpath, FileMode.Create, FileAccess.Write);
                var writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                var fontbold = FontFactory.GetFont("Arial", 10, Font.BOLD);
                var fontnormal = FontFactory.GetFont("Arial", 10, Font.NORMAL);
                var fontbig = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var fontbigred = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.RED);
                doc.Add(new Paragraph(" Allocated Cutlist", fontbold));

                var heading = new PdfPTable(5);
                heading.HorizontalAlignment = 0;
                heading.SpacingBefore = 1;
                heading.SpacingAfter = 10;
                heading.DefaultCell.Border = 0;
                heading.SetWidths(new int[] { 16, 9, 9, 9, 17 });
                heading.AddCell(new Phrase($"Job Number: {cutlist.JobNumber}", fontbold));
                heading.AddCell(new Phrase($"Sequence(s): {sequences}", fontbold));
                heading.AddCell(new Phrase($"Batch: {filebatch}", fontbold));
                heading.AddCell(new Phrase($"Release: {cutlist.Release}", fontbold));
                heading.AddCell(new Phrase($"Date: {DateTime.Now.ToString(new CultureInfo("en-US"))}", fontbold));
                heading.AddCell(new Phrase($"{cutlist.Thickness}    {cutlist.Grade}", fontbig));
                heading.AddCell(new Phrase(cutlist.CutMethod, fontbigred));
                heading.AddCell(new Phrase($"#{listOfCutNumbers[index]}", fontbigred));
                heading.AddCell(new Phrase(" ", fontbold));
                heading.AddCell(new Phrase(" ", fontbold));
                doc.Add(heading);

                var piecemarktable = new PdfPTable(9);
                piecemarktable.HorizontalAlignment = 0;
                piecemarktable.SpacingBefore = 10;
                piecemarktable.SpacingAfter = 10;
                piecemarktable.DefaultCell.Border = 2;
                piecemarktable.DefaultCell.HorizontalAlignment = 0;
                piecemarktable.SetWidths(new int[] { 1, 2, 2, 3, 3, 3, 2, 2, 4 });
                var qtycell = new PdfPCell(new Phrase("Qty", fontbold));
                qtycell.HorizontalAlignment = 1;
                qtycell.Border = 2;
                piecemarktable.AddCell(qtycell);
                piecemarktable.AddCell(new Phrase(" ", fontbold));
                piecemarktable.AddCell(new Phrase("Width", fontbold));
                piecemarktable.AddCell(new Phrase("Length", fontbold));
                piecemarktable.AddCell(new Phrase("Main Mark", fontbold));
                piecemarktable.AddCell(new Phrase("Assy Mark", fontbold));
                piecemarktable.AddCell(new Phrase("Seq", fontbold));

                piecemarktable.AddCell(new Phrase("Batch", fontbold));
                piecemarktable.AddCell(new Phrase("Notes", fontbold));
                piecemarktable.DefaultCell.Border = 0;
                foreach (Piecemark piecemark in cutlist.PieceMarks)
                {
                    qtycell = new PdfPCell(new Phrase(piecemark.Quantity.ToString(), fontnormal));
                    qtycell.HorizontalAlignment = 0;
                    qtycell.Border = 0;
                    piecemarktable.AddCell(qtycell);
                    piecemarktable.AddCell(new Phrase(" ", fontnormal));
                    piecemarktable.AddCell(new Phrase(piecemark.Width, fontnormal));
                    piecemarktable.AddCell(new Phrase(piecemark.Length, fontnormal));
                    piecemarktable.AddCell(new Phrase($"  {piecemark.MainMark}", fontnormal));
                    piecemarktable.AddCell(new Phrase($"   {piecemark.AssemblyMark}", fontnormal));
                    piecemarktable.AddCell(new Phrase(piecemark.Sequence, fontnormal));
                    piecemarktable.AddCell(new Phrase(filebatch, fontnormal));
                    piecemarktable.AddCell(new Phrase(piecemark.Routing, fontnormal));

                }
                doc.Add(piecemarktable);
                doc.Add(new Paragraph(cutlist.CalculateArea(), fontbold));
                doc.Add(new Paragraph($"Toal Pieces: {cutlist.TotalPieces}", fontbold));
                if (cutlist.Thickness == "PL1 1/2" || cutlist.Thickness == "PL1 1/4" || cutlist.Thickness == "PL1 3/8")
                {
                    var alreadyLogged = new List<Tuple<string, string>>();
                    foreach (Piecemark piecemark in cutlist.PieceMarks)
                    {
                        for (int i = 0; i < piecemark.HoleSizes.Count; i++)
                        {
                            var currentLog = new Tuple<string, string>(piecemark.HoleSizes[i].pieceMark, piecemark.HoleSizes[i].holeSize);
                            if (!alreadyLogged.Contains(currentLog))
                            {
                                doc.Add(new Paragraph($"{piecemark.HoleSizes[i].pieceMark} : {piecemark.HoleSizes[i].holeSize}"));
                                alreadyLogged.Add(currentLog);
                            }
                        }
                    }
                }
                doc.Close();
                fs.Close();

                if (print)
                {
                    try
                    {
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo()
                        {
                            CreateNoWindow = true,
                            Arguments = "\"" + "AM214PXER7535-DocControl" + "\"",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            UseShellExecute = true,
                            Verb = "PrintTo",
                            FileName = outputpath
                        };
                        p.Start();
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Could not print. Dagnabbit. Call James Heller and tell him you need admin privelages. Then wait five years.");
                    }
                }
                else
                    Process.Start(outputpath);

                index += 1;
            }
          
        }

        private bool IsNullOrWhiteSpace(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim().Length == 0;

        }
    }
    public class Tuple<T1, T2>
    {
        public Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public T1 Item1 { get; private set; }

        public T2 Item2 { get; private set; }
    }


}
