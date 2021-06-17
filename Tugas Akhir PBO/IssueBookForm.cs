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
    public partial class IssueBookForm : Form
    {

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLOCALDB;Initial Catalog=Mylibrarydb1;Integrated Security=True;Pooling=False");
        public IssueBookForm()
        {
            InitializeComponent();
        }
        private void FillStudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StdId from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(int));
            dt.Load(rdr);
            StdCb.ValueMember = "StdId";
            StdCb.DataSource = dt;
            Con.Close();
        }

        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>" + 0 + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BooKName", typeof(string));
            dt.Load(rdr);
            Bookcb.ValueMember = "BookName";
            Bookcb.DataSource = dt;
            Con.Close();
        }

        private void fetchstddata()
        {
            Con.Open();
            string query = "select * from StudentTbl where StdId=" + StdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                stdnameTb.Text = dr["StdName"].ToString();
                stddpmntTb.Text = dr["StdDep"].ToString();
                PhoneTb.Text = dr["StdPhone"].ToString();
            }
            Con.Close();
        }

        private void UpdateBook()
        {
            int Qty,newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + Bookcb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty - 1;
                string query1 = "update BookTbl set Qty=" + newQty + " where BookName='" + Bookcb.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }

            Con.Close();

        }

        private void UpdateBookCancellation()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + Bookcb.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + " where BookName='" + Bookcb.SelectedItem.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }

            Con.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        


        private void IssueBookForm_Load(object sender, EventArgs e)
        {
            FillStudent();
            FillBook();
            populate();
        }

        private void StdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void StdCb_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void StdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IssueNumTb.Text == "" || stdnameTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                string issuedate = IssueDate.Value.Day.ToString() + "/" + IssueDate.Value.Month.ToString() + "/" + IssueDate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into IssueTbl values(" + IssueNumTb.Text + "," + StdCb.SelectedValue.ToString() + ",'" + stdnameTb.Text + "','" + stddpmntTb.Text + "','"+PhoneTb.Text+"','"+Bookcb.SelectedValue.ToString()+"','"+issuedate+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Issued");
                Con.Close();
                UpdateBook();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IssueNumTb.Text == "")
            {
                MessageBox.Show("Enter IssueNumber");
            }
            else
            {

                Con.Open();
                string query = "delete from IssueTbl where IssueNum = " + IssueNumTb.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Issue Successfully Deleted");
                
                Con.Close();
               // UpdateBookCancellation();
                populate();
            }
        }

        private void IssueNumTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void IssueBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IssueNumTb.Text = IssueBookDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdCb.SelectedItem = IssueBookDGV.SelectedRows[0].Cells[1].Value.ToString();
            stdnameTb.Text = IssueBookDGV.SelectedRows[0].Cells[2].Value.ToString();
            stddpmntTb.Text = IssueBookDGV.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = IssueBookDGV.SelectedRows[0].Cells[4].Value.ToString();
            Bookcb.Text = IssueBookDGV.SelectedRows[0].Cells[5].Value.ToString();
        }
    }
}
