using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    class Student: User
    {
        private int level;
        private StudentProgress studentProgress;

        /// <summary>
        /// Constructor for a new student object
        /// </summary>
        /// <param name="username">The username of the new student</param>
        /// <param name="level">The level of the new student</param>
        /// <param name="studentProgress">The student's progress</param>
        internal Student(string username, int level, StudentProgress studentProgress)
        {
            this.Username = username;
            this.level = level;
            this.studentProgress = studentProgress;
        }

        /// <summary>
        /// Get a new student object with the given username from the database
        /// </summary>
        /// <param name="username">The username of the student to retrieve</param>
        internal Student(string username)
        {
            // TODO get from database
        }

        public int Level { get => level; }
        internal StudentProgress StudentProgress { get => studentProgress; }
    }
}
