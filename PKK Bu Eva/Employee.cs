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
    public partial class Employee : Form
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
        public Employee()
        {
            InitializeComponent();
            displayEmp();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9M6ITMU\BUATLKS;Initial Catalog=tugasskj;Integrated Security=True");

        private void reset ()
        {
            ENameTb.Text = "";
            EAddTb.Text = "";
            EPhoneTb.Text = "";
            EGenCb.SelectedIndex = -1;
        }

        private void displayEmp()
        {
            Con.Open();
            string Query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter (Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder (sda);
            var ds = new DataSet ();
            sda.Fill (ds);
            EmployeeDGV.DataSource = ds.Tables [0];
            Con.Close();
        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EAddTb.Text ==" " || EGenCb.SelectedIndex == -1 || EPhoneTb.Text == "")
            {
                MessageBox.Show("Mohon Masukkan Data Lebih Dahulu");
            }
            else
            {
                try
                {
                    
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EName, EPhone, EGen, EAdd, EPass) values (@En, @Ep, @Eg, @Ea, @Epa)", Con);
                    cmd.Parameters.AddWithValue("@En", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@Ep", EPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Eg", EGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ea", EAddTb.Text);
                    cmd.Parameters.AddWithValue("@Epa", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved / Data Saved");
                    Con.Close();
                    displayEmp();
                    reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ENameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ENameTb.Text == "")
            {
                Key = 0;
            }else
            {
                Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(Key == 0)
            {
                MessageBox.Show("Select Data Employee");
            }else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EId=@EmId",Con);
                cmd.Parameters.AddWithValue("@EmId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("employee Delete");
                Con.Close();
                displayEmp();
                reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EAddTb.Text == " " || EGenCb.SelectedIndex == -1 || EPhoneTb.Text == "")
            {
                MessageBox.Show("Mohon Masukkan Data Dengan Lebih Dahulu");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update EmployeeTbl set EName=@En, EPhone=@Ep, EGen=@Eg, EAdd=@Ea where EId=@EmId", Con);
                cmd.Parameters.AddWithValue("@En", ENameTb.Text);
                cmd.Parameters.AddWithValue("@Ep", EPhoneTb.Text);
                cmd.Parameters.AddWithValue("@Eg", EGenCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Ea", EAddTb.Text);
                cmd.Parameters.AddWithValue("@Emid", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Update Success");
                Con.Close();
                displayEmp();
                reset();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            Services obj = new Services();
            obj.Show();
            this.Hide();
        }
    }
}
