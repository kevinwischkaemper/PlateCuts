using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PNL_and_Cutlist_Generator
{
    public class CutLog
    {
        public string JobNumber { get; set; }
        public string Sequence
        {
            get
            {
                return sequenceList.Replace('-', ',');
            }
            set
            {
                sequenceList = value;
            }
        }
        public string CutNumber
        {
            get
            {
                return cutNumberList.Replace('-', ',');
            }
            set
            {
                cutNumberList = value;
            }
        }
        public string Thickness { get; set; }
        public string CutMethod { get; set; }
        public string BatchNumber
        {
            get
            {
                return batchNumberList.Replace('-', ',');
            }
            set
            {
                batchNumberList = value;
            }
        }
        public string TotalPieces
        {
            get
            {
                int total = 0;
                foreach(int a in listOfTotalPieces)
                {
                    total += a;
                }
                return total.ToString();
            }
            set
            {
                try
                {
                    listOfTotalPieces.Add(Convert.ToInt32(value));

                }
                catch (NullReferenceException)
                {
                    listOfTotalPieces = new List<int>();
                    listOfTotalPieces.Add(Convert.ToInt32(value));
                }
            }
        }
        public bool Nested { get; set; }
        public string cutNumberList;
        public string batchNumberList;
        public string sequenceList;
        public List<int> listOfTotalPieces;
    }
}