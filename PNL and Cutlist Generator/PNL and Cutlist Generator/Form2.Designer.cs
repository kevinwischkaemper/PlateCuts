namespace PNL_and_Cutlist_Generator
{
    partial class Form2
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
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAddtionalCutNumbersLabel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkContinue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(154, 69);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(49, 20);
            this.txtResult.TabIndex = 0;
            this.txtResult.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Additional Cut Number:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.MaximumSize = new System.Drawing.Size(300, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Certain plate thicknesses on this cutlist need to be split into more than one cut" +
    "list.";
            // 
            // labelAddtionalCutNumbersLabel
            // 
            this.labelAddtionalCutNumbersLabel.AutoSize = true;
            this.labelAddtionalCutNumbersLabel.Location = new System.Drawing.Point(11, 40);
            this.labelAddtionalCutNumbersLabel.MaximumSize = new System.Drawing.Size(300, 0);
            this.labelAddtionalCutNumbersLabel.Name = "labelAddtionalCutNumbersLabel";
            this.labelAddtionalCutNumbersLabel.Size = new System.Drawing.Size(15, 13);
            this.labelAddtionalCutNumbersLabel.TabIndex = 3;
            this.labelAddtionalCutNumbersLabel.Text = "hi";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(172, 95);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(65, 27);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(28, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkContinue
            // 
            this.checkContinue.AutoSize = true;
            this.checkContinue.Checked = true;
            this.checkContinue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkContinue.Location = new System.Drawing.Point(209, 71);
            this.checkContinue.Name = "checkContinue";
            this.checkContinue.Size = new System.Drawing.Size(127, 17);
            this.checkContinue.TabIndex = 6;
            this.checkContinue.Text = "Continue from original";
            this.checkContinue.UseVisualStyleBackColor = true;
            this.checkContinue.CheckedChanged += new System.EventHandler(this.checkContinueChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 131);
            this.Controls.Add(this.checkContinue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelAddtionalCutNumbersLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResult);
            this.Name = "Form2";
            this.Text = "Additional Cut Numbers Needed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelAddtionalCutNumbersLabel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkContinue;
    }
}