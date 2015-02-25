using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Gift.Business;

namespace WindowsFormsApplication2
{
    public partial class Registration : Form
    {
        int f1 = 0, f2 = 0, f3 = 0, f4 = 0, f5 = 0, f6 = 0, f7 = 0, f8 = 0;
        Class1 c = new Class1();
        public Registration()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            f1 = c.name(textBox1);                  //Validation
            f2 = c.contact(textBox2);               //Validation
            f3 = c.password(textBox3);              //Validation
            f4 = c.retype(textBox4, textBox3);      //Validation
            f5 = c.mail(textBox5);                  //Validation
            f6 = c.gender(comboBox1);               //Validation
            f7 = c.date(dateTimePicker1);           //Validation
            f8 = c.address(textBox6);               //Validation


            if (f1 == 0 && f2 == 0 && f3 == 0 && f4 == 0 && f5 == 0 && f6 == 0 && f7 == 0 && f8 == 0)
            {
                c.write(textBox1, textBox2, textBox3, textBox5, comboBox1, dateTimePicker1, textBox6);          //Add Customer
                ActiveForm.Hide();
                Login_Page form = new Login_Page();                                                             //Login Page
                form.Show();
            }

            if (f8 == 1)
            {
                address.Text = "*Invalid Characters";
                address.Show();
                textBox6.ResetText();
                textBox6.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(address.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f8 == 2)
            {
                address.Text = "*Too Short";
                address.Show();
                textBox6.ResetText();
                textBox6.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(address.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f8 == 3)
            {
                address.Text = "*Input Required";
                address.Show();
                textBox6.ResetText();
                textBox6.Focus();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(address.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                address.Hide();
            }

            if (f7 == 1)
            {
                dateTimePicker1.ResetText();
                dateTimePicker1.Focus();
                date.Text = "*Invalid Date";
                date.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(date.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                date.Hide();
            }

            if (f6 == 1)
            {
                comboBox1.ResetText();
                comboBox1.Focus();
                gender.Text = "*Input Required";
                gender.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(gender.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                gender.Hide();
            }

            if (f5 == 1)
            {
                textBox5.ResetText();
                textBox5.Focus();
                mail.Text = "*Invalid Input";
                mail.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(mail.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f5 == 2)
            {
                textBox5.ResetText();
                textBox5.Focus();
                mail.Text = "*Input Required";
                mail.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(mail.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f5 == 3)
            {
                textBox5.ResetText();
                textBox5.Focus();
                mail.Text = "*Already Exists";
                mail.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(mail.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                mail.Hide();
            }

            if (f4 == 1)
            {
                textBox4.ResetText();
                textBox4.Focus();
                password2.Text = "*Not Matching";
                password2.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password2.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f4 == 2)
            {
                textBox4.ResetText();
                textBox4.Focus();
                password2.Text = "*Input Required";
                password2.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password2.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                password2.Hide();
            }

            if (f3 == 1)
            {
                textBox3.ResetText();
                textBox4.ResetText();
                textBox3.Focus();
                password.Text = "  *Should Contain Alphabets,\nNumbers and Special Characters";
                password.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f3 == 2)
            {
                textBox3.ResetText();
                textBox4.ResetText();
                textBox3.Focus();
                password.Text = "*Shouldn't Start With Number\nor Special Character";
                password.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }

            }
            else if (f3 == 3)
            {
                textBox3.ResetText();
                textBox4.ResetText();
                textBox3.Focus();
                password.Text = "\n*Too short";
                password.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f3 == 4)
            {
                textBox3.ResetText();
                textBox4.ResetText();
                textBox3.Focus();
                password.Text = "\n*Input Required";
                password.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f3 == 5)
            {
                textBox3.ResetText();
                textBox4.ResetText();
                textBox3.Focus();
                password.Text = "\n*Only !,@,#,$";
                password.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(password.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                password.Hide();
            }

            if (f2 == 1)
            {
                textBox2.ResetText();
                textBox2.Focus();
                contact.Text = "*Only Digits";
                contact.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(contact.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f2 == 2)
            {
                textBox2.ResetText();
                textBox2.Focus();
                contact.Text = "*Too short";
                contact.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(contact.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }

            }
            else if (f2 == 3)
            {
                textBox2.ResetText();
                textBox2.Focus();
                contact.Text = "*Input Required";
                contact.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(contact.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                contact.Hide();
            }

            if (f1 == 1)
            {
                textBox1.ResetText();
                textBox1.Focus();
                name.Text = "*Invalid Format or Invalid Characters";
                name.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(name.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f1 == 2)
            {
                textBox1.ResetText();
                textBox1.Focus();
                name.Text = "*Input Required";
                name.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(name.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (f1 == 3)
            {
                textBox1.ResetText();
                textBox1.Focus();
                name.Text = "*Full Name";
                name.Show();
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log(name.Text, w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                name.Hide();
            }
        }
        private void form4_Load(object sender, EventArgs e)
        {
            name.Hide();
            contact.Hide();
            password.Hide();
            password2.Hide();
            address.Hide();
            gender.Hide();
            mail.Hide();
            date.Hide();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Select One");
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "ddd   dd, MMMM, yyyy";
            dateTimePicker1.Value = DateTime.Now;
            Submit.Focus();

        }

        private void button2_Click(object sender, EventArgs e)              //Reset
        {

            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox6.ResetText();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Select One");
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.ResetText();
            name.Hide();
            contact.Hide();
            password.Hide();
            password2.Hide();
            address.Hide();
            gender.Hide();
            mail.Hide();
            date.Hide();

        }

        private void form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            ActiveForm.Hide();
            Login_Page form = new Login_Page();
            form.Show();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            f1 = c.name(textBox1);                      //Validation
            if (f1 == 1)
            {
                name.Text = "*Invalid Format or Invalid Characters";
                name.Show();
            }
            else if (f1 == 2)
            {
                name.Text = "*Input Required";
                name.Show();
            }
            else if (f1 == 3)
            {
                name.Text = "*Full Name";
                name.Show();
            }
            else
            {
                name.Hide();
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            f2 = c.contact(textBox2);                   //Validation
            if (f2 == 1)
            {
                contact.Text = "*Only Digits";
                contact.Show();
            }
            else if (f2 == 2)
            {
                contact.Text = "*Too Short";
                contact.Show();
            }
            else if (f2 == 3)
            {
                contact.Text = "*Input Required";
                contact.Show();
            }
            else
            {
                contact.Hide();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            f3 = c.password(textBox3);                  //Validation
            if (f3 == 1)
            {
                password.Text = "   *Should Contain Alphabets,\nNumbers and Special Characters";
                password.Show();
            }
            else if (f3 == 2)
            {
                password.Text = "*Shouldn't Start With Number\nor Special Character";
                password.Show();
            }
            else if (f3 == 3)
            {
                password.Text = "\n*Too Short";
                password.Show();
            }
            else if (f3 == 4)
            {
                password.Text = "\n*Input Required";
                password.Show();
            }
            else if (f3 == 5)
            {
                password.Text = "\n*Only !,@,#,$";
                password.Show();
            }
            else
            {
                password.Hide();
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            f4 = c.retype(textBox4, textBox3);                  //Validation
            if (f4 == 1)
            {
                password2.Text = "*Not Matching";
                password2.Show();
            }
            else if (f4 == 2)
            {
                password2.Text = "*Input Required";
                password2.Show();
            }
            else
            {
                password2.Hide();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            f5 = c.mail(textBox5);                          //Validation
            if (f5 == 1)
            {
                mail.Text = "*Not Valid";
                mail.Show();
            }
            else if (f5 == 2)
            {
                mail.Text = "*Input Required";
                mail.Show();
            }
            else
            {
                mail.Hide();
            }

        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            f6 = c.gender(comboBox1);                       //Validation
            if (f6 == 1)
            {
                gender.Text = "*Input Required";
                gender.Show();
            }
            else
            {
                gender.Hide();
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            f7 = c.date(dateTimePicker1);                   //Validation
            if (f7 == 1)
            {
                date.Text = "*Invalid Date";
                date.Show();
            }
            else
            {
                date.Hide();
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            f8 = c.address(textBox6);                   //Validation
            if (f8 == 1)
            {
                address.Text = "*Invalid Characters";
                address.Show();
            }
            else if (f8 == 2)
            {
                address.Text = "*Too Short";
                address.Show();
            }
            else if (f8 == 3)
            {
                address.Text = "*Input Required";
                address.Show();
            }
            else
            {
                address.Hide();
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Male");
            comboBox1.Items.Add("Female");
        }

        private void button1_Click_1(object sender, EventArgs e)                //Home
        {
            DialogResult result = MessageBox.Show("Go Home???", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ActiveForm.Hide();
                Login_Page form = new Login_Page();
                form.Show();
            }
        }
    }
}
