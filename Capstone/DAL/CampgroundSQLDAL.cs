using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSQLDAL : ICampgroundDAL
    {
        private readonly string connectionString;

        public CampgroundSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Campground> GetAllCampgrounds(int parkId)
        {
            List<Campground> output = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @parkid", conn);
                    cmd.Parameters.AddWithValue("@parkid", parkId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground campground = ConvertRowToCampground(reader);
                        output.Add(campground);
                    }
                }
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("There was an error retrieving all available campgrounds.");
                throw;
            }
        }

        private static Campground ConvertRowToCampground(SqlDataReader reader)
        {
            return new Campground()
            {
                CampgroundId = Convert.ToInt32(reader["campground_id"]),
                ParkId = Convert.ToInt32(reader["park_id"]),
                Name = Convert.ToString(reader["name"]),
                OpenFromMM = Convert.ToInt32(reader["open_from_mm"]),
                OpenToMM = Convert.ToInt32(reader["open_to_mm"]),
                DailyFee = Convert.ToDecimal(reader["daily_fee"])
            };
        }
    }
}

