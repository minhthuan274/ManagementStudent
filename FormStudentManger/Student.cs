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
        public Khoa Khoa { get; set; }
    }
}
