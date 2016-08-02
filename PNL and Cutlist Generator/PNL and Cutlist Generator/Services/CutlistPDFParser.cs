using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PNL_and_Cutlist_Generator
{
    public class CutlistPDFParser
    {
        public int additionalCutNumbersNeeded = 0;

        public List<Cutlist> Parse(string filepath)
        {
            List<Cutlist> listofcutlists = new List<Cutlist>();
            SimplePDFParser _pdfparser = new SimplePDFParser();
            string text = _pdfparser.Parse(filepath);

            var newpageregex = new Regex("Allocated");
            var newpagematches = newpageregex.Matches(text);
            int indexoffset = 0;
            foreach (Match match in newpagematches)
            {
                text = text.Insert(match.Index + indexoffset, "\n");
                indexoffset += 1;
            }
            text = text.TrimStart(new[] { '\n' });
     
            var textlinearray = text.Split(new[] { '\n' });
            var spaces = new[] { ' ' };

            string batchnumber;
            try
            {
                batchnumber = textlinearray[2].Split(spaces)[1].TrimStart(new[] { '0' });
            }
            catch (Exception)
            {
                batchnumber = "";
            }

            string release = textlinearray[1].Split(spaces)[1].TrimStart(new[] { '0' });
            string jobnumber = textlinearray[1].Split(spaces)[4];

            var thicknessregex = new Regex(@"(\bPL\d\s((\d\d|\d)\/(\d\d|\d))\b)|(\bPL\d\s)|(\bPL((\d\d|\d)\/(\d\d|\d))\b)|(\bSH\d\d\b)");
            var newlineregex = new Regex(@"\n");
            var thicknessmatches = thicknessregex.Matches(text);
            var newlinematches = newlineregex.Matches(text);
            
            foreach (Match thickmatch in thicknessmatches)
            {
                var sequences = new List<string>();
                var BTOneMarks = new PiecemarkGroup();
                var BTTwoMarks = new PiecemarkGroup();
                var piecemarks = new List<Piecemark>();
                var thickidx = thickmatch.Index;
                int lineindex = 0;
                foreach (Match linematch in newlinematches)
                {
                    if (linematch.Index > thickidx)
                        break;
                    lineindex += 1;
                }
                var firstline = lineindex;
                string grade = "";
                while (true)
                {
                    try
                    {
                        if (textlinearray[lineindex].Contains("Scrap"))
                            break;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                    if (textlinearray[lineindex].Contains("Scrap"))
                        break;
                    if (textlinearray[lineindex].Contains("Allocated"))
                    {
                        lineindex += 5;
                        continue;
                    }
                    int thickfound = 0;
                    
                    if (lineindex == firstline)
                    {
                        var firstsplitline = textlinearray[lineindex].Split(spaces);
                        grade = firstsplitline[3];
                        if (IsNullOrWhiteSpace(grade))
                            grade = firstsplitline[4];
                        thickfound = 1;
                    }
                    int mixednumoffset = 0;
                    if (thickmatch.Value.Length > 6)
                        mixednumoffset = 1;
                    var splitline = textlinearray[lineindex].Split(spaces);
                    var reversesplitline = textlinearray[lineindex].Split(spaces).Reverse();
                    string mainmark = splitline[(7 + mixednumoffset) * thickfound + 2 * (1 - thickfound)];
                    string assemblymark = splitline[(8 + mixednumoffset) * thickfound + 3 * (1 - thickfound)];
                    string length = splitline[(6 + mixednumoffset) * thickfound + 1 * (1 - thickfound)];
                    string quantity = splitline[(5 + mixednumoffset) * thickfound + 0 * (1 - thickfound)];
                    string width;
                    if (!IsNullOrWhiteSpace(batchnumber))
                    width = reversesplitline.ElementAt(1);
                    else
                    width = reversesplitline.ElementAt(0);

                    string routing = "";
                    var YNreg = new Regex(@"((\bY\b)|(\bN\b))");
                    int firstrouting;
                    if (!IsNullOrWhiteSpace(batchnumber))
                        firstrouting = 3;
                    else
                        firstrouting = 2;
                    int routingIndex = 0;
                    for (int i = firstrouting; !YNreg.IsMatch(reversesplitline.ElementAt(i)); i++)
                    {
                        routing = reversesplitline.ElementAt(i) + " " + routing;
                        routingIndex = i;
                    };

                    string sequence = "";
                    //var numreg = new Regex(@"\d+");

                    //for (int i = routingIndex; !numreg.IsMatch(reversesplitline.ElementAt(i)); i++)
                    //{
                    //    sequence = reversesplitline.ElementAt(i + 1);
                    //}
                    for (int i = routingIndex + 1; reversesplitline.ElementAt(i) == "Y" || reversesplitline.ElementAt(i) == "N" || string.IsNullOrEmpty(reversesplitline.ElementAt(i)); i++)
                    {
                        sequence = reversesplitline.ElementAt(i + 1);
                    }

                    if (assemblymark == sequence)
                        assemblymark = "";
                    if (!routing.Contains("BUYOUT"))
                    {
                        var part = new Piecemark()
                        {
                            AssemblyMark = assemblymark,
                            MainMark = mainmark,
                            Length = length,
                            Quantity = Convert.ToInt32(quantity),
                            Width = width,
                            Routing = routing,
                            Thickness = thickmatch.Value,
                            Sequence = sequence
                        };
                        bool duplicate = false;
                        foreach (Piecemark addedpart in piecemarks)
                        {
                            if ((addedpart.AssemblyMark == part.AssemblyMark && !IsNullOrWhiteSpace(part.AssemblyMark)) || (IsNullOrWhiteSpace(part.AssemblyMark) && addedpart.MainMark == part.MainMark && IsNullOrWhiteSpace(addedpart.AssemblyMark)))
                            {
                                addedpart.Quantity += part.Quantity;
                                duplicate = true;
                            }
                        }
                        if (duplicate == false)
                            piecemarks.Add(part);
                    }
                    lineindex += 1;
                }
                var punches = new Regex(@"(\bP\b)|(PF)|(\bPCO\b)|(\bMP\b)|(BEV)|(\bFP\b)|(DR)");
                piecemarks = piecemarks.OrderByDescending(x => punches.IsMatch(x.Routing)).ThenBy(y => y.Quantity).ToList();
                foreach (Piecemark piecemark in piecemarks)
                {
                    sequences.Add(piecemark.Sequence);
                }
                if (thickmatch.Value == "PL1 1/2" || thickmatch.Value == "PL1 1/4" || thickmatch.Value == "PL1 3/8" || thickmatch.Value == "PL1 ")
                {
                    try
                    {
                        var jobDSTVProcesssor = new DSTVProcessor(jobnumber);
                        foreach (Piecemark piecemark in piecemarks)
                        {
                            string currentMark;
                            if (IsNullOrWhiteSpace(piecemark.AssemblyMark))
                                currentMark = piecemark.MainMark;
                            else
                                currentMark = piecemark.AssemblyMark;
                            var holeSizes = jobDSTVProcesssor.FindHoleDiameters(currentMark).Distinct().ToList();
                            piecemark.HoleSizes = holeSizes;
                        }
                    }
                    catch (Exception)
                    {
                         
                    }
                    
                }
                
                List<List<Piecemark>> ListOfGroups = new List<List<Piecemark>>();
                if (piecemarks.Count != 0)  //split the piecemarks into a List of list of piecemarks ("groups"), one list for each cutlist, only one cutmethod per list, max 29 per list
                {
                    bool first = true;
                    bool firstPlain = true;
                    int index = 0;
                    ListOfGroups.Add(new List<Piecemark>());
                    foreach (Piecemark piecemark in piecemarks)
                    {
                        if (firstPlain == true)
                        {
                            if (!punches.IsMatch(piecemark.Routing))
                            {
                                if (!first)
                                {
                                    index += 1;
                                    ListOfGroups.Add(new List<Piecemark>());
                                }
                                firstPlain = false;
                            }
                        }
                        if (ListOfGroups[index].Count == 29)
                        {
                            index += 1;
                            ListOfGroups.Add(new List<Piecemark>());
                        }
                        ListOfGroups[index].Add(piecemark);
                        first = false;
                    }
                }
                List<List<Piecemark>> combineableGroups = ListOfGroups.Where(x => x.Count < 29).ToList();
                if (combineableGroups.Count == 2)
                {
                    int BTTwoCount = 0;
                    int BTOneCount = 0;
                    foreach (Piecemark mark in combineableGroups[0]) //bt2
                    {
                        BTTwoCount += mark.Quantity;
                    }
                    foreach (Piecemark mark in combineableGroups[1]) //bt1
                    {
                        BTOneCount += mark.Quantity;
                    }
                    float percentBTTwo = (float)BTTwoCount / ((float)BTTwoCount + (float)BTOneCount);
                    float percentBTOne = (float)BTOneCount / ((float)BTTwoCount + (float)BTOneCount);
                    if (combineableGroups[0].Where(x => x.Routing.Contains("BEV")).Any() || percentBTTwo > 0.15)
                    {
                        if (BTOneCount < 40)
                        {
                            ListOfGroups = ListOfGroups.Where(x => x.Count == 29).ToList();
                            ListOfGroups.Add(combineableGroups[0].Union(combineableGroups[1]).ToList());
                        }
                    }
                    else if (percentBTTwo < 0.15 && BTTwoCount < 40)
                    {
                        ListOfGroups = ListOfGroups.Where(x => x.Count == 29).ToList();
                        ListOfGroups.Add(combineableGroups[0].Union(combineableGroups[1]).ToList());
                    }
                }
                foreach (List<Piecemark> piecemarkList in ListOfGroups)
                {
                    var currentCutlist = new Cutlist();
                    currentCutlist.Batchnumber = batchnumber;
                    currentCutlist.JobNumber = jobnumber;
                    currentCutlist.Release = release;
                    currentCutlist.Thickness = thickmatch.Value;
                    currentCutlist.Grade = grade;
                    currentCutlist.SequenceList = sequences.Distinct().ToList();
                    currentCutlist.PieceMarks = piecemarkList;
                    currentCutlist.TotalPieces = CalculateTotalPieces(piecemarkList);
                    currentCutlist.Area = currentCutlist.CalculateArea();
                    currentCutlist.CutMethod = AssignCutMethod(currentCutlist);
                    listofcutlists.Add(currentCutlist);
                    additionalCutNumbersNeeded += 1;
                }
                if (piecemarks.Count > 0)
                {
                    additionalCutNumbersNeeded -= 1;
                }
            }
            return listofcutlists;
        }

        private int CalculateTotalPieces(List<Piecemark> piecemarks)
        {
            int totalpieces = 0;
            foreach (Piecemark piecemark in piecemarks)
            {
                totalpieces += piecemark.Quantity;
            }
            return totalpieces;
        }

        private string AssignCutMethod(Cutlist cutlist)
        {
            string cutmethod = "BT1";
            int numberBTTwo = 0;
            int totalpieces = 0;
            var punches = new Regex(@"(\bP\b)|(PF)|(\bPCO\b)|(\bMP\b)|(BEV)");
            foreach (Piecemark piecemark in cutlist.PieceMarks)
            {
                if (punches.IsMatch(piecemark.Routing))
                    numberBTTwo += piecemark.Quantity;
                totalpieces += piecemark.Quantity;
            }
            int numberBTOne = totalpieces - numberBTTwo;
            if (Convert.ToDouble(numberBTTwo) / Convert.ToDouble(totalpieces) > 0.12f)
                cutmethod = "BT2";
            if (cutlist.PieceMarks.Where(x => x.Routing.Contains("BEV")).Any())
                cutmethod = "BT2";
            return cutmethod;
        }

        private string CalculateCutMethod(List<Piecemark> piecemarks)
        {
            string cutmethod = "BT1";
            int numberBTTwo = 0;
            int totalpieces = 0;
            var punches = new Regex(@"(\bP\b)|(PF)|(\bPCO\b)|(\bMP\b)|(BEV)");
            foreach (Piecemark piecemark in piecemarks)
            {
                if (punches.IsMatch(piecemark.Routing))
                    numberBTTwo += piecemark.Quantity;
                totalpieces += piecemark.Quantity;
            }
            int numberBTOne = totalpieces - numberBTTwo;
            if (Convert.ToDouble(numberBTTwo) / Convert.ToDouble(totalpieces) > 0.145f)
                cutmethod = "BT2";
            if (piecemarks.Where(x => x.Routing.Contains("BEV")).Any())
                cutmethod = "BT2";
            return cutmethod;
        }

        private bool IsNullOrWhiteSpace(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim().Length == 0;

        }
    }
}
