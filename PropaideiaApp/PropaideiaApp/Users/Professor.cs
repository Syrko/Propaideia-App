using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Users
{
    class Professor : User
    {
        internal Professor(string username, string name, string surname) : base(username, name, surname)
        {
        }
    }
}
