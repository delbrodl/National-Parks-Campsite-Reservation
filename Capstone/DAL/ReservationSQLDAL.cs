using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSQLDAL : IReservationDAL
    {
        private readonly string connectionString;

        public ReservationSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int BookReservation(Reservation reservation)
        {
            int confirmationId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date, create_date) VALUES (@siteid, @name, @fromdate, @todate, @createdate)", conn);
                    cmd.Parameters.AddWithValue("@name", reservation.Name);
                    cmd.Parameters.AddWithValue("@siteid", reservation.SiteId);
                    cmd.Parameters.AddWithValue("@fromdate", reservation.FromDate);
                    cmd.Parameters.AddWithValue("@todate", reservation.ToDate);
                    cmd.Parameters.AddWithValue("@createdate", reservation.CreateDate);
                    cmd.ExecuteNonQuery();

                    SqlCommand selectCmd = new SqlCommand("SELECT reservation_id FROM reservation WHERE name = @name AND site_id = @siteid AND from_date = @fromdate AND to_date = @todate AND create_date = @createdate", conn);
                    selectCmd.Parameters.AddWithValue("@name", reservation.Name);
                    selectCmd.Parameters.AddWithValue("@siteid", reservation.SiteId);
                    selectCmd.Parameters.AddWithValue("@fromdate", reservation.FromDate);
                    selectCmd.Parameters.AddWithValue("@todate", reservation.ToDate);
                    selectCmd.Parameters.AddWithValue("@createdate", reservation.CreateDate);
                    SqlDataReader reader = selectCmd.ExecuteReader();
                    reader.Read();
                    confirmationId = Convert.ToInt32(reader["reservation_id"]);

                    return confirmationId;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Failed to add reservation to database.");
                throw;
            }
        }

        // public IList<Reservation> GetReservations(int siteId)
        // {
        //    List<Reservation> output = new List<Reservation>();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT * FROM reservation WHERE site_id = @siteid", conn);
        //            cmd.Parameters.AddWithValue("@siteid", siteId);
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                Reservation reservation = ConvertRowToReservation(reader);
        //                output.Add(reservation);
        //            }
        //        }
        //        return output;
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine("There was an error retrieving all available reservations.");
        //        throw;
        //    }
        // }

        private static Reservation ConvertRowToReservation(SqlDataReader reader)
        {
            return new Reservation()
            {
                ReservationId = Convert.ToInt32(reader["reservation_id"]),
                SiteId = Convert.ToInt32(reader["site_id"]),
                Name = Convert.ToString(reader["name"]),
                FromDate = Convert.ToDateTime(reader["from_date"]),
                ToDate = Convert.ToDateTime(reader["to_date"]),
                CreateDate = Convert.ToDateTime(reader["create_date"])
            };
        }
    }
}
