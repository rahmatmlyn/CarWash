using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace PKK_Bu_Eva
{
    public partial class Wash : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
   (
       int nLeftRect,     // x-coordinate of upper-left corner
       int nTopRect,      // y-coordinate of upper-left corner
       int nRightRect,    // x-coordinate of lower-right corner
       int nBottomRect,   // y-coordinate of lower-right corner
       int nWidthEllipse, // height of ellipse
       int nHeightEllipse // width of ellipse
   );
        public Wash()
        {
            InitializeComponent();
            FillCust();
            FillServices();
            ENameLbl.Text = Login.Username;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9M6ITMU\BUATLKS;Initial Catalog=tugasskj;Integrated Security=True");
        private void FillCust()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand ("select CName from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CName", typeof(String));
            dt.Load(rdr);
            CustNameCb.ValueMember = "CName";
            CustNameCb.DataSource = dt;
            Con.Close();
        }
        private void FillServices()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select SName from ServiceTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(String));
            dt.Load(rdr);
            ServiceCb.ValueMember = "SName";
            ServiceCb.DataSource = dt;
            Con.Close();
        }

        private void GetCustData()
        {
            Con.Open();
            string query = "select * from CustomerTbl where CName='"+CustNameCb.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustPhoneTb.Text = dr["CPhone"].ToString();
            }
            Con.Close();
        }

        private void GetServiceData()
        {
            Con.Open();
            string query = "select * from ServiceTbl where SName='" + ServiceCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PriceTb.Text = dr["SPrice"].ToString();
            }
            Con.Close();
        }
        int n = 0, Grdtotal = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PriceTb.Text == "")
            {
                MessageBox.Show("Masukkan Data Dahulu");
            }
            else
            {

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ServiceDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ServiceCb.SelectedValue.ToString();
                newRow.Cells[2].Value = PriceTb.Text;
                ServiceDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + Convert.ToInt32(PriceTb.Text);
                MessageBox.Show("Data Berhasil Ditambah");
                TotalLbl.Text = "Rp" + Grdtotal;
                
            }
        }

        private void CustNameCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustData();
        }

        private void ServiceCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetServiceData();
        }

        private void TotalLbl_Click(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            CustNameCb.SelectedIndex = -1;
            CustPhoneTb.Text = "";
            ServiceCb.SelectedIndex = -1;
            PriceTb.Text = "";
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (CustPhoneTb.Text == "" )
            {
                MessageBox.Show("Mohon Masukkan Data Lebih Dahulu");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into InvoiceTbl (CustName, CustPhone, EName, Amt, IDate) values (@Cn, @Cp, @En, @Am, @Id)", Con);
                    cmd.Parameters.AddWithValue("@Cn", CustNameCb.Text);
                    cmd.Parameters.AddWithValue("@Cp", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@En", ENameLbl.Text);
                    cmd.Parameters.AddWithValue("@Am", Grdtotal);
                    cmd.Parameters.AddWithValue("@Id", TodayDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Invoice Saved / Data Saved");
                    Con.Close();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void CustNameCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
