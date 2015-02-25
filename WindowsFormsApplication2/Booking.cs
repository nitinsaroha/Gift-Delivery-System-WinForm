using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gift.Business;


namespace WindowsFormsApplication2
{
    public partial class Booking : Form
    {
        int f1 = 0, f2 = 0, f3 = 0;
        Class1 c = new Class1();
        string custid = "";
        public Booking(TextBox t)
        {
            InitializeComponent();
            custid = t.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f1 = c.addressValidate(textBox1);                   //Validation
            f2 = c.phoneValidate(textBox2);                     //Validation
            f3 = c.eDateValidate(dateTimePicker1);              //Validation
            if (f1 == 0 && f2 == 0 && f3 == 0)
            {
                DateTime d1 = DateTime.Today;
                string s1 = c.writeB(textBox1, textBox2, dateTimePicker1, comboBox3, d1, custid);               //Book GIft
                MessageBox.Show("Your Booking ID is " + s1);
                reset();
                c.quantity_updateB(s1);                                                                         //Update Quantity
            }

            if (f3 == 1)
            {
                label15.Text = "*Invalid Date";
                label15.Show();
                dateTimePicker1.ResetText();
                dateTimePicker1.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label15.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label15.Hide();
            }

            if (f2 == 1)
            {
                label14.Text = "*Invalid Phone No.";
                label14.Show();
                textBox2.ResetText();
                textBox2.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label14.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f2 == 2)
            {
                label14.Text = "*Too Short";
                label14.Show();
                textBox2.ResetText();
                textBox2.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label14.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f2 == 3)
            {
                label14.Text = "*Input Required";
                label14.Show();
                textBox2.ResetText();
                textBox2.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label14.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label14.Hide();
            }

            if (f1 == 1)
            {
                label13.Text = "*Invalid Characters";
                label13.Show();
                textBox1.ResetText();
                textBox1.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label13.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f1 == 2)
            {
                label13.Text = "*Too Short";
                label13.Show();
                textBox1.ResetText();
                textBox1.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label13.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f1 == 3)
            {
                label13.Text = "*Input Required";
                label13.Show();
                textBox1.ResetText();
                textBox1.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label13.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label13.Hide();
            }
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            c.formLoadDataB(comboBox1, comboBox2);              //Fill Gift Type and Occassion
            label13.Hide();
            label14.Hide();
            label15.Hide();
            string custname = c.custname(custid);
            label16.Text = "Welcome " + custname;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            c.button1ClickB(comboBox3, comboBox1, comboBox2);       //Search the gifts
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)             //Status Checking
        {
            SqlDataReader dr;
            dr = c.combo3changeB(comboBox3);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    label6.Text = dr[1].ToString();
                    if ((Int32)dr[0] <= 0)
                    {
                        label7.Text = "0";
                        label9.Text = "OUT OF STOCK";
                        textBox1.Hide();
                        textBox2.Hide();
                        dateTimePicker1.Hide();
                        button2.Hide();
                        label10.Hide();
                        label11.Hide();
                        label12.Hide();
                    }
                    else
                    {
                        label7.Text = dr[0].ToString();
                        label9.Text = "IN STOCK";
                        textBox1.Show();
                        textBox2.Show();
                        dateTimePicker1.Show();
                        button2.Show();
                        label10.Show();
                        label11.Show();
                        label12.Show();
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)                          //Reset
        {
            reset();
        }
        public void reset()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Fields Already Empty");
                label13.Hide();
                label14.Hide();
                label15.Hide();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Fields Already Empty", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                c.button1ClickB(comboBox3, comboBox1, comboBox2);
                textBox1.ResetText();
                textBox2.ResetText();
                label6.Text = "";
                label7.Text = "";
                label9.Text = "";
                label13.Hide();
                label14.Hide();
                label15.Hide();
                dateTimePicker1.ResetText();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ret = 1;
            ret = c.combo1changeB(comboBox1, comboBox2);
            if (ret == 0)
            {
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                textBox1.Text = "";
                textBox2.Text = "";
                label6.Text = "";
                label7.Text = "";
                label9.Text = "";
                dateTimePicker1.ResetText();
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            label6.Text = "";
            label7.Text = "";
            label9.Text = "";
            dateTimePicker1.ResetText();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)                  //Alignment
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }

        private void button5_Click(object sender, EventArgs e)                      //Logout
        {
            DialogResult result = MessageBox.Show("Are You Sure???", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ActiveForm.Hide();
                Login_Page form = new Login_Page();
                form.Show();
            }
        }

        private void booking_gift_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login_Page form = new Login_Page();
            form.Show();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            f1 = c.addressValidate(textBox1);                           //Validation
            if (f1 == 1)
            {
                label13.Text = "*Invalid Characters";
                label13.Show();
            }
            else if (f1 == 2)
            {
                label13.Text = "*Too Short";
                label13.Show();
            }
            else if (f1 == 3)
            {
                label13.Text = "*Input Required";
                label13.Show();
            }
            else
            {
                label13.Hide();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            f2 = c.phoneValidate(textBox2);                         //Validation
            if (f2 == 1)
            {
                label14.Text = "*Invalid Phone No.";
                label14.Show();
            }
            else if (f2 == 2)
            {
                label14.Text = "*Too Short";
                label14.Show();
            }
            else if (f2 == 3)
            {
                label14.Text = "*Input Required";
                label14.Show();
            }
            else
            {
                label14.Hide();
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            f3 = c.eDateValidate(dateTimePicker1);                      //Validation
            if (f3 == 1)
            {
                label15.Text = "*Invalid Date";
                label15.Show();
            }
            else
            {
                label15.Hide();
            }
        }
    }
}
