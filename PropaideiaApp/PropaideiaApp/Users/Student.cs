using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    /// <summary>
    /// Child class of User that is used for representing the users of type Student.
    /// It contains a level for measuring their progress and a StudentProgress object for saving the details of their progress.
    /// </summary>
    class Student : User
    {
        private int level;
        private StudentProgress studentProgress;

        /// <summary>
        /// Constructor that is mainly used by the StudentMapper class.
        /// </summary>
        /// <param name="username">The username of the student.</param>
        /// <param name="name">The name of the student.</param>
        /// <param name="surname">The surname of the student.</param>
        /// <param name="level">The level of the student.</param>
        /// <param name="studentProgress">The student's progress.</param>
        internal Student(string username, string name, string surname, int level, StudentProgress studentProgress) : base(username, name, surname)
        {
            this.level = level;
            this.studentProgress = studentProgress;
        }

        /// <summary>
        /// Overload for registering a new user. The missing fields will be defaulted by the database.
        /// </summary>
        /// <param name="username">The username of the student.</param>
        /// <param name="name">The name of the student.</param>
        /// <param name="surname">The surname of the student.</param>
        internal Student(string username, string name, string surname): base(username, name, surname) { }

        internal int Level { get => level; set => level = value; }
        internal StudentProgress StudentProgress { get => studentProgress; set => studentProgress = value; }
    }
}
