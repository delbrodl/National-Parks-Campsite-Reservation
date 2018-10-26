using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class NPReservationDBDALTest
    {
        public const string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";
        TransactionScope _transaction;

        public int ParkId;
        public int CampgroundId;
        public int SiteId;
        public int ReservationId;

        [TestInitialize]
        public void Initialize()
        {
            _transaction = new TransactionScope();

            string sql = File.ReadAllText("database.sql");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    ParkId = Convert.ToInt32(reader["parkId"]);
                    CampgroundId = Convert.ToInt32(reader["campgroundId"]);
                    SiteId = Convert.ToInt32(reader["siteId"]);
                    ReservationId = Convert.ToInt32(reader["reservationId"]);
                }
            }
        }

        [TestCleanup]  
        public void Cleanup()
        {
            _transaction.Dispose();
        }

        public int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
