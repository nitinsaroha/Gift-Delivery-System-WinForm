using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Gift.Data;
using System.Data;
using System.Data.SqlClient;


namespace Gift.Business
{
    public class Class1
    {
        Class2 c = new Class2();


        //Registration & Home Page

        //writes the details of new user to the database and displays the generated customer id.
        public void write(TextBox t1, TextBox t2, TextBox t3, TextBox t4, ComboBox c1, DateTimePicker dt1, TextBox t5)
        {
            string f = "";
            f = c.write(t1, t2, t3, t4, c1, dt1, t5);
            if (f.Length != 0)
            {
                MessageBox.Show("Your User ID is " + f);
            }
            else
            {
                MessageBox.Show("Database Write Error");
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Database Write Error", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
        }

        //validates the login credentials from the database and returns appropriate value.
        public int login(TextBox t1, TextBox t2)
        {
            int f = 0;
            f = c.login(t1, t2);
            return f;
        }

        //validates the name provided by the user.
        public int name(TextBox t)
        {
            int f = 0;
            Regex r = new Regex(@"[a-zA-Z]+\s{1}[a-zA-Z]+\s{1}[a-zA-Z]+");
            Regex s = new Regex(@"[a-zA-Z]+\s{1}[a-zA-Z]+");
            if (t.Text.Length > 0)
            {
                if (t.Text.Length >= 5)
                {
                    if (r.IsMatch(t.Text) == false && s.IsMatch(t.Text) == false)
                    {
                        f = 1;
                    }
                }
                else
                {
                    f = 3;
                }
            }
            else
            {
                f = 2;
            }
            return f;
        }

        //validates the contact number provided by the user. 
        public int contact(TextBox t)
        {
            int f = 0;
            if (t.Text.Length > 0)
            {
                if (t.Text.Length == 10)
                {
                    foreach (var c in t.Text)
                    {
                        if ((char.IsDigit(c) == false))
                        {
                            f = 1;
                        }
                    }
                }
                else
                {
                    f = 2;
                }
            }
            else
            {
                f = 3;
            }
            return f;
        }

        //validates the password provided by the user.
        public int password(TextBox t)
        {
            int x = 0, y = 0, f = 0, z = 0;
            string s = "!@#$";
            if (t.Text.Length > 0)
            {
                if (t.Text.Length >= 8)
                {
                    foreach (var c in t.Text)
                    {
                        if (char.IsLetterOrDigit(c) == false)
                        {
                            x++;
                            if (s.Contains(c) != true)
                            {
                                f = 5;
                                return f;
                            }
                        }
                        if (char.IsDigit(c) == true)
                        {
                            y++;
                        }
                        if (char.IsLetter(c) == true)
                        {
                            z++;
                        }
                    }
                    if (x < 1 || y < 1 || z < 1)
                    {
                        f = 1;
                    }
                    if (char.IsLetterOrDigit(t.Text[0]) == false || char.IsDigit(t.Text[0]) == true)
                    {
                        f = 2;
                    }
                }
                else
                {
                    f = 3;
                }
            }
            else
            {
                f = 4;
            }
            return f;
        }

        //validates the retyped password matches the password.
        public int retype(TextBox t1, TextBox t2)
        {
            int f = 0;
            if (t1.Text.Length > 0)
            {
                if (t1.Text != t2.Text)
                {
                    f = 1;
                }
            }
            else
            {
                f = 2;
            }
            return f;
        }

        //validates the email address provided by the user and checks whether it already exists in database.
        public int mail(TextBox t)
        {
            int f = 0;
            if (t.Text.Length > 0)
            {
                bool d = Regex.IsMatch(t.Text, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[_\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.))(com|in))$", RegexOptions.IgnoreCase);
                if (d == false)
                {
                    f = 1;
                }
                else
                {
                    int f1 = 0;
                    f1 = c.email(t);
                    if (f1 == 1)
                    {
                        f = 3;
                    }
                }
            }
            else
            {
                f = 2;
            }
            return f;
        }

        //validates the gender is provided by the user.
        public int gender(ComboBox t)
        {
            int f = 0;
            if (t.Text.Equals("Select One") == true)
            {
                f = 1;
            }
            return f;
        }

