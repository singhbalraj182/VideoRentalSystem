using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public class SqlMethod
    {
        private static SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-LTQK306;Initial Catalog=VideoRentalDB;Integrated Security=True");
        static SqlCommand myCmd;
        public static void GetCustomer(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getCustomer", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void GetVideo(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getVideo", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void GetBooking(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getBooking", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
            gv.Columns["CID"].Visible = false;
            gv.Columns["VID"].Visible = false;
            gv.Columns["Cost"].Visible = false;
        }
        public static void DeleteData(TextBox a, TextBox b, TextBox c,DateTimePicker d, string id)
        {
            string query = "delete from Customer where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Value = DateTime.Now;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void DeleteData(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f,String id)
        {
            string query = "delete from Video where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";
                e.Text = "";
                f.Value = DateTime.Now;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public static void DeleteData(String id)
        {
            string query = "delete from Booking where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void AddData(TextBox a, TextBox b, TextBox c,DateTimePicker d)
        {
            string query = "insert into Customer(Name,Phone,Address,JoinDate) values('" + a.Text + "','" + b.Text + "','" + c.Text + "','"+d.Text+"');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Value = DateTime.Now;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
        }
        public static void AddData(Button a, Button b, DateTimePicker c, DateTimePicker d)
        {
            int count = 0;
            string q = "select Copies from Video where ID=" + Convert.ToInt32(b.Tag) + ";";
            SqlDataReader dataReader;
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(q, myCon);
                dataReader = myCmd.ExecuteReader();
                dataReader.Read();
                count = dataReader.GetInt32(0);
                dataReader.Close();
                myCon.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
            if (count != 0)
            {
                string query = "insert into Booking(CID,VID,Start,Due,Status) values(" + Convert.ToInt32(a.Tag) + "," + Convert.ToInt32(b.Tag) + ",'" + c.Value.ToString("dd MMMM yy") + "','" + d.Value.ToString("dd MMMM yy") + "','Issue'); update Video set Copies=Copies-1 where ID=" + Convert.ToInt32(b.Tag) + "; ";
                try
                {
                    myCon.Open();
                    myCmd = new SqlCommand(query, myCon);
                    myCmd.ExecuteReader();
                    myCon.Close();
                    MessageBox.Show("Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    myCon.Close();
                }
            }
            else
            {
                MessageBox.Show("Video Copies Not Available...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static void AddData(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f)
        {
            string query = "insert into Video(Title,Genre,Cost,Language,Copies,PublishYear) values('" + a.Text + "','" + b.Text + "','" + Convert.ToInt32(c.Text) + "','" + d.Text + "'," + Convert.ToInt32(e.Text) + ",'" + f.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";
                e.Text = "";
                f.Value = DateTime.Now;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateData(TextBox a, TextBox b, TextBox c,DateTimePicker d, string id)
        {
            string query = "update Customer set Name='" + a.Text + "',Phone='" + b.Text + "', Address='" + c.Text + "',JoinDate='"+d.Text+"' where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Value = DateTime.Now;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateData(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f, String id)
        {
            string query = "update Video set Title='" + a.Text + "', Genre='" + b.Text + "', Cost='" + Convert.ToInt32(c.Text) + "', Language='" + d.Text + "', Copies=" + Convert.ToInt32(e.Text) + ",PublishYear='" + f.Text + "'  where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                d.Text = "";
                e.Text = "";
                c.Text = "";
                f.Value = DateTime.Now;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateData(Button a, Button b, DateTimePicker c, DateTimePicker d, String id, int i)
        {
            string query = "update Booking set CID=" + Convert.ToInt32(a.Tag) + ", VID=" + Convert.ToInt32(b.Tag) + ", Start='" + c.Value.ToString("dd MMMM yy") + "',Due='" + d.Value.ToString("dd MMMM yy") + "',Status='Return' where ID=" + Convert.ToInt32(id) + "; update Video set Copies=Copies+1 where ID=" + b.Tag + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Total Rent Cost is " + i.ToString() + "$", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void LoadLabel(Label a, Label b, Label c, Label d,Label e)
        {
            string query1 = "select Top 1 v.Title FROM Booking b,Video v where b.VID=v.ID group by b.VID,v.Title;";
            string query2 = "select Top 1 c.Name FROM Booking b,Customer c where b.CID=c.ID group by b.CID,c.Name;";
            string query3 = "select count(ID) FROM Customer;";
            string query4 = "select Cost from AllRented;";
            string query5 = "select count(ID) FROM Video;";
            SqlDataReader dr1, dr2, dr3, dr4 ,dr5;
            try
            {
                myCmd = new SqlCommand(query1, myCon);
                myCon.Open();
                dr1 = myCmd.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    a.Text = dr1.GetString(0);
                    dr1.Close();
                }
                myCon.Close();

                myCmd = new SqlCommand(query2, myCon);
                myCon.Open();
                dr2 = myCmd.ExecuteReader();
                if (dr2.HasRows)
                {
                    dr2.Read();
                    b.Text = dr2.GetString(0);
                    dr2.Close();
                }
                myCon.Close();
             
                myCmd = new SqlCommand(query3, myCon);
                myCon.Open();
                dr3 = myCmd.ExecuteReader();
                if (dr3.HasRows)
                {
                    dr3.Read();
                    c.Text = dr3.GetValue(0).ToString();
                    dr3.Close();
                }
                myCon.Close();
                
             myCmd = new SqlCommand(query4, myCon);
             myCon.Open();
             dr4 = myCmd.ExecuteReader();
             if (dr4.HasRows)
             {
                    int i=0;
                 while(dr4.Read())
                    {
                        i +=Convert.ToInt32(dr4.GetValue(0).ToString());
                    }
                d.Text ="$"+ i;
                 dr4.Close();
             }
             myCon.Close();

             myCmd = new SqlCommand(query5, myCon);
             myCon.Open();
             dr5 = myCmd.ExecuteReader();
             if (dr5.HasRows)
             {
                 dr5.Read();
                 e.Text = dr5.GetValue(0).ToString();
                    dr5.Close();
             }
             myCon.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
