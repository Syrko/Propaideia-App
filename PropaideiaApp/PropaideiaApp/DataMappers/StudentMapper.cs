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
    /// Contains the necessary database methods for the student objects
    /// </summary>
	class StudentMapper
	{
        /// <summary>
        /// Get a student user from the database
        /// </summary>
        /// <param name="username">Username of the student</param>
        /// <returns>A student object with the necessary values</returns>
        internal static Student Get(string username)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT * FROM users WHERE username=@username;";
                    cmd.Parameters.AddWithValue("@username", username);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    // Because we search with username which is unique
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string surname = reader.GetString(reader.GetOrdinal("surname"));

                        cmd = new SQLiteCommand(conn);
                        cmd.CommandText = "SELECT * FROM students WHERE username=@username;";
                        cmd.Parameters.AddWithValue("@username", username);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        int level = reader.GetInt32(reader.GetOrdinal("level"));
                        StudentProgress progress = StudentProgressMapper.Get(username);
                        
                        Student student = new Student(username, name, surname, level, progress);
                        reader.Close();
                        return student;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentMapper - Get: " + e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Updates a student entry in the database
        /// </summary>
        /// <param name="student">Student object with the necessary information to update the database.</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Update(Student student)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {

                        cmd.CommandText = "UPDATE users SET name=@name," +
                                                              " surname=@surname " +
                                                       "WHERE username=@username;";
                        cmd.Parameters.AddWithValue("@name", student.Name);
                        cmd.Parameters.AddWithValue("@surname", student.Surname);
                        cmd.Parameters.AddWithValue("@username", student.Username);
                        cmd.ExecuteNonQuery();

                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {

                        cmd.CommandText = "UPDATE students SET level=@level " +
                                                   "WHERE username=@username;";
                        cmd.Parameters.AddWithValue("@level", student.Level);
                        cmd.Parameters.AddWithValue("@username", student.Username);
                        cmd.ExecuteNonQuery();
                    }
                    StudentProgressMapper.Update(student.StudentProgress);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentMapper - Update: " + e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts a new student in the database
        /// </summary>
        /// <param name="student">A student object with the necessary information</param>
        /// <param name="password">The password of the new professor account</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Insert(Student student, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {

                        cmd.CommandText = "INSERT INTO users (username, user_type, name, surname, password) " +
                                                        "VALUES(@username, @user_type, @name, @surname, @password);";
                        cmd.Parameters.AddWithValue("@username", student.Username);
                        cmd.Parameters.AddWithValue("@user_type", UserTypes.STUDENT);
                        cmd.Parameters.AddWithValue("@name", student.Name);
                        cmd.Parameters.AddWithValue("@surname", student.Surname);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {

                        cmd.CommandText = "INSERT INTO students (username) VALUES(@username);";
                        cmd.Parameters.AddWithValue("@username", student.Username);
                        cmd.ExecuteNonQuery();
                    }
                    StudentProgressMapper.Insert(student.Username);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentMapper - Insert: " + e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes a student account
        /// </summary>
        /// <param name="student">Data of the student to be deleted</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Delete(Student student)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    StudentProgressMapper.Delete(student.StudentProgress);

                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {

                        cmd.CommandText = "DELETE FROM students WHERE username=@username;";
                        cmd.Parameters.AddWithValue("@username", student.Username);
                        cmd.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = "DELETE FROM users WHERE username=@username;";
                        cmd.Parameters.AddWithValue("@username", student.Username);
                        cmd.ExecuteNonQuery();
                    }
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentMapper - Delete: " + e.Message);
                    return false;
                }
            }
        }
    }
}
