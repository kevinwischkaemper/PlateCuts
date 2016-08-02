using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PNL_and_Cutlist_Generator
{
    public class PNL_Generator
    {
        public void GeneratePNLs(List<Cutlist> listofcutlists, string outputdirectory, List<int> listOfCutNumbers)
        {
            int index = 0;
            foreach (Cutlist cutlist in listofcutlists)
            {
                string filebatch;
                string sequences = "";
                foreach (string sequence in cutlist.SequenceList)
                {
                    sequences += $"{sequence},".TrimEnd(new[] { ',' });
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
                string pnlname = string.Format("{0} {1} {2}{4} BATCH {3}.pnl", listOfCutNumbers[index], cutlist.JobNumber, cutlist.Thickness.Replace("/", "").Trim(), filebatch, gradefifty);
                string outputpath = Path.Combine(outputdirectory, pnlname);
                var csv = new StringBuilder();
                string mark;

                string plasmaclass;
                if (cutlist.Thickness == "PL1/8" || cutlist.Thickness == "PL3/16")
                    plasmaclass = @"130Amp O2/Air [Bevel]";
                else if (cutlist.Thickness == "PL1/4" || cutlist.Thickness == "PL5/16")
                    plasmaclass = @"130Amp O2/Air (True Hole)";
                else if (cutlist.Thickness == "PL3/8")
                    plasmaclass = @"130Amp O2/Air [True Bevel] (True Hole)";
                else if (cutlist.Thickness == "PL1/2" || cutlist.Thickness == "PL5/8")
                    plasmaclass = @"260Amp O2/Air [Bevel] (True Hole)";
                else if (cutlist.Thickness == "PL3/4")
                    plasmaclass = @"260Amp O2/Air [True Bevel] (True Hole)";
                else if (cutlist.Thickness == "PL7/8")
                    plasmaclass = @"400Amp O2/Air [Bevel] (True Hole)";
                else if (cutlist.Thickness == "PL1")
                    plasmaclass = @"400Amp O2/Air [True Bevel] (True Hole)";
                else
                    plasmaclass = "   ";
                string remarks = $"cutnumber{listOfCutNumbers[index]}";
                double decthickness = FractionToDouble(cutlist.Thickness.TrimStart(new[] { 'P', 'L' }));
                if (cutlist.Thickness == "PL1/8")
                    decthickness = 0.1350;
                var blanks = "\n\n\n\n";
                csv.Append(blanks);
                foreach (Piecemark piecemark in cutlist.PieceMarks)
                {
                    if (!IsNullOrWhiteSpace(piecemark.AssemblyMark))
                        mark = piecemark.AssemblyMark;
                    else
                        mark = piecemark.MainMark;
                    var newline = $"G:\\BURNING TABLE 1\\2015 JOBS\\{cutlist.JobNumber}\\Batch {cutlist.Batchnumber}\\{cutlist.Thickness.Replace("/", "").Trim(' ')}\\{mark}.dxf,0,{piecemark.Quantity},0,5,0.00,0.00,I,2,0,CADFILE,0,   ,{mark},3,MS,{decthickness:F4},   ,   ,   ,   ,0,   ,<none>,{remarks},{plasmaclass},0,0,   ,   ,-1";
                    csv.AppendLine(newline);
                }

                File.WriteAllText(outputpath, csv.ToString());
                index += 1;
            }
        }
        double FractionToDouble(string fraction)
        {
            double result;

            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }
        private bool IsNullOrWhiteSpace(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim().Length == 0;

        }
    }
}
