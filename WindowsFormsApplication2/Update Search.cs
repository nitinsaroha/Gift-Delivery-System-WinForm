using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gift.Business;

namespace WindowsFormsApplication2
{
    public partial class Update_Search : Form
    {

        Class1 c = new Class1();
        public Update_Search()
        {
            InitializeComponent();
        }

        //Get details button Form1
        private void update_Click_1(object sender, EventArgs e)
        {
            int f = c.get_details(textBox1);
            if (f == 2 || f == 0)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Please enter a valid Booking ID", "Booking ID error", buttons, MessageBoxIcon.Exclamation);
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Please Enter a valid Booking ID", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f == 1)
            {
                ActiveForm.Hide();
                Update_Details form = new Update_Details(textBox1.Text.ToLower());
                form.Show();
            }
        }

        //Reset the booking ID of Form1
        private void reset_Click(object sender, EventArgs e)
        {
            c.reset_form1(textBox1);
        }


        private void Update_Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin form = new Admin();
            form.Show();
        }

        //Logout
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure???", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ActiveForm.Hide();
                Login_Page form = new Login_Page();
                form.Show();
            }
        }

        //Back
        private void button1_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }
    }
}
