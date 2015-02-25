using System;
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
    public partial class Update_Details : Form
    {
        Class1 c = new Class1();
        
        public class ComboboxItem
        {
            public string Text { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }

        public Update_Details(string strTextBox)
        {
            InitializeComponent();
            combo_items();
            label3.Text = strTextBox;
        }
        
        //show from1 after closing of this form
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Update_Search form = new Update_Search();
            form.Show();
        }

        //Center the panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }
        //reset button for status textbox
        private void button2_Click(object sender, EventArgs e)
        {
            c.reset_form2(comboBox1, dateTimePicker1);
        }

        //adding values to the combobox
        public void combo_items()
        {
            ComboboxItem item = new ComboboxItem();
            comboBox1.Items.Add(item.Text = "Delivered");
            comboBox1.Items.Add(item.Text = "Pending");
            comboBox1.Items.Add(item.Text = "On the way");
        }

        //Validation of data entered
        private void button1_Click(object sender, EventArgs e)
        {
            c.date_status_error(comboBox1, dateTimePicker1, label9, label3);
        }

        //Show Details
        private void Update_Details_Load(object sender, EventArgs e)
        {
            c.user_details(label2, label3, label4, label9);
        }

        //Logout
        private void button4_Click(object sender, EventArgs e)
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
        private void button3_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

    }
}