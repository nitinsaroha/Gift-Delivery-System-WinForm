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
    public partial class addgift : Form
    {

        Class1 c = new Class1();
        int f1 = 0, f2 = 0, f3 = 0, f4 = 0, f5 = 0, c1 = 0, c2 = 0;
        public addgift()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            c.fillgiftB(comboBox1);                     //Fill GIft Type
            c.filloccassion(comboBox2);                 //Fill Occassion
            comboBox1.Show();
            comboBox2.Show();
            comboBox1.SelectedValue = -1;
            comboBox2.SelectedValue = -1;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Hide();
            textBox4.Hide();
            button1.Enabled = false;
            button2.Enabled = false;
            label4.Enabled = false;
            label5.Enabled = false;
            label8.Hide();
            label9.Hide();
            label3.Hide();
            label7.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedValue = -1;
            textBox1.Text = "";
            textBox2.Text = "";

            if (comboBox1.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == true)
            {
                textBox3.Show();
                textBox1.Show();
                textBox2.Show();
                label4.Enabled = true;
                label5.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                label3.Hide();
                label7.Hide();
            }
            else
            {
                textBox3.Hide();
                button1.Enabled = true;
                button2.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                label3.Hide();
                label7.Hide();
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            if (comboBox2.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == true)
            {
                textBox4.Show();
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                label3.Hide();
                label7.Hide();
            }

            else
            {

                textBox4.Hide();
                button1.Enabled = true;
                button2.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                label3.Hide();
                label7.Hide();
            }

        }
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            c1 = c.comboleave1(comboBox1);                      //Validation
            if (c1 == 1)
            {
                label9.Show();
                label9.Text = "*Select Value";
            }
            else
            {
                label9.Hide();
                c1 = 0;

            }

        }
        private void comboBox2_Leave(object sender, EventArgs e)
        {
            c2 = c.comboleave2(comboBox2);                      //Validation
            if (c2 == 1)
            {
                label8.Show();
                label8.Text = "*Select Value";
            }
            else
            {
                label8.Hide();
                c2 = 0;

            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            f2 = c.gifttype(textBox3);                          //Validation
            if (f2 == 1)
            {
                label9.Text = "*Already Exists";
                label9.Show();
            }
            else if (f2 == 2)
            {
                label9.Text = "*Input Required";
                label9.Show();
            }
            else if (f2 == 3)
            {
                label9.Text = "*Alphabets Only";
                label9.Show();
            }
            else
            {
                f2 = 0;
                label9.Hide();
            }

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            f4 = c.occasion(textBox4);                          //Validation
            if (f4 == 1)
            {
                label8.Text = "*Already Exists";
                label8.Show();
            }
            else if (f4 == 2)
            {
                label8.Text = "*Input Required";
                label8.Show();
            }
            else if (f4 == 3)
            {
                label8.Text = "*Alphabets Only";
                label8.Show();
            }
            else
            {
                label8.Hide();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = " ", s2 = " ";
            f5 = 0;

            //Validation

            c1 = c.comboleave1(comboBox1);
            c2 = c.comboleave2(comboBox2);
            f2 = c.gifttype(textBox3);                      //Validation
            f4 = c.occasion(textBox4);                      //Validation
            f1 = c.costvalid(textBox1);                     //Validation
            f3 = c.quantityvalid(textBox2);                 //Validation

            if (comboBox1.Text == "")
            {
                label9.Show();
                label9.Text = "Input Required";
                c1 = 1;
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label9.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label9.Hide();
                c1 = 0;
            }

            if (comboBox2.Text == "")
            {
                label8.Show();
                label8.Text = "Input Required";
                c2 = 1;
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label8.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label8.Hide();
                c2 = 0;
            }

            if (f3 == 1)
            {
                label7.Text = "*Invalid Input";
                label7.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label7.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f3 == 2)
            {
                label7.Text = "*Can't Be Zero";
                label7.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label7.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f3 == 3)
            {
                label7.Text = "*Input Required";
                label7.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label9.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label7.Hide();
                f3 = 0;
            }

            if (f1 == 1)
            {
                label3.Text = "*Numeric Values Only";
                label3.Show();
                textBox1.ResetText();
                textBox1.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label3.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f1 == 2)
            {
                label3.Text = "*Input Required";
                label3.Show();
                textBox1.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(label3.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                label3.Hide();
                f1 = 0;
            }

            if (comboBox1.Text.Equals("others", StringComparison.OrdinalIgnoreCase) == true)
            {
                f2 = c.gifttype(textBox3);                          //Validation
                if (f2 == 1)
                {
                    label9.Text = "*Already Exists";
                    label9.Show();
                    textBox3.ResetText();
                    textBox3.Focus();
                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        c.Log(label9.Text, w);
                    }

                    using (StreamReader r = File.OpenText("log.txt"))
                    {
                        c.DumpLog(r);
                    }
                }
                else if (f2 == 2)
                {
                    label9.Text = "*Input Required";
                    label9.Show();
                    textBox3.Focus();
                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        c.Log(label9.Text, w);
                    }

                    using (StreamReader r = File.OpenText("log.txt"))
                    {
                        c.DumpLog(r);
                    }
                }
                else
                {
                    label9.Hide();
                }
            }

            if (comboBox2.Text.Equals("others", StringComparison.OrdinalIgnoreCase))
            {
                f4 = c.occasion(textBox4);                              //Validation
                if (f4 == 1)
                {
                    label8.Text = "*Already Exists";
                    label8.Show();
                    textBox4.ResetText();
                    textBox4.Focus();
                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        c.Log(label8.Text, w);
                    }

                    using (StreamReader r = File.OpenText("log.txt"))
                    {
                        c.DumpLog(r);
                    }
                }
                else if (f4 == 2)
                {
                    label8.Text = "*Input Required";
                    label8.Show();
                    textBox4.Focus();
                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        c.Log(label8.Text, w);
                    }

                    using (StreamReader r = File.OpenText("log.txt"))
                    {
                        c.DumpLog(r);
                    }
                }
                else
                {
                    label8.Hide();
                }
            }

            if (comboBox1.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == true
                && comboBox2.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == true &&
               comboBox1.Text != null && comboBox2.Text != null)
            {
                f2 = c.gifttype(textBox3);                             //Validation 
                f4 = c.occasion(textBox4);                            //Validation  
                f1 = c.costvalid(textBox1);                             //Validation
                f3 = c.quantityvalid(textBox2);                         //Validation
                if (f1 == 0 && f2 == 0 && f3 == 0 && f4 == 0 && c1 == 0 && c2 == 0)
                {
                    c.giftwrite(textBox3);                  //write Gift
                    c.occassionwrite(textBox4);             //write occassion
                    s1 = textBox3.Text;
                    s2 = textBox4.Text;
                    f5 = c.finalwrite(textBox1, textBox2, s1, s2);      //Add Gift
                }
                else
                {
                    f5 = 0;
                }
            }

            if (comboBox1.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == true
                  && comboBox2.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == false
                   && (comboBox2.Text != null))
            {
                f1 = c.costvalid(textBox1);                             //Validation
                f3 = c.quantityvalid(textBox2);                         //Validation
                f2 = c.gifttype(textBox3);
                if (f1 == 0 && f2 == 0 && f3 == 0 && c1 == 0 && c2 == 0)
                {
                    c.giftwrite(textBox3);                          //write Gift
                    s1 = textBox3.Text;
                    s2 = comboBox2.Text;
                    f5 = c.finalwrite(textBox1, textBox2, s1, s2);      //Add Gift
                }
                else
                {
                    f5 = 0;
                }
            }
            else if (comboBox1.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == false &&
                (comboBox1.Text != null)
                && comboBox2.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == true)
            {
                f4 = c.occasion(textBox4);                      //Validation
                f3 = c.quantityvalid(textBox2);                 //Validation
                f1 = c.costvalid(textBox1);
                if (f1 == 0 && f3 == 0 && f4 == 0 && c1 == 0 && c2 == 0)
                {
                    c.occassionwrite(textBox4);                     //write occassion
                    s1 = comboBox1.Text;
                    s2 = textBox4.Text;
                    f5 = c.finalwrite(textBox1, textBox2, s1, s2);      //Add Gift
                }
                else
                {
                    f5 = 0;
                }
            }
            else if (comboBox1.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == false
                && comboBox2.Text.Equals("Others", StringComparison.OrdinalIgnoreCase) == false
               && textBox3.Text != null && textBox4.Text != null)
            {
                f3 = c.quantityvalid(textBox2);                     //Validation
                f1 = c.costvalid(textBox1);                        //Validation 

                if (f1 == 0 && f3 == 0 && c1 == 0 && c2 == 0)
                {
                    s1 = comboBox1.Text;
                    s2 = comboBox2.Text;
                    f5 = c.finalwrite(textBox1, textBox2, s1, s2);      //Add Gift
                }
            }


            if (f5 == 1)                                                //Display details & Reset Form.
            {
                MessageBox.Show("Gift Id " + c.labelval() + " added successfully", "Success");
                textBox3.ResetText();
                textBox4.ResetText();
                textBox1.ResetText();
                textBox2.ResetText();
                comboBox1.ResetText();
                comboBox2.ResetText();
                c.fillgiftB(comboBox1);
                c.filloccassion(comboBox2);

            }

        }



        private void button2_Click(object sender, EventArgs e)          //Reset
        {
            c.fillgiftB(comboBox1);
            c.filloccassion(comboBox2);
            c.RecursiveClearTextBoxes(this.Controls);
            comboBox1.SelectedValue = -1;
            comboBox2.SelectedValue = -1;
            label3.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            f1 = c.costvalid(textBox1);                     //Validation
            if (f1 == 1)
            {
                label3.Text = "Invalid Input";
                label3.Show();
            }
            else if (f1 == 2)
            {
                label3.Text = "Input Required";
                label3.Show();
            }
            else
            {
                label3.Hide();
                f1 = 0;
            }

        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            f3 = c.quantityvalid(textBox2);                 //Validation
            if (f3 == 1)
            {
                label7.Text = "*Invalid Input";
                label7.Show();
            }
            else if (f3 == 2)
            {
                label7.Text = "*Can't Be Zero";
                label7.Show();
            }
            else if (f3 == 3)
            {
                label7.Text = "*Input Required";
                label7.Show();
            }
            else
            {
                label7.Hide();
                f3 = 0;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)                  //Alignment
        {
            panel1.Location = new Point(this.ClientSize.Width / 2 - this.panel1.Width / 2, this.ClientSize.Height / 2 - this.panel1.Height / 2);
            panel1.Anchor = AnchorStyles.None;
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

        private void button3_Click(object sender, EventArgs e)                      //Back
        {
            ActiveForm.Close();
        }

        private void Add_Gift_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin form = new Admin();
            form.Show();
        }




    }
}
