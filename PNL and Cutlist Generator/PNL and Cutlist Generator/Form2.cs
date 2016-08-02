using System;
using System.Windows.Forms;

namespace PNL_and_Cutlist_Generator
{
    public partial class Form2 : Form
    {
        public int additionalCutNumbers;
        public Form2(int additionalCutNumbers)
        {
            string numbers = additionalCutNumbers.ToString();
            InitializeComponent();
            labelAddtionalCutNumbersLabel.Text = $"Additional Cut Numbers needed: {numbers}";
        }

        private void checkContinueChanged(object sender, EventArgs e)
        {
            if (checkContinue.CheckState == CheckState.Unchecked)
            {
                txtResult.Visible = true;
                label1.Visible = true;
                txtResult.Text = "";
            }
            else
            {
                txtResult.Visible = false;
                label1.Visible = false;
                txtResult.Text = "continue";
            }
        }
    }
}
