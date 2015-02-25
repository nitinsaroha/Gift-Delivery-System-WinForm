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
    public partial class Delivery_Date : Form
    {
        int f1 = 0, f2 = 0;
        Class1 c = new Class1();
        public Delivery_Date()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1 = c.Date1(dateTimePicker1);                          //Validation
            f2 = c.Date2(dateTimePicker1, dateTimePicker2);         //Validation

            if (f1 == 0 && f2 == 0)
            {
                ActiveForm.Hide();
                Delivery_Option f = new Delivery_Option(dateTimePicker1, dateTimePicker2);
                f.Show();
            }

            if (f2 == 1)
            {
                dateTimePicker2.Focus();
                label5.Text = "*Should Be less \n than present";
                label5.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label4.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f2 == 2)
            {
                dateTimePicker2.Focus();
                label5.Text = "Should be \n greater than\n from booking\n   date";
                label5.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label4.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label5.Hide();
            }

            if (f1 == 1)
            {
                dateTimePicker1.Focus();
                label4.Text = "*Invalid Date";
                label4.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label4.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label4.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)          //Reset
        {
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            label4.Hide();
            label5.Hide();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            f1 = c.Date1(dateTimePicker1);                              //Validation
            if (f1 == 1)
            {
                label4.Text = "*Invalid Date";
                label4.Show();
            }
            else
            {
                label4.Hide();
            }
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            f2 = c.Date2(dateTimePicker1, dateTimePicker2);             //Validation
            if (f2 == 1)
            {
                label5.Text = "*Should be less \n than present";
                label5.Show();
            }
            else if (f2 == 2)
            {
                label5.Text = "*Should be \n greater than\n from booking date";
                label5.Show();
            }
            else
            {
                label5.Hide();
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)                   //Alignmant
        {
            panel1.Top = this.ClientSize.Height / 2 - this.panel1.Height / 2;
            panel1.Left = this.ClientSize.Width / 2 - this.panel1.Width / 2;
            panel1.Anchor = AnchorStyles.None;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin f = new Admin();
            f.Show();
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

        private void Delivery_Date_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            label4.Hide();
            label5.Hide();
        }
    }
}
