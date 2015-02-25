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
using System.Globalization;

namespace WindowsFormsApplication2
{
    public partial class Login_Page : Form
    {
        Class1 c = new Class1();
        int f = 0;
        public Login_Page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = c.login(textBox1, textBox2);                        //Login Validation
            if (f == 1)
            {
                ActiveForm.Hide();
                Admin form = new Admin();                           //Admin Login
                form.Show();
            }
            else if (f == 2)
            {
                ActiveForm.Hide();
                Booking form = new Booking(textBox1);               //Customer Login
                form.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username/Password", "Login Page");
                if (textBox1.Text.Length != 0)
                {
                    textBox2.ResetText();
                    textBox2.Focus();
                }
                else
                {
                    textBox1.Focus();
                }
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Invalid Username/Password", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)                      //Reset
        {
            if ((textBox1.Text == "" && textBox2.Text == "") == true)
            {
                MessageBox.Show("Enter Values", "Warning", MessageBoxButtons.OK);
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Enter Values", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                textBox1.ResetText();
                textBox2.ResetText();
                textBox1.Focus();
            }
        }

        private void Register_Click(object sender, EventArgs e)                     //New User
        {
            ActiveForm.Hide();
            Registration form = new Registration();
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = Login;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)                      //Alignment
        {
            panel1.Top = this.ClientSize.Height / 2 - this.panel1.Height / 2;
            panel1.Left = this.ClientSize.Width / 2 - this.panel1.Width / 2;
            panel1.Anchor = AnchorStyles.None;
        }

        private void button1_Click_1(object sender, EventArgs e)                        //Exit
        {
            DialogResult result = MessageBox.Show("Do you want to Exit???", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ActiveForm.Close();
            }
            else
            {
                textBox1.Focus();
            }
        }
    }
}
