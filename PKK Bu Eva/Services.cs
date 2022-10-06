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
    public partial class Services : Form
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
        public Services()
        {
            InitializeComponent();
            displayServ();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9M6ITMU\BUATLKS;Initial Catalog=tugasskj;Integrated Security=True");

        private void reset()
        {
            SNameTb.Text = "";
            PriceTb.Text = "";
        }

        private void displayServ()
        {
            Con.Open();
            string Query = "select * from ServiceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ServiceDGV.DataSource = ds.Tables[0];
            Con.Close();
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Mohon Masukkan Data  Lebih Dahulu");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ServiceTbl (SName, SPrice) values (@SN, @SP)", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SP", PriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Service Saved / Data Saved");
                    Con.Close();
                    displayServ();
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
        private void ServiceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SNameTb.Text = ServiceDGV.SelectedRows[0].Cells[1].Value.ToString();
            PriceTb.Text = ServiceDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (SNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ServiceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Data Service");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from ServiceTbl where SId=@SeId", Con);
                cmd.Parameters.AddWithValue("@SeId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Service Delete");
                Con.Close();
                displayServ();
                reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Mohon Masukkan Data Dengan Lebih Dahulu");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ServiceTbl set SName=@Sen, SPrice=@Sep where SId=@SeId", Con);
                cmd.Parameters.AddWithValue("@Sen", SNameTb.Text);
                cmd.Parameters.AddWithValue("@Sep", PriceTb.Text);
                cmd.Parameters.AddWithValue("@SeId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Service Update Success");
                Con.Close();
                displayServ();
                reset();
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

        private void label2_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
       
        }

        private void Services_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugasskjDataSet.ServiceTbl' table. You can move, or remove it, as needed.
            this.serviceTblTableAdapter.Fill(this.tugasskjDataSet.ServiceTbl);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Employee obj = new Employee();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }
    }
}
