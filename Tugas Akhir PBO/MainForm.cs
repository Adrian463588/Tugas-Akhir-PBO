using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIbrary
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IssueBookForm Issue = new IssueBookForm();
            Issue.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReturnBookForm RetBook = new ReturnBookForm();
            RetBook.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm student = new StudentForm();
            student.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookTbl book = new BookTbl();
            book.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LibrarianForm librarian = new LibrarianForm();
            librarian.Show();
            this.Hide();
        }
    }
}
