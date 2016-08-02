namespace PNL_and_Cutlist_Generator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSelectCutlist = new System.Windows.Forms.Button();
            this.filedialogSelectCutlist = new System.Windows.Forms.OpenFileDialog();
            this.btnGeneratePDF = new System.Windows.Forms.Button();
            this.lblComplete = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCutNumber = new System.Windows.Forms.TextBox();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.lblSelectedCutlistHeading = new System.Windows.Forms.Label();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.dgvCutLog = new System.Windows.Forms.DataGridView();
            this.comboJobNumbers = new System.Windows.Forms.ComboBox();
            this.lblSequences = new System.Windows.Forms.Label();
            this.lblBatches = new System.Windows.Forms.Label();
            this.txtSequences = new System.Windows.Forms.TextBox();
            this.txtBatches = new System.Windows.Forms.TextBox();
            this.checkShowBTOne = new System.Windows.Forms.CheckBox();
            this.comboPlateThickness = new System.Windows.Forms.ComboBox();
            this.btnCombineSelectedCutlists = new System.Windows.Forms.Button();
            this.btnSplitSelectedCutlist = new System.Windows.Forms.Button();
            this.checkShowNonNestedOnly = new System.Windows.Forms.CheckBox();
            this.btnViewSelectedCutlist = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCustomNest = new System.Windows.Forms.Button();
            this.btnRemoveFromNestManager = new System.Windows.Forms.Button();
            this.comboNestManagerThickness = new System.Windows.Forms.ComboBox();
            this.btnAddToNestManager = new System.Windows.Forms.Button();
            this.dgvNestManager = new System.Windows.Forms.DataGridView();
            this.btnPromoteNest = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblCutlistNumberQty = new System.Windows.Forms.Label();
            this.lblAdditonalNumberQuantity = new System.Windows.Forms.Label();
            this.textStartingAdditionalNumber = new System.Windows.Forms.TextBox();
            this.lblAdditionalNumbers = new System.Windows.Forms.Label();
            this.lblSelectedCutlistRelease = new System.Windows.Forms.Label();
            this.lblSelectedCutlistBatch = new System.Windows.Forms.Label();
            this.lblSelectedCutlistJob = new System.Windows.Forms.Label();
            this.dgvCutListGeneratorView = new System.Windows.Forms.DataGridView();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgvToDoList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.checkCombineNumbers = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutLog)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNestManager)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutListGeneratorView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDoList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectCutlist
            // 
            this.btnSelectCutlist.Location = new System.Drawing.Point(6, 3);
            this.btnSelectCutlist.Name = "btnSelectCutlist";
            this.btnSelectCutlist.Size = new System.Drawing.Size(97, 23);
            this.btnSelectCutlist.TabIndex = 0;
            this.btnSelectCutlist.Text = "Select Cutlist";
            this.btnSelectCutlist.UseVisualStyleBackColor = true;
            this.btnSelectCutlist.Click += new System.EventHandler(this.SelectCutlist);
            // 
            // filedialogSelectCutlist
            // 
            this.filedialogSelectCutlist.FileName = "openFileDialog1";
            this.filedialogSelectCutlist.InitialDirectory = "G:\\CUT SHEET FOLDER";
            // 
            // btnGeneratePDF
            // 
            this.btnGeneratePDF.Enabled = false;
            this.btnGeneratePDF.Location = new System.Drawing.Point(66, 118);
            this.btnGeneratePDF.Name = "btnGeneratePDF";
            this.btnGeneratePDF.Size = new System.Drawing.Size(172, 23);
            this.btnGeneratePDF.TabIndex = 3;
            this.btnGeneratePDF.Text = "Generate PNLs and Cutlists";
            this.btnGeneratePDF.UseVisualStyleBackColor = true;
            this.btnGeneratePDF.Click += new System.EventHandler(this.GeneratePDFs);
            // 
            // lblComplete
            // 
            this.lblComplete.AutoSize = true;
            this.lblComplete.Location = new System.Drawing.Point(156, 174);
            this.lblComplete.Name = "lblComplete";
            this.lblComplete.Size = new System.Drawing.Size(0, 13);
            this.lblComplete.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Starting Cutlist Number:";
            // 
            // txtCutNumber
            // 
            this.txtCutNumber.Location = new System.Drawing.Point(134, 65);
            this.txtCutNumber.Name = "txtCutNumber";
            this.txtCutNumber.Size = new System.Drawing.Size(49, 20);
            this.txtCutNumber.TabIndex = 6;
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = true;
            this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLog.Location = new System.Drawing.Point(111, 3);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(72, 17);
            this.chkLog.TabIndex = 7;
            this.chkLog.Text = "Save Log";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // lblSelectedCutlistHeading
            // 
            this.lblSelectedCutlistHeading.AutoSize = true;
            this.lblSelectedCutlistHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCutlistHeading.Location = new System.Drawing.Point(292, 15);
            this.lblSelectedCutlistHeading.Name = "lblSelectedCutlistHeading";
            this.lblSelectedCutlistHeading.Size = new System.Drawing.Size(0, 17);
            this.lblSelectedCutlistHeading.TabIndex = 10;
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Checked = true;
            this.chkPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrint.Location = new System.Drawing.Point(111, 19);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(47, 17);
            this.chkPrint.TabIndex = 11;
            this.chkPrint.Text = "Print";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // dgvCutLog
            // 
            this.dgvCutLog.AllowUserToAddRows = false;
            this.dgvCutLog.AllowUserToOrderColumns = true;
            this.dgvCutLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCutLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCutLog.Location = new System.Drawing.Point(5, 239);
            this.dgvCutLog.Name = "dgvCutLog";
            this.dgvCutLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCutLog.Size = new System.Drawing.Size(941, 470);
            this.dgvCutLog.TabIndex = 12;
            this.dgvCutLog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCutLogContentClick);
            // 
            // comboJobNumbers
            // 
            this.comboJobNumbers.Location = new System.Drawing.Point(5, 212);
            this.comboJobNumbers.Name = "comboJobNumbers";
            this.comboJobNumbers.Size = new System.Drawing.Size(105, 21);
            this.comboJobNumbers.TabIndex = 13;
            this.comboJobNumbers.SelectedIndexChanged += new System.EventHandler(this.ComboJobNumberChanged);
            // 
            // lblSequences
            // 
            this.lblSequences.AutoSize = true;
            this.lblSequences.Location = new System.Drawing.Point(117, 215);
            this.lblSequences.Name = "lblSequences";
            this.lblSequences.Size = new System.Drawing.Size(67, 13);
            this.lblSequences.TabIndex = 17;
            this.lblSequences.Text = "Sequence(s)";
            // 
            // lblBatches
            // 
            this.lblBatches.AutoSize = true;
            this.lblBatches.Location = new System.Drawing.Point(252, 217);
            this.lblBatches.Name = "lblBatches";
            this.lblBatches.Size = new System.Drawing.Size(46, 13);
            this.lblBatches.TabIndex = 18;
            this.lblBatches.Text = "Batch(s)";
            // 
            // txtSequences
            // 
            this.txtSequences.Location = new System.Drawing.Point(183, 212);
            this.txtSequences.Name = "txtSequences";
            this.txtSequences.Size = new System.Drawing.Size(59, 20);
            this.txtSequences.TabIndex = 19;
            this.txtSequences.Leave += new System.EventHandler(this.FilterBySequences);
            // 
            // txtBatches
            // 
            this.txtBatches.Location = new System.Drawing.Point(304, 214);
            this.txtBatches.Name = "txtBatches";
            this.txtBatches.Size = new System.Drawing.Size(75, 20);
            this.txtBatches.TabIndex = 20;
            this.txtBatches.Leave += new System.EventHandler(this.FilterByBatch);
            // 
            // checkShowBTOne
            // 
            this.checkShowBTOne.AutoSize = true;
            this.checkShowBTOne.Location = new System.Drawing.Point(534, 217);
            this.checkShowBTOne.Name = "checkShowBTOne";
            this.checkShowBTOne.Size = new System.Drawing.Size(112, 17);
            this.checkShowBTOne.TabIndex = 21;
            this.checkShowBTOne.Text = "Show BT1 Cutlists";
            this.checkShowBTOne.UseVisualStyleBackColor = true;
            this.checkShowBTOne.CheckedChanged += new System.EventHandler(this.CheckShowBTOneCutlists);
            // 
            // comboPlateThickness
            // 
            this.comboPlateThickness.Location = new System.Drawing.Point(398, 214);
            this.comboPlateThickness.Name = "comboPlateThickness";
            this.comboPlateThickness.Size = new System.Drawing.Size(121, 21);
            this.comboPlateThickness.TabIndex = 22;
            this.comboPlateThickness.SelectionChangeCommitted += new System.EventHandler(this.ComboMainThicknessChanged);
            // 
            // btnCombineSelectedCutlists
            // 
            this.btnCombineSelectedCutlists.Location = new System.Drawing.Point(675, 188);
            this.btnCombineSelectedCutlists.Name = "btnCombineSelectedCutlists";
            this.btnCombineSelectedCutlists.Size = new System.Drawing.Size(97, 23);
            this.btnCombineSelectedCutlists.TabIndex = 23;
            this.btnCombineSelectedCutlists.Text = "Combine Cutlists";
            this.btnCombineSelectedCutlists.UseVisualStyleBackColor = true;
            this.btnCombineSelectedCutlists.Click += new System.EventHandler(this.CombineSelectedCutlists);
            // 
            // btnSplitSelectedCutlist
            // 
            this.btnSplitSelectedCutlist.Location = new System.Drawing.Point(675, 213);
            this.btnSplitSelectedCutlist.Name = "btnSplitSelectedCutlist";
            this.btnSplitSelectedCutlist.Size = new System.Drawing.Size(97, 23);
            this.btnSplitSelectedCutlist.TabIndex = 24;
            this.btnSplitSelectedCutlist.Text = "Split Cutlist";
            this.btnSplitSelectedCutlist.UseVisualStyleBackColor = true;
            this.btnSplitSelectedCutlist.Click += new System.EventHandler(this.SplitSelectedCutlists);
            // 
            // checkShowNonNestedOnly
            // 
            this.checkShowNonNestedOnly.AutoSize = true;
            this.checkShowNonNestedOnly.Location = new System.Drawing.Point(534, 192);
            this.checkShowNonNestedOnly.Name = "checkShowNonNestedOnly";
            this.checkShowNonNestedOnly.Size = new System.Drawing.Size(137, 17);
            this.checkShowNonNestedOnly.TabIndex = 25;
            this.checkShowNonNestedOnly.Text = "Show Non-Nested Only";
            this.checkShowNonNestedOnly.UseVisualStyleBackColor = true;
            this.checkShowNonNestedOnly.CheckedChanged += new System.EventHandler(this.ShowNonNestedOnly);
            // 
            // btnViewSelectedCutlist
            // 
            this.btnViewSelectedCutlist.Enabled = false;
            this.btnViewSelectedCutlist.Location = new System.Drawing.Point(6, 32);
            this.btnViewSelectedCutlist.Name = "btnViewSelectedCutlist";
            this.btnViewSelectedCutlist.Size = new System.Drawing.Size(97, 23);
            this.btnViewSelectedCutlist.TabIndex = 26;
            this.btnViewSelectedCutlist.Text = "View";
            this.btnViewSelectedCutlist.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1367, 179);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCustomNest);
            this.tabPage1.Controls.Add(this.btnRemoveFromNestManager);
            this.tabPage1.Controls.Add(this.comboNestManagerThickness);
            this.tabPage1.Controls.Add(this.btnAddToNestManager);
            this.tabPage1.Controls.Add(this.dgvNestManager);
            this.tabPage1.Controls.Add(this.btnPromoteNest);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1359, 153);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nest Manager";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCustomNest
            // 
            this.btnCustomNest.Location = new System.Drawing.Point(7, 117);
            this.btnCustomNest.Name = "btnCustomNest";
            this.btnCustomNest.Size = new System.Drawing.Size(108, 22);
            this.btnCustomNest.TabIndex = 32;
            this.btnCustomNest.Text = "Custom Nest";
            this.btnCustomNest.UseVisualStyleBackColor = true;
            // 
            // btnRemoveFromNestManager
            // 
            this.btnRemoveFromNestManager.Location = new System.Drawing.Point(7, 89);
            this.btnRemoveFromNestManager.Name = "btnRemoveFromNestManager";
            this.btnRemoveFromNestManager.Size = new System.Drawing.Size(108, 22);
            this.btnRemoveFromNestManager.TabIndex = 29;
            this.btnRemoveFromNestManager.Text = "Remove Nest";
            this.btnRemoveFromNestManager.UseVisualStyleBackColor = true;
            // 
            // comboNestManagerThickness
            // 
            this.comboNestManagerThickness.FormattingEnabled = true;
            this.comboNestManagerThickness.Location = new System.Drawing.Point(7, 6);
            this.comboNestManagerThickness.Name = "comboNestManagerThickness";
            this.comboNestManagerThickness.Size = new System.Drawing.Size(100, 21);
            this.comboNestManagerThickness.TabIndex = 31;
            // 
            // btnAddToNestManager
            // 
            this.btnAddToNestManager.Location = new System.Drawing.Point(7, 60);
            this.btnAddToNestManager.Name = "btnAddToNestManager";
            this.btnAddToNestManager.Size = new System.Drawing.Size(108, 23);
            this.btnAddToNestManager.TabIndex = 28;
            this.btnAddToNestManager.Text = "Add Selected Nest";
            this.btnAddToNestManager.UseVisualStyleBackColor = true;
            this.btnAddToNestManager.Click += new System.EventHandler(this.AddToNestManager);
            // 
            // dgvNestManager
            // 
            this.dgvNestManager.AllowUserToAddRows = false;
            this.dgvNestManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNestManager.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNestManager.Location = new System.Drawing.Point(121, 0);
            this.dgvNestManager.Name = "dgvNestManager";
            this.dgvNestManager.Size = new System.Drawing.Size(1238, 156);
            this.dgvNestManager.TabIndex = 0;
            // 
            // btnPromoteNest
            // 
            this.btnPromoteNest.Location = new System.Drawing.Point(6, 31);
            this.btnPromoteNest.Name = "btnPromoteNest";
            this.btnPromoteNest.Size = new System.Drawing.Size(101, 23);
            this.btnPromoteNest.TabIndex = 30;
            this.btnPromoteNest.Text = "Promote Nest";
            this.btnPromoteNest.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkCombineNumbers);
            this.tabPage2.Controls.Add(this.lblCutlistNumberQty);
            this.tabPage2.Controls.Add(this.lblAdditonalNumberQuantity);
            this.tabPage2.Controls.Add(this.textStartingAdditionalNumber);
            this.tabPage2.Controls.Add(this.lblAdditionalNumbers);
            this.tabPage2.Controls.Add(this.lblSelectedCutlistRelease);
            this.tabPage2.Controls.Add(this.lblSelectedCutlistBatch);
            this.tabPage2.Controls.Add(this.lblSelectedCutlistJob);
            this.tabPage2.Controls.Add(this.dgvCutListGeneratorView);
            this.tabPage2.Controls.Add(this.btnSelectCutlist);
            this.tabPage2.Controls.Add(this.btnViewSelectedCutlist);
            this.tabPage2.Controls.Add(this.txtCutNumber);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.chkLog);
            this.tabPage2.Controls.Add(this.chkPrint);
            this.tabPage2.Controls.Add(this.btnGeneratePDF);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1359, 153);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cutlist Generator";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblCutlistNumberQty
            // 
            this.lblCutlistNumberQty.AutoSize = true;
            this.lblCutlistNumberQty.Location = new System.Drawing.Point(189, 68);
            this.lblCutlistNumberQty.Name = "lblCutlistNumberQty";
            this.lblCutlistNumberQty.Size = new System.Drawing.Size(56, 13);
            this.lblCutlistNumberQty.TabIndex = 35;
            this.lblCutlistNumberQty.Text = "#  needed";
            this.lblCutlistNumberQty.Visible = false;
            // 
            // lblAdditonalNumberQuantity
            // 
            this.lblAdditonalNumberQuantity.AutoSize = true;
            this.lblAdditonalNumberQuantity.Location = new System.Drawing.Point(188, 92);
            this.lblAdditonalNumberQuantity.Name = "lblAdditonalNumberQuantity";
            this.lblAdditonalNumberQuantity.Size = new System.Drawing.Size(101, 13);
            this.lblAdditonalNumberQuantity.TabIndex = 34;
            this.lblAdditonalNumberQuantity.Text = "# additional needed";
            this.lblAdditonalNumberQuantity.Visible = false;
            // 
            // textStartingAdditionalNumber
            // 
            this.textStartingAdditionalNumber.Location = new System.Drawing.Point(134, 89);
            this.textStartingAdditionalNumber.Name = "textStartingAdditionalNumber";
            this.textStartingAdditionalNumber.Size = new System.Drawing.Size(49, 20);
            this.textStartingAdditionalNumber.TabIndex = 33;
            this.textStartingAdditionalNumber.Visible = false;
            // 
            // lblAdditionalNumbers
            // 
            this.lblAdditionalNumbers.AutoSize = true;
            this.lblAdditionalNumbers.Location = new System.Drawing.Point(-3, 92);
            this.lblAdditionalNumbers.Name = "lblAdditionalNumbers";
            this.lblAdditionalNumbers.Size = new System.Drawing.Size(135, 13);
            this.lblAdditionalNumbers.TabIndex = 32;
            this.lblAdditionalNumbers.Text = "Starting Additional Number:";
            this.lblAdditionalNumbers.Visible = false;
            // 
            // lblSelectedCutlistRelease
            // 
            this.lblSelectedCutlistRelease.AutoSize = true;
            this.lblSelectedCutlistRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCutlistRelease.Location = new System.Drawing.Point(198, 37);
            this.lblSelectedCutlistRelease.Name = "lblSelectedCutlistRelease";
            this.lblSelectedCutlistRelease.Size = new System.Drawing.Size(53, 13);
            this.lblSelectedCutlistRelease.TabIndex = 31;
            this.lblSelectedCutlistRelease.Text = "Release";
            this.lblSelectedCutlistRelease.Visible = false;
            // 
            // lblSelectedCutlistBatch
            // 
            this.lblSelectedCutlistBatch.AutoSize = true;
            this.lblSelectedCutlistBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCutlistBatch.Location = new System.Drawing.Point(198, 20);
            this.lblSelectedCutlistBatch.Name = "lblSelectedCutlistBatch";
            this.lblSelectedCutlistBatch.Size = new System.Drawing.Size(40, 13);
            this.lblSelectedCutlistBatch.TabIndex = 30;
            this.lblSelectedCutlistBatch.Text = "Batch";
            this.lblSelectedCutlistBatch.Visible = false;
            // 
            // lblSelectedCutlistJob
            // 
            this.lblSelectedCutlistJob.AutoSize = true;
            this.lblSelectedCutlistJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCutlistJob.Location = new System.Drawing.Point(200, 4);
            this.lblSelectedCutlistJob.Name = "lblSelectedCutlistJob";
            this.lblSelectedCutlistJob.Size = new System.Drawing.Size(27, 13);
            this.lblSelectedCutlistJob.TabIndex = 29;
            this.lblSelectedCutlistJob.Text = "Job";
            this.lblSelectedCutlistJob.Visible = false;
            // 
            // dgvCutListGeneratorView
            // 
            this.dgvCutListGeneratorView.AllowUserToAddRows = false;
            this.dgvCutListGeneratorView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCutListGeneratorView.Location = new System.Drawing.Point(328, 3);
            this.dgvCutListGeneratorView.Name = "dgvCutListGeneratorView";
            this.dgvCutListGeneratorView.Size = new System.Drawing.Size(1028, 144);
            this.dgvCutListGeneratorView.TabIndex = 27;
            // 
            // dgvToDoList
            // 
            this.dgvToDoList.AllowUserToAddRows = false;
            this.dgvToDoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToDoList.Location = new System.Drawing.Point(953, 239);
            this.dgvToDoList.Name = "dgvToDoList";
            this.dgvToDoList.Size = new System.Drawing.Size(419, 470);
            this.dgvToDoList.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(953, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 29;
            this.label2.Text = "To-Do List";
            // 
            // checkCombineNumbers
            // 
            this.checkCombineNumbers.AutoSize = true;
            this.checkCombineNumbers.Checked = true;
            this.checkCombineNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkCombineNumbers.Location = new System.Drawing.Point(111, 36);
            this.checkCombineNumbers.Name = "checkCombineNumbers";
            this.checkCombineNumbers.Size = new System.Drawing.Size(82, 17);
            this.checkCombineNumbers.TabIndex = 36;
            this.checkCombineNumbers.Text = "Combine #s";
            this.checkCombineNumbers.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 713);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvToDoList);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkShowNonNestedOnly);
            this.Controls.Add(this.btnSplitSelectedCutlist);
            this.Controls.Add(this.btnCombineSelectedCutlists);
            this.Controls.Add(this.comboPlateThickness);
            this.Controls.Add(this.checkShowBTOne);
            this.Controls.Add(this.txtBatches);
            this.Controls.Add(this.txtSequences);
            this.Controls.Add(this.lblBatches);
            this.Controls.Add(this.lblSequences);
            this.Controls.Add(this.comboJobNumbers);
            this.Controls.Add(this.dgvCutLog);
            this.Controls.Add(this.lblSelectedCutlistHeading);
            this.Controls.Add(this.lblComplete);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Plate Cuts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormClose);
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutLog)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNestManager)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutListGeneratorView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDoList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectCutlist;
        private System.Windows.Forms.OpenFileDialog filedialogSelectCutlist;
        private System.Windows.Forms.Button btnGeneratePDF;
        private System.Windows.Forms.Label lblComplete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCutNumber;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.Label lblSelectedCutlistHeading;
        public System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.ComboBox comboJobNumbers;
        private System.Windows.Forms.Label lblSequences;
        private System.Windows.Forms.Label lblBatches;
        private System.Windows.Forms.TextBox txtSequences;
        private System.Windows.Forms.TextBox txtBatches;
        private System.Windows.Forms.CheckBox checkShowBTOne;
        private System.Windows.Forms.ComboBox comboPlateThickness;
        private System.Windows.Forms.Button btnCombineSelectedCutlists;
        private System.Windows.Forms.Button btnSplitSelectedCutlist;
        private System.Windows.Forms.CheckBox checkShowNonNestedOnly;
        private System.Windows.Forms.Button btnViewSelectedCutlist;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvNestManager;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnAddToNestManager;
        private System.Windows.Forms.Button btnRemoveFromNestManager;
        private System.Windows.Forms.Button btnPromoteNest;
        private System.Windows.Forms.ComboBox comboNestManagerThickness;
        private System.Windows.Forms.DataGridView dgvCutListGeneratorView;
        private System.Windows.Forms.Button btnCustomNest;
        private System.Windows.Forms.DataGridView dgvToDoList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSelectedCutlistRelease;
        private System.Windows.Forms.Label lblSelectedCutlistBatch;
        private System.Windows.Forms.Label lblSelectedCutlistJob;
        public System.Windows.Forms.DataGridView dgvCutLog;
        private System.Windows.Forms.TextBox textStartingAdditionalNumber;
        private System.Windows.Forms.Label lblAdditionalNumbers;
        private System.Windows.Forms.Label lblCutlistNumberQty;
        private System.Windows.Forms.Label lblAdditonalNumberQuantity;
        public System.Windows.Forms.CheckBox checkCombineNumbers;
    }
}

