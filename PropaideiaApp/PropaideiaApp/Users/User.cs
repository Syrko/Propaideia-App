using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    /// <summary>
    /// Base class for both types of users -- contains their common information.
    /// </summary>
    abstract class User
    {
        private string username;
        private string name;
        private string surname;

        protected User(string username, string name, string surname)
        {
            this.username = username;
            this.name = name;
            this.surname = surname;
        }

        internal string Name { get => name; set => name = value; }
        internal string Surname { get => surname; set => surname = value; }
        internal string Username { get => username; set => username = value; }
    }
}
