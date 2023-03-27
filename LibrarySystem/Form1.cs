using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LibrarySystem
{
    public partial class BookForm : Form
    {
        public static string dbInfo()
        {
            string _server = "127.0.0.1";
            int _port = 3306;
            string _database = "world";
            string _id = "root";
            string _pw = "0817";
            string _conncectionAddress = string.Format("Server={0}; Port={1}; Database={2}; Uid={3}; Pwd={4}",
                _server, _port, _database, _id, _pw);

            return _conncectionAddress;
        }

        public void init()
        {
            Text = "도서 관리";
            txtIdx.ReadOnly = true;
        }

        public BookForm()
        {
            InitializeComponent();
            init();

            string _connectionAddress = dbInfo();

            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(_connectionAddress))
                {
                    dbConn.Open();

                    string selectBookQuery = string.Format("SELECT *FROM books;");
                    MySqlCommand command1 = new MySqlCommand(selectBookQuery, dbConn);
                    MySqlDataAdapter adapter1 = new MySqlDataAdapter(command1);
                    DataTable datatable1 = new DataTable();
                    adapter1.Fill(datatable1);
                    BookDataGridView.DataSource = datatable1;

                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddBooks_Click(object sender, EventArgs e)
        {
            string _connectionAddress = dbInfo();
            if (txtIsbn.Text == "" || txtBookName.Text == "" || txtPublisher.Text == "" || txtPages.Text == "")
            {
                MessageBox.Show("도서 정보를 입력해 주세요!");
            }
            else
            {
                try
                {
                    using (MySqlConnection dbConn = new MySqlConnection(_connectionAddress))
                    {
                        dbConn.Open();

                        string insertBookQuery = string.Format("INSERT INTO books (Isbn, BookName, Publisher, Page)" +
                            "VALUES ('{0}','{1}','{2}','{3}');",
                            txtIsbn.Text, txtBookName.Text, txtPublisher.Text, txtPages.Text);
                        MySqlCommand command2 = new MySqlCommand(insertBookQuery, dbConn);

                        if (command2.ExecuteNonQuery() != 1)
                            MessageBox.Show("도서 정보 입력 실패!");
                        else
                            MessageBox.Show("ISBN: " + txtIsbn.Text + ", 도서 정보 입력 성공!");

                        string selectBookQuery = string.Format("SELECT * FROM books;");
                        MySqlCommand command1 = new MySqlCommand(selectBookQuery, dbConn);

                        MySqlDataAdapter adapter1 = new MySqlDataAdapter(command1);
                        DataTable datatable1 = new DataTable();
                        adapter1.Fill(datatable1);
                        BookDataGridView.DataSource = datatable1;

                        dbConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                txtIdx.Text = "";
                txtIsbn.Text = "";
                txtBookName.Text = "";
                txtPublisher.Text = "";
                txtPages.Text = "";
            }
        }

        private void BookDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdx.Text = BookDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtIsbn.Text = BookDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtBookName.Text = BookDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPublisher.Text = BookDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPages.Text = BookDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string _connectionAddress = dbInfo();
            int idx = 0;
            string Isbn = "";
            if (txtIsbn.Text == "" || txtBookName.Text == "" || txtPublisher.Text == "" || txtPages.Text == "")
            {
                MessageBox.Show("도서 정보를 입력해 주세요!");
            }
            else
            {
                idx = int.Parse(txtIdx.Text.Trim());
                Isbn = txtIsbn.Text.Trim();
            }
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(_connectionAddress))
                {
                    dbConn.Open();

                    string deleteBookQuery = string.Format("DELETE FROM books WHERE Idx='{0}' AND Isbn='{1}';", idx, Isbn);
                    MySqlCommand command3 = new MySqlCommand(deleteBookQuery, dbConn);

                    if (command3.ExecuteNonQuery() != 1)
                        MessageBox.Show("해당 도서 품목이 삭제가 안됨!");
                    else
                        MessageBox.Show("도서 품목이 삭제됨!");

                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            txtIdx.Text = "";
            txtIsbn.Text = "";
            txtBookName.Text = "";
            txtPublisher.Text = "";
            txtPages.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string _connectionAddress = dbInfo();

            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(_connectionAddress))
                {
                    dbConn.Open();

                    string selectBookQuery = string.Format("SELECT * FROM books;");

                    MySqlCommand command4 = new MySqlCommand(selectBookQuery, dbConn);
                    MySqlDataAdapter adapter4 = new MySqlDataAdapter(command4);
                    DataTable datatable4 = new DataTable();
                    adapter4.Fill(datatable4);
                    BookDataGridView.DataSource = datatable4;

                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            txtIdx.Text = "";
            txtIsbn.Text = "";
            txtBookName.Text = "";
            txtPublisher.Text = "";
            txtPages.Text = "";
        }

        private void btnModiBooks_Click(object sender, EventArgs e)
        {
            string _connectionAddress = dbInfo();

            int idx = 0;
            string Isbn = "", BookName = "", Publisher = "", Page = "";

            if (txtIsbn.Text == "" || txtBookName.Text == "" || txtPublisher.Text == "" || txtPages.Text == "")
            {
                MessageBox.Show("도서 정보를 입력해 주세요!");
            }
            else
            {
                idx = int.Parse(txtIdx.Text.Trim());
                Isbn = txtIsbn.Text.Trim();
                BookName = txtBookName.Text.Trim();
                Publisher = txtPublisher.Text.Trim();
                Page = txtPages.Text.Trim();
            }
            try
            {
                using (MySqlConnection dbConn = new MySqlConnection(_connectionAddress))
                {
                    string updateBooksQuery = string.Format("UPDATE books SET Isbn='{0}', BookName='{1}', Publisher='{2}', Page='{3}' WHERE idx={4};",
                        Isbn, BookName, Publisher, Page, idx);

                    dbConn.Open();

                    MySqlCommand command5 = new MySqlCommand(updateBooksQuery, dbConn);

                    if (command5.ExecuteNonQuery() != 1)
                        MessageBox.Show("도서 정보 수정 실패!");
                    else
                        MessageBox.Show("도서 정보 수정 완료!");

                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            txtIdx.Text = "";
            txtIsbn.Text = "";
            txtBookName.Text = "";
            txtPublisher.Text = "";
            txtPages.Text = "";
        }
    }
}
