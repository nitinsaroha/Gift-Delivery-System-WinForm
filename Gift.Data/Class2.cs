using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp;
using System.Diagnostics;
using PdfSharp.Drawing.Pdf;
using System.Drawing;
using System.Configuration;
using System.Globalization;

namespace Gift.Data
{
    public class Class2
    {

        string cs = Properties.Settings.Default.connect;                        //Connection String.
        string log = "log.txt";


        //Add Customer

        //Writes the new user details into the database.
        public string write(TextBox t1, TextBox t2, TextBox t3, TextBox t4, ComboBox c1, DateTimePicker dt1, TextBox t5)
        {
            string s = "";
            try
            {
                string connectionString = cs;
                string query = "insertion";
                string query2 = "custid";

                SqlConnection cn = new SqlConnection(connectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(t1.Text.ToString()));
                cmd.Parameters.AddWithValue("@contact", t2.Text);
                cmd.Parameters.AddWithValue("@pass", t3.Text);
                cmd.Parameters.AddWithValue("@email", t4.Text);
                cmd.Parameters.AddWithValue("@gender", c1.Text);
                cmd.Parameters.AddWithValue("@dob", dt1.Value.Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@addr", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(t5.Text.ToString()));
                cmd.ExecuteNonQuery();
                cn.Close();

                SqlCommand cm1 = new SqlCommand(query2, cn);
                cm1.CommandType = CommandType.StoredProcedure;
                cm1.Parameters.AddWithValue("@inp", t4.Text);
                cn.Open();
                using (SqlDataReader dbr = cm1.ExecuteReader())
                {
                    while (dbr.Read())
                    {
                        s = dbr.GetString(0);
                    }
                }
                cn.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            return s;
        }

        //Checks whether the email is already present in the database
        public int email(TextBox t)
        {
            int f = 0;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = cs;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "FindString";
            command.Parameters.AddWithValue("@inp", t.Text);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    f = 1;
                    return f;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return f;
        }

        //Checks for the login credentials in the database.
        public int login(TextBox t1, TextBox t2)
        {

            int f = 0;
            try
            {
                //string str = cs;
                string query = "admin_login";
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inp1", t1.Text);
                cmd.Parameters.AddWithValue("@inp2", t2.Text);
                SqlDataReader dbr;
                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                    f = 1;
                }
                con.Close();
                if (f == 0)
                {
                    query = "customer_login ";
                    cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inp1", t1.Text);
                    cmd.Parameters.AddWithValue("@inp2", t2.Text);
                    con.Open();
                    dbr = cmd.ExecuteReader();
                    while (dbr.Read())
                    {
                        f = 2;
                    }
                    con.Close();
                }
            }
            catch (Exception es)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + es.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            return f;
        }



        //Book Gift

        //fills the data into the gift type combobox.
        public void formLoadData(ComboBox comboBox1, ComboBox comboBox2)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(" select distinct gift as giftD from sub_item ", conn);
            DataTable t1 = new DataTable();
            da1.Fill(t1);
            comboBox1.DataSource = t1;
            comboBox1.DisplayMember = "giftD".ToString();
            comboBox1.ValueMember = "giftD";
            conn.Close();

        }

        //fills the data into the sub id combobox.
        public void button1Click(ComboBox comboBox3, ComboBox comboBox1, ComboBox comboBox2)
        {

            //adding data to gift subtype combobox3
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlDataAdapter da3 = new SqlDataAdapter(" select Sub_id from Sub_Item where gift = '" + comboBox1.Text.ToString() + "' AND  occ='" + comboBox2.Text.ToString() + "'", conn);
            DataTable t3 = new DataTable();
            da3.Fill(t3);
            comboBox3.DataSource = t3;
            comboBox3.DisplayMember = "Sub_id".ToString();
            comboBox3.ValueMember = "Sub_id";
        }

