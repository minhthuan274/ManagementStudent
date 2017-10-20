using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormStudentManger
{
    class Student
    {
        public string MSSV { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string id_khoa { get; set; }

        public Student(string mssv, string name, string lop, DateTime birthDay, string gender, string id_khoa)
        {
            MSSV = mssv;
            Name = name;
            Class = lop;
            BirthDay = birthDay;
            Gender = gender;
            this.id_khoa = id_khoa;
        }
    }
}
