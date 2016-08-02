using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PNL_and_Cutlist_Generator
{
    internal class DSTVProcessor
    {
        string JobNumber;
        public DSTVProcessor(string jobNumber)
        {
            JobNumber = jobNumber;
        }
        public List<HoleSizes> FindHoleDiameters(string piecemark)
        {
            string partFile = FileHelper.GetFiles(FindJobCNCDirectory()).ToList().Where(x => Path.GetFileNameWithoutExtension(x) == piecemark).First();
            string text = File.ReadAllText(partFile);

            var blocktitlematch = new Regex("\\n[A-Z][A-Z]\\r\\n");
            List<Block> blocklist = new List<Block>();
            foreach (Match blockmatch in blocktitlematch.Matches(text))
            {
                if (blockmatch.Value.Contains("ST"))
                    continue;
                if (blockmatch.Value.Contains("EN"))
                    break;
                Block block = new Block();
                block.Lines = new List<string>();
                var splitblock = text.Substring(blockmatch.Index, blockmatch.NextMatch().Index - blockmatch.Index).Split(new[] { '\r' });
                block.Type = splitblock[0].TrimStart(new[] { '\n' });
                for (int i = 1; i < splitblock.Count() - 1; i++)
                {
                    block.Lines.Add(splitblock[i].TrimStart(new[] { '\n' }));
                }
                blocklist.Add(block);
            }

            var holeList = new List<HoleSizes>();
            foreach (Block block in blocklist.Where(x => x.Type == "BO"))
            {
                for (int i = 0; i < block.Lines.Count; i++)
                {
                    string[] blocklinevalues = Regex.Split(block.Lines[i].Trim(), @"\s+");
                    int first;
                    if (IsNullOrWhiteSpace(blocklinevalues[0]) || Regex.IsMatch(blocklinevalues[0], @"[a-z]"))
                        first = 1;
                    else
                        first = 0;
                    string holeDiameter = blocklinevalues[first + 2];
                    var hole = new HoleSizes(){ holeSize = holeDiameter, pieceMark = piecemark };
                    holeList.Add(hole);
                }
            }
            return holeList;
        }

        internal string FindJobCNCDirectory()
        {
            try
            {
                string[] anyjobfolders = Directory.GetDirectories(@"Z:\Drawings in Process\2015 JOBS");
                var listfolders = anyjobfolders.ToList<string>();
                string rightjob = anyjobfolders.ToList<string>().Where(p => p.Contains(JobNumber.ToString())).First<string>();
                string[] rightjobfolders = Directory.GetDirectories(rightjob);
                listfolders = anyjobfolders.ToList<string>();
                return rightjobfolders.ToList<string>().Where(p => p.Contains("NC1")).First<string>();
            }
            catch (Exception)
            {
                try
                {
                    string[] anyjobfolders = Directory.GetDirectories(@"Z:\Drawings in Process\2016 JOBS");
                    var listfolders = anyjobfolders.ToList<string>();
                    string rightjob = anyjobfolders.ToList<string>().Where(p => p.Contains(JobNumber.ToString())).First<string>();
                    string[] rightjobfolders = Directory.GetDirectories(rightjob);
                    listfolders = anyjobfolders.ToList<string>();
                    return rightjobfolders.ToList<string>().Where(p => p.Contains("NC1")).First<string>();
                }
                catch (Exception)
                {
                    return "Job NC1s folder not found";
                }
            }
        }

        private bool IsNullOrWhiteSpace(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim().Length == 0;

        }
    }
}