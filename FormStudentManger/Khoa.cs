using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormStudentManger
{
    class Khoa
    {
        public int id_khoa { get; set; }
        public string Name { get; set; }

        public Khoa()
        {

        }

        public Khoa(int id, string name)
        {
            this.id_khoa = id;
            this.Name = name;
        }
    }
}
