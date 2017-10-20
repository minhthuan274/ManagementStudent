using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            MessageBox.Show("Update successful");

            cnn.Close();
        }

        public void AddStudent(string mssv, string name, string lop, DateTime birthDay, string gender, string khoa)
        {
            var cnn = this.initConnection();
            var sBirthDay = birthDay.Date.ToString("yyyyMMdd");
            var id_khoa = khoaService.getIdKhoa(khoa).id_khoa;

            string text = "INSERT INTO student VALUES(" +
                $"N'{mssv}'," +
                $"N'{name}'," +
                $"N'{lop}'," +
                $"N'{birthDay}'," +
                $"N'{gender}', " +
                $"N'{id_khoa}')";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Add successful");
            cnn.Close();
        }

        public void DeleteStudent(string mssv)
        {
            var cnn = this.initConnection();
            string text = $"DELETE FROM Student WHERE MSSV='{mssv}'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted successful");

            cnn.Close();
        }

    }
}
