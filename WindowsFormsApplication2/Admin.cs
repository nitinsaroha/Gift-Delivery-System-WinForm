using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gift.Business;
using System.Globalization;

namespace WindowsFormsApplication2
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)                          //Logout
        {
            DialogResult result = MessageBox.Show("Are You Sure???", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ActiveForm.Hide();
                Login_Page form = new Login_Page();
                form.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)                          //Update Details
        {
            ActiveForm.Hide();
            Update_Search form = new Update_Search();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)                          //Report generation
        {
            ActiveForm.Hide();
            Delivery_Date form = new Delivery_Date();
            form.Show();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login_Page form = new Login_Page();
            form.Show();
        }

        private void Admin_Paint(object sender, PaintEventArgs e)                       //Alignment
        {
            panel1.Top = this.ClientSize.Height / 2 - this.panel1.Height / 2;
            panel1.Left = this.ClientSize.Width / 2 - this.panel1.Width / 2;
            panel1.Anchor = AnchorStyles.None;
        }

        private void gift_Click(object sender, EventArgs e)                             //Add Gift
        {
            ActiveForm.Hide();
            addgift form = new addgift();
            form.Show();
        }
    }
}
