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
        public List<Student> Students { get; set; }

        private KhoaService khoaService = new KhoaService();
        private static string ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Wolf_development;Integrated Security=True";
        private DataHelper dataHelper = new DataHelper();

        public void UpdateStudent(string mssv, string name, string lop, DateTime birthDay, string gender, string khoa)
        {
            var sBirthDay = birthDay.Date.ToString("yyyyMMdd");
            var id_khoa = khoaService.getIdKhoa(khoa).id_khoa;
            string text = $"UPDATE Student SET Name='{name}', Class='{lop}', BirthDay='{sBirthDay}', Gender='{gender}', id_khoa='{id_khoa}'" +
                          $"WHERE MSSV='{mssv}'";

            dataHelper.DB_ExecuteNonQuery(text);

            MessageBox.Show("Update successful");
        }

        public void AddStudent(string mssv, string name, string lop, DateTime birthDay, string gender, string khoa)
        {
            var sBirthDay = birthDay.Date.ToString("yyyyMMdd");
            var id_khoa = khoaService.getIdKhoa(khoa).id_khoa;

            string text = "INSERT INTO student VALUES(" +
                $"N'{mssv}'," +
                $"N'{name}'," +
                $"N'{lop}'," +
                $"N'{birthDay}'," +
                $"N'{gender}', " +
                $"N'{id_khoa}')";

            dataHelper.DB_ExecuteNonQuery(text);
            MessageBox.Show("Add successful");
        }

        public void DeleteStudent(string mssv)
        {
            string text = $"DELETE FROM Student WHERE MSSV='{mssv}'";
            dataHelper.DB_ExecuteNonQuery(text);
            MessageBox.Show("Deleted successful");
        }

    }
}
