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
    /// Contains the necessary database methods for the StudentProgress objects
    /// </summary>
	class StudentProgressMapper
	{
        /// <summary>
        /// Get a student's progress from the database
        /// </summary>
        /// <param name="username">Username of the student</param>
        /// <returns>A StudentProgress object with the necessary values</returns>
        internal static StudentProgress Get(string username)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "SELECT * FROM studentProgress WHERE username=@username;";
                    cmd.Parameters.AddWithValue("@username", username);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    // Because we search with username which is unique
                    if (reader.HasRows)
                    {
                        reader.Read();
                        List<int> prop = new List<int>();
                        for(int i = 1; i <= 10; i++)
                        {
                            int temp = reader.GetInt32(reader.GetOrdinal("propaideia" + i.ToString()));
                            prop.Add(temp);
                        }
                        int final = reader.GetInt32(reader.GetOrdinal("finalExam"));
                        StudentProgress progress = new StudentProgress(username, prop, final);
                        return progress;
                    }
                    else
                        return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at ProgressReportMapper - Get: " + e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Updates a student's progress in the database
        /// </summary>
        /// <param name="progress">Progress object to be updated.</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Update(StudentProgress progress)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "UPDATE studentProgress SET propaideia1=@propaideia1," +
                                                                 "propaideia2=@propaideia2," +
                                                                 "propaideia3=@propaideia3," +
                                                                 "propaideia4=@propaideia4," +
                                                                 "propaideia5=@propaideia5," +
                                                                 "propaideia6=@propaideia6," +
                                                                 "propaideia7=@propaideia7," +
                                                                 "propaideia8=@propaideia8," +
                                                                 "propaideia9=@propaideia9," +
                                                                 "propaideia10=@propaideia10," +
                                                                 "finalExam=@finalExam " +
                                                             "WHERE username=@username;";
                    cmd.Parameters.AddWithValue("@username", progress.Username);
                    for (int i = 1; i <= 10; i++)
                    {
                        cmd.Parameters.AddWithValue("@propaideia" + i.ToString(), progress.PropaideiaProgress[i-1]);
                    }
                    cmd.Parameters.AddWithValue("@finalExam", progress.FinalExam);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentProgressMapper - Update: " + e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts a new progress object in the database.
        /// Only the username of the student is required for insertion. The values will default to 0.
        /// </summary>
        /// <param name="username">Username of the student</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Insert(string username)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "INSERT INTO studentProgress (username) VALUES (@username);";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentProgressMapper - Insert: " + e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes a student's progress
        /// </summary>
        /// <param name="progress">Data of the student's progress to be deleted</param>
        /// <returns>True if operation is successful</returns>
        internal static bool Delete(StudentProgress progress)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data source=" + Database.DATABASE_NAME + ";"))
            {
                try
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    cmd.CommandText = "DELETE FROM studentProgress WHERE username=@username;";
                    cmd.Parameters.AddWithValue("@username", progress.Username);
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception at StudentProgress - Delete: " + e.Message);
                    return false;
                }
            }
        }
    }
}
