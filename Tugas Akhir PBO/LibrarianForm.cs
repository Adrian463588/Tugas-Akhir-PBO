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
    public partial class LibrarianForm : Form
    {

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLOCALDB;Initial Catalog=Mylibrarydb1;Integrated Security=True;Pooling=False");
        public LibrarianForm()
        {
            InitializeComponent();
        }


    

        private void LibrarianForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        public void populate()
        {
            Con.Open();
            string query = "select * from LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrarianDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LibId.Text == "" || LibName.Text == "" || Libpass.Text == "" || Libphone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into LibrarianTbl values(" + LibId.Text + ",'" + LibName.Text + "','" + Libpass.Text + "','" + Libphone.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Added Successfully");
                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(LibId.Text == "")
            {
                MessageBox.Show("Enter The Librarian Id");
            }
            else
            {
               
                Con.Open();
                string query = "delete from librarianTbl where LibId = " + LibId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfullly Deleted");
                Con.Close();
                populate();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(LibId.Text == "" || LibName.Text == "" || Libpass.Text == "" || Libphone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update LibrarianTbl set LibName= '" + LibName.Text + "', LibPassword= '" + Libpass.Text + "', LibPhone = '" + Libphone.Text + "'where LibId=" + LibId.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LibId.Text = LibrarianDGV.SelectedRows[0].Cells[0].Value.ToString();
            LibName.Text = LibrarianDGV.SelectedRows[0].Cells[1].Value.ToString();
            Libpass.Text = LibrarianDGV.SelectedRows[0].Cells[2].Value.ToString();
            Libphone.Text = LibrarianDGV.SelectedRows[0].Cells[3].Value.ToString();

        }
    }
}
