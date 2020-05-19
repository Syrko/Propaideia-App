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

        protected string Username { get => username; set => username = value; }
    }
}
