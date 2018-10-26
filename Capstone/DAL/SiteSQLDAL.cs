using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSQLDAL : ISiteDAL
    {
        private readonly string _connectionString;

        private readonly string _query = "SELECT TOP 5 * FROM site Left Join " +
            "(SELECT site_id FROM reservation WHERE (from_date BETWEEN @startdate AND @enddate) OR (to_date BETWEEN @startdate AND @enddate) OR (@startdate BETWEEN from_date AND to_date) OR (@enddate BETWEEN from_date AND to_date)) " +
            "AS filtered ON site.site_id = filtered.site_id " +
            "WHERE filtered.site_id IS NULL AND @campgroundid = site.campground_id ORDER BY site.site_number, site.max_occupancy";


        public SiteSQLDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Site> GetAvailableSites(int campgroundId, DateTime startDate, DateTime endDate)
        {
            List<Site> output = new List<Site>();

            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(_query, conn);
                    cmd.Parameters.AddWithValue("@startdate", startDate);
                    cmd.Parameters.AddWithValue("@enddate", endDate);
                    cmd.Parameters.AddWithValue("@campgroundid", campgroundId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertRowToSite(reader);
                        output.Add(site);
                    }
                }
                return output;
            }
            catch(SqlException ex)
            {
                Console.WriteLine("There was an error retrieving available sites.");
                throw;
            }
        }

        private static Site ConvertRowToSite(SqlDataReader reader)
        {
            return new Site()
            {
                SiteId = Convert.ToInt32(reader["site_id"]),
                CampgroundId = Convert.ToInt32(reader["campground_id"]),
                SiteNumber = Convert.ToInt32(reader["site_number"]),
                MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]),
                Accessible = Convert.ToBoolean(reader["accessible"]),
                MaxRVLength = Convert.ToInt32(reader["max_rv_length"]),
                Utilities = Convert.ToBoolean(reader["utilities"])
            };
        }
    }
}
