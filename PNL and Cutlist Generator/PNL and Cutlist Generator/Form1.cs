using Equin.ApplicationFramework;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NestManagerLibrary;
using PNL_and_Cutlist_Generator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PNL_and_Cutlist_Generator
{
    public partial class Form1 : Form
    {
        List<Nest> ListOfNests = new List<Nest>();
        List<Nest> ListOfNewNests = new List<Nest>();
        BindingList<ToDoItem> ListOfToDoItems = new BindingList<ToDoItem>();
        static NestManager NestManager;
        private string SelectedCutlists;
        List<Cutlist> ListOfCutlists = new List<Cutlist>();
        public List<string> LoggedJobNumbers = new List<string>();
        
        public static List<CutLog> ListOfCutLogs = new List<CutLog>();
        public static BindingListView<CutLog> LogMainDataSource = new BindingListView<CutLog>(ListOfCutLogs);

        private static string filterJobNumber = @".+";
        private static string filterSequence = @".+";
        private static string filterBatchNumber = @".+";
        private static string filterCutMethod = @"BT2";
        private static string filterMainThickness = @".+";
        private static string filterNested = @"(True)|(False)";

        private string LogFile = @"G:\Kevins Scripts, Lisps, Bats, AHK\Apps\PlateCuts\cutlistlog.csv";
        bool print;

        public static string CutSheetFolderPath = @"G:\CUT SHEET FOLDER";
        public int FirstAdditionalCutnumber;
        public int NumberOfAdditionalCutnumbers;
        public List<int> CutNumberList = new List<int>();
        FileStream LogStream;
        FileStream AccesssStream;
        static bool WaitingForRefresh = false;


        public Form1()
        {
            InitializeComponent();
            NestManager = new NestManager(dgvNestManager, comboNestManagerThickness, btnRemoveFromNestManager, btnPromoteNest, btnCustomNest, false);
            NestManager.OnRefresh += new NestManager.RefreshHandler(UpdateNestList);
            dgvToDoList.DataSource = ListOfToDoItems;
        }

        private void UpdateNestList(object sender, RefreshEventArguments e)
        {
            List<Nest> UnNestedNests = e.NestList.Where(x => x.Nested == false).ToList();
            ListOfToDoItems.Clear();
            if (UnNestedNests.Count == 0)
            {
                if (WaitingForRefresh)
                    dgvCutLog.Columns[7].ReadOnly = false;
                return;
            }
            foreach (Nest nest in UnNestedNests)
            {
                if (nest.CutNumber == "RECUT")
                {
                    ToDoItem newItem = new ToDoItem()
                    {
                        BatchNumber = nest.BatchNumber,
                        Thickness = nest.Thickness,
                        CutNumber = nest.CutNumber,
                        Action = "who knows"
                    };
                    ListOfToDoItems.Add(newItem);
                }
                else
                {
                    ToDoItem newItem = new ToDoItem()
                    {
                        BatchNumber = nest.BatchNumber,
                        Thickness = nest.Thickness,
                        CutNumber = nest.CutNumber,
                        Action = "who knows"
                    };
                    ListOfToDoItems.Add(newItem);
                    ListOfCutLogs.Where(x => x.CutNumber == nest.CutNumber && x.BatchNumber == nest.BatchNumber && x.Thickness.Replace("/", "") == nest.Thickness.Replace("_", " ")).ToList().ForEach(y => y.Nested = false);
                }
            }
            dgvCutLog.Refresh();
            if (WaitingForRefresh)
                dgvCutLog.Columns[7].ReadOnly = false;
        }

        private void ApplyMainFilters()
        {
            LogMainDataSource.ApplyFilter(x =>
            Regex.IsMatch(x.JobNumber, filterJobNumber)
            && x.Sequence.Split(new[] { ',' }).Any(p => Regex.IsMatch(p, filterSequence))
            && x.BatchNumber.Split(new[] { ',' }).Any(p => Regex.IsMatch(p, filterBatchNumber))
            && Regex.IsMatch(x.CutMethod, filterCutMethod)
            && Regex.IsMatch(x.Thickness, filterMainThickness)
            && Regex.IsMatch(x.Nested.ToString(), filterNested));
        }

        private void UpdateThicknessComboBox()
        {
            var sourceList = ListOfCutLogs.Where(x =>
            Regex.IsMatch(x.JobNumber, filterJobNumber)
            && x.Sequence.Split(new[] { ',' }).Any(p => Regex.IsMatch(p, filterSequence))
            && x.BatchNumber.Split(new[] { ',' }).Any(p => Regex.IsMatch(p, filterBatchNumber))
            && Regex.IsMatch(x.CutMethod, filterCutMethod)
            && Regex.IsMatch(x.Nested.ToString(), filterNested)).Select(x => x.Thickness).Distinct().ToList();
            sourceList.Add("All");
            comboPlateThickness.DataSource = sourceList;
            int currentIndex = comboPlateThickness.FindString(filterMainThickness.TrimStart(new[] { '^' }).TrimEnd(new[] { '$' }));
            if (currentIndex == -1)
            {
                currentIndex = comboPlateThickness.FindString("All");
                filterMainThickness = @".+";
            }
            comboPlateThickness.SelectedIndex = currentIndex;
            comboPlateThickness.Update();
            ApplyMainFilters();
        }

        private void SaveMainLog()
        {
            LogMainDataSource.RemoveFilter();
            var csvlog = new StringBuilder();
            string nested;
            foreach (CutLog cutlog in ListOfCutLogs)
            {
                if (cutlog.Nested == true)
                    nested = "nested";
                else
                    nested = " ";
                string totalPiecesList = "";
                foreach (int total in cutlog.listOfTotalPieces)
                {
                    totalPiecesList = $"{totalPiecesList}-{total.ToString()}".TrimStart(new[] { '-' });
                }
                csvlog.AppendLine($"{cutlog.JobNumber}, {cutlog.sequenceList}, {cutlog.cutNumberList}, {cutlog.Thickness.Trim()}, {cutlog.batchNumberList}, {cutlog.CutMethod}, {totalPiecesList}, {nested}");
            }
            LogStream.Dispose();
            File.WriteAllText(LogFile, csvlog.ToString());
            LogStream = new FileStream(LogFile, FileMode.Open, FileAccess.ReadWrite);
        }

        public void MainFormLoad(object sender, EventArgs e)
        {
            try
            {
                AccesssStream = new FileStream(@"G:\Kevins Scripts, Lisps, Bats, AHK\Apps\PlateCuts\access.txt", FileMode.Open, FileAccess.ReadWrite);
                LogStream = new FileStream(LogFile, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not optain write access to log file");
                Application.Exit();
            }
            var accessWriter = new StreamWriter(AccesssStream);
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            accessWriter.WriteLine(string.Format("{0}", userName));
            accessWriter.Dispose();
            LogStream.Dispose();
            string[] text = File.ReadAllLines(LogFile);
            LogStream = new FileStream(LogFile, FileMode.Open, FileAccess.ReadWrite);
            foreach (string line in text)
            {
                var splitline = line.Split(new[] { ',' });
                if (splitline[0].Length != 10)
                    continue;

                var log = new CutLog
                {
                    JobNumber = splitline[0].Trim(),
                    Sequence = splitline[1].Trim(),
                    CutNumber = splitline[2].Trim(),
                    Thickness = splitline[3].Trim(),
                    BatchNumber = splitline[4].Trim(),
                    CutMethod = splitline[5].Trim()
                };

                if (log.BatchNumber.Trim().Length == 0 || string.IsNullOrEmpty(log.BatchNumber))
                    log.BatchNumber = log.Sequence;
                if (splitline[7].Contains("nest"))
                    log.Nested = true;
                try
                {
                    log.listOfTotalPieces = splitline[6].Split(new[] { '-' }).Select(x => Convert.ToInt32(x)).ToList();
                }
                catch (Exception)
                {
                    log.listOfTotalPieces.Add(0);
                }
                ListOfCutLogs.Add(log);
                LoggedJobNumbers.Add(log.JobNumber.Trim());
                LoggedJobNumbers.Add("All");
            }
            this.comboJobNumbers.DataSource = LoggedJobNumbers.Distinct().ToList();
            this.comboJobNumbers.Update();
            dgvCutLog.DataSource = LogMainDataSource;
            DataGridViewButtonColumn openCutlistPDFButtonColumn = new DataGridViewButtonColumn();
            openCutlistPDFButtonColumn.Name = "Open Cutlist";
            openCutlistPDFButtonColumn.Text = "Open Cutlist";
            openCutlistPDFButtonColumn.UseColumnTextForButtonValue = true;
            int columnIndex = 8;
            if (dgvCutLog.Columns["Open Cutlist"] == null)
            {
                dgvCutLog.Columns.Insert(columnIndex, openCutlistPDFButtonColumn);
            }
            ApplyMainFilters();
            dgvCutLog.Update();
            for (int i = 0; i < 7; i++)
            {
                dgvCutLog.Columns[i].ReadOnly = true;
            }
        }
        
        private void ComboJobNumberChanged(object sender, EventArgs e)
        {            
            if (comboJobNumbers.SelectedValue.ToString() == "All")
            {
                filterJobNumber = @".+";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
            else
            {
                filterJobNumber = comboJobNumbers.SelectedValue.ToString();
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
            txtSequences.Text = "";
            txtBatches.Text = "";
            var args = new EventArgs();
            FilterBySequences(sender, args);
            FilterByBatch(sender, args);
        }

        private void SelectCutlist(object sender, EventArgs e)
        {
            this.txtCutNumber.Text = "";
            lblSelectedCutlistJob.Visible = false;
            lblSelectedCutlistBatch.Visible = false;
            lblSelectedCutlistRelease.Visible = false;
            lblCutlistNumberQty.Visible = false;
            lblAdditonalNumberQuantity.Visible = false;
            lblAdditionalNumbers.Visible = false;
            textStartingAdditionalNumber.Visible = false;
            DialogResult result = this.filedialogSelectCutlist.ShowDialog();
            if (result == DialogResult.OK)
            {
                SelectedCutlists = this.filedialogSelectCutlist.FileName;
                this.lblComplete.Text = "";
                this.filedialogSelectCutlist.InitialDirectory = Path.GetDirectoryName(SelectedCutlists);
                if (IsNullOrWhiteSpace(SelectedCutlists) || !File.Exists(SelectedCutlists))
                    return;
                CutlistPDFParser parser = new CutlistPDFParser();
                ListOfCutlists = parser.Parse(SelectedCutlists);
                dgvCutListGeneratorView.DataSource = ListOfCutlists;
                dgvCutListGeneratorView.Update();
                this.btnGeneratePDF.Enabled = true;
                try
                {
                    this.txtCutNumber.Text = Regex.Match(Regex.Match(File.ReadAllText(SelectedCutlists), @"Contents\(\d+\)").Value, @"\d+").Value;
                }
                catch (Exception)
                {
                }
                if (this.txtCutNumber.Text == "")
                {
                    try
                    {
                        this.txtCutNumber.Text = Regex.Match(Regex.Match(File.ReadAllText(SelectedCutlists), @"Tm \(\d+\)").Value, @"\d+").Value;
                    }
                    catch (Exception)
                    {
                        this.txtCutNumber.Text = "Not Found";
                    }
                }
                NumberOfAdditionalCutnumbers = parser.additionalCutNumbersNeeded;
                if (NumberOfAdditionalCutnumbers > 0)
                {
                    FirstAdditionalCutnumber = ShowAdditonalCutNumberDialog(parser.additionalCutNumbersNeeded);
                }
                lblCutlistNumberQty.Text = $"{ListOfCutlists.Count - NumberOfAdditionalCutnumbers} needed";
                lblCutlistNumberQty.Visible = true;
                if (NumberOfAdditionalCutnumbers > 0)
                {
                    lblAdditionalNumbers.Visible = true;
                    textStartingAdditionalNumber.Visible = true;
                    lblAdditonalNumberQuantity.Text = $"{NumberOfAdditionalCutnumbers} additional needed";
                    lblAdditonalNumberQuantity.Visible = true;
                }
                lblSelectedCutlistJob.Text = $"Job {ListOfCutlists.First().JobNumber}";
                lblSelectedCutlistJob.Visible = true;
                lblSelectedCutlistBatch.Text = $"Batch {ListOfCutlists.First().Batchnumber}";
                lblSelectedCutlistBatch.Visible = true;
                lblSelectedCutlistRelease.Text = $"Release {ListOfCutlists.First().Release}";
                lblSelectedCutlistRelease.Visible = true;
            }
        }

        private bool IsNullOrWhiteSpace(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim().Length == 0;

        }

        public int ShowAdditonalCutNumberDialog(int additionalCutNumbers)
        {
            Form2 testDialog = new Form2(additionalCutNumbers);

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                if (testDialog.txtResult.Text == "continue")
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        return Convert.ToInt32(testDialog.txtResult.Text);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Starting additional cut number must be an integer");
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
                //this.txtResult.Text = "Cancelled";
            }
        }

        private void GeneratePDFs(object sender, EventArgs e)
        {
            CutNumberList.Clear();

            if (IsNullOrWhiteSpace(this.txtCutNumber.Text))
            {
                MessageBox.Show("You must input a starting cutsheet number first");
                return;
            }
            try
            {
                Convert.ToInt32(this.txtCutNumber.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Starting cutsheet number must be an integer");
                return;
            }
            int _startingCutNumber = Convert.ToInt32(this.txtCutNumber.Text);
            int incrementedNumber = _startingCutNumber;
            int incrementedAdditionalNumbers = FirstAdditionalCutnumber;
            for (int p = 0; p < ListOfCutlists.Count - NumberOfAdditionalCutnumbers; p++)
            {
                CutNumberList.Add(incrementedNumber);
                incrementedNumber += 1;
            }
            if (incrementedAdditionalNumbers > 0)
            {
                for (int q = 0; q < NumberOfAdditionalCutnumbers; q++)
                {
                    CutNumberList.Add(incrementedAdditionalNumbers);
                    incrementedAdditionalNumbers += 1;
                }
            }
            else
            {
                for (int q = 0; q < NumberOfAdditionalCutnumbers; q++)
                {
                    CutNumberList.Add(incrementedNumber);
                    incrementedNumber += 1;
                }
            }
                 
            
            string _outputdirectory = Path.GetDirectoryName(SelectedCutlists);
            
            var cutlistgen = new CutListGenerator();
            if (this.chkPrint.CheckState == CheckState.Checked)
                print = true;
            else
                print = false;

            cutlistgen.GenerateCutlistsPDFs(ListOfCutlists, CutNumberList, _outputdirectory, print);

            var pnlgen = new PNL_Generator();
            pnlgen.GeneratePNLs(ListOfCutlists, _outputdirectory, CutNumberList);

            if (this.chkLog.CheckState == CheckState.Checked)
            {
                var csvlog = new StringBuilder();
                var newcutnumber = _startingCutNumber;
                int lognumberIndex = 0;
                foreach (Cutlist cutlist in ListOfCutlists)
                {
                    string sequences = "";
                    foreach (string sequence in cutlist.SequenceList)
                    {
                        sequences += $"{sequence}-";
                    }
                    sequences = sequences.TrimEnd(new[] { '-' });
                    string gradeFifty = " ";
                    if (cutlist.Grade.Contains("-50"))
                    {
                        gradeFifty = "GR50";
                    }
                    var cutlog = new CutLog()
                    {
                        JobNumber = cutlist.JobNumber.Trim(),
                        sequenceList = sequences,
                        cutNumberList = CutNumberList[lognumberIndex].ToString().Trim(),
                        Thickness = $"{cutlist.Thickness.Trim()} {gradeFifty}",
                        batchNumberList = cutlist.Batchnumber.Trim(),
                        CutMethod = cutlist.CutMethod.Trim(),
                        listOfTotalPieces = new List<int>() { cutlist.TotalPieces }
                    };
                    ListOfCutLogs.Add(cutlog);
                    lognumberIndex += 1;
                }
                SaveMainLog();
            }
            SelectedCutlists = "";
            this.lblComplete.Text = "Complete";
        }

        private void MainFormClose(object sender, FormClosedEventArgs e)
        {
            SaveMainLog();
            LogStream.Dispose();
            AccesssStream.Dispose();
        }

        private void FilterBySequences(object sender, EventArgs e)
        {
            if (IsNullOrWhiteSpace(txtSequences.Text))
            {
                filterSequence = @".+";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
            else
            {
                var splitValues = txtSequences.Text.Split(new[] { ',' });
                for(int i = 0; i < splitValues.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (splitValues[0].Contains("-"))
                        {
                            var splitRange = splitValues[0].Split(new[] { '-' });
                            bool first = true;
                            for (int p = Convert.ToInt32(splitRange[0]); p < Convert.ToInt32(splitRange[1]) + 1; p++)
                            {
                                if (first)
                                {
                                    filterSequence = $"(^{p.ToString()}$)";
                                    first = false;
                                }
                                else filterSequence = $"{filterSequence}|(^{p.ToString()}$)";
                            }
                        }
                        else
                        filterSequence = $"(^{splitValues[0]}$)";
                    }
                    else
                    {
                        if (splitValues[i].Contains("-"))
                        {
                            var splitRange = splitValues[i].Split(new[] { '-' });
                            for (int p = Convert.ToInt32(splitRange[0]); p < Convert.ToInt32(splitRange[1]) + 1; p++)
                            {
                                filterSequence = $"{filterSequence}|(^{p.ToString()}$)";
                            }
                        }
                        else
                        filterSequence = $"{filterSequence}|(^{splitValues[i]}$)";
                    }
                }
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
        }

        private void CheckShowBTOneCutlists(object sender, EventArgs e)
        {
            if (checkShowBTOne.CheckState == CheckState.Checked)
            {
                filterCutMethod = @".+";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
            else
            {
                filterCutMethod = "BT2";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
        }

        private void CombineSelectedCutlists(object sender, EventArgs e)
        {
            if (dgvCutLog.SelectedRows.Count < 2)
            {
                MessageBox.Show("Must select at least two cutlists to combine");
                return;
            }           
            List<List<string>> combinedPropertyLists = new List<List<string>>();
            //property order in combined list is cut number, batch number, sequence, total pieces
            List<string> combinedCutNumbers = new List<string>();
            List<string> combinedBatchNumbers = new List<string>();
            List<string> combinedSequences = new List<string>();
            List<string> combinedTotalPieces = new List<string>();
            List<CutLog> selectedCutlogs = new List<CutLog>();
            CutLog combinedCutLog = new CutLog() { CutMethod = "BT2", Nested = false, TotalPieces = "0" };
            foreach (DataGridViewRow row in dgvCutLog.SelectedRows)
            {
                CutLog currentcutlog = new CutLog()
                {
                    JobNumber = row.Cells[0].FormattedValue.ToString(),
                    sequenceList = row.Cells[1].FormattedValue.ToString(),
                    cutNumberList = row.Cells[2].FormattedValue.ToString(),
                    Thickness = row.Cells[3].FormattedValue.ToString(),
                    batchNumberList = row.Cells[5].FormattedValue.ToString(),
                    TotalPieces = row.Cells[6].FormattedValue.ToString(),
                };
                selectedCutlogs.Add(currentcutlog);
                combinedPropertyLists.Add(new List<string>() { currentcutlog.cutNumberList, currentcutlog.batchNumberList, currentcutlog.sequenceList, currentcutlog.TotalPieces });
                combinedCutNumbers.Add(currentcutlog.cutNumberList);
                combinedBatchNumbers.Add(currentcutlog.batchNumberList);
                combinedSequences.Add(currentcutlog.sequenceList);
                combinedTotalPieces.Add(currentcutlog.TotalPieces);
                combinedCutLog.JobNumber = currentcutlog.JobNumber;
                combinedCutLog.Thickness = currentcutlog.Thickness;
            }
            combinedPropertyLists = combinedPropertyLists.OrderBy(x => Convert.ToInt32(x[1])).ToList();
            string cutnumbers = "";
            List<int> totalPieces = new List<int>();
            string batchNumbers = "";
            string sequences = "";

            for (int i = 0; i < combinedPropertyLists.Count; i++)
            {
                cutnumbers = $"{cutnumbers}-{combinedPropertyLists[i][0]}";
                batchNumbers = $"{batchNumbers}-{combinedPropertyLists[i][1]}";
                sequences = $"{sequences}-{combinedPropertyLists[i][2]}";
                totalPieces.Add(Convert.ToInt32(combinedPropertyLists[i][3]));
            }
            combinedCutLog.CutNumber = cutnumbers.Trim('-');
            combinedCutLog.batchNumberList = batchNumbers.Trim('-');
            combinedCutLog.sequenceList = sequences.Trim('-');
            combinedCutLog.listOfTotalPieces = totalPieces;
            string jobFolderPath = Directory.GetDirectories(CutSheetFolderPath).Where(x => x.Contains(combinedCutLog.JobNumber)).First();
            List<string> cutNumberList = combinedCutLog.cutNumberList.Split(new[] { '-' }).ToList();
            List<string> batchNumberList = combinedCutLog.batchNumberList.Split(new[] { '-' }).ToList();

            var cutListPDFList = new List<string>();
            for (int i = 0; i < cutNumberList.Count; i++)
            {
                string cutListPDF = FileHelper.GetFiles(jobFolderPath).Where(x => Path.GetFileNameWithoutExtension(x).StartsWith(cutNumberList[i]) && Path.GetFileNameWithoutExtension(x).Contains(batchNumberList[i]) && Path.GetExtension(x).Contains("pdf")).First();
                cutListPDFList.Add(cutListPDF);
            }
            foreach (string pdfFile in cutListPDFList)
            {
                FileStream pdfStream;
                try
                {
                    pdfStream = new FileStream(pdfFile, FileMode.Open, FileAccess.ReadWrite);
                    pdfStream.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not optain write access to cutlist PDF");
                    return;
                }
            }
            string outPutFile = $"{jobFolderPath}//{combinedCutLog.CutNumber} {combinedCutLog.JobNumber} {combinedCutLog.Thickness.Replace("/", "")} BATCH {combinedCutLog.BatchNumber}.pdf";

            foreach (CutLog currentcutlog in selectedCutlogs)
            {
                ListOfCutLogs.Remove(ListOfCutLogs.Where(x =>
                x.JobNumber == currentcutlog.JobNumber
                && x.sequenceList == currentcutlog.sequenceList
                && x.cutNumberList == currentcutlog.cutNumberList
                && x.Thickness == currentcutlog.Thickness
                && x.batchNumberList == currentcutlog.batchNumberList).First());
            }
            

            GenerateCombinedCutlistandDeleteOldOnes(cutListPDFList.ToArray(), outPutFile);
            CombinePNLs(combinedCutLog, jobFolderPath, cutNumberList, batchNumberList);
            ListOfCutLogs.Add(combinedCutLog);
            SaveMainLog();
            ApplyMainFilters();
        }

        private void CombinePNLs(CutLog combinedCutLog, string jobFolderPath, List<string> cutNumberList, List<string> batchNumberList)
        {
            var cutListPNLList = new List<string>();
            for (int i = 0; i < cutNumberList.Count; i++)
            {
                string cutListPNL = FileHelper.GetFiles(jobFolderPath).Where(x => Path.GetFileNameWithoutExtension(x).StartsWith(cutNumberList[i]) && Path.GetFileNameWithoutExtension(x).Contains($"BATCH {batchNumberList[i]}") && Path.GetExtension(x).Contains("pnl")).First();
                cutListPNLList.Add(cutListPNL);
            }
            string outPutFile = $"{jobFolderPath}//{combinedCutLog.CutNumber} {combinedCutLog.JobNumber} {combinedCutLog.Thickness.Replace("/", "")} BATCH {combinedCutLog.BatchNumber}.pnl";
            StringBuilder newPNL = ConsolidatePNLs(cutListPNLList);
            File.WriteAllText(outPutFile, newPNL.ToString());
        }

        private void GenerateCombinedCutlistandDeleteOldOnes(string[] fileNames, string outFile)
        {
            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));
            if (writer == null)
            {
                return;
            }

            // step 3: we open the document
            document.Open();

            foreach (string fileName in fileNames)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }

                PRAcroForm form = reader.AcroForm;
                if (form != null)
                {
                    writer.AddDocument(reader);
                }

                reader.Close();
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();

            foreach (string fileName in fileNames)
            {
                File.Delete(fileName);
            }

        }

        private void dgvCutLogContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCutLog.Columns["Open Cutlist"].Index)
            {
                int rowIndex = e.RowIndex;
                string jobFolderPath = Directory.GetDirectories(CutSheetFolderPath).Where(x => x.Contains(dgvCutLog.Rows[rowIndex].Cells[0].FormattedValue.ToString())).First();
                try
                {
                    string cutlistPDFFile = FileHelper.GetFiles(jobFolderPath).Where(x =>
                        Path.GetFileNameWithoutExtension(x).StartsWith(dgvCutLog.Rows[rowIndex].Cells[2].FormattedValue.ToString())
                        && Path.GetFileNameWithoutExtension(x).Contains(dgvCutLog.Rows[rowIndex].Cells[5].FormattedValue.ToString())
                        && Path.GetFileNameWithoutExtension(x).Contains(dgvCutLog.Rows[rowIndex].Cells[3].FormattedValue.ToString().Replace("/","").Replace("GR50","").Trim())
                        && Path.GetFileName(x).EndsWith(".pdf")
                        ).First();
                    Process.Start(cutlistPDFFile);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cutlist PDF not found");
                }
            }
            else if (e.ColumnIndex == 7)
            {
                //dgvCutLog.CellContentClick -= dgvCutLogContentClickHandler;
                dgvCutLog.Columns[7].ReadOnly = true;
                bool state;
                Nest nest = new Nest()
                {
                    BatchNumber = dgvCutLog.Rows[e.RowIndex].Cells[5].FormattedValue.ToString(),
                    Thickness = dgvCutLog.Rows[e.RowIndex].Cells[3].FormattedValue.ToString().Replace(" ", "_").Replace("/", ""),
                    CutNumber = dgvCutLog.Rows[e.RowIndex].Cells[2].FormattedValue.ToString()
                };
                if (!NestManager.MainDataSet.Tables[0].Select($"CutNumber Like '{nest.CutNumber}'").Any())
                {
                    if (!(bool)dgvCutLog.SelectedRows[0].Cells[7].FormattedValue)
                    {
                        dgvCutLog.Columns[7].ReadOnly = false;
                        return;
                    }
                    var result = MessageBox.Show("Add nest to Nest Manager?", "Cutnumber not detected on Nest Manger", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        nest.JobNumber = dgvCutLog.SelectedRows[0].Cells[0].FormattedValue.ToString();
                        nest.Sequence = dgvCutLog.SelectedRows[0].Cells[1].FormattedValue.ToString();
                        nest.Nested = (bool)dgvCutLog.SelectedRows[0].Cells[7].FormattedValue;
                        nest.Partial = false;
                        nest.Completed = false;
                        nest.completedate = "";
                        nest.FileName = $"{nest.CutNumber} {nest.JobNumber} {dgvCutLog.SelectedRows[0].Cells[3].FormattedValue.ToString().Replace("/", "")} BATCH {nest.BatchNumber}";
                        nest.OrderID = 0;
                        nest.partslist = "na";
                        NestManager.AddNest(nest);
                    }
                    else
                    {
                        dgvCutLog.Columns[7].ReadOnly = false;
                        return;
                    }
                }

                if (dgvCutLog.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString() == "True")
                    state = true;
                else
                    state = false;
                WaitingForRefresh = true;
                NestManager.MarkNestedAs(nest, state);
                //dgvCutLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCutLogContentClick);
            }
        }

        private void FilterByBatch(object sender, EventArgs e)
        {
            if (IsNullOrWhiteSpace(txtBatches.Text))
            {
                filterBatchNumber = @".+";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
            else
            {
                var splitValues = txtBatches.Text.Split(new[] { ',' });
                for (int i = 0; i < splitValues.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (splitValues[0].Contains("-"))
                        {
                            var splitRange = splitValues[0].Split(new[] { '-' });
                            bool first = true;
                            for (int p = Convert.ToInt32(splitRange[0]); p < Convert.ToInt32(splitRange[1]) + 1; p++)
                            {
                                if (first)
                                {
                                    filterBatchNumber = $"(^{p.ToString()}$)";
                                    first = false;
                                }
                                else filterBatchNumber = $"{filterBatchNumber}|(^{p.ToString()}$)";
                            }
                        }
                        else
                            filterBatchNumber = $"(^{splitValues[0]}$)";
                    }
                    else
                    {
                        if (splitValues[i].Contains("-"))
                        {
                            var splitRange = splitValues[i].Split(new[] { '-' });
                            for (int p = Convert.ToInt32(splitRange[0]); p < Convert.ToInt32(splitRange[1]) + 1; p++)
                            {
                                filterBatchNumber = $"{filterBatchNumber}|(^{p.ToString()}$)";
                            }
                        }
                        else
                            filterBatchNumber = $"{filterBatchNumber}|(^{splitValues[i]}$)";
                    }
                }
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
        }

        private void ShowNonNestedOnly(object sender, EventArgs e)
        {
            if (checkShowNonNestedOnly.CheckState == CheckState.Checked)
            {
                filterNested = "False";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
            else
            {
                filterNested = @"(True)|(False)";
                ApplyMainFilters();
                UpdateThicknessComboBox();
            }
               
        }

        private void SplitSelectedCutlists(object sender, EventArgs e)
        {
            FileStream combinedPDFStream;

            // if multiple cutlists are selected, or selected cutlist only has one cutnumber, return
            try
            {
                if (dgvCutLog.SelectedRows.Count > 1 || dgvCutLog.SelectedRows[0].Cells[2].FormattedValue.ToString().Split(new[] { ',' }).Count() == 1)
                {
                    MessageBox.Show("Must have only one combined cutlist selected to split.");
                    return;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Must have only one combined cutlist selected to split.");
                return;
            }
            
                //create new cutlog from selected dgv row
                CutLog currentcutlog = new CutLog()
                {
                    JobNumber = dgvCutLog.SelectedRows[0].Cells[0].FormattedValue.ToString(),
                    sequenceList = dgvCutLog.SelectedRows[0].Cells[1].FormattedValue.ToString().Replace(",","-"),
                    cutNumberList = dgvCutLog.SelectedRows[0].Cells[2].FormattedValue.ToString().Replace(",", "-"),
                    Thickness = dgvCutLog.SelectedRows[0].Cells[3].FormattedValue.ToString(),
                    CutMethod = dgvCutLog.SelectedRows[0].Cells[4].FormattedValue.ToString(),
                    batchNumberList = dgvCutLog.SelectedRows[0].Cells[5].FormattedValue.ToString().Replace(",", "-"),
                };
                currentcutlog.listOfTotalPieces = ListOfCutLogs.Where(x =>
                x.JobNumber == currentcutlog.JobNumber
                && x.sequenceList == currentcutlog.sequenceList
                && x.cutNumberList == currentcutlog.cutNumberList
                && x.Thickness == currentcutlog.Thickness
                && x.batchNumberList == currentcutlog.batchNumberList).First()
                .listOfTotalPieces;

                //determine the path of the combined pdf to split
                string jobFolderPath = Directory.GetDirectories(CutSheetFolderPath).Where(x => x.Contains(currentcutlog.JobNumber)).First();
                string combinedPDF = $"{jobFolderPath}\\{currentcutlog.CutNumber} {currentcutlog.JobNumber} {currentcutlog.Thickness.Replace("/", "")} BATCH {currentcutlog.BatchNumber}.pdf";

                //check if the pdf is currently open
                try
                {
                    combinedPDFStream = new FileStream(combinedPDF, FileMode.Open, FileAccess.ReadWrite);
                    combinedPDFStream.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not optain write access to cutlist PDF");
                    return;
                }

                //remove the current combined cutlog from the datasource
                ListOfCutLogs.Remove(ListOfCutLogs.Where(x =>
                x.JobNumber == currentcutlog.JobNumber
                && x.sequenceList == currentcutlog.sequenceList
                && x.cutNumberList == currentcutlog.cutNumberList
                && x.Thickness == currentcutlog.Thickness
                && x.batchNumberList == currentcutlog.batchNumberList).First());

                

                //create new single cutlist PDFs and logs
                string[] combinedCutNumberList = currentcutlog.cutNumberList.Split(new[] { '-' });
                string[] combinedBatchNumberList = currentcutlog.batchNumberList.Split(new[] { '-' });
                string[] combinedSequenceList = currentcutlog.sequenceList.Split(new[] { '-' });
                for (int i = 0; i < combinedCutNumberList.Count(); i++)
                {
                    string batchFolderPath = Directory.GetDirectories(jobFolderPath).Where(x => x.Contains($"Batch {combinedBatchNumberList[i]}") || x.Contains($"BATCH {combinedBatchNumberList[i]}")).First();
                    string outPutPDFPath = $"{batchFolderPath}\\{combinedCutNumberList[i]} {currentcutlog.JobNumber} {currentcutlog.Thickness.Replace("/", "")} BATCH {combinedBatchNumberList[i]}.pdf";
                    ExtractPage(combinedPDF, outPutPDFPath, i + 1);

                    CutLog singleCutLog = new CutLog()
                    {
                        JobNumber = currentcutlog.JobNumber,
                        Sequence = combinedSequenceList[i],
                        CutNumber = combinedCutNumberList[i],
                        Thickness = currentcutlog.Thickness,
                        BatchNumber = combinedBatchNumberList[i],
                        CutMethod = currentcutlog.CutMethod,
                        Nested = false,
                        listOfTotalPieces = new List<int>(){ currentcutlog.listOfTotalPieces[i] }
                    };
                    ListOfCutLogs.Add(singleCutLog);
                }
                SaveMainLog();
                //delete combined PDFs and PNLs
                File.Delete(combinedPDF);
                File.Delete(combinedPDF.Replace(".pdf", ".pnl"));
                ApplyMainFilters();
                UpdateThicknessComboBox();
            
        }

        public void ExtractPage(string sourcePdfPath, string outputPdfPath, int pageNumber)
        {
             PdfReader reader = null;
             Document document = null;
             PdfCopy pdfCopyProvider = null;
             PdfImportedPage importedPage = null;

             try
             {
                   // Intialize a new PdfReader instance with the contents of the source Pdf file:
                   reader = new PdfReader(sourcePdfPath);

                   // Capture the correct size and orientation for the page:
                   document = new Document(reader.GetPageSizeWithRotation(pageNumber));
 
                   // Initialize an instance of the PdfCopyClass with the source 
                   // document and an output file stream:
                   pdfCopyProvider = new PdfCopy(document,
                       new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                   document.Open();
 
                   // Extract the desired page number:
                   importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
                   pdfCopyProvider.AddPage(importedPage);
                   document.Close();
                   reader.Close();
             }
             catch (Exception ex)
             {
                   throw ex;
             }
         }

        private StringBuilder ConsolidatePNLs(List<string> PNLFileList)
        {
            StringBuilder finalPNL = new StringBuilder();
            List<PNLLine> PNLLineList = new List<PNLLine>();
            string decthickness = "   ";
            string plasmaclass = "   ";

            foreach (string file in PNLFileList)
            {
                var lines = File.ReadAllLines(file).Skip(4).ToArray();
                decthickness = lines[0].Split(new[] { ',' })[16];
                plasmaclass = lines[0].Split(new[] { ',' })[25];
                foreach (string line in lines)
                {
                    var splitline = line.Split(new[] { ',' });
                    bool found = false;
                    foreach(PNLLine PNLLine in PNLLineList)
                    {
                        if (PNLLine.mark == splitline[13])
                        {
                            PNLLine.quantity += Convert.ToInt32(splitline[2]);
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        PNLLineList.Add(new PNLLine() { mark = splitline[13], path = splitline[0], quantity = Convert.ToInt32(splitline[2]) });
                } 
            }
            for (int i = 0; i < 4; i++)
            {
                finalPNL.AppendLine(" ");
            }
            int remarkindex = 0;
            string[] remarks = new string[] { "Help", "I", "am", "stuck", "inside", "this", "computer", "and", "I", "cant", "get", "out", "of", "it" };
            foreach (PNLLine PNLLine in PNLLineList)
            {
                var newline = $"{PNLLine.path},0,{PNLLine.quantity},0,5,0.00,0.00,I,2,0,CADFILE,0,   ,{PNLLine.mark},3,MS,{decthickness},   ,   ,   ,   ,0,   ,<none>,{remarks[remarkindex]},{plasmaclass},0,0,   ,   ,-1";
                finalPNL.AppendLine(newline);
                if (remarkindex == 13)
                    remarkindex = 0;
                else
                    remarkindex += 1;
            }
            return finalPNL;
        }

        private void ComboMainThicknessChanged(object sender, EventArgs e)
        {
            if (comboPlateThickness.SelectedValue.ToString() == "All")
            {
                filterMainThickness = @".+";
            }
            else
                filterMainThickness = $"^{comboPlateThickness.SelectedValue.ToString()}$";
            ApplyMainFilters();
        }


        private void AddToNestManager(object sender, EventArgs e)
        {
            if (dgvCutLog.SelectedRows.Count != 1)
            {
                MessageBox.Show("Must have only one cutlist selected to add to Nest Manager");
                return;
            }
            Nest nest = new Nest();
            nest.JobNumber = dgvCutLog.SelectedRows[0].Cells[0].FormattedValue.ToString();
            nest.Sequence = dgvCutLog.SelectedRows[0].Cells[1].FormattedValue.ToString();
            nest.CutNumber = dgvCutLog.SelectedRows[0].Cells[2].FormattedValue.ToString();
            nest.Thickness = dgvCutLog.SelectedRows[0].Cells[3].FormattedValue.ToString().Replace("/", "").Replace(" ","_");
            nest.BatchNumber = dgvCutLog.SelectedRows[0].Cells[5].FormattedValue.ToString();
            nest.Nested = (bool)dgvCutLog.SelectedRows[0].Cells[7].FormattedValue;
            nest.Partial = false;
            nest.Completed = false;
            nest.completedate = "";
            nest.FileName = $"{nest.CutNumber} {nest.JobNumber} {dgvCutLog.SelectedRows[0].Cells[3].FormattedValue.ToString().Replace("/","")} BATCH {nest.BatchNumber}";
            nest.OrderID = 0;
            nest.partslist = "na";

            NestManager.AddNest(nest);
            return;
        }
    }
}