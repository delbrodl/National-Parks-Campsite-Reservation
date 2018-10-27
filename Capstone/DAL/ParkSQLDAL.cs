using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSQLDAL : IParkDAL
    {
        private readonly string connectionString;

        public ParkSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetAllAvailableParks()
        {
            List<Park> output = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park park = ConvertRowToPark(reader);
                        output.Add(park);
                    }
                }

                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("There was an error retrieving all available parks.");
                throw;
            }
        }

        private static Park ConvertRowToPark(SqlDataReader reader)
        {
            return new Park()
            {
                ParkId = Convert.ToInt32(reader["park_id"]),
                Name = Convert.ToString(reader["name"]),
                Location = Convert.ToString(reader["location"]),
                EstablishedDate = Convert.ToDateTime(reader["establish_date"]),
                Area = Convert.ToInt32(reader["area"]),
                Visitors = Convert.ToInt32(reader["visitors"]),
                Description = Convert.ToString(reader["description"])
            };
        }

        // public Park GetParkInfo(int parkId)
        // {
        //    Park park = new Park();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM park WHERE park_id = @parkid", conn);
        //            cmd.Parameters.AddWithValue("@parkid", parkId);
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            reader.Read();
        //            park = ConvertRowToPark(reader);
        //        }
        //        return park;
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("There was an error selecting Park information.");
        //        throw;
        //    }
        // }
    }
}
