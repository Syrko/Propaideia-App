using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    class Student : User
    {
        private int level;
        private StudentProgress studentProgress;

        internal Student(string username, string name, string surname, int level, StudentProgress studentProgress) : base(username, name, surname)
        {
            this.level = level;
            this.studentProgress = studentProgress;
        }

        internal int Level { get => level; set => level = value; }
        internal StudentProgress StudentProgress { get => studentProgress; set => studentProgress = value; }
    }
}
