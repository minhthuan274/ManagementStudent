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
        private KhoaService khoaService = new KhoaService();
        private StudentsService studentService = new StudentsService();
        public Form1()
        {
            InitializeComponent();
            this.getKhoa();
            ColumnHeader h1 = new ColumnHeader();
            ColumnHeader h2 = new ColumnHeader();
            h1.Text = "Name SV";
            h2.Text = "Name Khoa";
           
        }

        string connectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Wolf_development;Integrated Security=True";

        SqlConnection initConnection()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = this.connectionString;
            cnn.Open();
            return cnn;
        }

        private void getKhoa()
        {
            var cnn = this.initConnection();
            string text = "select * from Khoa";
            var cmd = new SqlCommand(text, cnn);
            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                cbKhoa.Items.Add(r["Name"].ToString());
            }

            r.Close();
            cnn.Close();


        }

        // Get all students
        private void button1_Click(object sender, EventArgs e)
        {
            ShowDataGridView();
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
            //ListViewItem item = listSinhVien.SelectedItems[0];
            //var mssv = item.Text;
            //var cnn = this.initConnection();
            //string text = $"DELETE FROM Student WHERE MSSV='{mssv}'";
            //SqlCommand cmd = new SqlCommand(text, cnn);
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Deleted successful");
            
            //cnn.Close();
            //this.getAllStudents();
        }

        // 
        private void button5_Click(object sender, EventArgs e)
        {
            var cnn = this.initConnection();
            //listSinhVien.Items.Clear();
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
            var birthDay = birthDatePicker.Value;
            var gender = cbGender.SelectedItem;
            var khoa = cbKhoa.SelectedItem;

            this.studentService.UpdateStudent(MSSV, name, lop, birthDay, gender.ToString(), khoa.ToString());
            
            this.getAllStudents();
        }

        private void listSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var cnn = this.initConnection();
            //if (listSinhVien.SelectedItems.Count > 0)
            //{
            //    ListViewItem item = listSinhVien.SelectedItems[0];
            //    var mssv = item.Text;
            //    var NameKhoa = item.SubItems[1].Text;
            //    MessageBox.Show(mssv + NameKhoa);
            //    string text = $"select * from Student where MSSV='{mssv}'";
            //    SqlCommand cmd = new SqlCommand(text, cnn);
            //    SqlDataReader r = cmd.ExecuteReader();

            //    while (r.Read())
            //    {
            //        txtMSSV.Text = r["MSSV"].ToString();
            //        txtName.Text = r["Name"].ToString();
            //        txtClass.Text = r["Class"].ToString();
            //        birthDatePicker.Text = r["BirthDay"].ToString();
            //        if ((r["Gender"].ToString()) == "Male")
            //            cbGender.Text = cbGender.Items[0].ToString();
            //        else
            //            cbGender.Text = cbGender.Items[1].ToString();
            //    }

            //    r.Close();
            //}
            //cnn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //listSinhVien.Items.Clear();
        }

        private void getAllStudents()
        {
            //listSinhVien.Items.Clear();
            //var cnn = this.initConnection();
            ////var text = "SELECT MSSV, Name as NameSv, name as NameKhoa " +
            ////    "FROM SinhVien join inner Khoa" +
            ////    "on SinhVien.id_khoa=Khoa.id_khoa";

            //var text2 = "select Students.Name as NameSv, Khoas.name as NameKhoa " +
            //            "from Students" +
            //            "inner join Khoas on Students.id_khoa = Khoas.id_khoa";
            //SqlCommand cmd = new SqlCommand(text2, cnn);
            //SqlDataReader r = cmd.ExecuteReader();
            //while (r.Read())
            //{
            //    string[] row = { r["NameSv"].ToString(), r["NameKhoa"].ToString() };
            //    ListViewItem item = new ListViewItem(row);
            //    listSinhVien.Items.Add(item);
            //}
            //cnn.Close();
        }

        private void ShowDataGridView()
        {
            var cnn = this.initConnection();
            var text2 = "select MSSV, Student.Name as NameSv, Khoa.name as NameKhoa " +
                       "from Student " +
                       "inner join Khoa on Student.id_khoa = Khoa.id_khoa";
            SqlCommand cmd = new SqlCommand(text2, cnn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "SV");

            dataGridSinhVien.DataSource = dataset.Tables["SV"];
            cnn.Close();

        }

        private void dataGridSinhVien_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridSinhVien.SelectedCells.Count > 0)
            {
                var cnn = this.initConnection();
                int selectedRowIndex = dataGridSinhVien.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridSinhVien.Rows[selectedRowIndex];
                string mssv = Convert.ToString(selectedRow.Cells["MSSV"].Value);
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

                    var khoaSelected = khoaService.getKhoa(Convert.ToInt32(r["id_khoa"].ToString()));
                    cbKhoa.Text = khoaSelected.Name;
                }

                r.Close();
            }
        }
    }
}
