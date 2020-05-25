using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using PropaideiaApp.Users;

namespace PropaideiaApp.DataMappers
{
    class ProfessorMapper
    {
        //internal static 
        internal static Professor Get(string username)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT * FROM users WHERE username='@username';";
                    cmd.Parameters.AddWithValue("@username", username);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    // Because we search with username which is unique
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string surname = reader.GetString(reader.GetOrdinal("surname"));
                        Professor prof = new Professor(username, name, surname);
                        return prof;
                    }
                    else
                        return null;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception at ProfessorMapper - Get: " + e.Message);
                    return null;
                }
            }
        }

        internal static bool Update(string username, Professor professor)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT * FROM users WHERE username='@username';";
                    cmd.Parameters.AddWithValue("@username", username);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        cmd = new SQLiteCommand(conn);

                        cmd.CommandText = "UPDATE users SET " +
                                                        "username=@new_username," +
                                                        "name=@name," +
                                                        "surname=@surname" +
                                                        "WHERE username='@username';";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@new_username", professor.Username);
                        cmd.Parameters.AddWithValue("@name", professor.Name);
                        cmd.Parameters.AddWithValue("@surname", professor.Surname);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Couldn't edit professor!");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at ProfessorMapper - Update: " + e.Message);
                    return false;
                }
            }
        }

        internal static bool Insert(Professor professor, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT * FROM users WHERE username='@username';";
                    cmd.Parameters.AddWithValue("@username", professor.Username);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        cmd = new SQLiteCommand(conn);

                        cmd.CommandText = "INSERT INTO users (username, user_type, name, surname, password) " +
                                                        "username=@username," +
                                                        "user_type=@user_type" +
                                                        "name=@name," +
                                                        "surname=@surname" +
                                                        "password=@password;";
                        cmd.Parameters.AddWithValue("@username", professor.Username);
                        cmd.Parameters.AddWithValue("@user_type", UserTypes.PROFESSOR);
                        cmd.Parameters.AddWithValue("@name", professor.Name);
                        cmd.Parameters.AddWithValue("@surname", professor.Surname);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Couldn't insert professor!");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at ProfessorMapper - Insert: " + e.Message);
                    return false;
                }
            }
        }

        internal static bool Delete(Professor professor)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "DELETE FROM users WHERE username=@username;";
                    cmd.Parameters.AddWithValue("@username", professor.Username);
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at ProfessorMapper - Insert: " + e.Message);
                    return false;
                }
            }
        }
    }
}
