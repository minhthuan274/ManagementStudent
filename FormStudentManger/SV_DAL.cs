using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FormStudentManger
{
    class SV_DAL
    {
        public List<Student> Students { get; set; }
        public List<Khoa> Khoas { get; set; }
        public List<RenderSV> Renders { get; set; }
        public DataHelper DataHelper { get; set; }
        public SV_DAL()
        {
            this.DataHelper = new DataHelper();
        }

        public List<Student> GetListStudents()
        {
            List<Student> list = new List<Student>();

            string query = "select * from Student";
            var table = this.DataHelper.DB_Table(query);
            foreach (DataRow row in table.Rows)
            {
                var mssv = row[0].ToString();
                var name = row[1].ToString();
                var lop = row[2].ToString();
                var birthDay = DateTime.Parse(row[3].ToString());
                var gender = row[4].ToString();
                var id_khoa = row[5].ToString();

                var student = new Student(mssv, name, lop, birthDay, gender, id_khoa);

                list.Add(student);
            }
            this.Students = list;
            return list;
        }

        public List<Khoa> getKhoas()
        {
            this.Khoas = new List<Khoa> { };

            string query = "select * from Khoa";
            var table = this.DataHelper.DB_Table(query);
            foreach (DataRow row in table.Rows)
            {
                var id_khoa = Convert.ToInt32(row[0].ToString());
                var name = row[1].ToString();

                var khoa = new Khoa(id_khoa, name);
                this.Khoas.Add(khoa);
            }

            return this.Khoas;
        }

        public List<RenderSV> GetRender()
        {
            this.Renders = new List<RenderSV> { };
            var text2 = "select MSSV, Student.Name as NameSv, Khoa.name as NameKhoa " +
                       "from Student " +
                       "inner join Khoa on Student.id_khoa = Khoa.id_khoa";

            var table = this.DataHelper.DB_Table(text2);

            foreach (DataRow row in table.Rows)
            {
                var render = new RenderSV();
                render.mssv = row[0].ToString();
                render.Name = row[1].ToString();
                render.NameKhoa = row[2].ToString();

                this.Renders.Add(render);
            }

            return this.Renders;
        }
    }
}
