using System;

namespace PNL_and_Cutlist_Generator
{
    public class HoleSizes
    {
        public string pieceMark { get; set; }
        public string holeSize
        {
            get
            {
                return milSize.ConvertMilsToFracInches();
            }
            set
            {
                milSize = Convert.ToDouble(value);
            }
        }
        private double milSize;
    }
}