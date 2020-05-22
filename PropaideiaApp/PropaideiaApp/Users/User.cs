using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
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

        protected string Name { get => name; set => name = value; }
        protected string Surname { get => surname; set => surname = value; }
        protected string Username { get => username; set => username = value; }
    }
}
