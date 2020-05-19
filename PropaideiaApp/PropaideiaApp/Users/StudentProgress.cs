using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    /// <summary>
    /// Class that has the necessary information of a student's progress.
    /// A default is created when the a new student is registered.
    /// You always retrieve the copy from the database.
    /// </summary>
    class StudentProgress
    {
        // TODO change class diagram
        private string username;
        private List<int> propaideiaProgress;
        private int finalExam;

        StudentProgress(string username)
        {
            // TODO get from database
        }
    }
}
