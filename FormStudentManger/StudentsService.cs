using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FormStudentManger
{
    class StudentsService
    {
        private KhoaService khoaService = new KhoaService();
        private static string ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Wolf_development;Integrated Security=True";

        private SqlConnection initConnection()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = StudentsService.ConnectionString;
            cnn.Open();
            return cnn;
        }
        public void UpdateStudent(string mssv, string name, string lop, DateTime birthDay, string gender, string khoa)
        {
            var cnn = this.initConnection();
            var sBirthDay = birthDay.Date.ToString("yyyyMMdd");
            var id_khoa = khoaService.getIdKhoa(khoa).id_khoa;
            string text = $"UPDATE Student SET Name='{name}', Class='{lop}', BirthDay='{sBirthDay}', Gender='{gender}', id_khoa='{id_khoa}'" +
                          $"WHERE MSSV='{mssv}'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();

            cnn.Close();
        }

        //public Student[] getStudents()
        //{
        //    var cnn = this.initConnection();

        //    var text2 = "select MSSV, Student.Name as NameSv, Khoa.name as NameKhoa " +
        //               "from Student " +
        //               "inner join Khoa on Student.id_khoa = Khoa.id_khoa";
        //    SqlCommand cmd = new SqlCommand(text2, cnn);
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = cmd;

        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset);

        //    dataGridSinhVien.DataSource = dataset.Tables[0];
        //    cnn.Close();
        //}
    }
}
