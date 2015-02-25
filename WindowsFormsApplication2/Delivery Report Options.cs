using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using Gift.Business;

namespace WindowsFormsApplication2
{
    public partial class Delivery_Option : Form
    {
        DateTimePicker dta = new DateTimePicker();
        DateTimePicker dtb = new DateTimePicker();
        Class1 c=new Class1();
        public Delivery_Option(DateTimePicker dt1,DateTimePicker dt2)
        {
            InitializeComponent();
            dta = dt1;
            dtb = dt2;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)                   //Alignment
        {
            panel1.Top = this.ClientSize.Height / 2 - this.panel1.Height / 2;
            panel1.Left = this.ClientSize.Width / 2 - this.panel1.Width / 2;
            panel1.Anchor = AnchorStyles.None;
        }

        private void button2_Click(object sender, EventArgs e)                      //View as pdf
        {
            c.report(dta, dtb);
        }

        private void button1_Click(object sender, EventArgs e)                      //View on Page
        {
            ActiveForm.Hide();
            Delivery_Form a = new Delivery_Form(dta, dtb);
            a.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delivery_Date a = new Delivery_Date();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)                      //Back
        {
            ActiveForm.Close();
        }

        private void button4_Click(object sender, EventArgs e)                      //Logout
        {
            DialogResult result = MessageBox.Show("Are You Sure???", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ActiveForm.Hide();
                Login_Page form = new Login_Page();
                form.Show();
            }
        }

    }
}