        //writes the Booking details and returns the Booking ID.
        public string write(TextBox t1, TextBox t2, DateTimePicker dt1, ComboBox c1, DateTime d1, string custid)
        {
            string s = "";
            try
            {
                string connectionString = cs;
                string query = "bookinsertion";
                string query2 = "bookingid";

                SqlConnection cn = new SqlConnection(connectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@addr", t1.Text);
                cmd.Parameters.AddWithValue("@custid", custid);
                cmd.Parameters.AddWithValue("@phoneno", t2.Text);
                cmd.Parameters.AddWithValue("@gsubid", c1.Text);
                cmd.Parameters.AddWithValue("@edate", dt1.Value.Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@bdate", d1.Date.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
                cn.Close();

                SqlCommand cm1 = new SqlCommand(query2, cn);
                cm1.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dbr = cm1.ExecuteReader())
                {
                    while (dbr.Read())
                    {
                        s = dbr.GetString(0);
                    }
                }
                cn.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log(e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            return s;
        }

        //fills the data into the labels on the form.
        public SqlDataReader combo3change(ComboBox comboBox3)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Quantity,cost from Sub_Item where sub_id='" + comboBox3.Text + "'", conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }

        //reduces the quantity once a gift is booked.
        public void quantity_update(string bid)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update Sub_Item set Quantity=Quantity-1 where Sub_id=(select gsubid from booking_details where bookingid='" + bid + "')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        //fills the occassion combobox.
        public int combo1changeB(ComboBox comboBox1, ComboBox comboBox2)
        {
            int ret = 0;
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlDataAdapter da2 = new SqlDataAdapter(" select Distinct(occ) from sub_item where gift='" + comboBox1.Text.ToString() + "'", conn);
            DataTable t2 = new DataTable();
            da2.Fill(t2);
            comboBox2.DataSource = t2;
            comboBox2.DisplayMember = "occ".ToString();
            comboBox2.ValueMember = "occ";
            conn.Close();
            if (t2.Rows.Count == 0)
            {
                ret = 0;
            }
            else
            {
                ret = 1;
            }
            return ret;
        }

        //Return the name of the customer according to customer id.
        public string custname(string x)
        {
            string y = "";
            SqlDataReader dr;
            SqlConnection cn = new SqlConnection(cs);
            cn.Open();
            SqlCommand cmd2 = new SqlCommand("select name from customer where custid='" + x + "'", cn);
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                y = dr[0].ToString();
            }
            cn.Close();
            string[] z = y.Split(' ');
            y = z[0];
            return y;
        }



        //Report Generation

        //Fills the Gift Type Combobox.
        public void gifttype(ComboBox c, DateTimePicker dt1, DateTimePicker dt2)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlDataAdapter da1 = new SqlDataAdapter("select gt.Type from gtype gt join sub_item si on gt.Type=si.gift join booking_details bd on si.sub_id=bd.gsubid where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' group by gt.Type;", conn);
            DataTable t1 = new DataTable();
            da1.Fill(t1);
            c.DataSource = t1;
            c.DisplayMember = "Type".ToString();
            c.ValueMember = "Type";
            c.SelectedIndex = -1;
            conn.Close();
        }

        //Fills the Sub Id Combobox.
        public void subid(ComboBox c1, string c)
        {
            try
            {
                SqlConnection conn1 = new SqlConnection(cs);
                conn1.Open();
                //SqlDataAdapter da2 = new SqlDataAdapter(" select sub_id from Sub_item where Gift_id=(select id from Gift_type where Gift_type=" + c + ")", conn1);
                //DataTable t2 = new DataTable();
                //da2.Fill(t2);
                SqlDataReader dr;
                SqlCommand cmd;
                cmd = new SqlCommand("select gsubid from booking_details bd join sub_item si on bd.gsubid=si.sub_id where si.gift='" + c + "' group by bd.gsubid", conn1);
                //c1.DataSource = t2;
                dr = cmd.ExecuteReader();
                c1.DisplayMember = "sub_id".ToString();
                c1.Items.Clear();
                c1.ValueMember = "sub_id";
                //c1.Dispose();
                while (dr.Read())
                {
                    c1.Items.Add(dr[0].ToString());
                }
                conn1.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }


        }

        //Fills the Booking Id Combobox.
        public void bookingid(ComboBox c2, string a, DateTimePicker dt1, DateTimePicker dt2)
        {
            try
            {
                SqlConnection conn2 = new SqlConnection(cs);
                conn2.Open();
                SqlDataReader dr1;
                SqlCommand cmd1;
                cmd1 = new SqlCommand("select bookingid from booking_details where gsubid ='" + a + "' and bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "'", conn2);
                dr1 = cmd1.ExecuteReader();
                c2.DisplayMember = "bookingid".ToString();
                c2.Items.Clear();
                c2.ValueMember = "bookingid";

                while (dr1.Read())
                {
                    c2.Items.Add(dr1[0].ToString());
                }
                conn2.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }

        }

        //Display the Status according to data given by admin.
        public void status(Label l5, Label l7, Label l9, ComboBox c3)
        {
            try
            {
                SqlConnection conn3 = new SqlConnection(cs);
                conn3.Open();
                SqlDataReader dr2;
                SqlCommand cmd2;
                cmd2 = new SqlCommand("select status1,convert(varchar(10),edate,111),convert(varchar(10),adate,111) from booking_details where bookingid='" + c3.Text + "'", conn3);
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    l5.Text = dr2[0].ToString();
                    l7.Text = dr2[1].ToString();
                    l9.Text = dr2[2].ToString();
                }
                conn3.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }

        }

        //Display the Total Amount per Gift Type.
        public void tamount(ComboBox c1, Label l11, DateTimePicker dt1, DateTimePicker dt2)
        {
            try
            {
                SqlConnection conn3 = new SqlConnection(cs);
                conn3.Open();
                SqlDataReader dr2;
                SqlCommand cmd2;
                cmd2 = new SqlCommand("select SUM(si.cost) from gtype gt join sub_item si on si.gift=gt.Type join booking_details bd on bd.gsubid=si.sub_id where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' and gt.Type='" + c1.Text + "'", conn3);
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    l11.Text = dr2[0].ToString();
                }
                conn3.Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
        }

        //Display the Amount per Gift Sub id.
        public void amount(ComboBox c2, Label l13, DateTimePicker dt1, DateTimePicker dt2)
        {
            try
            {
                SqlConnection conn3 = new SqlConnection(cs);
                conn3.Open();
                SqlDataReader dr2;
                SqlCommand cmd2;
                cmd2 = new SqlCommand("select sum(si.cost) from sub_item si join booking_details bd on si.sub_id=bd.gsubid where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' and si.sub_id='" + c2.Text + "'", conn3);
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    l13.Text = dr2[0].ToString();
                }
                conn3.Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
        }

        //Display the Quantity sold out of sub id.
        public void quantity(ComboBox c2, Label l15, DateTimePicker dt1, DateTimePicker dt2)
        {
            try
            {
                SqlConnection conn3 = new SqlConnection(cs);
                conn3.Open();
                SqlDataReader dr2;
                SqlCommand cmd2;
                cmd2 = new SqlCommand("select count(si.sub_id) from sub_item si join booking_details bd on si.sub_id=bd.gsubid where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' and si.sub_id='" + c2.Text + "'", conn3);
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    l15.Text = dr2[0].ToString();
                }
                conn3.Close();
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
        }

        //Generate a pdf report according to input given by admin.
        public void report(DateTimePicker dt1, DateTimePicker dt2)
        {
            try
            {
                string connetionString = null;
                SqlConnection connection;
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();
                int i = 0;
                string sql = null;
                int yPoint = 0;
                string gifttype = null;
                string subid = null;
                string bookingid = null;
                string bdate = null;
                string edate = null;
                string adate = null;
                string status = null;
                string amount = null;
                string tamount = null;
                string ordered = null;


                connetionString = cs;
                sql = "select gt.Type, si.sub_id, bd.bookingid, convert(varchar(10),bd.bdate,111),convert(varchar(10),bd.edate,111), convert(varchar(10),bd.adate,111), bd.status1 from gType gt join sub_item si on gt.type=si.gift join booking_details bd on si.sub_id=bd.gsubid where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' order by gt.Type, bd.gsubid, bd.bookingid; ";
                connection = new SqlConnection(connetionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                connection.Close();

                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "REPORT";
                PdfPage pdfPage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Times New Roman", 08, XFontStyle.Regular);
                XFont bold = new XFont("Arial", 08, XFontStyle.Bold);
                XFont head = new XFont("Arial", 18, XFontStyle.Bold);
                yPoint = yPoint + 80;
                graph.DrawString("GIFT DELIVERY REPORT", head, XBrushes.Black, new XRect(210, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                yPoint = yPoint + 50;
                graph.DrawString("Gift Type", bold, XBrushes.Black, new XRect(25, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Sub Item Id", bold, XBrushes.Black, new XRect(125, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Booking Id", bold, XBrushes.Black, new XRect(225, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Booking Date", bold, XBrushes.Black, new XRect(325, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Expected Delivery Date", bold, XBrushes.Black, new XRect(400, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Actual Delivery Date", bold, XBrushes.Black, new XRect(525, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Status", bold, XBrushes.Black, new XRect(625, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                yPoint = yPoint + 30;

                for (i = 0; i <= (ds.Tables[0].Rows.Count - 1); i++)
                {
                    gifttype = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    subid = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    bookingid = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    bdate = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                    edate = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    adate = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                    status = ds.Tables[0].Rows[i].ItemArray[6].ToString();

                    graph.DrawString(gifttype, font, XBrushes.Black, new XRect(25, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(subid, font, XBrushes.Black, new XRect(125, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(bookingid, font, XBrushes.Black, new XRect(225, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(bdate, font, XBrushes.Black, new XRect(325, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(edate, font, XBrushes.Black, new XRect(400, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(adate, font, XBrushes.Black, new XRect(525, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(status, font, XBrushes.Black, new XRect(625, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    yPoint = yPoint + 20;
                }

                ds.Reset();
                sql = "select si.sub_id,COUNT(bd.gsubid),sum(si.cost) from sub_item si join booking_details bd on si.sub_id=bd.gsubid where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' group by si.sub_id";
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                connection.Close();

                yPoint = yPoint + 50;
                int s = yPoint;
                graph.DrawString("Sub Item Id", bold, XBrushes.Black, new XRect(70, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Quantity", bold, XBrushes.Black, new XRect(130, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Amount", bold, XBrushes.Black, new XRect(190, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                yPoint = yPoint + 30;

                for (i = 0; i <= (ds.Tables[0].Rows.Count - 1); i++)
                {
                    subid = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                    ordered = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                    amount = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                    graph.DrawString(subid, font, XBrushes.Black, new XRect(70, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(ordered, font, XBrushes.Black, new XRect(130, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(amount, font, XBrushes.Black, new XRect(190, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    yPoint = yPoint + 20;
                }

                ds.Reset();
                sql = "select gt.Type,SUM(si.cost) from gtype gt join sub_item si on si.gift=gt.Type join booking_details bd on bd.gsubid=si.sub_id where bd.bdate between '" + dt1.Value.Date + "' and '" + dt2.Value.Date + "' group by gt.Type;";
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                connection.Close();

                yPoint = s;
                graph.DrawString("Gift Type", bold, XBrushes.Black, new XRect(350, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Total Amount", bold, XBrushes.Black, new XRect(450, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                yPoint = yPoint + 30;

                for (i = 0; i <= (ds.Tables[0].Rows.Count - 1); i++)
                {
                    gifttype = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                    tamount = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                    graph.DrawString(gifttype, font, XBrushes.Black, new XRect(350, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(tamount, font, XBrushes.Black, new XRect(450, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    yPoint = yPoint + 20;
                }

                string pdfFilename = "Report.pdf";
                pdf.Save(pdfFilename);
                Process.Start(pdfFilename);
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
        }



        //Update Details

        //Update the details for a booking id.
        public void get_data_form2(ComboBox comboBox1, DateTimePicker dateTimePicker1, Label label3)
        {

            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand sc = new SqlCommand("update booking_details set status1 = '" + comboBox1.Text + "', adate = '" + dateTimePicker1.Value.Date + "'where bookingid = '" + label3.Text + "';", conn);
            int o = sc.ExecuteNonQuery();
            MessageBox.Show("Gift Delivery status updated successfully");
            conn.Close();
        }

        //Validate the booking id.
        public int get_booking_form1(TextBox textbox1)
        {
            int f = 0;
            SqlDataReader dr;
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlCommand sc = new SqlCommand("select bookingid from booking_details where bookingid= '" + textbox1.Text + "';", conn);
            dr = sc.ExecuteReader();
            if (dr.HasRows)
            {
                f = 1;
            }
            conn.Close();
            return f;
        }

        //Return Status of Booking Id.
        public void user_data(Label label2, Label label3, Label label4, Label label9)
        {
            SqlDataReader dr;
            string connectionString = cs;
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd2 = new SqlCommand("select phoneno, addr, edate from booking_details where bookingid = '" + label3.Text + "';", cn);
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                label4.Text = dr[0].ToString();
                label2.Text = dr[1].ToString();
                label9.Text = dr[2].ToString();
            }
            cn.Close();
        }



        //Add Gift

        //Check whether the gift type is already present in the database.
        public int giftvalidate(TextBox t)
        {
            int f = 0;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = cs;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "FindGift";
            command.Parameters.AddWithValue("@inp", t.Text.ToUpper());
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    f = 1;
                    return f;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return f;
        }

        //Check whether the occassion type is already present in the database.
        public int occassionvalidate(TextBox t)
        {
            int f = 0;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = cs;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "FindOccas";
            command.Parameters.AddWithValue("@inp1", t.Text);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    f = 1;
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return f;
        }

        //Writing the new gift type into the database.
        public int giftwrite(TextBox t)
        {
            int f = 0;
            try
            {
                string connectionString = cs;
                string query = "gift_insertion";
                SqlConnection cn = new SqlConnection(connectionString);
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inp", t.Text.ToUpper());
                cmd.ExecuteNonQuery();
                cn.Close();
                f = 1;
            }
            catch (Exception ex)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + ex.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            return f;
        }

        //Writing the details of new gift and returning a sub_id.
        public int finalwrite(TextBox t, TextBox t1, string S1, string S2)
        {
            int f = 0;
            try
            {
                string connectionString = cs;
                string query = "occgift";
                SqlConnection cn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.Parameters.AddWithValue("@gift", S1.ToUpper());
                cmd.Parameters.AddWithValue("@occ", S2.ToUpper());
                cmd.Parameters.AddWithValue("@cost", t.Text);
                cmd.Parameters.AddWithValue("@qun", t1.Text);
                cmd.ExecuteNonQuery();
                f = 1;
                cn.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            return f;
        }

        //Writing a new occassion to database.
        public int occassionwrite(TextBox t)
        {
            int f = 0;
            try
            {
                string connectionString = cs;
                string query = "occassion_insertion";
                SqlConnection cn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.Parameters.AddWithValue("@inp", t.Text.ToUpper());
                cmd.ExecuteNonQuery();
                f = 1;
                cn.Close();
            }
            catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(log))
                {
                    Log("Exception " + e.Message, w);
                }

                using (StreamReader r = File.OpenText(log))
                {
                    DumpLog(r);
                }
            }
            return f;
        }

        //Fill data into gift type combobox.
        public void fillgift(ComboBox c1)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(" select Type from gtype where id>1200 order by id", conn);
            DataTable t1 = new DataTable();
            da1.Fill(t1);
            c1.DataSource = t1;
            c1.DisplayMember = "Type".ToString();
            c1.ValueMember = "Type";
            conn.Close();
        }

        //fill data into occassion Combobox.
        public void filloccassion(ComboBox c2)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(" select name from occassion where id>100 order by id ", conn);
            DataTable t1 = new DataTable();
            da1.Fill(t1);
            c2.DataSource = t1;
            c2.DisplayMember = "name".ToString();
            c2.ValueMember = "name";
            conn.Close();

        }

        //Return the most recent gift Sub_id.
        public string labelval()
        {
            string f = " ";
            SqlDataReader dr;
            string connectionString = cs;
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd2 = new SqlCommand("select top 1 sub_id from sub_item order by sub_id desc", cn);
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                f = dr[0].ToString();
            }
            cn.Close();
            return f;
        }



        //Log File

        //Write Log File
        public void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        //Dump Log File
        public void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}