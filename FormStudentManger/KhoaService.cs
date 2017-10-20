using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FormStudentManger
{
    class KhoaService
    {
        private static string ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Wolf_development;Integrated Security=True";

        private SqlConnection initConnection()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = KhoaService.ConnectionString;
            cnn.Open();
            return cnn;
        }

        public Khoa getKhoa(int id)
        {
            var cnn = this.initConnection();
            string text = $"select * from Khoa where id_khoa={id}";
            var cmd = new SqlCommand(text, cnn);
            SqlDataReader r =  cmd.ExecuteReader();
            Khoa khoa = new Khoa();
            while (r.Read())
            {
                khoa = new Khoa(Convert.ToInt32(r["id_khoa"].ToString()), r["name"].ToString());
            }
            r.Close();
            cnn.Close();

            return khoa;
        }

        public Khoa getIdKhoa(string name)
        {
            var cnn = this.initConnection();
            string text = $"select * from Khoa where name='{name}'";
            var cmd = new SqlCommand(text, cnn);
            SqlDataReader r = cmd.ExecuteReader();
            Khoa khoa = new Khoa();
            while (r.Read())
            {
                khoa = new Khoa(Convert.ToInt32(r["id_khoa"].ToString()), r["name"].ToString());
            }
            r.Close();
            cnn.Close();

            return khoa;
        }
         
    }
}