        //validates the address provided by the user.
        public int address(TextBox t)
        {
            int f = 0;
            string s = "`!@$%^&*:;<>?|+=_";
            if (t.Text.Length > 0)
            {
                if (t.Text.Length >= 20)
                {
                    foreach (var x in t.Text)
                    {
                        if (s.Contains(x) == true)
                        {
                            f = 1;
                            return f;
                        }
                    }
                }
                else
                {
                    f = 2;
                }
            }
            else
            {
                f = 3;
            }
            return f;
        }

        //validates the date of birth provided by the user.
        public int date(DateTimePicker t)
        {
            int f = 0;
            DateTime dt = t.Value;
            if (DateTime.Compare(dt.Date, DateTime.Now.Date) >= 0)
            {
                f = 1;
            }
            return f;
        }



        //Booking Gift

        //validates the expected delivery date provided by the user.
        public int eDateValidate(DateTimePicker dtp)
        {
            int f = 0;
            DateTime dt = dtp.Value;
            if (DateTime.Compare(dt.Date, DateTime.Now.Date) <= 0)
            {
                f = 1;
            }
            return f;
        }

        //validates the reference phone number provided by the user.
        public int phoneValidate(TextBox t1)
        {
            Regex reg = new Regex("^([0-9]{10})$");
            int f = 0;
            if (t1.Text.Length > 0)
            {
                if (t1.Text.Length == 10)
                {
                    if (reg.IsMatch(t1.Text) == false)
                    {
                        f = 1;
                    }
                }
                else
                {
                    f = 2;
                }
            }
            else
            {
                f = 3;
            }
            return f;
        }

        //validates the delivery address provided by the user.
        public int addressValidate(TextBox t2)
        {
            int f = 0;
            string s = "!@#$%^&*";
            if (t2.Text.Length > 0)
            {
                if (t2.Text.Length >= 20)
                {
                    foreach (var x in t2.Text)
                    {
                        if (s.Contains(x) == true)
                        {
                            f = 1;
                            return f;
                        }
                    }
                }
                else
                {
                    f = 2;
                }
            }
            else
            {
                f = 3;
            }
            return f;
        }

        //Fills the data from database into the comboboxes.
        public void formLoadDataB(ComboBox ComboBox1, ComboBox ComboBox2)
        {
            c.formLoadData(ComboBox1, ComboBox2);
        }

        //Searches the itmes according to the input provided by the user.
        public void button1ClickB(ComboBox ComboBox3, ComboBox ComboBox1, ComboBox ComboBox2)
        {
            c.button1Click(ComboBox3, ComboBox1, ComboBox2);
        }

        //Write the details into the database and show the respective booking id
        public string writeB(TextBox t1, TextBox t2, DateTimePicker dt1, ComboBox c1, DateTime d1, string custid)
        {

            String s1 = c.write(t1, t2, dt1, c1, d1, custid);
            return s1;
        }

        //Fills the data into the labels according to a change in other combobox.
        public SqlDataReader combo3changeB(ComboBox ComboBox3)
        {
            SqlDataReader sdr = c.combo3change(ComboBox3);
            return sdr;
        }

        //Reduces the number of items from the database once a gift is booked.
        public void quantity_updateB(string bid)
        {
            c.quantity_update(bid);
        }

        //Displays the sub ids from the database accoring to input selected by the user.
        public int combo1changeB(ComboBox ComboBox1, ComboBox ComboBox2)
        {
            return c.combo1changeB(ComboBox1, ComboBox2);
        }

        //Return the customer name based on the customer id.
        public string custname(string x)
        {
            string z = c.custname(x);
            return z;
        }



        //Report Generation

        //Validates the from booking date provided by the admin
        public int Date1(DateTimePicker dt)
        {
            int f1 = 0;
            if (DateTime.Compare(dt.Value.Date, DateTime.Now.Date) >= 0)
            {
                f1 = 1;
            }
            return f1;
        }

