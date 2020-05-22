using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    class StudentProgress
    {
        private string username;
        private List<int> propaideiaProgress;
        private int finalExam;

        public StudentProgress(string username, List<int> propaideiaProgress, int finalExam)
        {
            this.username = username;
            this.propaideiaProgress = propaideiaProgress;
            this.finalExam = finalExam;
        }

        internal string Username { get => username; set => username = value; }
        internal List<int> PropaideiaProgress { get => propaideiaProgress; set => propaideiaProgress = value; }
        internal int FinalExam { get => finalExam; set => finalExam = value; }
    }
}
