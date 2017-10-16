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


namespace FormStudentManger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";

        SqlConnection initConnection()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = this.connectionString;
            cnn.Open();
            return cnn;
        }

        // Get all students
        private void button1_Click(object sender, EventArgs e)
        {
            this.getAllStudents();
            
        }

        // Add new student
        private void button2_Click(object sender, EventArgs e)
        {
            var cnn = this.initConnection();
            var MSSV = txtMSSV.Text;
            var name = txtName.Text;
            var lop = txtClass.Text;
            var birthDay = birthDatePicker.Value.Date.ToString("yyyyMMdd");
            var gender = cbGender.SelectedItem;
            string text = "INSERT INTO student VALUES(" +
                $"N'{MSSV}'," +
                $"N'{name}'," +
                $"N'{lop}'," +
                $"N'{birthDay}'," +
                $"N'{gender}')";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Add successful");
            cnn.Close();
            this.getAllStudents();

        }


        // Delete Student
        private void button4_Click(object sender, EventArgs e)
        {
            ListViewItem item = listSinhVien.SelectedItems[0];
            var mssv = item.Text;
            var cnn = this.initConnection();
            string text = $"DELETE FROM Student WHERE MSSV='{mssv}'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted successful");
            
            cnn.Close();
            this.getAllStudents();
        }

        // 
        private void button5_Click(object sender, EventArgs e)
        {
            var cnn = this.initConnection();
            listSinhVien.Items.Clear();
            string text = $"SELECT * FROM Student where Name='{txtSearch.Text}'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteScalar();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                string[] row = { r["MSSV"].ToString(), r["Name"].ToString(), r["Class"].ToString(),
                                 DateTime.Parse(r["BirthDay"].ToString()).ToString("dd/MM/yyyy"),
                                 r["Gender"].ToString() };
                ListViewItem item = new ListViewItem(row);
                listSinhVien.Items.Add(item);
            }

            r.Close();
            cnn.Close();
            
        }

        // Update student's info button click 
        private void button3_Click(object sender, EventArgs e)
        {
            var cnn = this.initConnection();
            var MSSV = txtMSSV.Text;
            var name = txtName.Text;
            var lop = txtClass.Text;
            var birthDay = birthDatePicker.Value.Date.ToString("yyyyMMdd");
            var gender = cbGender.SelectedItem;

            string text = $"UPDATE Student SET Name='{name}', Class='{lop}', BirthDay='{birthDay}', Gender='{gender}' " +
                          $"WHERE MSSV='{MSSV}'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();

            cnn.Close();
            this.getAllStudents();
        }

        private void listSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cnn = this.initConnection();
            if (listSinhVien.SelectedItems[0] != null)
            {
                ListViewItem item = listSinhVien.SelectedItems[0];
                var mssv = item.Text;

                string text = $"select * from Student where MSSV='{mssv}'";
                SqlCommand cmd = new SqlCommand(text, cnn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    txtMSSV.Text = r["MSSV"].ToString();
                    txtName.Text = r["Name"].ToString();
                    txtClass.Text = r["Class"].ToString();
                    birthDatePicker.Text = r["BirthDay"].ToString();
                    if ((r["Gender"].ToString()) == "Male")
                        cbGender.Text = cbGender.Items[0].ToString();
                    else
                        cbGender.Text = cbGender.Items[1].ToString();
                }

                r.Close();
            }
            cnn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listSinhVien.Items.Clear();
        }

        private void getAllStudents()
        {
            listSinhVien.Items.Clear();
            var cnn = this.initConnection();
            var text = "SELECT * FROM Student";
            SqlCommand cmd = new SqlCommand(text, cnn);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                string[] row = { r["MSSV"].ToString(), r["Name"].ToString(), r["Class"].ToString(),
                                 DateTime.Parse(r["BirthDay"].ToString()).ToString("dd/MM/yyyy"),
                                 r["Gender"].ToString() };
                ListViewItem item = new ListViewItem(row);
                listSinhVien.Items.Add(item);
            }
            cnn.Close();
        }
    }
}