        //validates the to booking date provided by the admin.
        public int Date2(DateTimePicker dt, DateTimePicker dt1)
        {
            int f2 = 0;
            if (DateTime.Compare(dt1.Value.Date, DateTime.Now.Date) > 0)
            {
                f2 = 1;
            }
            else if (dt.Value.Date > dt1.Value.Date)
            {
                f2 = 2;
            }
            return f2;
        }

        //Fills the gift type into the combobox from the database according to the dates selected by admin.
        public void gifttype(ComboBox c1, DateTimePicker dt1, DateTimePicker dt2)
        {
            c.gifttype(c1, dt1, dt2);
        }

        //Fills the sub id into the combobox according to gifttype combobox.
        public void subid(ComboBox c2, string c1)
        {
            c.subid(c2, c1);
        }

        //Fills the booking ids according to subid selected by admin.
        public void bookingid(ComboBox c3, string c2, DateTimePicker dt1, DateTimePicker dt2)
        {
            c.bookingid(c3, c2, dt1, dt2);
        }

        //Display the status of each booking id selected by the admin.
        public void status(Label l5, Label l7, Label l9, ComboBox c3)
        {
            c.status(l5, l7, l9, c3);
        }

        //Generate a pdf file report and display it.
        public void report(DateTimePicker dt1, DateTimePicker dt2)
        {
            c.report(dt1, dt2);
        }

        //Calculate the total amount of a certain gift type.
        public void tamount(ComboBox c1, Label l11, DateTimePicker dt1, DateTimePicker dt2)
        {
            c.tamount(c1, l11, dt1, dt2);
        }

        //Calculate the amount of a certain sub id.
        public void amount(ComboBox c2, Label l13, DateTimePicker dt1, DateTimePicker dt2)
        {
            c.amount(c2, l13, dt1, dt2);
        }

        //Calculate the quantity that has been sold.
        public void quantity(ComboBox c2, Label l15, DateTimePicker dt1, DateTimePicker dt2)
        {
            c.quantity(c2, l15, dt1, dt2);
        }



        //Update Details

        //Validate the booking id provided by the admin.
        public int get_details(TextBox textBox1)
        {
            int g = 0;
            int f = c.get_booking_form1(textBox1);
            if (textBox1.Text == "" || textBox1.Text.Length < 15)
            {
                g = 2;
            }
            else if (f == 1)
            {
                g = 1;
                //int f = 0;
            }
            else
            {
                g = 0;
            }
            return g;
        }

        //reset Booking_id in Update_Search
        public void reset_form1(TextBox textbox1)
        {
            textbox1.Text = "";
        }

        //reset everything in Update_Details
        public void reset_form2(ComboBox ComboBox1, DateTimePicker dateTimePicker1)
        {
            ComboBox1.SelectedIndex = -1;
            dateTimePicker1.ResetText();
        }

        //Validating the input provided and updating the database according to data provided by the admin.
        public void date_status_error(ComboBox ComboBox1, DateTimePicker dateTimePicker1, Label label9, Label label3)
        {
            if (ComboBox1.Text == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Please select status", "Status error", buttons, MessageBoxIcon.Exclamation);
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Please select Status", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (dateTimePicker1.Text == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Please select Actual Delievry Date", "Date error", buttons, MessageBoxIcon.Exclamation);
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("Please select Actual Delivery Date", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else if (dateTimePicker1.Value < Convert.ToDateTime(label9.Text))
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Actual Delivery date cannot be less then Expected Delivery date", "Date error", buttons, MessageBoxIcon.Exclamation);
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    c.Log("PActual Delivery date cannot be less than Expected Delivery date", w);
                }

                using (StreamReader r = File.OpenText("log.txt"))
                {
                    c.DumpLog(r);
                }
            }
            else
            {
                c.get_data_form2(ComboBox1, dateTimePicker1, label3);
            }
        }

        //Get details from database according to details provided by the admin.
        public void user_details(Label label2, Label label3, Label label4, Label label9)
        {
            c.user_data(label2, label3, label4, label9);
        }

        public Label label3 { get; set; }



        //Add Gift

        //Write the new gift type to database.
        public int giftwrite(TextBox t)
        {
            int f = 0;
            f = c.giftwrite(t);
            return f;
        }

