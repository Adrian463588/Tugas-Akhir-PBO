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

namespace LIbrary
{

    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLOCALDB;Initial Catalog=Mylibrarydb1;Integrated Security=True;Pooling=False");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from LibrarianTbl where LibName = '" + UnameTb.Text + "' and LibPassword = '" + PasswordTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1")
            {
                this.Hide();
                MainForm main = new MainForm();
                main.Show();

            }
            else
            {
                MessageBox.Show("Wrong UserName Or Password");
            }
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
