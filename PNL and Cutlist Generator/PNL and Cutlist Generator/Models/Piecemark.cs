using System;
using System.Collections.Generic;

namespace PNL_and_Cutlist_Generator
{
    public class Piecemark
    {
        public List<HoleSizes> HoleSizes;
        public string Thickness;
        public string MainMark;
        public string AssemblyMark;
        public int Quantity;
        public string Routing;
        public string Sequence;
        public double lengthinches;
        string length;
        public string Length
        {
            get { return length; }
            set 
            {
                var trimmedvalue = value.Trim();
                var component = trimmedvalue.Split(new[] { '\'', '-', '/' });
                lengthinches = 12 * Convert.ToDouble(component[0]);
                if (component.Length > 1 )
                {
                    if (!IsNullOrWhiteSpace(component[1]))
                        lengthinches += Convert.ToDouble(component[1]);
                }
                if (component.Length == 4)
                    lengthinches += Convert.ToDouble(component[2]) / Convert.ToDouble(component[3]);
                length = value;
            }
        }
        public double widthinches;
        string width;
        public string Width
        {
            get { return width; }
            set
            {
                var trimmedvalue = value.Trim();
                var component = trimmedvalue.Split(new[] { '-', '/' });
                widthinches = Convert.ToDouble(component[0]);
                if (component.Length > 1)
                    widthinches += Convert.ToDouble(component[1]) / Convert.ToDouble(component[2]);
                width = value;
            }
        }

        private bool IsNullOrWhiteSpace(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim().Length == 0;

        }
    }
}