        //Validate the gift type selected by the admin.
        public int gifttype(TextBox t)
        {
            int f = 0;
            if (t.Text.Length > 0)
            {
                foreach (var c in t.Text)
                {
                    if (char.IsDigit(c) == false)
                    {
                        f = 1;
                    }
                }
                if (f == 1)
                {
                    f = c.giftvalidate(t);
                }
                else
                {
                    f = 3;
                }

            }
            else if (t.Text.Length == 0)
            {
                f = 2;
            }
            else
            {
                f = 0;
            }
            return f;
        }

        //Validates the occassion provided by the admin.
        public int occasion(TextBox t)
        {
            int f = 0;
            if (t.Text.Length > 0)
            {
                foreach (var x in t.Text)
                {
                    if (char.IsLetter(x) == false)
                    {
                        f = 3;
                    }
                    if (f != 3)
                    {
                        f = c.occassionvalidate(t);
                    }
                }
            }
            else
            {
                f = 2;
            }
            return f;
        }

        //Validate the Combobox as soon as the focus is removed.
        public int comboleave1(ComboBox t)
        {
            int f = 0;
            if (t.Text.Length == 0)
            {
                f = 1;
            }
            else
            {
                f = 0;
            }
            return f;

        }

        //Validate the Combobox as soon as the focus is removed.
        public int comboleave2(ComboBox t)
        {
            int f = 0;
            if (t.Text.Length == 0)
            {
                f = 1;
            }
            else
            {
                f = 0;
            }
            return f;

        }

        //Write the new occassion to the database.
        public int occassionwrite(TextBox t)
        {
            int f = 0;
            f = c.occassionwrite(t);
            return f;
        }

        //fiils the combobox with data from database.
        public void fillgiftB(ComboBox c1)
        {
            c.fillgift(c1);
        }

        //fills the combobox with data from database.
        public void filloccassion(ComboBox c2)
        {
            c.filloccassion(c2);

        }

        //validate the cost provided by the admin.
        public int costvalid(TextBox t1)
        {
            int f = 0;
            if (t1.Text.Length > 0)
            {
                foreach (var c in t1.Text)
                {
                    if (char.IsDigit(c) == false || Regex.IsMatch(t1.Text, @"^\d*(\.\d\d)?$") == false)
                    {
                        f = 1;
                    }
                    else
                    {
                        if (Regex.IsMatch(t1.Text, @"^0*(\.0*)?$"))
                        {
                            f = 1;
                        }
                        else
                        {
                            f = 0;
                        }
                    }

                }
            }
            else if (t1.Text.Length == 0)
            {
                f = 2;
            }
            else
            {
                f = 0;
            }
            return f;
        }

        //validate the quantity provided by the admin.
        public int quantityvalid(TextBox t1)
        {
            int f = 0;
            if (t1.Text.Length > 0)
            {
                if (Convert.ToDouble(t1.Text.ToString()) != 0)
                {
                    foreach (var x in t1.Text)
                    {
                        if (char.IsDigit(x) == false)
                        {
                            f = 1;
                        }
                    }
                }
                else
                {
                    f = 2;
                }
            }
            else
            {
                f = 3;
            }
            return f;

        }

        //clears all the textboxes on the form.
        public void RecursiveClearTextBoxes(Control.ControlCollection cc)
        {
            foreach (Control ctrl in cc)
            {
                TextBox tb = ctrl as TextBox;
                if (tb != null)
                    tb.Clear();
                else
                    RecursiveClearTextBoxes(ctrl.Controls);
            }
        }

        //Write the gift details provided by admin to datatbase.
        public int finalwrite(TextBox t, TextBox t1, string S, string S1)
        {
            int f = 0;
            f = c.finalwrite(t, t1, S, S1);
            return f;
        }

        //Returns the most recent Gift Id.
        public string labelval()
        {
            string f = " ";
            f = c.labelval();
            return f;
        }


        //Log File

        //Write Log
        public void Log(string logMessage, TextWriter w)
        {
            c.Log(logMessage, w);
        }

        public void DumpLog(StreamReader r)
        {
            c.DumpLog(r);
        }
    }
}
