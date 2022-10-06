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
    public partial class Customers : Form
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

        public Customers()
        {
            InitializeComponent();
            displayCust();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9M6ITMU\BUATLKS;Initial Catalog=tugasskj;Integrated Security=True");

        private void reset()
        {
            CNameTb.Text = "";
            CAddTb.Text = "";
            CCarTb.Text = "";
            CPhoneTb.Text = "";
            CStatusCb.SelectedIndex = -1;
        }

        private void displayCust()
        {
            Con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CAddTb.Text == " " || CStatusCb.SelectedIndex == -1 || CPhoneTb.Text == "" || CCarTb.Text == "")
            {
                MessageBox.Show("Mohon Masukkan Data Lebih Dahulu");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CName, CPhone, CAdd, CStatus, CCar) values (@Cn, @Cp, @Ca, @Cs, @Cc)", Con);
                    cmd.Parameters.AddWithValue("@Cn", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@Cp", CPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Ca", CAddTb.Text);
                    cmd.Parameters.AddWithValue("@Cs", CStatusCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Cc", CCarTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customers Saved ");
                    Con.Close();
                    displayCust();
                    reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int Key = 0;
        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CNameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            CPhoneTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            CAddTb.Text= CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            CStatusCb.SelectedItem = CustomersDGV.SelectedRows[0].Cells[4].Value.ToString();
            CCarTb.Text = CustomersDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Data Customers");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CId=@CuId", Con);
                cmd.Parameters.AddWithValue("@CuId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Delete");
                Con.Close();
                displayCust();
                reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CAddTb.Text == " " || CStatusCb.SelectedIndex == -1 || CPhoneTb.Text == "" || CCarTb.Text == "")
            {
                MessageBox.Show("Mohon Masukkan Data Dengan Lebih Dahulu");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update Customertbl set CName=@Cun, CPhone=@Cup, CAdd=@Cua, CStatus=@Cus, CCar=@Cuc where CId=@CuId", Con);
                cmd.Parameters.AddWithValue("@Cun", CNameTb.Text);
                cmd.Parameters.AddWithValue("@Cup", CPhoneTb.Text);
                cmd.Parameters.AddWithValue("@Cua", CAddTb.Text);
                cmd.Parameters.AddWithValue("@Cus", CStatusCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Cuc", CCarTb.Text);
                cmd.Parameters.AddWithValue("@CuId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Update Success");
                Con.Close();
                displayCust();
                reset();
            }
        }

        private void CNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Services obj = new Services();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
