using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    /// <summary>
    /// Child class of User that is used for representing the users of type professor.
    /// </summary>
    class Professor : User
    {
        internal Professor(string username, string name, string surname) : base(username, name, surname)
        {
        }
    }
}
