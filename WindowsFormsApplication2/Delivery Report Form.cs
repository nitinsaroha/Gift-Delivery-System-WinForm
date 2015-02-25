using System;
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
    public partial class Delivery_Form : Form
    {
        Class1 c = new Class1();
        DateTimePicker dt = new DateTimePicker();
        DateTimePicker dt4 = new DateTimePicker();
        public Delivery_Form(DateTimePicker dt1, DateTimePicker dt2)
        {
            InitializeComponent();
            dt = dt1;
            dt4 = dt2;
        }
        
        private void Form3_Load_1(object sender, EventArgs e)
        {
            c.gifttype(comboBox1,dt,dt4);                   //fill Gift Type
            label2.Hide();
            comboBox2.Hide();
            label3.Hide();
            comboBox3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            label15.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            c.subid(comboBox2, comboBox1.Text);                     //fill gift sub id
            label2.Show();
            comboBox2.Show();
            comboBox2.ResetText();
            comboBox3.ResetText();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            c.tamount(comboBox1,label11,dt,dt4);                    //show total amount of the gift type selected
            label10.Show();
            label11.Show();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            label15.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            c.bookingid(comboBox3, comboBox2.Text, dt, dt4);                    //fill booking id
            label3.Show();
            comboBox3.Show();
            comboBox3.ResetText();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            c.amount(comboBox2,label13,dt,dt4);
            label12.Show();
            label13.Show();
            c.quantity(comboBox2, label15, dt, dt4);
            label14.Show();
            label15.Show();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delivery_Option f = new Delivery_Option(dt,dt4);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)                      //Reset
        {
            comboBox1.SelectedValue=-1;
            comboBox2.ResetText();
            comboBox2.Hide();
            comboBox3.ResetText();
            comboBox3.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            label15.Hide();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            c.status(label5, label7, label9, comboBox3);                        //Display the details of the booking id
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            label8.Show();
            label9.Show();
        }

        private void Form3_Paint(object sender, PaintEventArgs e)                       //Alignment
        {
            panel1.Top = this.ClientSize.Height / 2 - this.panel1.Height / 2;
            panel1.Left = this.ClientSize.Width / 2 - this.panel1.Width / 2;
            panel1.Anchor = AnchorStyles.None;
            
        }

        private void button2_Click(object sender, EventArgs e)                          //Back
        {
            ActiveForm.Close();
        }

        private void button3_Click(object sender, EventArgs e)                          //Logout
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
