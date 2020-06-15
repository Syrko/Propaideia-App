using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using PropaideiaApp.Users;

namespace PropaideiaApp.DataMappers
{
    /// <summary>
    /// Contains the necessary database methods for the professor objects
    /// </summary>
    class ProfessorMapper
    {
        /// <summary>
        /// Get a professor user from the database
        /// </summary>
        /// <param name="username">Username of the professor</param>
        /// <returns>A professor object with the necessary values</returns>
        internal static Professor Get(string username)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT * FROM users WHERE username=@username AND user_type=@user_type;";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@user_type", UserTypes.PROFESSOR);
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

        /// <summary>
        /// Updates a professor entry in the database
        /// </summary>
        /// <param name="username">Username of the professor to be updated.
        /// If the user has changed username this should be the old one.</param>
        /// <param name="professor">Professor object with the necessary information to update the database.</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Update(string username, Professor professor)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "UPDATE users SET username=@new_username," +
                                                       "name=@name," +
                                                       "surname=@surname " +
                                                   "WHERE username=@username;";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@new_username", professor.Username);
                    cmd.Parameters.AddWithValue("@name", professor.Name);
                    cmd.Parameters.AddWithValue("@surname", professor.Surname);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at ProfessorMapper - Update: " + e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts a new professor in the database
        /// </summary>
        /// <param name="professor">A professor object with the necessary information</param>
        /// <param name="password">The password of the new professor account</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Insert(Professor professor, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "INSERT INTO users (username, user_type, name, surname, password) " +
                                                        "VALUES(@username, @user_type, @name, @surname, @password);";
                    cmd.Parameters.AddWithValue("@username", professor.Username);
                    cmd.Parameters.AddWithValue("@user_type", UserTypes.PROFESSOR);
                    cmd.Parameters.AddWithValue("@name", professor.Name);
                    cmd.Parameters.AddWithValue("@surname", professor.Surname);
                    cmd.Parameters.AddWithValue("@password", password);
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

        /// <summary>
        /// Deletes a professor account
        /// </summary>
        /// <param name="professor">Data of the professor to be deleted</param>
        /// <returns>True if operation is successful</returns>
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
                    Console.WriteLine("Exception at ProfessorMapper - Delete: " + e.Message);
                    return false;
                }
            }
        }
    }
}
