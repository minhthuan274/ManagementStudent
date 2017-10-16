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

        private void button1_Click(object sender, EventArgs e)
        {        
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";
            
            var text = "SELECT * FROM Student";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cnn.Open();
            SqlDataReader r = cmd.ExecuteReader();
            listSinhVien.Clear();
            while (r.Read())
            {
                ListViewItem item = new ListViewItem(new String[] { r["MSSV"].ToString(), r["Name"].ToString(), r["Class"].ToString() }, -1);
                listSinhVien.Items.Add(item);
                listBox1.Items.Add(r["MSSV"].ToString());
            }
            cnn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";
            cnn.Open();
            string text = "INSERT INTO student VALUES(" +
                "N'107'," +
                "N'Thuan'," +
                "N'15T2'," +
                "N'19970404'," +
                "1)";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";
            cnn.Open();
            string text = "DELETE FROM Student WHERE MSSV='106'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            MessageBox.Show(cmd.ExecuteNonQuery().ToString());
            
            cnn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";
            cnn.Open();
            string text = "SELECT * FROM Student where Name='Thuan'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteScalar();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                ListViewItem item = new ListViewItem(new String[] { r["MSSV"].ToString(), r["Name"].ToString(), r["Class"].ToString() }, -1);
                listSinhVien.Items.Add(item);
            }
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";
            cnn.Open();
            string text = "UPDATE Student SET Name='thuan' WHERE Name='cho'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            cmd.ExecuteNonQuery();

            cnn.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var item = listBox1.Items[index].ToString();
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-BINHTHU;Initial Catalog=Khan_development;Integrated Security=True";
            cnn.Open();

            string text = "select * from Student where MSSV='" + item + "'";
            SqlCommand cmd = new SqlCommand(text, cnn);
            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                txtMSSV.Text = r["MSSV"].ToString();
                txtName.Text = r["Name"].ToString();
                txtClass.Text = r["Class"].ToString();
                birthDatePicker.Text = r["BirthDay"].ToString();
                if ((r["Gender"].ToString()) == true.ToString())
                    cbGender.Text = cbGender.Items[0].ToString(); 
                else
                    cbGender.Text = cbGender.Items[1].ToString();
            }

            r.Close();


            //string text2 = "select Class from Student GROUP By Class";
            string text2 = "select distinct Class from Student";
            SqlCommand cmd2 = new SqlCommand(text2, cnn);
            SqlDataReader r2 = cmd2.ExecuteReader();

            while (r2.Read())
            {
                cbClass.Items.Add(r2["Class"].ToString());
            }

            cnn.Close();
        }
    }
}
