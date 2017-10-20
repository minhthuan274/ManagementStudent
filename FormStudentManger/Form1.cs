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

        private SV_DAL svDal = new SV_DAL();

        public DataHelper DataHelper { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.getKhoa();
            this.DataHelper = new DataHelper();
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
            var khoas = this.svDal.getKhoas();
            foreach (var khoa in khoas)
            {
                cbKhoa.Items.Add(khoa.Name);
            }
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
            var birthDay = birthDatePicker.Value.Date;
            var khoa = cbKhoa.SelectedItem.ToString();
            var gender = cbGender.SelectedItem.ToString();

            studentService.AddStudent(MSSV, name, lop, birthDay, gender, khoa);
            this.ShowDataGridView();
        }


        // Delete Student
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridSinhVien.SelectedCells.Count > 0)
            {
                var cnn = this.initConnection();
                int selectedRowIndex = dataGridSinhVien.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridSinhVien.Rows[selectedRowIndex];
                string mssv = Convert.ToString(selectedRow.Cells["MSSV"].Value);
                studentService.DeleteStudent(mssv);
            }
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
            dataGridSinhVien.DataSource = this.svDal.GetRender() ;
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
