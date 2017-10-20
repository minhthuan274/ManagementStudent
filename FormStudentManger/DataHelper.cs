using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FormStudentManger
{
    
    public class DataHelper 
    {
        private  string connectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Wolf_development;Integrated Security=True";
        private SqlConnection cnn;
        public DataHelper()
        {
            cnn = new SqlConnection();
            cnn.ConnectionString = connectionString;
        }

        public void DB_ExecuteNonQuery(string query)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public SqlDataReader DB_ExecuteReader(string query)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);

            SqlDataReader reader =  cmd.ExecuteReader(CommandBehavior.CloseConnection);;
            cnn.Close();
            return reader;
        }

        public DataTable DB_Table(string query)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            cnn.Close();
            return dataset.Tables[0];
        }
    }
}
