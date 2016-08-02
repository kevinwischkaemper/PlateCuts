using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PNL_and_Cutlist_Generator
{
    public class Cutlist
    {
        public string Thickness { get; set; }
        public string Grade { get; set; }
        public List<Piecemark> PieceMarks;
        public string JobNumber;
        public string Batchnumber;
        public List<string> SequenceList = new List<string>();
        public string Release;
        public string CutMethod { get; set; }
        public string Area { get; set; }
        public int TotalPieces { get; set; }
        public List<HoleSizes> HoleSizes;

        public string CalculateArea()
        {
            double totalarea = 0;
            foreach (Piecemark piecemark in PieceMarks)
            {
                totalarea += piecemark.widthinches * piecemark.lengthinches * Convert.ToDouble(piecemark.Quantity);
            }
            if (Thickness.Contains("PL1/8"))
            {
                Area = $"48\" x {Convert.ToString(totalarea / 48)}\"";
            }
            else if (Thickness.Contains("PL3/4"))
                Area = $"96\" x {Convert.ToString(totalarea / 96)}\"";
            else
                Area = $"72\" x {Convert.ToString(totalarea / 72)}\"";
            return Area;

        }
        public string AssignCutMethod()
        {
            string cutmethod = "BT1";
            var punches = new Regex(@"(\bP\b)|(PF)|(\bPCO\b)|(\bMP\b)|(BEV)");
            int numberpunched = 0;
            int numberfit = 0;

            foreach (Piecemark piecemark in PieceMarks)
            {
                if (punches.IsMatch(piecemark.Routing))
                    numberpunched += piecemark.Quantity;
                else
                    numberfit += piecemark.Quantity;
            }
            if (Convert.ToDouble(numberpunched) / Convert.ToDouble(TotalPieces) > 0.10f)
                cutmethod = "BT2";
            if (PieceMarks.Where(x => x.Routing.Contains("BEV")).Any())
                cutmethod = "BT2";
            
            
            return cutmethod;
        }
    }
}