using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideoRentalSystem
{
    public partial class RentalForm : Form
    {
        public RentalForm()
        {
            InitializeComponent();
        }
        int id;
        int cost;
        private void Booking_Load(object sender, EventArgs e)
        {
            SqlMethod.LoadLabel(movieLbl,customerLbl, custLbl, incomeLbl,videoLbl);
            bookBtn.PerformClick();
            id = -1;
        }
        private void bookBtn_Click(object sender, EventArgs e)
        {
            addBtn.Text = "Issue";
            updateBtn.Text = "Return";
            dataGV.Tag = "Booking";
            bookBtn.ForeColor = Color.WhiteSmoke;
            videoBtn.ForeColor = Color.Gray;
            custBtn.ForeColor = Color.Gray;
            SqlMethod.GetBooking(dataGV);
        }
        private void custBtn_Click(object sender, EventArgs e)
        {
            addBtn.Text = "Add";
            updateBtn.Text = "Update";
            dataGV.Tag = "Customer";
            bookBtn.ForeColor = Color.Gray;
            videoBtn.ForeColor = Color.Gray;
            custBtn.ForeColor = Color.WhiteSmoke;
            SqlMethod.GetCustomer(dataGV);
        }

        private void videoBtn_Click(object sender, EventArgs e)
        {
            addBtn.Text = "Add";
            updateBtn.Text = "Update";
            dataGV.Tag = "Video";
            bookBtn.ForeColor = Color.Gray;
            videoBtn.ForeColor = Color.WhiteSmoke;
            custBtn.ForeColor = Color.Gray;
            SqlMethod.GetVideo(dataGV);
        }
        private void dataGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGV.Columns.Count != 0 && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dataGV.Tag.ToString() == "Booking")
                {
                    DataGridViewRow row = dataGV.Rows[e.RowIndex];
                    id = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    customerBtn.Tag = row.Cells["CID"].Value.ToString();
                    titleBtn.Tag = row.Cells["VID"].Value.ToString();
                    customerBtn.Text = row.Cells["Customer"].Value.ToString();
                    titleBtn.Text = row.Cells["Video"].Value.ToString();
                    cost = Convert.ToInt32(row.Cells["Cost"].Value.ToString());
                    startPK.Text = row.Cells["Booking Date"].Value.ToString();
                    endPK.Text = row.Cells["Return Date"].Value.ToString();
                }
                else if (dataGV.Tag.ToString() == "Video")
                {
                    DataGridViewRow row = dataGV.Rows[e.RowIndex];
                    id = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    titleTxt.Text = row.Cells["Title"].Value.ToString();
                    String a = row.Cells["Cost"].Value.ToString();
                    costTxt.Text = row.Cells["Cost"].Value.ToString().Remove(a.Length - 2, 2);
                    copiesTxt.Text = row.Cells["Copies"].Value.ToString();
                    genreTxt.Text = row.Cells["Genre"].Value.ToString();
                    languageTxt.Text = row.Cells["Language"].Value.ToString();
                    titleBtn.Tag = row.Cells["ID"].Value.ToString();
                    titleBtn.Text = row.Cells["Title"].Value.ToString();
                    yearTxt.Value = new DateTime(Convert.ToInt32(row.Cells["PublishYear"].Value.ToString()), 1, 1);
                }
                else if (dataGV.Tag.ToString() == "Customer")
                {
                    DataGridViewRow row = dataGV.Rows[e.RowIndex];
                    id = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    nameTxt.Text = row.Cells["Name"].Value.ToString();
                    cnctTxt.Text = row.Cells["Phone"].Value.ToString();
                    addTxt.Text = row.Cells["Address"].Value.ToString();
                    jdateTxt.Text = row.Cells["JoinDate"].Value.ToString();
                    customerBtn.Tag = row.Cells["ID"].Value.ToString();
                    customerBtn.Text = row.Cells["Name"].Value.ToString();
                }
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            if (dataGV.Tag.ToString() == "Booking")
            {
                customerBtn.Text = "Select Customer";
                titleBtn.Text = "Select Video";
                startPK.Value = DateTime.Now;
                endPK.Value = DateTime.Now;
                label6.Text = "Issue Video";
                bookSaveBtn.Text = "Issue";
                panel5.Visible = true;
            }
            else if (dataGV.Tag.ToString() == "Video")
            {
                titleTxt.Text = "";
                costTxt.Text = "";
                copiesTxt.Text = "";
                genreTxt.Text = "";
                languageTxt.Text = "";
                yearTxt.Value = DateTime.Now;
                panel11.Visible = true;
                videoTitle.Text = "Add Video";
                videoSave.Text = "Add";
            }
            else if (dataGV.Tag.ToString() == "Customer")
            {
                nameTxt.Text = "";
                cnctTxt.Text = "";
                addTxt.Text = "";
                jdateTxt.Value = DateTime.Now;
                panel9.Visible = true;
                label7.Text = "Add Customer";
                custSaveBtn.Text = "Add";
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (dataGV.Tag.ToString() == "Booking")
            {
                if (customerBtn.Text != "Select Customer" && titleBtn.Text != "Select Video" && id != -1)
                {
                    label6.Text = "Return Video";
                    bookSaveBtn.Text = "Return";
                    panel5.Visible = true;
                }
            }
            else if (dataGV.Tag.ToString() == "Video")
            {
                if (titleTxt.Text != "" && id != -1)
                {
                    panel11.Visible = true;
                    videoTitle.Text = "Update Video";
                    videoSave.Text = "Update";
                }
            }
            else if (dataGV.Tag.ToString() == "Customer")
            {
                if (nameTxt.Text != "" && id != -1)
                {
                    panel9.Visible = true;
                    label7.Text = "Update Customer";
                    custSaveBtn.Text = "Update";
                }
            }
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGV.Tag.ToString() == "Booking")
            {
                if (id != -1 && customerBtn.Text != "Select Customer" && titleBtn.Text != "Select Video")
                {
                    SqlMethod.DeleteData(id.ToString());
                    customerBtn.Text = "Select Customer";
                    titleBtn.Text = "Select Video";
                    startPK.Value = DateTime.Now;
                    endPK.Value = DateTime.Now;
                }
                bookBtn.PerformClick();
                id = -1;
            }
            else if (dataGV.Tag.ToString() == "Video")
            {
                if (id != -1 && titleTxt.Text != "" && costTxt.Text != "" && genreTxt.Text != "" && copiesTxt.Text != "" && languageTxt.Text != "")
                {
                    SqlMethod.DeleteData(titleTxt, genreTxt, costTxt, languageTxt, copiesTxt, yearTxt, id.ToString());
                }
                videoBtn.PerformClick();
                id = -1;
            }
            else if (dataGV.Tag.ToString() == "Customer")
            {
                if (nameTxt.Text != "" && id != -1)
                {
                    SqlMethod.DeleteData(nameTxt, cnctTxt, addTxt, jdateTxt, id.ToString());
                    custBtn.PerformClick();
                    id = -1;
                }
            }
        }
        private void videoSave_Click(object sender, EventArgs e)
        {
            if (titleTxt.Text != "" && genreTxt.Text != "" && languageTxt.Text != "" && costTxt.Text != "")
            {
                if (videoSave.Text == "Add")
                {
                    SqlMethod.AddData(titleTxt, genreTxt, costTxt, languageTxt, copiesTxt, yearTxt);
                    id = -1;
                }
                else
                {
                    if (id != -1)
                    {
                        SqlMethod.UpdateData(titleTxt, genreTxt, costTxt, languageTxt, copiesTxt, yearTxt, id.ToString());
                        id = -1;
                    }
                }
                videoBtn.PerformClick();
            }
        }
        private void bookSaveBtn_Click(object sender, EventArgs e)
        {
            if (customerBtn.Text != "Select Customer")
            {
                if (bookSaveBtn.Text == "Return")
                {
                    if (id != -1)
                    {
                        int a = cost * Convert.ToInt32((endPK.Value - startPK.Value).TotalDays);
                        if (a == 0)
                            a = cost;
                        SqlMethod.UpdateData(customerBtn, titleBtn, startPK, endPK, id.ToString(), a);
                        customerBtn.Text = "Select Customer";
                        titleBtn.Text = "Select Video";
                        startPK.Value = DateTime.Now;
                        endPK.Value = DateTime.Now;
                        id = -1;
                    }
                }
                else
                {
                    if (customerBtn.Text != "Select Customer")
                    {
                        SqlMethod.AddData(customerBtn, titleBtn, startPK, endPK);
                        customerBtn.Text = "";
                        titleBtn.Text = "";
                        startPK.Value = DateTime.Now;
                        endPK.Value = DateTime.Now;
                        id = -1;
                    }
                }
                bookBtn.PerformClick();
            }
        }
        private void customerBtn_Click(object sender, EventArgs e)
        {
            custBtn.PerformClick();
        }

        private void titleBtn_Click(object sender, EventArgs e)
        {
            videoBtn.PerformClick();
        }

        private void custSaveBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text != "" && cnctTxt.Text != "" && addTxt.Text != "")
            {
                if (custSaveBtn.Text == "Add")
                {
                    SqlMethod.AddData(nameTxt, cnctTxt, addTxt, jdateTxt);
                    id = -1;
                }
                else
                {
                    SqlMethod.UpdateData(nameTxt, cnctTxt, addTxt, jdateTxt, id.ToString());
                    id = -1;
                }
                custBtn.PerformClick();
            }
        }
        private void videoCloseBtn_Click(object sender, EventArgs e)
        {
            customerBtn.Text = "Select Customer";
            titleBtn.Text = "Select Video";
            startPK.Value = DateTime.Now;
            endPK.Value = DateTime.Now;
            panel5.Visible = false;
        }
        private void videoClose_Click(object sender, EventArgs e)
        {
            titleTxt.Text = "";
            costTxt.Text = "";
            copiesTxt.Text = "";
            genreTxt.Text = "";
            languageTxt.Text = "";
            yearTxt.Value = DateTime.Now;
            panel11.Visible = false;
        }
        private void custCloseBtn_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            nameTxt.Text = "";
            cnctTxt.Text = "";
            addTxt.Text = "";
            jdateTxt.Value = DateTime.Now;
        }
    }
}