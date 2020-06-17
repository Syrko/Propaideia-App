using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PropaideiaApp
{
    class Database
    {
        internal const string DATABASE_NAME = "database.db";

        internal static string Login(string username, string password)
        {
            using(SQLiteConnection conn = new SQLiteConnection("Data source=" + DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT user_type FROM users WHERE username=@username AND password=@password;";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        return null;
                    }
                    else
                    {
                        reader.Read();
                        string user_type = reader.GetString(reader.GetOrdinal("user_type"));
                        return user_type;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception at Login: " + e.Message);
                    return null;
                }
            }
        }

        internal static void Initialize_Database()
        {
            using(SQLiteConnection conn = new SQLiteConnection("Data source=" + DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    // Create the users table
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS users(username text NOT NULL PRIMARY KEY, " +
                                                                        "password text NOT NULL," +
                                                                        "user_type text NOT NULL," +
                                                                        "name text NOT NULL," +
                                                                        "surname text NOT NULL);";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // Create the students table
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS students(username text NOT NULL PRIMARY KEY, " +
                                                                            "level integer DEFAULT 1," +
                                                                            "FOREIGN KEY(username) REFERENCES users(username));";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // Create the studentProgress table
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS studentProgress(username text NOT NULL PRIMARY KEY, " +
                                                                                    "propaideia1 integer DEFAULT 0," +
                                                                                    "propaideia2 integer DEFAULT 0," +
                                                                                    "propaideia3 integer DEFAULT 0," +
                                                                                    "propaideia4 integer DEFAULT 0," +
                                                                                    "propaideia5 integer DEFAULT 0," +
                                                                                    "propaideia6 integer DEFAULT 0," +
                                                                                    "propaideia7 integer DEFAULT 0," +
                                                                                    "propaideia8 integer DEFAULT 0," +
                                                                                    "propaideia9 integer DEFAULT 0," +
                                                                                    "propaideia10 integer DEFAULT 0," +
                                                                                    "finalExam integer DEFAULT 0," +
                                                                                    "FOREIGN KEY(username) REFERENCES students(username));";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception at database initialization: " + e.Message);
                }
            }
        }

        internal static void InsertDummyData()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + DATABASE_NAME + ";"))
            {
                List<string> names = new List<string>()
                {
                    "John", "Terry", "Eric", "Michael", "Graham", "Terry",
                };
                List<string> surnames = new List<string>()
                {
                    "Cleese", "Gilliam", "Idle", "Palin", "Chapman", "Jones"
                };

                try
                {
                    conn.Open();

                    SQLiteCommand cmd;

                    // Insert users
                    cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "INSERT INTO users(username, password, user_type, name, surname) " +
                                        "VALUES(@username, @password, @user_type, @name, @surname);";
                    cmd.Parameters.AddWithValue("@username", surnames[0]);
                    cmd.Parameters.AddWithValue("@password", "admin");
                    cmd.Parameters.AddWithValue("@user_type", UserTypes.PROFESSOR);
                    cmd.Parameters.AddWithValue("@name", names[0]);
                    cmd.Parameters.AddWithValue("@surname", surnames[0]);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    for (int i = 1; i < names.Count; i++)
                    {
                        cmd = new SQLiteCommand(conn);
                        cmd.CommandText = "INSERT INTO users(username, password, user_type, name, surname) " +
                                            "VALUES(@username, @password, @user_type, @name, @surname);";
                        cmd.Parameters.AddWithValue("@username", surnames[i]);
                        cmd.Parameters.AddWithValue("@password", "pass" + surnames[i]);
                        cmd.Parameters.AddWithValue("@user_type", UserTypes.STUDENT);
                        cmd.Parameters.AddWithValue("@name", names[i]);
                        cmd.Parameters.AddWithValue("@surname", surnames[i]);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }

                    // Insert students
                    for (int i = 1; i < names.Count; i++)
                    {
                        cmd = new SQLiteCommand(conn);
                        cmd.CommandText = "INSERT INTO students(username, level) " +
                                            "VALUES(@username, @level);";
                        cmd.Parameters.AddWithValue("@username", surnames[i]);
                        cmd.Parameters.AddWithValue("@level", 1);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }

                    // Insert progress
                    for (int i = 1; i < names.Count; i++)
                    {
                        cmd = new SQLiteCommand(conn);
                        cmd.CommandText = "INSERT INTO studentProgress(username) " +
                                            "VALUES(@username);";
                        cmd.Parameters.AddWithValue("@username", surnames[i]);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception at inserting sample data: " + e.Message);
                }
            }
        }
    }

    struct UserTypes
    {
        public const string STUDENT = "student";
        public const string PROFESSOR = "professor";
    }
}
